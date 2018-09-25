using iTextSharp.text;
using iTextSharp.text.pdf;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Class
{
    public class QR
    {

        
        // Genera QR con iTextSharp
        public  void generate()
        {

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("Este es un texto", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            //Image i = new Image();
            //Bitmap qrCodeImage = qrCode.GetGraphic(20);
            string v = string.Empty;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    v = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    Console.WriteLine(v);
                }
                // plBarCode.Controls.Add(imgBarCode);
            }

        }


         // Propuesta CRECE
        public void ObtenerQRDesdeCRECE()
        {
            Document doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            string pdfFilePath = "C:\\Desarrollo";
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pdfFilePath + "\\pdf.pdf", FileMode.Open));
            doc.Open();
            try

            {
                Paragraph paragraph = new Paragraph("Getsssssssting Started ITextSharp.");
                string imageURL = "C:\\Desarrollo\\HI16157.png";
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("http://192.168.2.209/creceimp/qr.php?valor=123");
                //Resize image depend upon your need
                jpg.ScaleToFit(140f, 120f);
                //Give space before image
                jpg.SpacingBefore = 10f;
                //Give some space after the image
                jpg.SpacingAfter = 1f;
                jpg.Alignment = Element.ALIGN_LEFT;
                doc.Add(paragraph);
                doc.Add(jpg);
            }
            catch (Exception ex)
            {
            }
            finally

            {
                doc.Close();
            }
            ManipulatePdf();
        }

        private  void ManipulatePdf()
        {
            using (Stream inputPdfStream = new FileStream("C:\\Desarrollo\\pdf.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream inputImageStream = new FileStream("C:\\Desarrollo\\HI16157.png", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream outputPdfStream = new FileStream("C:\\Desarrollo\\result.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var reader = new PdfReader(inputPdfStream);
                var stamper = new PdfStamper(reader, outputPdfStream);
                var pdfContentByte = stamper.GetOverContent(1);

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);
                image.ScaleAbsolute(new iTextSharp.text.Rectangle(50, 50));
                image.SetAbsolutePosition(100, 100);
                pdfContentByte.AddImage(image);
                stamper.Close();
            }
        }




    }
}
