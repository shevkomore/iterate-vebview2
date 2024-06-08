using iterate.file;
using iterate.ui.data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iterate
{
    public class IterateApplicationContext: ApplicationContext
    {
        public readonly string HOME_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "iterate");

        public UIEventBus UIEventBus;
        public ProjectManager projectManager;

        SidePanelOpenButton sidePanelOpenButton;
        SidePanel sidePanel;
        ProjectSelectForm projectSelectForm;

        //IN TESTING

        FileObserver observer;
        public IterateApplicationContext() 
        {
            Directory.CreateDirectory(HOME_PATH);
            projectManager = new ProjectManager(this);

            UIEventBus = new UIEventBus(projectManager);

            projectSelectForm = new ProjectSelectForm(this);
            DialogResult selection = projectSelectForm.ShowDialog();
            if (selection != DialogResult.OK)
            {
                Application.Exit();
                Environment.Exit(0);
                return;
            }
            projectManager.SelectProject(projectSelectForm.id);
            observer = new FileObserver(this,projectManager.CurrentProject.Path);
            observer.watcher.Changed += onRepoChanged;
            StartTrackMode();
        }

        private void onRepoChanged(object sender, System.IO.FileSystemEventArgs e)
        {
            sidePanel.SendVersionListLatest(new List<iterate.file.Version> { new iterate.file.Version() { Created=DateTime.Now, Description="hhhhhhhhhhhhhhhhhhhhhhhhh" } });
        }

        public void StartTrackMode()
        {
            sidePanelOpenButton = new SidePanelOpenButton(this);
            sidePanel = new SidePanel(this);
            sidePanelOpenButton.Location = new System.Drawing.Point(
                Screen.PrimaryScreen.Bounds.Width - sidePanelOpenButton.Width,
                Screen.PrimaryScreen.Bounds.Height - sidePanelOpenButton.Height / 2 - 100
                );
            sidePanel.Location = new System.Drawing.Point(
                Screen.PrimaryScreen.Bounds.Width - sidePanel.Width,
                0
                );
            sidePanel.Height = Screen.PrimaryScreen.WorkingArea.Height;
            sidePanelOpenButton.Show();

            sidePanel.Hide();
        }
        public void OpenSidePanel()
        {
            sidePanelOpenButton.Hide();
            sidePanel.Show();
        }
        public void CloseSidePanel()
        {
            sidePanelOpenButton.Show();
            sidePanel.Hide();
        }
    }
}
