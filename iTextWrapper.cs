using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AaronHodgsonTextToPDF
{
    public class iTextWrapper : IPdfWriter
    {
        private Document Document { get; set; }
        private Paragraph Paragraph { get; set; }
        private Text Text { get; set; }

        public iTextWrapper()
        {
            string outputDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Output\\output.pdf";

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
            
            Paragraph.SetMarginLeft(indentValue * 20);
        }

        public void CloseDocument()
        {
            CommitParagraph(); //Ensure all paragraphs have been committed before closing the document.
            Document.Close();
        }
    }
}
