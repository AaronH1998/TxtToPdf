using System;

namespace AaronHodgsonTextToPDF
{
    public class ConversionController
    {
        private readonly IFileRepository fileRepository;
        public ConversionController(IFileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        public void Run(string inputDir, ITextToPdfConverter textToPdfConverter, IPdfWriter pdfWriter)
        {
            if (string.IsNullOrEmpty(inputDir)) throw new ArgumentException();
            if (textToPdfConverter == null || pdfWriter == null) throw new ArgumentNullException();

            foreach (var line in fileRepository.GetLines(inputDir))
            {
                textToPdfConverter.Convert(line);
            }
            pdfWriter.CommitParagraph();
        }
    }
}
