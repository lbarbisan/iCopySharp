// iCopy - Simple Photocopier
// Copyright (C) 2007-2018 Matteo Rossi

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;

namespace iCopy
{

    public class PortableSettingsProvider : SettingsProvider
    {
        private string settingsPath = "";
        private const string SETTINGSROOT = "Settings"; // XML Root Node

        public override void Initialize(string name, NameValueCollection col)
        {
            base.Initialize(ApplicationName, col);
        }

        public override string Name
        {
            get
            {
                return "PortableSettingsProvider";
            }
        }

        public override string ApplicationName
        {
            get
            {
                if (Application.ProductName.Trim().Length > 0)
                {
                    return Application.ProductName;
                }
                else
                {
                    var fi = new System.IO.FileInfo(Application.ExecutablePath);
                    return fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length);
                }
            }
            set
            {
                // Do nothing
            }
        }

        public bool HasWritePermission(string folder)
        {
            try
            {
                System.IO.File.Create("iCopy.settings");
            }
            catch (UnauthorizedAccessException ex)
            {
                return false;
            }
            var oFp = new System.Security.Permissions.FileIOPermission(System.Security.Permissions.FileIOPermissionAccess.Write, folder);
            return oFp.IsSubsetOf((System.Security.IPermission)AppDomain.CurrentDomain.PermissionSet);
        }

        public virtual string GetAppSettingsPath()
        {
            return System.IO.Path.Combine(Utilities.GetWritablePath(), GetAppSettingsFileName());
        }

        public virtual string GetAppSettingsFileName()
        {
            // Used to determine the filename to store the settings
            return ApplicationName + ".settings";
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection propvals)
        {
            // Iterate through the settings to be stored
            // Only dirty settings are included in propvals, and only ones relevant to this provider
            foreach (SettingsPropertyValue propval in propvals)
                SetValue(propval);

            try
            {
                SettingsXML.Save(GetAppSettingsPath());
            }
            catch (UnauthorizedAccessException ex)
            {
                throw;
            }
            catch (System.IO.DirectoryNotFoundException d)
            {
                System.IO.Directory.CreateDirectory(Utilities.GetWritablePath());
                SettingsXML.Save(GetAppSettingsPath());
            }
            catch (Exception e)
            {
                // Ignore other exceptions
            }
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection props)
        {
            // Create new collection of values
            var values = new SettingsPropertyValueCollection();

            // Iterate through the settings to be retrieved
            foreach (SettingsProperty setting in props)
            {
                var value = new SettingsPropertyValue(setting);
                value.IsDirty = false;
                value.SerializedValue = GetValue(setting);
                values.Add(value);
            }
            return values;
        }

        private XmlDocument m_SettingsXML;

        private XmlDocument SettingsXML
        {
            get
            {
                // If we dont hold an xml document, try opening one.  
                // If it doesnt exist then create a new one ready.
                if (m_SettingsXML is null)
                {
                    m_SettingsXML = new XmlDocument();

                    try
                    {
                        m_SettingsXML.Load(GetAppSettingsPath());
                    }
                    catch (Exception ex)
                    {
                        // Check if the file is in the alternate place
                        try
                        {
                            m_SettingsXML.Load(GetAppSettingsPath());
                        }
                        catch (Exception e)
                        {
                            // If both files are absent, create new document
                            var dec = m_SettingsXML.CreateXmlDeclaration("1.0", "utf-8", string.Empty);
                            m_SettingsXML.AppendChild(dec);

                            XmlNode nodeRoot;

                            nodeRoot = m_SettingsXML.CreateNode(XmlNodeType.Element, SETTINGSROOT, "");
                            m_SettingsXML.AppendChild(nodeRoot);

                        }
                    }
                }

                return m_SettingsXML;
            }
        }

