using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Xsl;

namespace ConsoleXml
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckXmlSchema();
            CheckXslt("Books.xml", "BookToHTML.xslt", "books.html");

            Console.ReadLine();
        }

        private static void CheckXmlSchema()
        {
            var xmlReaderSettings = new XmlReaderSettings();

            xmlReaderSettings.Schemas.Add("http://library.by/catalog", "BookSchema.xsd");
            xmlReaderSettings.ValidationEventHandler += (s, ex) =>
                Console.WriteLine("Line {0}:{1} Message: {2}", ex.Exception.LineNumber, ex.Exception.LinePosition, ex.Message);

            xmlReaderSettings.ValidationFlags = xmlReaderSettings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
            xmlReaderSettings.ValidationType = ValidationType.Schema;

            XmlReader reader = XmlReader.Create("books.xml", xmlReaderSettings);
            while (reader.Read())
            {
                // just read - validation messages will be typed in ValidationEventHandler
            }
            XmlReader reader1 = XmlReader.Create("books1.xml", xmlReaderSettings);

            while (reader1.Read())
            {
                // just read - validation messages will be typed in ValidationEventHandler
            }
        }

        private static void CheckXslt(string xmlPath, string xsltPath, string htmlPath)
        {
            var xslCompiledTransform = new XslCompiledTransform();
            xslCompiledTransform.Load(xsltPath, new XsltSettings(false, true), null);
            xslCompiledTransform.Transform(xmlPath, htmlPath);
        }
    }
}
