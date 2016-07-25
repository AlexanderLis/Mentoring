using System;
using System.Xml;
using System.Xml.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks._01.Extra.SpecificationCheck
{
    [TestClass]
    public class SpecificationTest
    {
        private XmlChecker xmlChecker;

        [TestInitialize]
        public void Init()
        {
            xmlChecker = new XmlChecker("http://cob.wk/specification", "SpecificationSchemaXSD.xsd");
        }

        [TestMethod]
        public void TestDocumentIsValid()
        {
            xmlChecker.Read();
            var t = xmlChecker.Errors;
        }
    }
}