        private string GetValue(SettingsProperty setting)
        {
            string ret = "";
            XmlNode node;
            try
            {
                if (IsRoaming(setting))
                {
                    node = SettingsXML.SelectSingleNode(SETTINGSROOT + "/" + setting.Name);
                }
                else
                {
                    string xpath = string.Format("{0}/Machine[@Name='{1}']/{2}", SETTINGSROOT, My.MyProject.Computer.Name, setting.Name);
                    node = SettingsXML.SelectSingleNode(xpath);
                }

                if (setting.SerializeAs == SettingsSerializeAs.Xml)
                {
                    ret = node.InnerXml;
                }
                else
                {
                    ret = node.InnerText;
                }
            }

            catch (Exception ex)
            {
               /* if (setting.Defaultvalue != null)
                {
                    ret = setting.DefaultValue.ToString();
                }
                else
                {
                    ret = "";
                }*/
            }

            return ret;
        }

        private void SetValue(SettingsPropertyValue propVal)
        {

            XmlElement MachineNode;
            XmlElement SettingNode;

            // Determine if the setting is roaming.
            // If roaming then the value is stored as an element under the root
            // Otherwise it is stored under a machine name node 
            try
            {
                if (IsRoaming(propVal.Property))
                {
                    SettingNode = (XmlElement)SettingsXML.SelectSingleNode(SETTINGSROOT + "/" + propVal.Name);
                }
                else
                {
                    string xpath = string.Format("{0}/Machine[@Name='{1}']/{2}", SETTINGSROOT, My.MyProject.Computer.Name, propVal.Name);
                    SettingNode = (XmlElement)SettingsXML.SelectSingleNode(xpath);
                }
            }
            catch (Exception ex)
            {
                SettingNode = null;
            }

            // Check to see if the node exists, if so then set its new value
            if (SettingNode != null)
            {
                if (propVal.Property.SerializeAs == SettingsSerializeAs.Xml)
                {
                    SettingNode.InnerXml = propVal.SerializedValue.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                }
                else
                {
                    SettingNode.InnerText = propVal.SerializedValue.ToString();
                }
            }
            else if (IsRoaming(propVal.Property))
            {
                // Store the value as an element of the Settings Root Node
                SettingNode = SettingsXML.CreateElement(propVal.Name);
                if (propVal.Property.SerializeAs == SettingsSerializeAs.Xml)
                {
                    SettingNode.InnerXml = propVal.SerializedValue.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                }
                else
                {
                    SettingNode.InnerText = propVal.SerializedValue.ToString();
                }
                SettingsXML.SelectSingleNode(SETTINGSROOT).AppendChild(SettingNode);
            }
            else
            {
                // Its machine specific, store as an element of the machine name node,
                // creating a new machine name node if one doesnt exist.
                try
                {
                    string xpath = string.Format("{0}/Machine[@Name={1}]", SETTINGSROOT, My.MyProject.Computer.Name);
                    MachineNode = (XmlElement)SettingsXML.SelectSingleNode(xpath);
                }
                catch (Exception ex)
                {
                    MachineNode = SettingsXML.CreateElement("Machine");
                    MachineNode.SetAttribute("Name", My.MyProject.Computer.Name);
                    SettingsXML.SelectSingleNode(SETTINGSROOT).AppendChild(MachineNode);
                }

                if (MachineNode is null)
                {
                    MachineNode = SettingsXML.CreateElement("Machine");
                    MachineNode.SetAttribute("Name", My.MyProject.Computer.Name);
                    SettingsXML.SelectSingleNode(SETTINGSROOT).AppendChild(MachineNode);
                }

                SettingNode = SettingsXML.CreateElement(propVal.Name);
                if (propVal.Property.SerializeAs == SettingsSerializeAs.Xml)
                {
                    SettingNode.InnerXml = propVal.SerializedValue.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                }
                else
                {
                    SettingNode.InnerText = propVal.SerializedValue.ToString();
                }
                MachineNode.AppendChild(SettingNode);
            }
        }

        private bool IsRoaming(SettingsProperty prop)
        {
            // Determine if the setting is marked as Roaming
            foreach (DictionaryEntry d in prop.Attributes)
            {
                Attribute a = (Attribute)d.Value;
                if (a is SettingsManageabilityAttribute)
                {
                    return true;
                }
            }
            return false;
        }

    }
}