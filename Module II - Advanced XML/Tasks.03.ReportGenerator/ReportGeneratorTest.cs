using System;
using System.Xml.Xsl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks._03.ReportGenerator
{
    [TestClass]
    public class ReportGeneratorTest
    {
        [TestMethod]
        public void XsltTest()
        {
            var xsl = new XslCompiledTransform();
            xsl.Load("XmlToHtmlXSLT.xslt");
            xsl.Transform("Document.xml", null, Console.Out);
        }
    }
}
