using System;
using System.IO;
using System.Text;
using System.Xml.Xsl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks._03.Report
{
    [TestClass]
    public class ReportGenerator
    {
        [TestMethod]
        public void ReportGeneratorTest()
        {
            var xsl = new XslCompiledTransform();
            xsl.Load("XmlToHtmlXSLT.xsl");
            var xslParams = new XsltArgumentList();
            xslParams.AddParam("Date", "", DateTime.Now.ToShortDateString());

            StringWriter writer = new StringWriter();
            xsl.Transform("books.xml", xslParams, writer);

            var html = writer.ToString();
            File.WriteAllText(@"output.html", html, Encoding.UTF8);

            writer.Close();
		

        }
    }
}
