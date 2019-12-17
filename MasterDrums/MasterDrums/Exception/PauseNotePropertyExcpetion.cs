using System;
using System.Runtime.Serialization;

namespace MasterDrums.Model
{
    public class PauseNotePropertyExcpetion : Exception
    {
        /// <summary>
        /// Contructor method of the <c>PauseNotePropertyException</c> class.
        /// It already includes a standard message.
        /// </summary>
        public PauseNotePropertyExcpetion()
            : base("The note pause doesn't have image and sound.") { }
        
    }
}