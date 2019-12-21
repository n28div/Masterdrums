using MasterDrums.View;
using MasterDrums.Model;
using System.Windows.Forms;

namespace MasterDrums.Controller
{
    /// <summary>
    /// Entry point of the game, each controller, view and model is managed through this class.
    /// </summary>
    public class MainController : IController
    {
        private MainView _view;

        /// <summary>
        /// Constructor method.
        /// Sets the form so it will be viewed by the user and creates the required objects.
        /// </summary>
        public MainController() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            this._view = new MainView(this);
        }

        /// <summary>
        /// Method used to start the application.
        /// </summary>
        public void Run()
        {
            Application.Run(this._view);
        }

        public void Quit()
        {
            Application.Exit();
        }
    }
}
