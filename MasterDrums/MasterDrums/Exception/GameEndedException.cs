using System;

namespace MasterDrums.Exception
{
    public class GameEndedException : SystemException
    {
        /// <summary>
        /// Exception launched when the game must be ended.
        /// </summary>
        public GameEndedException() : base() { }
    }
}
