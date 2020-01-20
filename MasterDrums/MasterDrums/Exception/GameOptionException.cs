using System;

namespace MasterDrums.Model
{
    /// <summary>
    /// Exception raised when the user name or the initial bpms are not set
    /// </summary>
    public class GameOptionException : SystemException
    {
        public GameOptionException(string msg) : base(msg) { }
    }
}