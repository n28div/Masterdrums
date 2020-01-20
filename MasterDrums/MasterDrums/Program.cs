using System;
using MasterDrums.Controller;
using MasterDrums.View;
using System.Windows.Forms;

namespace MasterDrums
{
    static class Program
    {
        /// <summary>
        /// Application entry point. 
        /// Creates the view and the controller and connects them.
        /// The model is created in the controller.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MainController controller = new MainController();
            MainView view = new MainView(controller);
        }
    }
}
