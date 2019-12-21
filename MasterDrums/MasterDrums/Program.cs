using System;
using MasterDrums.Controller;

namespace MasterDrums
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MainController controller = new MainController();
            controller.Run();
        }
    }
}
