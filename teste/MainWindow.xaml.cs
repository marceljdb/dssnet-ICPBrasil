﻿using EU.Europa.EC.Markt.Dss;
using EU.Europa.EC.Markt.Dss.Signature;
using EU.Europa.EC.Markt.Dss.Signature.Cades;
using EU.Europa.EC.Markt.Dss.Signature.Token;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace teste
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private X509Certificate2 GetCertificate(string certID)
        {
            // Access Personal (MY) certificate store of current user
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.MaxAllowed);

            // Find the certificate we'll use to sign            
            X509Certificate2 certificate = null;
            foreach (X509Certificate2 cert in store.Certificates)
                if (cert.Subject.Contains(certID))
                {
                    certificate = cert;
                    break;
                }

            if (certificate == null)
                throw new Exception("Nenhum certificado válido foi encontrado.");

            return certificate;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var service = new CAdESService();

            // Creation of MS CAPI signature token
            var cert = GetCertificate("47199695004");
            //cert.Import(@"Resources\Certificado DEMOLINER E CIA LTDA.p12","renan2", System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.Exportable);
            var token = new MSCAPISignatureToken { Cert = cert };


            var certBouncy = DotNetUtilities.FromX509Certificate(token.Cert);

            //String privateKeyFileName = "C:\\myPrivateKey.der";
            //Path path = Path.getget(privateKeyFileName);
            //    byte[] privKeyByteArray = File.ReadAllBytes(@"Resources\PA_AD_RB_v2_1.der");
            //  var priv = Asn1Object.FromByteArray(privKeyByteArray);           
            //            var privStruct = new RsaPrivateKeyStructure((Asn1Sequence)priv);
            //var sequence = (Asn1Sequence)priv;
            //var encodable = (Asn1Encodable)sequence[2];
            //var testeString = ((Org.BouncyCastle.Asn1.Asn1OctetString)encodable).GetOctets();
            //var teste = Encoding.ASCII.GetBytes(encodable.ToString());
            var value = new byte[] { 221, 87, 201, 138, 67, 19, 188, 19, 152, 206, 101, 67, 211, 128, 36, 88, 149, 124, 247, 22, 174, 50, 148, 236, 77, 140, 38, 37, 18, 145, 230, 193 };
            
            //URL Verificador - https://verificador.iti.gov.br/verificador.xhtml
            var parameters = new SignatureParameters
            {
                SignatureAlgorithm = SignatureAlgorithm.RSA,
                SignatureFormat = SignatureFormat.CAdES_BES,
                DigestAlgorithm = DigestAlgorithm.SHA256,
                SignaturePackaging = SignaturePackaging.ENVELOPING,
                SigningCertificate = certBouncy,
                SigningDate = DateTime.UtcNow,
                SignaturePolicy = SignaturePolicy.EXPLICIT,
                SignaturePolicyHashValue = value, 
                SignaturePolicyID = "2.16.76.1.7.1.1.2.1",
                SignaturePolicyHashAlgo = "SHA-256"              
                         
            };
            

            var toBeSigned = new FileDocument(@"Resources\teste.pdf");
            var bytes = Streams.ReadAll(toBeSigned.OpenStream());
            service.contentBytes = bytes;
            var iStream = service.ToBeSigned(new FileDocument(""), parameters);

            var signatureValue = token.Sign(iStream, parameters.DigestAlgorithm, token.GetKeys()[0]);
            var dest = @"Resources\teste.p7s";

            var signedDocument = service.SignDocument(new FileDocument(""), parameters, signatureValue);            

            if (File.Exists(dest)) File.Delete(dest);
            var fout = File.OpenWrite(dest);
            signedDocument.OpenStream().CopyTo(fout);
            fout.Close();
        }
    }
    
}
