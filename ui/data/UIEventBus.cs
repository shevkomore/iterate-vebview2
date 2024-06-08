using iterate.file;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iterate.ui.data
{
    public class UIEventBus : INotifier
    {
        public UIDataHandler<Notification> Notification = new UIDataHandler<Notification>("notification", new ui.Notification {loading = false, message = "Event bus suucessfully connected" });
        public UIDataHandler<List<ProjectManager.ProjectData>> Projects;

        public UIEventBus(ProjectManager projectManager) 
        {
            Projects = new UIDataHandler<List<ProjectManager.ProjectData>>("repos", projectManager.GetAllProjects());
        }
        public void Notify(Notification notification)
        {
            Notification.Update(notification);
        }
    }
}
