using AaronHodgsonTextToPDF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace TextToPdfTests
{
    [TestClass]
    public class ConversionControllerTests
    {
        [TestMethod]
        public void Run_ConvertsNTimes()
        {
            //assign
            var mockFileRepository = new Mock<IFileRepository>();
            var mockPdfWriter = new Mock<IPdfWriter>();
            var mockConverter = new Mock<ITextToPdfConverter>();
            
            var inputDir = "not used";
            var lines = new string[] { ".large\n", "Hello\n", ".bold\n", "World!" };

            mockFileRepository.Setup(p => p.GetLines(inputDir)).Returns(lines);

            var conversionController = new ConversionController(mockFileRepository.Object);

            //act
            conversionController.Run(inputDir, mockConverter.Object,mockPdfWriter.Object);
           
            //assert
            mockConverter.Verify(mock => mock.Convert(It.IsAny<string>()), Times.Exactly(4));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyInputDir_ThrowsArgumentException()
        {
            var mockFileRepository = new Mock<IFileRepository>();
            var mockPdfWriter = new Mock<IPdfWriter>();
            var mockConverter = new Mock<ITextToPdfConverter>();
            var inputDir = "";

            var conversionController = new ConversionController(mockFileRepository.Object);

            conversionController.Run(inputDir, mockConverter.Object, mockPdfWriter.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullConverter_ThrowsArgumentNullException()
        {
            var mockFileRepository = new Mock<IFileRepository>();
            var mockPdfWriter = new Mock<IPdfWriter>();           

            var inputDir = "notempty";

            var conversionController = new ConversionController(mockFileRepository.Object);

            conversionController.Run(inputDir, null, mockPdfWriter.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullWriter_ThrowsArgumentNullException()
        {
            var mockFileRepository = new Mock<IFileRepository>();
            var mockConverter = new Mock<ITextToPdfConverter>();


            var inputDir = "notempty";

            var conversionController = new ConversionController(mockFileRepository.Object);

            conversionController.Run(inputDir, mockConverter.Object, null);
        }
    }
}
