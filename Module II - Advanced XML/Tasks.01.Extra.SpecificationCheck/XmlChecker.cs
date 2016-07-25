using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace Tasks._01.Extra.SpecificationCheck
{
    public class XmlChecker
    {
        public string SchemaPath { get; set; }

        public string TargetNamespace { get; set; }

        public XmlReaderSettings Settings { get; set; }

        public List<string> Errors { get; set; }

        public XmlChecker(string targetNamespace, string schemaPath)
        {
            SchemaPath = schemaPath;
            TargetNamespace = targetNamespace;
            Initialize();
        }

        public void Initialize()
        {
            Settings = new XmlReaderSettings();

            Settings.Schemas.Add(TargetNamespace, SchemaPath);
            Settings.ValidationEventHandler +=
                delegate(object sender, ValidationEventArgs e)
                {
                    Errors.Add( String.Format("[{0}:{1}] {2}", e.Exception.LineNumber, e.Exception.LinePosition, e.Message));
                };

            Settings.ValidationFlags = Settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
            Settings.ValidationType = ValidationType.Schema;

            Errors = new List<string>();
        }

        public void Read()
        {
            Errors.Clear();
            var reader = XmlReader.Create("Document.xml", Settings);

            while (reader.Read()) ;
        }
    }
}
