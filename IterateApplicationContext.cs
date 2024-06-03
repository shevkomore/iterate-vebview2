using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iterate
{
    public class IterateApplicationContext: ApplicationContext
    {
        SidePanelOpenButton sidePanelOpenButton;
        SidePanel sidePanel;
        ProjectSelectForm projectSelectForm;
        public IterateApplicationContext() 
        {
            projectSelectForm = new ProjectSelectForm();
            DialogResult selection = projectSelectForm.ShowDialog();
            StartTrackMode();
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
