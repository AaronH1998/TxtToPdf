using AaronHodgsonTextToPDF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TextToPdfTests
{
    [TestClass]
    public class SerialiserTests
    {
        [TestMethod]
        public void EmptyFile_ReturnsSingleEmptyElement()
        {
            var serialiser = new GearsetSerialiser();
            var actual = serialiser.Deserialise(string.Empty);


            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("", actual[0]);
        }

        [TestMethod]
        public void SingleLine_ReturnsSinglePopulatedElement()
        {
            var serialiser = new GearsetSerialiser();

            var actual = serialiser.Deserialise(".large");

            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(".large", actual[0]);

        }

        [TestMethod]
        public void ThreeLines_ReturnsThreePopulatedElements()
        {
            //asign
            var serialiser = new GearsetSerialiser();

            //act
            var actual = serialiser.Deserialise(".large\n.regular\nabcde");

            //assert
            Assert.AreEqual(3, actual.Length);
            Assert.AreEqual(".large", actual[0]);
            Assert.AreEqual(".regular", actual[1]);
            Assert.AreEqual("abcde", actual[2]);
        }

        [TestMethod]
        public void ThreeLines_ReturnsTrimmedLines()
        {
            //assign
            var serialiser = new GearsetSerialiser();

            //act
            var actual = serialiser.Deserialise(".large\r\n.regular\r\nabcde\r");

            //assert
            Assert.AreEqual(3, actual.Length);
            Assert.AreEqual(".large", actual[0]);
            Assert.AreEqual(".regular", actual[1]);
            Assert.AreEqual("abcde", actual[2]);
        }
    }
}
