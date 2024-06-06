using iterate.file;
using iterate.ui;
using Newtonsoft.Json;
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
            context.UIEventBus.Notification.DataUpdated += (s, e) => context.UIEventBus.Notification.SendToView(webView21);
            context.UIEventBus.Notification.SendToView(webView21);
        }

        public void SendVersionListLatest(List<iterate.file.Version> versions)
        {
            var message = new iterate.ui.message.Command<List<iterate.file.Version>>() {data = versions };
            webView21.ExecuteScriptAsync($"window.receiveFromWinForms('versions',{JsonConvert.SerializeObject(message)}");
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
