using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDrums.Model
{
    class PauseNote : INote
    {
        /// <summary>
        /// Class that extends the INote abstract class and represents the pause note 
        /// </summary>

        /// <summary>
        /// Constructor of the class that sets the note position.
        /// </summary>
        /// <param name="pos">The note position (left or right)</param>
        public PauseNote(notePosition pos)
        {
            this._position = pos;
            this._imagePath = null;
            this._soundPath = null;
        }

        /// <summary>
        /// Image path is non avaiable for the pause note
        /// </summary>
        public override String ImagePath
        {
            get => throw new PauseNotePropertyExcpetion();
        }

        /// <summary>
        /// Sound path is non avaiable for the pause note
        /// </summary>
        public override String SoundPath
        {
            get => throw new PauseNotePropertyExcpetion();
        }

        /// <summary>
        /// The pause note hit points are zero
        /// </summary>
        public override int HitPoint => 0;

    }
}
