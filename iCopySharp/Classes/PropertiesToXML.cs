using System;
using System.Xml;
using Microsoft.VisualBasic.CompilerServices;
using WIA;

namespace iCopy
{

    static class PropertiesToXMLType
    {

        public static void PropertiesToXML(WIA.Properties properties, XmlTextWriter writer)
        {
            foreach (Property prop in properties)
            {

                writer.WriteStartElement("Property");
                try
                {

                    writer.WriteAttributeString("ID", prop.PropertyID.ToString());
                    writer.WriteAttributeString("Name", prop.Name);
                    writer.WriteAttributeString("IsReadOnly", Conversions.ToString(prop.IsReadOnly));
                    writer.WriteAttributeString("Type", string.Format("{0}", (WiaPropertyType)prop.Type));
                    writer.WriteAttributeString("Value", prop.ToString());

                    writer.WriteStartElement("Subtype");
                    writer.WriteAttributeString("Type", string.Format("{0}", prop.SubType));
                    switch (prop.SubType)
                    {
                        case WiaSubType.UnspecifiedSubType:
                            {
                                writer.WriteAttributeString("Count", 0.ToString());
                                break;
                            }
                        case WiaSubType.FlagSubType:
                            {
                                writer.WriteAttributeString("Count", string.Format("{0}", prop.SubTypeValues.Count));
                                foreach (object o in prop.SubTypeValues)
                                {
                                    writer.WriteStartElement("Flag");
                                    writer.WriteString(o.ToString());
                                    writer.WriteEndElement();
                                }

                                break;
                            }
                        case WiaSubType.ListSubType:
                            {
                                writer.WriteAttributeString("Count", string.Format("{0}", prop.SubTypeValues.Count));
                                foreach (object o in prop.SubTypeValues)
                                {
                                    writer.WriteStartElement("Value");
                                    writer.WriteString(o.ToString());
                                    writer.WriteEndElement();
                                }

                                break;
                            }
                        case WiaSubType.RangeSubType:
                            {
                                writer.WriteAttributeString("Count", 3.ToString());
                                writer.WriteElementString("Min", prop.SubTypeMin.ToString());
                                writer.WriteElementString("Max", prop.SubTypeMax.ToString());
                                writer.WriteElementString("Step", prop.SubTypeStep.ToString());
                                break;
                            }

                        default:
                            {
                                break;
                            }

                    }
                    writer.WriteEndElement();
                }
                catch (Exception ex)
                {
                    writer.WriteString("Couldn't evaluate");
                }
                writer.WriteEndElement();
            }

            writer.Flush();
        }
    }
}