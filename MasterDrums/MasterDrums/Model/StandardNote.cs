using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDrums.Model
{
    /// <summary>
    /// Class that extends the INote abstract class and represents a standard note 
    /// </summary>
    class StandardNote : INote
    {
        private Image _image = null;

        public StandardNote(notePosition pos)
        {
            this._position = pos;
        }

        public override int HitPoint => 100;

        /// <summary>
        /// Random from green, blue and yellow
        /// </summary>
        public override Image Image {
            get
            {
                Image im;

                if (this._image == null)
                {
                    Random rnd = new Random();
                    int r = rnd.Next(0, 3);

                    if (r == 0)
                        im = Resource.yellow;
                    else if (r == 1)
                        im = Resource.blue;
                    else
                        im = Resource.green;

                    this._image = im;
                }
                else
                    im = this._image;

                return im;
            }
        }
    }
}
