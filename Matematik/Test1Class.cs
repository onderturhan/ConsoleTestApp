using System;

namespace Matematik
{
    public class PublicClass
    {
        public string PublicVeri { get; set; }
        private string PrivateVeri { get; set; }
        internal string InternalVeri { get; set; }

        void topla()
        {
            //Ok PublicVeri
            //Ok PrivateVeri
            //Ok InternalVeri
        }
    }

    internal class InternalClass
    {
        public string PublicVeri { get; set; }
        private string PrivateVeri { get; set; }
        internal string InternalVeri { get; set; }

        void topla()
        {
            //Ok PublicVeri
            //Ok PrivateVeri
            //Ok InternalVeri
        }
    }

    //private class PrivateClass
    //{
    //    public string PublicVeri { get; set; }
    //    private string PrivateVeri { get; set; }
    //    internal string InternalVeri { get; set; }

    //    void topla()
    //    {
    //        //Ok PublicVeri
    //        //Ok PrivateVeri
    //        //Ok InternalVeri
    //    }
    //}

    public class Topla
    {
        void hesapla()
        {
            PublicClass p = new PublicClass();
            //Ok p.PublicVeri
            //Not p.PrivateVeri
            //Ok p.InternalVeri

            InternalClass i = new InternalClass();
            //Ok i.PublicVeri
            //Not i.PrivateVeri
            //Ok i.InternalVeri

            //Not PrivateClass pr = new PrivateClass();
        }
    }



    public class PublicBaseClass
    {
        protected string ProtectedVeri { get; set; }
        protected internal string ProtectedInternalVeri { get; set; }
        private protected string PrivateProtectedVeri { get; set; }
    }

    public class PublicDerrivedClass : PublicBaseClass
    {
        void topla()
        {
            //Ok ProtectedVeri
            //Ok ProtectedInternalVeri
            //Ok PrivateProtectedVeri

            PublicBaseClass bc = new PublicBaseClass();
            //Not bc.ProtectedVeri
            //Ok bc.ProtectedInternalVeri
            //Not bc.PrivateProtectedVeri
        }
    }
}