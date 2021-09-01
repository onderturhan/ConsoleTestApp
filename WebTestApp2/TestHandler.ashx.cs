using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Pkcs;
using System.Web;
using System.Web.Configuration;

namespace WebTestApp1
{
    /// <summary>
    /// Summary description for TestHandler
    /// </summary>
    public class TestHandler : System.Web.IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string sTo = context.Request.Headers["AS2-To"];
            string sFrom = context.Request.Headers["AS2-From"];
            string sMessageID = context.Request.Headers["Message-ID"];

            if (context.Request.HttpMethod == "POST" || context.Request.HttpMethod == "PUT" ||
               (context.Request.HttpMethod == "GET" && context.Request.QueryString.Count > 0))
            {

                if (sFrom == null || sTo == null)
                {
                    //Invalid AS2 Request.
                    //Section 6.2 The AS2-To and AS2-From header fields MUST be present
                    //    in all AS2 messages
                    if (!(context.Request.HttpMethod == "GET" && context.Request.QueryString[0].Length == 0))
                    {
                        AS2Receive.BadRequest(context.Response, "Invalid or unauthorized AS2 request received.");
                    }
                }
                else
                {
                    string dropLoc = "C:\\Users\\OnderTurhan\\Downloads\\";
                    AS2Receive.Process(context.Request, /*WebConfigurationManager.AppSettings["DropLocation"]*/ dropLoc);
                }
            }
            else
            {
                AS2Receive.GetMessage(context.Response);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }


    public class AS2Receive
    {
        public static void GetMessage(HttpResponse response)
        {
            response.StatusCode = 200;
            response.StatusDescription = "Okay";

            response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 3.2 Final//EN"">"
            + @"<HTML><HEAD><TITLE>Generic AS2 Receiver</TITLE></HEAD>"
            + @"<BODY><H1>200 Okay</H1><HR>This is to inform you that the AS2 interface is working and is "
            + @"accessable from your location.  This is the standard response to all who would send a GET "
            + @"request to this page instead of the POST context.Request defined by the AS2 Draft Specifications.<HR></BODY></HTML>");
        }

        public static void BadRequest(HttpResponse response, string message)
        {
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.StatusDescription = "Bad context.Request";

            response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 3.2 Final//EN"">"
            + @"<HTML><HEAD><TITLE>400 Bad context.Request</TITLE></HEAD>"
            + @"<BODY><H1>400 Bad context.Request</H1><HR>There was a error processing this context.Request.  The reason given by the server was:"
            + @"<P><font size=-1>" + message + @"</Font><HR></BODY></HTML>");
        }

        public static void Process(HttpRequest request, string dropLocation)
        {
            string filenameFormat = request.Headers["Subject"];
            string filename = filenameFormat.Split(' ')[0];

            byte[] data = request.BinaryRead(request.TotalBytes);
            bool isEncrypted = request.ContentType.Contains("application/pkcs7-mime");
            bool isSigned = request.ContentType.Contains("application/pkcs7-signature");

            string message = string.Empty;

            if (isSigned)
            {
                string messageWithMIMEHeaders = System.Text.ASCIIEncoding.ASCII.GetString(data);
                string contentType = request.Headers["Content-Type"];

                message = AS2MIMEUtilities.ExtractPayload(messageWithMIMEHeaders, contentType);
            }
            else if (isEncrypted) // encrypted and signed inside
            {
                byte[] decryptedData = AS2Encryption.Decrypt(data);

                string messageWithContentTypeLineAndMIMEHeaders = System.Text.ASCIIEncoding.ASCII.GetString(decryptedData);

                // when encrypted, the Content-Type line is actually stored in the start of the message
                int firstBlankLineInMessage = messageWithContentTypeLineAndMIMEHeaders.IndexOf(Environment.NewLine + Environment.NewLine);
                string contentType = messageWithContentTypeLineAndMIMEHeaders.Substring(0, firstBlankLineInMessage);

                message = AS2MIMEUtilities.ExtractPayload(messageWithContentTypeLineAndMIMEHeaders, contentType);
            }
            else // not signed and not encrypted
            {
                message = System.Text.ASCIIEncoding.ASCII.GetString(data);
            }

            System.IO.File.WriteAllText(dropLocation + filename, message);
        }
    }

    internal class AS2Encryption
    {
        internal static byte[] Decrypt(byte[] encodedEncryptedMessage)
        {
            EnvelopedCms envelopedCms = new EnvelopedCms();
            envelopedCms.Decode(encodedEncryptedMessage);
            envelopedCms.Decrypt();
            return envelopedCms.Encode();
        }
    }

    public class AS2MIMEUtilities
    {
        public const string MESSAGE_SEPARATOR = "\r\n\r\n";

        /// <summary>
        /// Extracts the payload from a signed message, by looking for boundaries
        /// Ignores signatures and does checking - should really validate the signature
        /// </summary>
        public static string ExtractPayload(string message, string contentType)
        {
            string boundary = GetBoundaryFromContentType(contentType);

            if (!boundary.StartsWith("--"))
                boundary = "--" + boundary;

            int firstBoundary = message.IndexOf(boundary);
            int blankLineAfterBoundary = message.IndexOf(MESSAGE_SEPARATOR, firstBoundary) + (MESSAGE_SEPARATOR).Length;
            int nextBoundary = message.IndexOf(MESSAGE_SEPARATOR + boundary, blankLineAfterBoundary);
            int payloadLength = nextBoundary - blankLineAfterBoundary;

            return message.Substring(blankLineAfterBoundary, payloadLength);
        }

        /// <summary>
        /// Extracts the boundary from a Content-Type string
        /// </summary>
        /// <param name="contentType">e.g: multipart/signed; protocol="application/pkcs7-signature"; micalg="sha1"; boundary="_956100ef6a82431fb98f65ee70c00cb9_"</param>
        /// <returns>e.g: _956100ef6a82431fb98f65ee70c00cb9_</returns>
        public static string GetBoundaryFromContentType(string contentType)
        {
            return Trim(contentType, "boundary=\"", "\"");
        }

        /// <summary>
        /// Trims the string from the end of startString until endString
        /// </summary>
        private static string Trim(string str, string start, string end)
        {
            int startIndex = str.IndexOf(start) + start.Length;
            int endIndex = str.IndexOf(end, startIndex);
            int length = endIndex - startIndex;

            return str.Substring(startIndex, length);
        }
    }
}