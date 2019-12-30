using System;
using MasterDrums.Controller;
using MasterDrums.View;
using System.Windows.Forms;

namespace MasterDrums
{
    static class Program
    {
        /// <summary>
        /// Application entry point
        /// </summary>
        [STAThread]
        static void Main()
        {
            MainController controller = new MainController();
            MainView view = new MainView(controller);
        }
    }
}
