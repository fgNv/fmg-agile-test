using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileContentTest.Tests
{
    [TestClass]
    public class MinhaCdnTests
    {
        private string _destinationPath = "result.csv";

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(_destinationPath);
        }

        [TestMethod]
        public async Task ConvertLog()
        {
            var sourceUrl = "https://docs.google.com/document/d/1QH3RhRZnoJJMmKbA8Nt3Ax15lQYKyVcmAm8CAEux36o/export?format=txt";
            var converter = new MinhaCdnToAgoraLogConverter();
            var application = new LogConverterApplication(converter);
            await application.ConvertMinhaCdnToAgora(sourceUrl, _destinationPath);

            Assert.IsTrue(File.Exists(_destinationPath));

            var resultContent = File.ReadAllText(_destinationPath);
            
            var expectedResultWithoutHeader = "\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT" + Environment.NewLine;
            expectedResultWithoutHeader += "\"MINHA CDN\" POST 200 /myImages 319 101 MISS" + Environment.NewLine;
            expectedResultWithoutHeader += "\"MINHA CDN\" GET 404 /not-found 143 199 MISS" + Environment.NewLine;
            expectedResultWithoutHeader += "\"MINHA CDN\" GET 200 /robots.txt 245 312 REFRESH_HIT";
                        
            var resultWithoutHeader = String.Join(Environment.NewLine, resultContent.Split(Environment.NewLine).Skip(3).ToArray());

            Assert.AreEqual(expectedResultWithoutHeader, resultWithoutHeader);
        }
    }
}
