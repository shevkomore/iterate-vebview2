using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iterate.ui.data
{
    public class UIDataHandler<T>
    {
        string identifier;
        T data;
        public UIDataHandler(string identifier, T data)
        {
            this.data = data;
            this.identifier = identifier;
        }
        public event EventHandler<T> DataUpdated;
        public void Update(T data)
        {
            this.data = data;
            DataUpdated?.Invoke(this, data);
        }
        public void SendToView(WebView2 view)
        {
            view.ExecuteScriptAsync($"console.log('Loaded data:',{JsonConvert.SerializeObject(data)})");
            view.ExecuteScriptAsync($"window.data.{identifier} = {JsonConvert.SerializeObject(data)}");
        }
        public void SendToViewWhenInitialized(WebView2 view)
        {
            view.CoreWebView2InitializationCompleted += (s, e) =>
            {
                if (!e.IsSuccess) return;
                view.CoreWebView2.DOMContentLoaded += (s2,e2) => SendToView(view);
            };
        }
    }
}
