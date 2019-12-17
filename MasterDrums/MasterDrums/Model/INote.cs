using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDrums.Model
{
    abstract class INote
    {

        /// <summary>
        /// Abstract class that represents the base note
        /// </summary>
        /// 

        /// <summary>
        /// Enumerator for the note position (left or right).
        /// </summary>
        public enum notePosition
        {
            /// <summary>Left arm.</summary>
            Left,
            /// <summary>Right arm.</summary>
            Right
        }

        protected String _imagePath;
        protected String _soundPath;
        protected notePosition _position;

        /// <summary>
        /// The image displayed in the note PictureBox
        /// </summary>
        /// <returns>
        /// The path where the image can be find
        /// </returns>
        public virtual String ImagePath
        {
            get => this._imagePath;
        }

        /// <summary>
        /// The sound played when the user hits the note
        /// </summary>
        /// <returns>
        /// The path where the sound can be find
        /// </returns>
        public virtual String SoundPath
        {
            get => this._soundPath;
        }

        /// <summary>
        /// The hit points of the note
        /// </summary>
        /// <returns>
        /// The hit points of the note
        /// </returns>
        public abstract int HitPoint
        {
            get;
        }

        /// <summary>
        /// The note position
        /// </summary>
        /// <returns>
        /// The note position
        /// </returns>
        public notePosition Position
        {
            get => this._position;
        }

    }
}
