using System;

namespace MasterDrums.Model
{
    /// <summary>
    /// Class that produces alternated notes (starting from right hand)
    /// </summary>
    class AlternatedHandNoteGenerator : INoteGenerator
    {
        /// <summary>
        /// Initial position is right hand
        /// </summary>
        private INote.notePosition _lastHand = INote.notePosition.Right;

        public AlternatedHandNoteGenerator(int bpm) : base(bpm) { }

        public AlternatedHandNoteGenerator() : base() { }

        public override string ToString() => "Mani alternate";

        /// <summary>
        /// Generate the next note. The probability of it being special is 20%.
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
