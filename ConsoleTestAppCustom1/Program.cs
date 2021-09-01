using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleTestAppCustom1
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = File.ReadAllBytes("testbaslik.xml");

            AS2Send.SendFile(
                  uri: new Uri("https://localhost:44350/TestHandler.ashx")
                , filename: "testbaslik.xml"
                , fileData: bytes
                , from: "from data"
                , to: "to data"
                , proxySettings : new ProxySettings
                {
                    Domain = "",
                    Name = "",
                    Password = "",
                    Username = ""
                }
                , timeoutMs: 100 * 1000
                , signingCertFilename : null
                , signingCertPassword : null
                , recipientCertFilename : null);
            Console.WriteLine("Hello World!");
        }



    }
    public struct ProxySettings
    {
        public string Name;
        public string Username;
        public string Password;
        public string Domain;
    }

    public class AS2Send
    {
        public static HttpStatusCode SendFile(Uri uri, string filename, byte[] fileData, string from, string to, ProxySettings proxySettings, int timeoutMs, string signingCertFilename, string signingCertPassword, string recipientCertFilename)
        {
            if (String.IsNullOrEmpty(filename)) 
                throw new ArgumentNullException("filename");
            if (fileData.Length == 0) 
                throw new ArgumentException("filedata");
            byte[] content = fileData;
            //Initialise the request
            HttpWebRequest http = (HttpWebRequest)WebRequest.Create(uri);
            if (!String.IsNullOrEmpty(proxySettings.Name))
            {
                WebProxy proxy = new WebProxy(proxySettings.Name);
                NetworkCredential proxyCredential = new NetworkCredential();
                proxyCredential.Domain = proxySettings.Domain;
                proxyCredential.UserName = proxySettings.Username;
                proxyCredential.Password = proxySettings.Password;
                proxy.Credentials = proxyCredential;
                http.Proxy = proxy;
            }
            //Define the standard request objects
            http.Method = "POST";
            http.AllowAutoRedirect = true;
            http.KeepAlive = true;
            http.PreAuthenticate = false; //Means there will be two requests sent if Authentication required.
            http.SendChunked = false;
            http.UserAgent = "MY SENDING AGENT";

            //These Headers are common to all transactions
            http.Headers.Add("Mime-Version", "1.0");
            http.Headers.Add("AS2-Version", "1.2");
            http.Headers.Add("AS2-From", from);
            http.Headers.Add("AS2-To", to);
            http.Headers.Add("Subject", filename + " transmission.");
            http.Headers.Add("Message-Id", "<AS2_" + DateTime.Now.ToString("hhmmssddd") + ">");
            http.Timeout = timeoutMs;

            string contentType = (Path.GetExtension(filename) == ".xml") ? "application/xml" : "application/EDIFACT";

            bool encrypt = !string.IsNullOrEmpty(recipientCertFilename);
            bool sign = !string.IsNullOrEmpty(signingCertFilename);

            if (!sign && !encrypt)
            {
                http.Headers.Add("Content-Transfer-Encoding", "binary");
                http.Headers.Add("Content-Disposition", "inline; filename=\"" + filename + "\"");
            }
            if (sign)
            {
                // Wrap the file data with a mime header
                content = AS2MIMEUtilities.CreateMessage(contentType, "binary", "", content);
                content = AS2MIMEUtilities.Sign(content, signingCertFilename, signingCertPassword, out contentType);
                http.Headers.Add("EDIINT-Features", "multiple-attachments");

            }
            if (encrypt)
            {
                if (string.IsNullOrEmpty(recipientCertFilename))
                {
                    throw new ArgumentNullException(recipientCertFilename, "if encrytionAlgorithm is specified then recipientCertFilename must be specified");
                }
                byte[] signedContentTypeHeader = System.Text.ASCIIEncoding.ASCII.GetBytes("Content-Type: " + contentType + Environment.NewLine);
                byte[] contentWithContentTypeHeaderAdded = AS2MIMEUtilities.ConcatBytes(signedContentTypeHeader, content);
                content = AS2Encryption.Encrypt(contentWithContentTypeHeaderAdded, recipientCertFilename, EncryptionAlgorithm.DES3);
                contentType = "application/pkcs7-mime; smime-type=enveloped-data; name=\"smime.p7m\"";
            }

            http.ContentType = contentType;
            http.ContentLength = content.Length;
            SendWebRequest(http, content);
            return HandleWebResponse(http);
        }

        private static HttpStatusCode HandleWebResponse(HttpWebRequest http)
        {
            HttpWebResponse response = (HttpWebResponse)http.GetResponse();
            var resultCode = response.StatusCode;
            response.Close();
            return resultCode;
        }

        private static void SendWebRequest(HttpWebRequest http, byte[] fileData)
        {
            Stream oRequestStream = http.GetRequestStream();
            oRequestStream.Write(fileData, 0, fileData.Length);
            oRequestStream.Flush();
            oRequestStream.Close();
        }
    }


    public static class EncryptionAlgorithm
    {
        public static string DES3 = "3DES";
        public static string RC2 = "RC2";
    }

    public class AS2Encryption
    {
        internal static byte[] Encode(byte[] arMessage, string signerCert, string signerPassword)
        {
            X509Certificate2 cert = new X509Certificate2(signerCert, signerPassword);
            ContentInfo contentInfo = new ContentInfo(arMessage);
            SignedCms signedCms = new SignedCms(contentInfo, true); // <- true detaches the signature
            CmsSigner cmsSigner = new CmsSigner(cert);
            signedCms.ComputeSignature(cmsSigner);
            byte[] signature = signedCms.Encode();
            return signature;
        }

        internal static byte[] Encrypt(byte[] message, string recipientCert, string encryptionAlgorithm)
        {
            if (!string.Equals(encryptionAlgorithm, EncryptionAlgorithm.DES3) && !string.Equals(encryptionAlgorithm, EncryptionAlgorithm.RC2))
                throw new ArgumentException("encryptionAlgorithm argument must be 3DES or RC2 - value specified was:" + encryptionAlgorithm);
            X509Certificate2 cert = new X509Certificate2(recipientCert);
            ContentInfo contentInfo = new ContentInfo(message);
            EnvelopedCms envelopedCms = new EnvelopedCms(contentInfo,
                new AlgorithmIdentifier(new System.Security.Cryptography.Oid(encryptionAlgorithm))); // should be 3DES or RC2
            CmsRecipient recipient = new CmsRecipient(SubjectIdentifierType.IssuerAndSerialNumber, cert);
            envelopedCms.Encrypt(recipient);
            byte[] encoded = envelopedCms.Encode();
            return encoded;
        }

        internal static byte[] Decrypt(byte[] encodedEncryptedMessage, out string encryptionAlgorithmName)
        {
            EnvelopedCms envelopedCms = new EnvelopedCms();
            // NB. the message will have been encrypted with your public key.
            // The corresponding private key must be installed in the Personal Certificates folder of the user
            // this process is running as.
            envelopedCms.Decode(encodedEncryptedMessage);
            envelopedCms.Decrypt();
            encryptionAlgorithmName = envelopedCms.ContentEncryptionAlgorithm.Oid.FriendlyName;
            return envelopedCms.Encode();
        }

    }






    public class AS2MIMEUtilities
    {
        public const string MESSAGE_SEPARATOR = "\r\n\r\n";
        public AS2MIMEUtilities()
        {
        }

        /// <summary>
        /// return a unique MIME style boundary
        /// this needs to be unique enought not to occur within the data
        /// and so is a Guid without - or { } characters.
        /// </summary>
        /// <returns></returns>
        protected static string MIMEBoundary()
        {
            return "_" + Guid.NewGuid().ToString("N") + "_";
        }

        /// <summary>
        /// Creates the a Mime header out of the components listed.
        /// </summary>
        /// <param name="sContentType">Content type</param>
        /// <param name="sEncoding">Encoding method</param>
        /// <param name="sDisposition">Disposition options</param>
        /// <returns>A string containing the three headers.</returns>
        public static string MIMEHeader(string sContentType, string sEncoding, string sDisposition)
        {
            string sOut = "";

            sOut = "Content-Type: " + sContentType + Environment.NewLine;
            if (sEncoding != "")
                sOut += "Content-Transfer-Encoding: " + sEncoding + Environment.NewLine;
            if (sDisposition != "")
                sOut += "Content-Disposition: " + sDisposition + Environment.NewLine;
            sOut = sOut + Environment.NewLine;

            return sOut;
        }

        /// <summary>
        /// Return a single array of bytes out of all the supplied byte arrays.
        /// </summary>
        /// <param name="arBytes">Byte arrays to add</param>
        /// <returns>The single byte array.</returns>
        public static byte[] ConcatBytes(params byte[][] arBytes)
        {
            long lLength = 0;
            long lPosition = 0;
            //Get total size required.
            foreach (byte[] ar in arBytes)
                lLength += ar.Length;
            //Create new byte array
            byte[] toReturn = new byte[lLength];
            //Fill the new byte array
            foreach (byte[] ar in arBytes)
            {
                ar.CopyTo(toReturn, lPosition);
                lPosition += ar.Length;
            }
            return toReturn;
        }

        /// <summary>
        /// Create a Message out of byte arrays (this makes more sense than the above method)
        /// </summary>
        /// <param name="sContentType">Content type ie multipart/report</param>
        /// <param name="sEncoding">The encoding provided...</param>
        /// <param name="sDisposition">The disposition of the message...</param>
        /// <param name="abMessageParts">The byte arrays that make up the components</param>
        /// <returns>The message as a byte array.</returns>
        public static byte[] CreateMessage(string sContentType, string sEncoding, string sDisposition, params byte[][] abMessageParts)
        {
            int iHeaderLength = 0;
            return CreateMessage(sContentType, sEncoding, sDisposition, out iHeaderLength, abMessageParts);
        }
        /// <summary>
        /// Create a Message out of byte arrays (this makes more sense than the above method)
        /// </summary>
        /// <param name="sContentType">Content type ie multipart/report</param>
        /// <param name="sEncoding">The encoding provided...</param>
        /// <param name="sDisposition">The disposition of the message...</param>
        /// <param name="iHeaderLength">The length of the headers.</param>
        /// <param name="abMessageParts">The message parts.</param>
        /// <returns>The message as a byte array.</returns>
        public static byte[] CreateMessage(string sContentType, string sEncoding, string sDisposition, out int iHeaderLength, params byte[][] abMessageParts)
        {
            long lLength = 0;
            long lPosition = 0;

            //Only one part... Add headers only...
            if (abMessageParts.Length == 1)
            {
                byte[] bHeader = ASCIIEncoding.ASCII.GetBytes(MIMEHeader(sContentType, sEncoding, sDisposition));
                iHeaderLength = bHeader.Length;
                return ConcatBytes(bHeader, abMessageParts[0]);
            }
            else
            {
                // get boundary and "static" subparts.
                string sBoundary = MIMEBoundary();
                byte[] bPackageHeader = ASCIIEncoding.ASCII.GetBytes(MIMEHeader(sContentType + "; boundary=\"" + sBoundary + "\"", sEncoding, sDisposition));
                byte[] bBoundary = ASCIIEncoding.ASCII.GetBytes(Environment.NewLine + "--" + sBoundary + Environment.NewLine);
                byte[] bFinalFooter = ASCIIEncoding.ASCII.GetBytes(Environment.NewLine + "--" + sBoundary + "--" + Environment.NewLine);
                //Calculate the total size required.
                iHeaderLength = bPackageHeader.Length;
                foreach (byte[] ar in abMessageParts)
                    lLength += ar.Length;
                lLength += iHeaderLength + bBoundary.Length * abMessageParts.Length +
                    bFinalFooter.Length;
                //Create new byte array to that size.
                byte[] toReturn = new byte[lLength];
                //Copy the headers in.
                bPackageHeader.CopyTo(toReturn, lPosition);
                lPosition += bPackageHeader.Length;
                //Fill the new byte array in by coping the message parts.
                foreach (byte[] ar in abMessageParts)
                {
                    bBoundary.CopyTo(toReturn, lPosition);
                    lPosition += bBoundary.Length;
                    ar.CopyTo(toReturn, lPosition);
                    lPosition += ar.Length;
                }
                //Finally add the footer boundary.
                bFinalFooter.CopyTo(toReturn, lPosition);
                return toReturn;
            }
        }

        /// <summary>
        /// Signs a message and returns a MIME encoded array of bytes containing the signature.
        /// </summary>
        /// <param name="arMessage"></param>
        /// <param name="bPackageHeader"></param>
        /// <returns></returns>
        public static byte[] Sign(byte[] arMessage, string signerCert, string signerPassword, out string sContentType)
        {
            byte[] bInPKCS7 = new byte[0];
            // get a MIME boundary
            string sBoundary = MIMEBoundary();
            // Get the Headers for the entire message.
            sContentType = "multipart/signed; protocol=\"application/pkcs7-signature\"; micalg=\"sha1\"; boundary=\"" + sBoundary + "\"";
            // Define the boundary byte array.
            byte[] bBoundary = ASCIIEncoding.ASCII.GetBytes(Environment.NewLine + "--" + sBoundary + Environment.NewLine);
            // Encode the header for the signature portion.
            byte[] bSignatureHeader = ASCIIEncoding.ASCII.GetBytes(MIMEHeader("application/pkcs7-signature; name=\"smime.p7s\"", "base64", "attachment; filename=smime.p7s"));
            // Get the signature.
            byte[] bSignature = AS2Encryption.Encode(arMessage, signerCert, signerPassword);
            // convert to base64
            string sig = Convert.ToBase64String(bSignature) + MESSAGE_SEPARATOR;
            bSignature = System.Text.ASCIIEncoding.ASCII.GetBytes(sig);
            // Calculate the final footer elements.
            byte[] bFinalFooter = ASCIIEncoding.ASCII.GetBytes("--" + sBoundary + "--" + Environment.NewLine);
            // Concatenate all the above together to form the message.
            bInPKCS7 = ConcatBytes(bBoundary, arMessage, bBoundary,
                bSignatureHeader, bSignature, bFinalFooter);
            return bInPKCS7;
        }
    }

    //Program test = new Program();
    //test.SerializeObject("XmlNamespaces.xml");


    //XmlWriter writer = null;

    //           try
    //           {

    //               // Create an XmlWriterSettings object with the correct options.
    //               XmlWriterSettings settings = new XmlWriterSettings();
    //               settings.Indent = true;
    //               settings.IndentChars = ("\t");
    //               settings.OmitXmlDeclaration = true;

    //               // Create the XmlWriter object and write some content.
    //               writer = XmlWriter.Create("data.xml", settings);
    //               writer.WriteStartElement("aaa","book","asdsadas");
    //               writer.WriteElementString("item", "bbb",  "tesing");
    //               writer.WriteEndElement();

    //               writer.Flush();
    //           }
    //           finally
    //           {
    //               if (writer != null)
    //                   writer.Close();
    //           }




    //public void SerializeObject(string filename)
    //{
    //    XmlSerializer s = new XmlSerializer(typeof(Books));
    //    // Writing a file requires a TextWriter.
    //    TextWriter t = new StreamWriter(filename);

    //    /* Create an XmlSerializerNamespaces object and add two
    //    prefix-namespace pairs. */
    //    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
    //    ns.Add("books", "http://www.cpandl.com");
    //    ns.Add("money", "http://www.cohowinery.com");

    //    // Create a Book instance.
    //    Book b = new Book();
    //    b.TITLE = "A Book Title";
    //    Price p = new Price();
    //    p.price = (decimal)9.95;
    //    p.currency = "US Dollar";
    //    b.PRICE = p;
    //    Books bks = new Books();
    //    bks.Book = b;
    //    s.Serialize(t, bks, ns);
    //    t.Close();
    //}
    //public class Books
    //{
    //    [XmlElement(Namespace = "http://www.cohowinery.com")]
    //    public Book Book;
    //}

    //[XmlType(Namespace = "http://www.cpandl.com")]
    //public class Book
    //{
    //    [XmlElement(Namespace = "http://www.cpandl.com")]
    //    public string TITLE;
    //    [XmlElement(Namespace = "http://www.cohowinery.com")]
    //    public Price PRICE;
    //}

    //public class Price
    //{
    //    [XmlAttribute(Namespace = "http://www.cpandl.com")]
    //    public string currency;
    //    [XmlElement(Namespace = "http://www.cohowinery.com")]
    //    public decimal price;
    //}
}
