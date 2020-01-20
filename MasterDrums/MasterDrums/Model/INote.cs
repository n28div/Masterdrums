using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDrums.Model
{
    /// <summary>
    /// Abstract class that represents a base note
    /// </summary>
    public abstract class INote
    {
        /// <summary>
        /// Enumerator for the note position (left or right).
        /// </summary>
        public enum notePosition
        {
            Left,
            Right
        }

        protected notePosition _position;

        /// <summary>
        /// The image displayed for the note
        /// </summary>
        /// <returns>
        /// The image instance
        /// </returns>
        public abstract Image Image {
            get;
        }

        /// <summary>
        /// The points gained by the user when a perfect hit is performed
        /// </summary>
        /// <returns>
        /// The hit points
        /// </returns>
        public abstract int HitPoint
        {
            get;
        }

        /// <summary>
        /// The note position on the screen (left or right)
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
