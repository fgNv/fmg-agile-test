using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AgileContentTest.Tests
{
    [TestClass]
    public class SimblingNumberFinderTests
    {
        [TestMethod]
        public void Providing213ShouldRetrieve321()
        {
            var finder = new GreatestSiblingNumberFinder();
            var result = finder.Find(213);

            Assert.AreEqual(result, 321);
        }

        [TestMethod]
        public void Providing553ShouldRetrieve553()
        {
            var finder = new GreatestSiblingNumberFinder();
            var result = finder.Find(553);

            Assert.AreEqual(result, 553);
        }

        [TestMethod]
        public void WhenResultIsGreaterThan100000000ShouldRetrieveMinus1()
        {
            var finder = new GreatestSiblingNumberFinder();
            var result = finder.Find(120100001);

            Assert.AreEqual(result, -1);
        }
    }
}
