namespace MasterDrums.Utils
{
    /// <summary>
    /// Singleton interface implementation.
    /// </summary>
    public class ISingleton
    {
        private static ISingleton _instance = null;

        protected ISingleton() { }

        public static ISingleton Instance()
        {
            if (ISingleton._instance == null)
                ISingleton._instance = new ISingleton();

            return ISingleton._instance;
        }
    }
}
