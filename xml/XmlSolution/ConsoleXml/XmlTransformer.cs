using System.Xml.Xsl;

namespace ConsoleXml
{
    public class XmlTransformer
    {
        public void Transform(string xmlPath, string xsltPath, string htmlPath)
        {
            var xslCompiledTransform = new XslCompiledTransform();
            xslCompiledTransform.Load(xsltPath, new XsltSettings(false, true), null);
            xslCompiledTransform.Transform(xmlPath, htmlPath);
        }
    }
}
