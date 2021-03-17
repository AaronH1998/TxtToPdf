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
            
            var textToPdfConverter = new TextToPdfConverter(pdfWriter);

            for (var x = 0; x < 10; x++) //Included to demonstrate paginaton.
            {
                new ConversionController(new FileRepository()).Run(args[0], textToPdfConverter, pdfWriter);
            }
            pdfWriter.CloseDocument();
        }
    }
}