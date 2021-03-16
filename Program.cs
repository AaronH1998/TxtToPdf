using System;
using System.IO;
using System.Linq;

namespace AaronHodgsonTextToPDF
{
    class Program
    {
        //Program requires one arguement which specifies the location of the text file to be converted.
        static void Main(string[] args)
        {
            var pdfWriter =  new iTextWrapper();
            
            var conversionController = new ConversionController(pdfWriter);

            for (var x = 0; x < 10; x++) //Included to demonstrate paginaton.
            {
                foreach (var line in GetLines(args[0]))
                {
                    conversionController.Convert(line);
                }
                pdfWriter.CommitParagraph();
            }

            pdfWriter.CloseDocument();
        }

        private static string[] GetLines(string inputDir)
        {
            using (StreamReader reader = new StreamReader(inputDir))
            {
                var fileRepository = new FileRepository();
                return fileRepository.ReadSourceDocument(reader);
            }
        }
    }
}
