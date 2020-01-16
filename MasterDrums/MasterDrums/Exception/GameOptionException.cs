using System;
using System.Runtime.Serialization;

namespace MasterDrums.Model
{
    public class GameOptionException : SystemException
    {
        /// <summary>
        /// Contructor method of the <c>InsertedNameException</c> class.
        /// </summary>
        public GameOptionException(string msg) : base(msg) { }
    }
}