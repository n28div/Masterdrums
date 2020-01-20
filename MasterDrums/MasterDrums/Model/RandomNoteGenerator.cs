using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterDrums.Utils;

namespace MasterDrums.Model
{
    /// <summary>
    /// Class that produces the notes to be played randomly
    /// </summary>
    class RandomNoteGenerator : INoteGenerator
    {
        public RandomNoteGenerator(int bpm) : base(bpm) { }

        public RandomNoteGenerator() : base() { }

        public override string ToString() => "Combinazioni casuali";

        /// <summary>
        /// Generate a random note.
        /// Same probability of being left or right (50% left 50% right)
        /// 60% of probability of being a standard note, 30% of being a pause 
        /// 10% of being special.
        /// </summary>
        /// <returns>The note instance</returns>
        public override INote NextNote()
        {
            Random rnd = new Random();
            INote.notePosition pos;
            INote outNote;

            // right or left position is determined with a random boolean
            if (rnd.Next(0, 2) == 0)
                pos = INote.notePosition.Left;
            else
                pos = INote.notePosition.Right;

            // note type is determined with a random number from 1 to 10
            int noteType = rnd.Next(1, 11);

            // if the random number is in the range [1, 6] a standard note is generated
            // if in the range [7, 9] a pause note is generated
            // if in the range (9, 10] a special note is generated
            if (noteType <= 6)
                outNote = new StandardNote(pos);
            else if (noteType <= 9)
                outNote = new PauseNote(pos);
            else  {
                outNote = new SpecialNote(pos);
            }

            return outNote;
        }
    }
}
