namespace TheBeerHouse.My
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Xml.Linq;

    [DebuggerNonUserCode, CompilerGenerated, EditorBrowsable(EditorBrowsableState.Never)]
    internal sealed class InternalXmlHelper
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        private InternalXmlHelper()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static XAttribute CreateAttribute(XName name, object value)
        {
            if (value == null)
            {
                return null;
            }
            return new XAttribute(name, RuntimeHelpers.GetObjectValue(value));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static XAttribute CreateNamespaceAttribute(XName name, XNamespace ns)
        {
            XAttribute a = new XAttribute(name, ns.NamespaceName);
            a.AddAnnotation(ns);
            return a;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable RemoveNamespaceAttributes(string[] inScopePrefixes, XNamespace[] inScopeNs, List<XAttribute> attributes, IEnumerable obj)
        {
            if (obj != null)
            {
                IEnumerable<XElement> elems = obj as IEnumerable<XElement>;
                if (elems != null)
                {
                    RemoveNamespaceAttributesClosure closure1 = new RemoveNamespaceAttributesClosure(inScopePrefixes, inScopeNs, attributes);
                    return elems.Select<XElement, XElement>(new Func<XElement, XElement>(closure1.ProcessXElement));
                }
                RemoveNamespaceAttributesClosure closure2 = new RemoveNamespaceAttributesClosure(inScopePrefixes, inScopeNs, attributes);
                return obj.Cast<object>().Select<object, object>(new Func<object, object>(closure2.ProcessObject));
            }
            return obj;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static object RemoveNamespaceAttributes(string[] inScopePrefixes, XNamespace[] inScopeNs, List<XAttribute> attributes, object obj)
        {
            if (obj != null)
            {
                XElement elem = obj as XElement;
                if (elem != null)
                {
                    return RemoveNamespaceAttributes(inScopePrefixes, inScopeNs, attributes, elem);
                }
                IEnumerable elems = obj as IEnumerable;
                if (elems != null)
                {
                    return RemoveNamespaceAttributes(inScopePrefixes, inScopeNs, attributes, elems);
                }
            }
            return obj;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static XElement RemoveNamespaceAttributes(string[] inScopePrefixes, XNamespace[] inScopeNs, List<XAttribute> attributes, XElement e)
        {
            if (e != null)
            {
                XAttribute nextA;
                for (XAttribute a = e.FirstAttribute; a != null; a = nextA)
                {
                    nextA = a.NextAttribute;
                    if (a.IsNamespaceDeclaration)
                    {
                        XNamespace ns = a.Annotation<XNamespace>();
                        string prefix = a.Name.LocalName;
                        if (ns != null)
                        {
                            if ((((inScopePrefixes != null) && (inScopeNs != null)) ? 1 : 0) != 0)
                            {
                                int lastIndex = inScopePrefixes.Length - 1;
                                int VB$t_i4$L0 = lastIndex;
                                for (int i = 0; i <= VB$t_i4$L0; i++)
                                {
                                    string currentInScopePrefix = inScopePrefixes[i];
                                    XNamespace currentInScopeNs = inScopeNs[i];
                                    if (prefix.Equals(currentInScopePrefix))
                                    {
                                        if (ns == currentInScopeNs)
                                        {
                                            a.Remove();
                                        }
                                        a = null;
                                        break;
                                    }
                                }
                            }
                            if (a != null)
                            {
                                if (attributes != null)
                                {
                                    int lastIndex = attributes.Count - 1;
                                    int VB$t_i4$L1 = lastIndex;
                                    for (int i = 0; i <= VB$t_i4$L1; i++)
                                    {
                                        XAttribute currentA = attributes[i];
                                        string currentInScopePrefix = currentA.Name.LocalName;
                                        XNamespace currentInScopeNs = currentA.Annotation<XNamespace>();
                                        if ((currentInScopeNs != null) && prefix.Equals(currentInScopePrefix))
                                        {
                                            if (ns == currentInScopeNs)
                                            {
                                                a.Remove();
                                            }
                                            a = null;
                                            break;
                                        }
                                    }
                                }
                                if (a != null)
                                {
                                    a.Remove();
                                    attributes.Add(a);
                                }
                            }
                        }
                    }
                }
            }
            return e;
        }

        [Extension]
        public static string this[XElement source, XName name]
        {
            get
            {
                return (string) source.Attribute(name);
            }
            set
            {
                source.SetAttributeValue(name, value);
            }
        }

        [Extension]
        public static string this[IEnumerable<XElement> source, XName name]
        {
            get
            {
                foreach (XElement item in source)
                {
                    return (string) item.Attribute(name);
                }
                return null;
            }
            set
            {
                foreach (XElement item in source)
                {
                    item.SetAttributeValue(name, value);
                    break;
                }
            }
        }

        [Extension]
        public static string this[IEnumerable<XElement> source]
        {
            get
            {
                foreach (XElement item in source)
                {
                    return item.Value;
                }
                return null;
            }
            set
            {
                foreach (XElement item in source)
                {
                    item.Value = value;
                    break;
                }
            }
        }

        [DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never), CompilerGenerated]
        private sealed class RemoveNamespaceAttributesClosure
        {
            private readonly List<XAttribute> m_attributes;
            private readonly XNamespace[] m_inScopeNs;
            private readonly string[] m_inScopePrefixes;

            [EditorBrowsable(EditorBrowsableState.Never)]
            internal RemoveNamespaceAttributesClosure(string[] inScopePrefixes, XNamespace[] inScopeNs, List<XAttribute> attributes)
            {
                this.m_inScopePrefixes = inScopePrefixes;
                this.m_inScopeNs = inScopeNs;
                this.m_attributes = attributes;
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            internal object ProcessObject(object obj)
            {
                XElement elem = obj as XElement;
                if (elem != null)
                {
                    return InternalXmlHelper.RemoveNamespaceAttributes(this.m_inScopePrefixes, this.m_inScopeNs, this.m_attributes, elem);
                }
                return obj;
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            internal XElement ProcessXElement(XElement elem)
            {
                return InternalXmlHelper.RemoveNamespaceAttributes(this.m_inScopePrefixes, this.m_inScopeNs, this.m_attributes, elem);
            }
        }
    }
}

