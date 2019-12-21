using MasterDrums.Controller;
using System.Windows.Forms;
using System.Drawing;

namespace MasterDrums.View
{
    public class MainView: Form, IView
    {
        private MainMenuPanel _mainMenuPanel;
        private IController _controller;

        /// <summary>
        /// Constructor that sets the controller to interact with the application model.
        /// </summary>
        /// <param name="controller">The instance of the controller</param>
        public MainView(IController controller)
        {
            this._controller = controller;
            this._mainMenuPanel = new MainMenuPanel(this);

            this.InitializeComponent();
            this.ShowMenuView();
        }

        /// <summary>
        /// Method used to initialize form components
        /// </summary>
        private void InitializeComponent()
        {
            // Main form setup
            this.SuspendLayout();
            this.ClientSize = new Size(1280, 720);
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

            this.MainMenuPanelSetup();
        }

        /// <summary>
        /// Sets up the main menu panel in the form and hides it
        /// </summary>
        private void MainMenuPanelSetup()
        {
            int mainMenuPanelWidth = this.ClientSize.Width / 2;
            int mainMenuPanelHeight = this.ClientSize.Height / 2;
            int mainMenuPanelX = (this.ClientSize.Width / 2) - (mainMenuPanelWidth / 2);
            int mainMenuPanelY = (this.ClientSize.Height / 2) - (mainMenuPanelHeight / 2);
            this._mainMenuPanel.Size = new Size(mainMenuPanelWidth, mainMenuPanelHeight);
            this._mainMenuPanel.Location = new Point(mainMenuPanelX, mainMenuPanelY);
            this.Controls.Add(this._mainMenuPanel);
            this._mainMenuPanel.Hide();
        }

        /// <summary>
        /// Remove all controls from the main panel
        /// </summary>
        public void ClearView()
        {
            this._mainMenuPanel.Hide();
        }

        /// <summary>
        /// Shows the main menu
        /// </summary>
        public void ShowMenuView()
        {
            this.ClearView();
            this._mainMenuPanel.Show();
        }

        public void ShowCommandsView()
        {
            throw new System.NotImplementedException();
        }

        public void ShowInstructionView()
        {
            throw new System.NotImplementedException();
        }

        public void ShowOptionView()
        {
            throw new System.NotImplementedException();
        }

        public void ShowHighscoreView()
        {
            throw new System.NotImplementedException();
        }

        public void StartNewGame()
        {
            throw new System.NotImplementedException();
        }

        public void Quit()
        {
            this._controller.Quit();
        }
    }
}
