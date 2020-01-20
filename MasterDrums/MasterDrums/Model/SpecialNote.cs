using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDrums.Model
{
    class SpecialNote : INote
    {
        /// <summary>
        /// Class that extends the INote abstract class and represents the special note 
        /// </summary>

        /// <summary>
        /// Constructor of the class that sets the note position. The sound and image path is set to the default value
        /// </summary>
        /// <param name="pos">The note position (left or right)</param>
        public SpecialNote(notePosition pos)
        {
            this._position = pos;
        }

        /// <summary>
        /// The special note hit points are 200
        /// </summary>
        public override int HitPoint => 200;

        public override Image Image => Resource.special;

        public override string SoundPath => throw new NotImplementedException();
    }
}
