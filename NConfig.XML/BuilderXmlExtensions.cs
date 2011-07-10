using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NConfig.Model;
using System.Xml.Linq;
using System.Reflection;
using NConfig.XML;

namespace NConfig
{
    public static class BuilderXmlExtensions
    {
        public static Configure AddFromXml(this Configure source, string fileName)
        {
            foreach (var section in new XmlLoader().LoadFile(fileName))
            {
                source.SectionsProviders.Add(section.Name, section.ToSectionProvider(source));
            }
            return source;
        }
    }
}
