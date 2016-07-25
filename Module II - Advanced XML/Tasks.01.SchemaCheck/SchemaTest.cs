using System;
using System.Xml;
using System.Xml.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks._01.SchemaCheck
{
    [TestClass]
    public class SchemaTest
    {
        XmlReaderSettings settings;

        [TestInitialize]
        public void Init()
        {
            settings = new XmlReaderSettings();

            settings.Schemas.Add("http://library.by/catalog", "BooksCatalogSchemaXSD.xsd");
            settings.ValidationEventHandler +=
                delegate(object sender, ValidationEventArgs e)
                {
                    Console.WriteLine("[{0}:{1}] {2}", e.Exception.LineNumber, e.Exception.LinePosition, e.Message);
                };

            settings.ValidationFlags = settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;
        }

        [TestMethod]
        public void TestDocumentIsValid()
        {
            XmlReader reader = XmlReader.Create("books.xml", settings);

            while (reader.Read()) ;
        }
    }
}
