using iterate.ui;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iterate
{
    public partial class SidePanel : Form
    {
        IterateApplicationContext context;
        public SidePanel(IterateApplicationContext context)
        {
            this.context = context;
            InitializeComponent();
            webView21.WebMessageReceived += WebView21_WebMessageReceived;
        }

        private void WebView21_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            JObject obj = JObject.Parse(e.WebMessageAsJson);
            if (obj["command"] != null)
            {
                string command = obj["command"].Value<string>();
                if(command == "close")
                {
                    context.CloseSidePanel();
                }
            }
        }

        private void SidePanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            context.CloseSidePanel();
        }
    }
}
