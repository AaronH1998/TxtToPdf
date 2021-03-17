using Microsoft.VisualStudio.TestTools.UnitTesting;
using AaronHodgsonTextToPDF;
using Moq;
using iText.Layout.Properties;
using System;

namespace TextToPdfTests
{
    [TestClass]
    public class TextToPdfConverterTests
    {
        [TestMethod]
        public void LargeCommand_SetFontSizeTo20()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".large");

            //assert
            mockPdfWriter.Verify(mock => mock.SetFontSize(20), Times.Once);
        }

        [TestMethod]
        public void NormalCommand_SetFontSizeTo20()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".normal");

            //assert
            mockPdfWriter.Verify(mock => mock.SetFontSize(12), Times.Once);
        }

        [TestMethod]
        public void ItalicsCommand_SetItalicText()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".italics");

            //assert
            mockPdfWriter.Verify(mock => mock.SetItalic(), Times.Once);
        }

        [TestMethod]
        public void BoldCommand_SetBoldText()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".bold");

            //assert
            mockPdfWriter.Verify(mock => mock.SetBold(), Times.Once);
        }

        [TestMethod]
        public void FillCommand_ParagraphFillsSpace()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".fill");

            //assert
            mockPdfWriter.Verify(mock => mock.Fill(), Times.Once);
        }

        [TestMethod]
        public void RegularCommand_DoesNothing()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".regular");

            //assert
            mockPdfWriter.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void ParagraphCommand_CommitsParagraph()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".paragraph");

            //assert
            mockPdfWriter.Verify(mock => mock.CommitParagraph(), Times.Once);
        }

        [TestMethod]
        public void NoFillCommand_CommitsParagraph()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".nofill");

            //assert
            mockPdfWriter.Verify(mock => mock.CommitParagraph(), Times.Once);
        }

        [TestMethod]
        public void ZeroIndent_DoesNothing()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".indent");

            //assert
            mockPdfWriter.Verify(mock => mock.Indent(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void InvalidIndentCommand_ThrowsFormatException()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".indent +a");

            //assert - Exception
        }

        [TestMethod]
        public void PositiveIndentCommand_IndentsParagraph()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".indent +2");

            //assert
            mockPdfWriter.Verify(mock => mock.Indent(2), Times.Once);
        }

        [TestMethod]
        public void LargeIndentCommand_IndentsParagraph()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".indent +10000");

            //assert
            mockPdfWriter.Verify(mock => mock.Indent(10000), Times.Once);
        }

        [TestMethod]
        public void IndentWithSpaceCommand_IndentsParagraph()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".indent + 10000");

            //assert
            mockPdfWriter.Verify(mock => mock.Indent(10000), Times.Once);
        }

        [TestMethod]
        public void NegativeIndentCommand_DoesNothing()
        {
            //assign
            var mockPdfWriter = new Mock<IPdfWriter>();
            var textToPdfConverter = new TextToPdfConverter(mockPdfWriter.Object);

            //act
            textToPdfConverter.Convert(".indent -2");

            //assert
            mockPdfWriter.VerifyNoOtherCalls();
        }
    }
}