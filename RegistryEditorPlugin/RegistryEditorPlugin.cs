﻿using System;
using System.IO;
using System.Windows.Forms;
using CKAN;
using Newtonsoft.Json;
using ZTn.Json.Editor.Forms;

namespace RegistryEditorPlugin
{

    public class RegistryEditorPlugin : CKAN.IGUIPlugin
    {

        public static readonly string VERSION = "v1.0.0";

        private JsonEditorMainForm m_Form = null;
        private ToolStripMenuItem m_MenuItem = null;

        public override void Initialize()
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem();

            menuItem.Name = "pluginsToolStripMenuItem";
            menuItem.Size = new System.Drawing.Size(176, 22);
            menuItem.Text = "RegistryEditor";
            menuItem.Click += new System.EventHandler(menuItem_Click);
            Main.Instance.settingsToolStripMenuItem.DropDownItems.Add(menuItem);
            m_MenuItem = menuItem;
        }

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            Main.Instance.Enabled = false;
            m_Form = new JsonEditorMainForm();
            var json = JsonConvert.SerializeObject(Main.Instance.CurrentInstance.Registry);
            m_Form.SetJsonSourceStream(GenerateStreamFromString(json));
            m_Form.ShowDialog();
            Main.Instance.Enabled = true;
        }

        public override void Deinitialize()
        {
            Main.Instance.settingsToolStripMenuItem.DropDownItems.Remove(m_MenuItem);
        }

        public override string GetName()
        {
            return "RegistryEditor";
        }

        public override CKAN.Version GetVersion()
        {
            return new CKAN.Version(VERSION);
        }

    }

}
