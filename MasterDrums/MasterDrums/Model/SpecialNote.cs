using System.Drawing;

namespace MasterDrums.Model
{
    /// <summary>
    /// Class that represents a special note 
    /// </summary>
    class SpecialNote : INote
    {
        public SpecialNote(notePosition pos)
        {
            this._position = pos;
        }

        public override int HitPoint => 200;

        public override Image Image => Resource.special;
    }
}
