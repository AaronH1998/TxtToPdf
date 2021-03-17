using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Drawing;

namespace AaronHodgsonTextToPDF
{
    public class iTextWrapper : IPdfWriter
    {
        private Document Document { get; set; }
        private Paragraph Paragraph { get; set; }
        private Text Text { get; set; }

        public iTextWrapper(string outputDir)
        {
            if (string.IsNullOrEmpty(outputDir)) 
                throw new ArgumentException();

            PdfWriter writer = new PdfWriter(outputDir);
            PdfDocument pdf = new PdfDocument(writer);
            Document = new Document(pdf);

            StartParagraph();
        }

        private void StartParagraph()
        {
            Paragraph = new Paragraph();
            Text = new Text("");
        }

        public void AddToParagraph(string text)
        {
            if (text == null) 
                throw new ArgumentNullException();

            Text.SetText(text);
            Paragraph.Add(Text);
            Text = new Text("");              
        }

        public void CommitParagraph()
        {
            Document.Add(Paragraph);
            StartParagraph();
        }

        public void SetFontSize(float size)
        {
            if (size == null)
                throw new ArgumentNullException();
            Text.SetFontSize(size);
        }

        public void SetItalic()
        {
            Text.SetItalic();
        }

        public void SetBold()
        {
            Text.SetBold();
        }

        public void Fill()
        {
            Paragraph.SetTextAlignment(TextAlignment.JUSTIFIED);
        }

        public void Indent(int indentValue)
        {
            using (Graphics graphics = Graphics.FromImage(new Bitmap(1, 1)))
            {
                float indentUnit = graphics.MeasureString("WWWWW", new Font("Helvetica", 12, FontStyle.Regular, GraphicsUnit.Point)).Width; 
                //Requirements stated that each unit should be the size of the string "WWWWW"

                Paragraph.SetMarginLeft(indentValue * indentUnit);
            }
        }

        public void CloseDocument()
        {
            CommitParagraph(); //Ensure all paragraphs have been committed before closing the document.
            Document.Close();
        }
    }
}
