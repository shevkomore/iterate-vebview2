using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iterate
{
    public partial class ProjectSelectForm : Form
    {
        public string command;
        public string path;
        public ProjectSelectForm()
        {
            InitializeComponent();
            webView21.WebMessageReceived += WebView21_WebMessageReceived;
        }

        private void WebView21_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            JObject data = JObject.Parse(e.WebMessageAsJson);
            if (data["command"] != null)
            {
                string command = data["command"].Value<string>();
                if (command == "open-repo")
                {
                    this.command = command;
                    this.path = data["path"].Value<string>();
                    DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                if (command == "create-repo")
                {
                    this.command = command;
                    DialogResult res = openFileDialog1.ShowDialog(this);
                    if (res == DialogResult.OK)
                    {
                        this.path = openFileDialog1.FileName;
                        DialogResult = DialogResult.OK;
                    }
                    else return;
                }
            }
        }
    }
}
