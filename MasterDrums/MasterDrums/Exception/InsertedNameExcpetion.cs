using System;
using System.Runtime.Serialization;

namespace MasterDrums.Model
{
    public class InsertedNameException : Exception
    {
        /// <summary>
        /// Contructor method of the <c>InsertedNameException</c> class.
        /// It already includes a standard message.
        /// </summary>
        public InsertedNameException()
            : base("The name of the user can't be null or whitespaces") { }
        
    }
}