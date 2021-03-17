using System;
using System.IO;
using System.Linq;

namespace AaronHodgsonTextToPDF
{
    class Program
    {
        //Program requires two arguements:
        //1. args[0] = Location of the text file to be converted. Please Edit before running.
        //2. args[1] = Location of the pdf file to be outputted. Please Edit before running.
        static void Main(string[] args)
        {
            if(string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]))
                throw new ArgumentNullException();
            

            var pdfWriter =  new iTextWrapper(args[1]);
            
            var textToPdfConverter = new TextToPdfConverter(pdfWriter);

            for (var x = 0; x < 9; x++) //Included to demonstrate paginaton.
            {
                new ConversionController(new FileRepository()).Run(args[0], textToPdfConverter, pdfWriter);
            }
            pdfWriter.CloseDocument();
        }
    }
}