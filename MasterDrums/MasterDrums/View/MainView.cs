using MasterDrums.Model;
using MasterDrums.Controller;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace MasterDrums.View
{
    public class MainView : Form, IMainView
    {
        private IController _controller;

        private MainMenuPanel _mainMenuPanel;
        private NewGamePanel _newGamePanel;
        private PlayingPanel _playingPanel;
        private GamePausePanel _gamePausePanel;
        private HighscoresPanel _highscoresPanel;

        /// <summary>
        /// Constructor that sets the controller to interact with the application model
        /// </summary>
        /// <param name="controller">The instance of the controller</param>
        public MainView(IController controller)
        {
            Application.EnableVisualStyles();

            this._controller = controller;
            this._controller.MainView = this;

            // Create panels
            this._mainMenuPanel = new MainMenuPanel(this);
            this._newGamePanel = new NewGamePanel(this);
            this._playingPanel = new PlayingPanel(this, this._controller);
            this._gamePausePanel = new GamePausePanel(this);
            this._highscoresPanel = new HighscoresPanel(this);

            this.KeyPreview = true;
            this.KeyUp += (s, e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        if (this._playingPanel.IsRunning)
                            this.LeftNoteHit();
                        break;
                    case Keys.N:
                        if (this._playingPanel.IsRunning)
                            this.RightNoteHit();
                        break;

                    case Keys.Escape:
                        if (this._playingPanel.IsRunning)
                            this.PauseGame();
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

            // fullscreen
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            this.ClientSize = new Size(screenWidth, screenHeight);
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

            // Formatting each panel with right dimension
            this.MainMenuPanelSetup();
            this.NewGamePanelSetup();
            this.PlayingPanelSetup();
            this.GamePausePanelSetup();
            this.HighscoresPanelSetup();
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
        /// Sets up the highscores panel in the form and hides it
        /// </summary>
        private void HighscoresPanelSetup()
        {
            int highscoresPanelWidth = this.ClientSize.Width / 2;
            int highscoresPanelHeight = this.ClientSize.Height / 2;
            int highscoresPanelX = (this.ClientSize.Width / 2) - (highscoresPanelWidth / 2);
            int highscoresPanelY = (this.ClientSize.Height / 2) - (highscoresPanelHeight / 2);
            this._highscoresPanel.Size = new Size(highscoresPanelWidth, highscoresPanelHeight);
            this._highscoresPanel.Location = new Point(highscoresPanelX, highscoresPanelY);
            this.Controls.Add(this._highscoresPanel);
            this._highscoresPanel.Hide();
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
        /// Sets up the game pause panel in the form and hides it
        /// </summary>
        private void GamePausePanelSetup()
        {
            int panelWidth = this.ClientSize.Width / 2;
            int panelHeight = this.ClientSize.Height / 2;
            int panelX = (this.ClientSize.Width / 2) - (panelWidth / 2);
            int panelY = (this.ClientSize.Height / 2) - (panelHeight / 2);
            this._gamePausePanel.Size = new Size(panelWidth, panelHeight);
            this._gamePausePanel.Location = new Point(panelX, panelY);
            this.Controls.Add(this._gamePausePanel);
            this._gamePausePanel.Hide();
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
            this._highscoresPanel.Hide();
            this._newGamePanel.Hide();
            this._playingPanel.Hide();
            this._gamePausePanel.Hide();
        }

        /// <summary>
        /// Shows the main menu
        /// </summary>
        public void ShowMainMenuView() => this._mainMenuPanel.Show();

        /// <summary>
        /// Shows the highscores view
        /// </summary>
        public void ShowHighscoresView() => this._highscoresPanel.Show();

        /// <summary>
        /// Shows the new game option menu
        /// </summary>
        public void ShowNewGameView() => this._newGamePanel.Show();

        /// <summary>
        /// Shows the game pause menu
        /// </summary>
        public void ShowGamePauseView() {
            this._gamePausePanel.Show();
            this._gamePausePanel.BringToFront();
        }

        /// <summary>
        /// Hides the game pause menu
        /// </summary>
        public void HideGamePauseView() => this._gamePausePanel.Hide();

        /// <summary>
        /// Hides the playing panel
        /// </summary>
        public void HidePlayingPanelView() => this._playingPanel.Hide();

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
        /// Shows the main menu panel
        /// </summary>
        public void MainMenu()
        {
            this.ClearView();
            this.ShowMainMenuView();
        }

        /// <summary>
        /// Called when the user clicks on the new game button in the main menu panel.
        /// Shows the new game panel which will ask the user to insert his name, initial bpm and game mode
        /// </summary>
        public void NewGame()
        {
            this.ClearView();
            this._newGamePanel.ClearTxtUsername();
            this.ShowNewGameView();
        }

        /// <summary>
        /// Shows the highscores view
        /// </summary>
        public void Highscores()
        {
            if(Game.LoadBestResults() != null)
            { 
                this.ClearView();
                this.ShowHighscoresView();
            }
            else
            {
                MessageBox.Show("Nessun risultato presente!");
            }
        }

        /// <summary>
        /// Close the application
        /// </summary>
        public void Quit()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Hide all the panels and shows the gaming panel.
        /// </summary>
        /// <param name="playerName">The player name</param>
        /// <param name="initialBpm">The initial bpm</param>
        /// <param name="gameMode">The game mode selected</param>
        public void StartGame(string playerName, int initialBpm, INoteGenerator gameMode)
        {
            this.ClearView();
            this.ShowPlayingView();

            this._controller.PlayerName = playerName;
            this._controller.InitialBpm = initialBpm;

            this._playingPanel.GameMode = gameMode;
            this._playingPanel.StartGame();
        }

        /// <summary>
        /// Show the pause panel (on top of the playing panel).
        /// Communicate to the playing panel that the user wants to pause the game
        /// </summary>
        public void PauseGame()
        {
            this.ShowGamePauseView();
            this._playingPanel.PauseGame();
        }

        /// <summary>
        /// Hide the pause panel.
        /// Communicate to the playing panel that the user wants to resume the game
        /// </summary>
        public void ResumeGame()
        {
            this.HideGamePauseView();
            this._playingPanel.ResumeGame();
        }

        /// <summary>
        /// Stop the current game and return to the initial menu.
        /// </summary>
        public void StopGame()
        {
            this.HidePlayingPanelView();
            this.HideGamePauseView();
            this.ShowMainMenuView();
        }

        /// <summary>
        /// Left note has been hit
        /// </summary>
        public void LeftNoteHit()
        {
            this._playingPanel.LeftNoteHit();
        }

        /// <summary>
        /// Right note ha been hit
        /// </summary>
        public void RightNoteHit()
        {
            this._playingPanel.RightNoteHit();
        }

        /// <summary>
        /// Refresh the highscores panel after new records are added
        /// </summary>
        public void RefreshHighscores()
        {
            this._highscoresPanel = new HighscoresPanel(this);
            this.HighscoresPanelSetup();
        }

    }
}
