using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterDrums.Utils;

namespace MasterDrums.Model
{
    /// <summary>
    /// Class that produces the notes to be played randomly.
    /// The class is a singleton.
    /// </summary>
    class RandomNoteGenerator : INoteGenerator
    {
        private static RandomNoteGenerator _instance = null;

        /// <summary>
        /// RandomNoteGenerator constructor simply calls the INoteGenerator constructor.
        /// </summary>
        /// <param name="bpm">The initial bpm</param>
        public RandomNoteGenerator(int bpm) : base(bpm) { }

        /// <summary>
        /// RandomNoteGenerator constructor simply calls the empty INoteGenerator constructor.
        /// </summary>
        public RandomNoteGenerator() : base() { }

        /// <summary>
        /// The method used to get the note generator instance.
        /// If the instance is already created the bpm parameter is ignored.
        /// </summary>
        /// <param name="bpm">The initial bpm parameter</param>
        /// <returns>The RandomNoteGenerator instance</returns>
        public RandomNoteGenerator Instance(int bpm)
        {
            if (_instance == null)
                _instance = new RandomNoteGenerator(bpm);

            return _instance;
        }

        /// <summary>
        /// The method used to get the note generator instance.
        /// Default bpm to 50.
        /// </summary>
        /// <returns>The RandomNoteGenerator instance</returns>
        public RandomNoteGenerator Instance()
        {
            if (_instance == null)
                _instance = new RandomNoteGenerator();

            return _instance;
        }

        /// <summary>
        /// Generate a random note
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
