using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AaronHodgsonTextToPDF
{
    public class ConversionController
    {
        private readonly IPdfWriter pdfController;

        public ConversionController(IPdfWriter pdfController)
        {
            this.pdfController = pdfController;
        }

        public void Convert(string line)
        {

            if (line.StartsWith(".large"))
            {
                pdfController.SetFontSize(20);
            }
            else if (line.StartsWith(".normal"))
            {
                pdfController.SetFontSize(12); //Can be swallowed as format is reset upon creating a new Text.
            }
            else if (line.StartsWith(".italics"))
            {
                pdfController.SetItalic();
            }
            else if (line.StartsWith(".fill"))
            {
                pdfController.Fill();
            }
            else if (line.StartsWith(".nofill"))
            {
                pdfController.CommitParagraph();
            }
            else if (line.StartsWith(".bold"))
            {
                pdfController.SetBold();
            }
            else if (line.StartsWith(".regular"))
            {
                //.regular command is swallowed as format is reset upon creating a new Text.
            }
            else if (line.StartsWith(".indent"))
            {
                if (line.Contains("+"))
                {
                    var indentValue = System.Convert.ToInt32(line.Split("+").Last());
                    pdfController.Indent(indentValue);
                }
                //negative indent is swallowed as format is reset upon creating a new Paragraph
            }
            else if (line.StartsWith(".paragraph"))
            {
                pdfController.CommitParagraph();
            }
            else
            {
                pdfController.AddToParagraph(line);
            }
        }
    }
}
