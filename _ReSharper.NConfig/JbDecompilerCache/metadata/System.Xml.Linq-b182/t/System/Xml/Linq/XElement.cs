// Type: System.Xml.Linq.XElement
// Assembly: System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Xml.Linq.dll

using MS.Internal.Xml.Linq.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Xml.Linq
{
    [TypeDescriptionProvider(typeof (XTypeDescriptionProvider<XElement>))]
    [XmlSchemaProvider(null, IsAny = true)]
    public class XElement : XContainer, IXmlSerializable
    {
        public XElement(XName name);
        public XElement(XName name, object content);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public XElement(XName name, params object[] content);

        public XElement(XElement other);
        public XElement(XStreamingElement other);
        public static IEnumerable<XElement> EmptySequence { get; }
        public XAttribute FirstAttribute { get; }
        public bool HasAttributes { get; }
        public bool HasElements { get; }
        public bool IsEmpty { get; }

        public XAttribute LastAttribute { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public XName Name { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        public override XmlNodeType NodeType { get; }
        public string Value { get; set; }

        #region IXmlSerializable Members

        XmlSchema IXmlSerializable.GetSchema();
        void IXmlSerializable.ReadXml(XmlReader reader);
        void IXmlSerializable.WriteXml(XmlWriter writer);

        #endregion

        [CLSCompliant(false)]
        public static explicit operator string(XElement element);

        [CLSCompliant(false)]
        public static explicit operator bool(XElement element);

        [CLSCompliant(false)]
        public static explicit operator bool?(XElement element);

        [CLSCompliant(false)]
        public static explicit operator int(XElement element);

        [CLSCompliant(false)]
        public static explicit operator int?(XElement element);

        [CLSCompliant(false)]
        public static explicit operator uint(XElement element);

        [CLSCompliant(false)]
        public static explicit operator uint?(XElement element);

        [CLSCompliant(false)]
        public static explicit operator long(XElement element);

        [CLSCompliant(false)]
        public static explicit operator long?(XElement element);

        [CLSCompliant(false)]
        public static explicit operator ulong(XElement element);

        [CLSCompliant(false)]
        public static explicit operator ulong?(XElement element);

        [CLSCompliant(false)]
        public static explicit operator float(XElement element);

        [CLSCompliant(false)]
        public static explicit operator float?(XElement element);

        [CLSCompliant(false)]
        public static explicit operator double(XElement element);

        [CLSCompliant(false)]
        public static explicit operator double?(XElement element);

        [CLSCompliant(false)]
        public static explicit operator decimal(XElement element);

        [CLSCompliant(false)]
        public static explicit operator decimal?(XElement element);

        [CLSCompliant(false)]
        public static explicit operator DateTime(XElement element);

        [CLSCompliant(false)]
        public static explicit operator DateTime?(XElement element);

        [CLSCompliant(false)]
        public static explicit operator DateTimeOffset(XElement element);

        [CLSCompliant(false)]
        public static explicit operator DateTimeOffset?(XElement element);

        [CLSCompliant(false)]
        public static explicit operator TimeSpan(XElement element);

        [CLSCompliant(false)]
        public static explicit operator TimeSpan?(XElement element);

        [CLSCompliant(false)]
        public static explicit operator Guid(XElement element);

        [CLSCompliant(false)]
        public static explicit operator Guid?(XElement element);

        public IEnumerable<XElement> AncestorsAndSelf();
        public IEnumerable<XElement> AncestorsAndSelf(XName name);
        public XAttribute Attribute(XName name);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public IEnumerable<XAttribute> Attributes();

        public IEnumerable<XAttribute> Attributes(XName name);
        public IEnumerable<XNode> DescendantNodesAndSelf();
        public IEnumerable<XElement> DescendantsAndSelf();
        public IEnumerable<XElement> DescendantsAndSelf(XName name);
        public XNamespace GetDefaultNamespace();
        public XNamespace GetNamespaceOfPrefix(string prefix);
        public string GetPrefixOfNamespace(XNamespace ns);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static XElement Load(string uri);

        public static XElement Load(string uri, LoadOptions options);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static XElement Load(Stream stream);

        public static XElement Load(Stream stream, LoadOptions options);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static XElement Load(TextReader textReader);

        public static XElement Load(TextReader textReader, LoadOptions options);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static XElement Load(XmlReader reader);

        public static XElement Load(XmlReader reader, LoadOptions options);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static XElement Parse(string text);

        public static XElement Parse(string text, LoadOptions options);
        public void RemoveAll();
        public void RemoveAttributes();
        public void ReplaceAll(object content);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void ReplaceAll(params object[] content);

        public void ReplaceAttributes(object content);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void ReplaceAttributes(params object[] content);

        public void Save(string fileName);
        public void Save(string fileName, SaveOptions options);
        public void Save(Stream stream);
        public void Save(Stream stream, SaveOptions options);
        public void Save(TextWriter textWriter);
        public void Save(TextWriter textWriter, SaveOptions options);
        public void Save(XmlWriter writer);
        public void SetAttributeValue(XName name, object value);
        public void SetElementValue(XName name, object value);
        public void SetValue(object value);
        public override void WriteTo(XmlWriter writer);
    }
}
