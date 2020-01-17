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
    class AlternatedHandNoteGenerator : INoteGenerator
    {
        private static AlternatedHandNoteGenerator _instance = null;
        private INote.notePosition _lastHand = INote.notePosition.Right;

        /// <summary>
        /// RandomNoteGenerator constructor simply calls the INoteGenerator constructor.
        /// </summary>
        /// <param name="bpm">The initial bpm</param>
        public AlternatedHandNoteGenerator(int bpm) : base(bpm) { }

        /// <summary>
        /// RandomNoteGenerator constructor simply calls the empty INoteGenerator constructor.
        /// </summary>
        public AlternatedHandNoteGenerator() : base() { }

        /// <summary>
        /// The method used to get the note generator instance.
        /// If the instance is already created the bpm parameter is ignored.
        /// </summary>
        /// <param name="bpm">The initial bpm parameter</param>
        /// <returns>The RandomNoteGenerator instance</returns>
        public AlternatedHandNoteGenerator Instance(int bpm)
        {
            if (_instance == null)
                _instance = new AlternatedHandNoteGenerator(bpm);

            return _instance;
        }

        /// <summary>
        /// The method used to get the note generator instance.
        /// Default bpm to 50.
        /// </summary>
        /// <returns>The RandomNoteGenerator instance</returns>
        public AlternatedHandNoteGenerator Instance()
        {
            if (_instance == null)
                _instance = new AlternatedHandNoteGenerator();

            return _instance;
        }

        /// <summary>
        /// The name of the generator
        /// </summary>
        /// <returns>The name of the generator</returns>
        public override string ToString()
        {
            return "Mani alternate";
        }

        /// <summary>
        /// Generate a random note
        /// </summary>
        /// <returns>The note instance</returns>
        public override INote NextNote()
        {
            Random rnd = new Random();
            INote outNote;

            this._lastHand = (this._lastHand == INote.notePosition.Right) ? 
                   INote.notePosition.Left : 
                   INote.notePosition.Right;

            // if note is special is determined with a random number from 1 to 5
            int noteType = rnd.Next(1, 5);

            // if the random number is in the range [1, 4] a standard note is generated
            if (noteType <= 4)
                outNote = new StandardNote(this._lastHand);
            else
                outNote = new SpecialNote(this._lastHand);

            return outNote;
        }
    }
}
