using MasterDrums.Controller;
using System.Windows.Forms;
using System.Drawing;

namespace MasterDrums.View
{
    public class MainView: Form, IView
    {
        private IController _controller;
        private MainMenuPanel _mainMenuPanel;
        private PlayerNamePanel _playerNamePanel;

        /// <summary>
        /// Constructor that sets the controller to interact with the application model.
        /// </summary>
        /// <param name="controller">The instance of the controller</param>
        public MainView(IController controller)
        {
            this._controller = controller;
            this._mainMenuPanel = new MainMenuPanel(this);
            this._playerNamePanel = new PlayerNamePanel(this);

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
            this.PlayerNamePanelSetup();
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
        /// Sets up the player name panel in the form and hides it
        /// </summary>
        private void PlayerNamePanelSetup()
        {
            int panelWidth = this.ClientSize.Width / 2;
            int panelHeight = this.ClientSize.Height / 2;
            int panelX = (this.ClientSize.Width / 2) - (panelWidth / 2);
            int panelY = (this.ClientSize.Height / 2) - (panelHeight / 2);
            this._playerNamePanel.Size = new Size(panelWidth, panelHeight);
            this._playerNamePanel.Location = new Point(panelX, panelY);
            this.Controls.Add(this._playerNamePanel);
            this._playerNamePanel.Hide();
        }

        /// <summary>
        /// Remove all controls from the main panel
        /// </summary>
        private void ClearView()
        {
            this._mainMenuPanel.Hide();
            this._playerNamePanel.Hide();
        }

        /// <summary>
        /// Shows the main menu
        /// </summary>
        public void ShowMenuView()
        {
            this.ClearView();
            this._mainMenuPanel.Show();
        }

        /// <summary>
        /// Shows the player name menu
        /// </summary>
        public void ShowPlayerNameView()
        {
            this.ClearView();
            this._playerNamePanel.Show();
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

        /// <summary>
        /// Called when the user click on the Start new game button.
        /// If the username is set then the game starts, otherwise the player name panel is showed.
        /// </summary>
        public void StartNewGame()
        {
            if (this._controller.PlayerName == null)
                this.ShowPlayerNameView();
            else
            {
                this._controller.StartGame();
                // start game
            }

        }

        public void Quit()
        {
            this._controller.Quit();
        }

        /// <summary>
        /// Set the players name
        /// </summary>
        /// <param name="name">The player name</param>
        public void SetPlayerName(string n) {
            this._controller.PlayerName = n;
        }
    }
}
