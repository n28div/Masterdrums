using MasterDrums.Model;
using MasterDrums.Controller;
using System;
using System.Windows.Forms;
using System.Drawing;


namespace MasterDrums.View
{
    public class MainView: Form, IMainView
    {
        private IController _controller;
        private MainMenuPanel _mainMenuPanel;
        private NewGamePanel _newGamePanel;
        private PlayingPanel _playingPanel;

        /// <summary>
        /// Constructor that sets the controller to interact with the application model.
        /// </summary>
        /// <param name="controller">The instance of the controller</param>
        public MainView(IController controller)
        {
            Application.EnableVisualStyles();
            
            this._controller = controller;
            this._controller.MainView = this;

            this._mainMenuPanel = new MainMenuPanel(this);
            this._newGamePanel = new NewGamePanel(this);
            this._playingPanel = new PlayingPanel(this);

            this.KeyPreview = true;
            this.KeyUp += (s, e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        this.LeftNoteHit();
                        break;
                    case Keys.N:
                        this.RightNoteHit();
                        break;
                    case Keys.Escape:
                        this.Quit();
                        break;
                }
            };

            this.InitializeComponent();
            this.ShowMainMenuView();

            Application.Run(this);
        }

        #region Panels setup methods
        /// <summary>
        /// Method used to initialize form components
        /// </summary>
        private void InitializeComponent()
        {
            // Main form setup
            this.SuspendLayout();

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.ClientSize = new Size(screenWidth, screenHeight);
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

            this.MainMenuPanelSetup();
            this.NewGamePanelSetup();
            this.PlayingPanelSetup();
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
        private void NewGamePanelSetup()
        {
            int panelWidth = this.ClientSize.Width / 2;
            int panelHeight = this.ClientSize.Height / 2;
            int panelX = (this.ClientSize.Width / 2) - (panelWidth / 2);
            int panelY = (this.ClientSize.Height / 2) - (panelHeight / 2);
            this._newGamePanel.Size = new Size(panelWidth, panelHeight);
            this._newGamePanel.Location = new Point(panelX, panelY);
            this.Controls.Add(this._newGamePanel);
            this._newGamePanel.Hide();
        }

        /// <summary>
        /// Sets up the playing panel in the form and hides it
        /// </summary>
        private void PlayingPanelSetup()
        {
            int panelWidth = this.ClientSize.Width;
            int panelHeight = this.ClientSize.Height;
            int panelX = 0;
            int panelY = 0;
            this._playingPanel.Size = new Size(panelWidth, panelHeight);
            this._playingPanel.Location = new Point(panelX, panelY);
            this.Controls.Add(this._playingPanel);
            this._playingPanel.Hide();
        }
        #endregion

        #region Panels display methods
        /// <summary>
        /// Remove all controls from the main panel
        /// </summary>
        private void ClearView()
        {
            this._mainMenuPanel.Hide();
            this._newGamePanel.Hide();
        }

        /// <summary>
        /// Shows the main menu
        /// </summary>
        public void ShowMainMenuView() => this._mainMenuPanel.Show();

        /// <summary>
        /// Shows the player name menu
        /// </summary>
        public void ShowNewGameView() => this._newGamePanel.Show();

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
        /// Shows the playing view
        /// </summary>
        public void ShowPlayingView()
        {
            this._playingPanel.Draw();
            this._playingPanel.Show();
        }
        #endregion

        /// <summary>
        /// Called when the user clicks on the new game button in the main menu panel.
        /// Shows the new game panel which will ask the user to insert his name, initial bpm and game mode.
        /// </summary>
        public void NewGame()
        {
            this.ClearView();
            this.ShowNewGameView();
        }

        public void Highscores()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Close the application
        /// </summary>
        public void Quit()
        {
            this._controller.StopGame();
            Application.Exit();
        }

        /// <summary>
        /// Hide all the panels and show tha gaming panel.
        /// Communicate to the controller that the user wants to start the game
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="initialBpm"></param>
        /// <param name="gameMode"></param>
        public void StartGame(string playerName, int initialBpm, INoteGenerator gameMode)
        {
            this.ClearView();
            this.ShowPlayingView();

            this._controller.PlayerName = playerName;
            this._controller.InitialBpm = initialBpm;
            this._controller.GameMode = gameMode;

            this._controller.StartGame();
        }

        /// <summary>
        /// Left note has been hit
        /// </summary>
        public void LeftNoteHit()
        {
            this._playingPanel.LeftNoteHit();
        }

        /// <summary>
        /// Right note hit attempt
        /// </summary>
        public void RightNoteHit()
        {
            this._playingPanel.RightNoteHit();
        }

        /// <summary>
        /// Show a left note on the screen.
        /// </summary>
        public void LaunchLeftNote(INote note)
        {
            // Invoke is needed cause the method is called from another thread.
            // Form control update is not permitted cross-thread! 
            this.Invoke(new Action<INote>(this._playingPanel.LaunchLeftNote), new object[] { note });
        }

        /// <summary>
        /// Show a right note on the screen.
        /// </summary>
        public void LaunchRightNote(INote note)
        {
            // Invoke is needed cause the method is called from another thread.
            // Form control update is not permitted cross-thread!
            this.Invoke(new Action<INote>(this._playingPanel.LaunchRightNote), new object[] { note });
        }
    }
}
