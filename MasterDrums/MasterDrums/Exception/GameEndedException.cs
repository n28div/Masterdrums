using System;

namespace MasterDrums.Exception
{
    /// <summary>
    /// Exception launched when the user lose the game and it must end
    /// </summary>
    public class GameEndedException : SystemException
    {
        public GameEndedException() : base() { }
    }
}
