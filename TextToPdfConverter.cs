using System;
using System.Linq;

namespace AaronHodgsonTextToPDF
{
    public class TextToPdfConverter : ITextToPdfConverter
    {
        private readonly IPdfWriter pdfWriter;

        public TextToPdfConverter(IPdfWriter pdfWriter)
        {
            this.pdfWriter = pdfWriter;
        }

        public void Convert(string line)
        {
            if (line == null) 
                throw new ArgumentNullException();

            if (line.StartsWith(".large"))
            {
                pdfWriter.SetFontSize(20);
            }
            else if (line.StartsWith(".normal"))
            {
                pdfWriter.SetFontSize(12); //Can be swallowed as format is reset upon creating a new Text.
            }
            else if (line.StartsWith(".italics"))
            {
                pdfWriter.SetItalic();
            }
            else if (line.StartsWith(".fill"))
            {
                pdfWriter.Fill();
            }
            else if (line.StartsWith(".nofill"))
            {
                pdfWriter.CommitParagraph();
            }
            else if (line.StartsWith(".bold"))
            {
                pdfWriter.SetBold();
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
                    pdfWriter.Indent(indentValue);
                }
                //negative indent is swallowed as format is reset upon creating a new Paragraph
            }
            else if (line.StartsWith(".paragraph"))
            {
                pdfWriter.CommitParagraph();
            }
            else
            {
                pdfWriter.AddToParagraph(line);
            }
        }
    }
}
