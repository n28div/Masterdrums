using System;
using System.Collections.Generic;
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
            this._imagePath = @"";
            this._soundPath = @"";
        }

        /// <summary>
        /// Constructor of the class that sets the note position and the sound and image path
        /// </summary>
        /// <param name="pos">The note position</param>
        /// <param name="imagePath">The image path relative to the executable folder</param>
        /// <param name="soundPath">The sound path relative to the executable folder</param>
        public SpecialNote(notePosition pos, String imagePath, String soundPath)
        {
            // TODO: da controllare se sound path e image path esistono 
            this._position = pos;
            this._imagePath = imagePath;
            this._soundPath = soundPath;
        }

        /// <summary>
        /// The standard note hit points are 100
        /// </summary>
        public override int HitPoint => 200;

    }
}
