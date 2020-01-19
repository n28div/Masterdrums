using MasterDrums.Exception;
using MasterDrums.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MasterDrums.Model
{
    /// <summary>
    /// The game class contains the game state informations.
    /// It is a singleton class.
    /// </summary>
    public class Game : IGame
    {
        private string _playerName = null;
        private int _bpm = -1;
        private int _score = 0;
        private int _wastedNotes = 0;
        private int _hittedNotes = 0;
        private List<Tuple<int, String>> _results = new List<Tuple<int, String>>();
        

        /// <summary>
        /// Creates the game instance and sets the initial bpm
        /// </summary>
        /// <param name="initialBpm">The initial bpm</param>
        public Game(int initialBpm) : base()
        {
            this._results = LoadBestResults();
            this._bpm = initialBpm;
        }

        /// <summary>
        /// The player's name
        /// </summary>
        public string PlayerName {
            get => this._playerName;
            set => this._playerName = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        /// <summary>
        /// The BPM at which the user is playing
        /// </summary>
        public int Bpm => this._bpm;

        /// <summary>
        /// The user score
        /// </summary>
        public int Score => this._score;

        /// <summary>
        /// A note is considered as wasted if its perfomed 200ms before or after it would naturally occur
        /// </summary>
        public int NoteWastedMs
        {
            get => 50;
        }

        /// <summary>
        /// Method called when a note has been hit
        /// </summary>
        /// <param name="note">The note hitted</param>
        /// <param name="deltaT">The distance in time from the perfect hit time</param>
        public void Hit(INote note, double deltaT)
        {
            // every 2ms a 1 point penalty is added 
            int penalty = (int)Math.Round(deltaT / 2.0);
            this._score += (note.HitPoint - penalty);

            this._hittedNotes++;
            if ((this._hittedNotes % 5) == 0)
                this._bpm++;
        }

        /// <summary>
        /// Called when an empty hit has been performed
        /// </summary>
        public void Hit()
        {
            this._wastedNotes++;

            if (this._wastedNotes >= 20)
                throw new GameEndedException();
        }

        /// <summary>
        /// Called to serialize the user score on a file
        /// </summary>
        public void SerializeScore()
        {
            this._results = Game.LoadBestResults();
            
            Tuple<int, String> t = new Tuple<int, String>(this._score, this._playerName);
            if (this._score != 0)
            {
                this.AddAndOrderResult(t);
                this.AddResultToFile();
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int WastedNotesRemaining
        {
            get => (20 - this._wastedNotes);
        }

        /// <summary>
        /// Loads the record from the file containg the records
        /// </summary>
        /// <returns>A list of record in the format score - name</returns>
        public static List<Tuple<int, String>> LoadBestResults()
        {
            List<Tuple<int, String>> results = new List<Tuple<int, String>>();
            StreamReader sr = new StreamReader("../../record.csv");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(';');
                if (!string.IsNullOrEmpty(line))
                {
                    results.Add(new Tuple<int, String>(int.Parse(values[1]), values[0]));
                }
            }

            results.Sort();
            results.Reverse();
            sr.Close();
            return results;
        } 

        /// <summary>
        /// Add a new result to the internal list
        /// </summary>
        /// <param name="t">The new result</param>
        private void AddAndOrderResult(Tuple<int, String> t)
        {
            this._results.Add(t);
            this._results.Sort();
            this._results.Reverse();
        }

        /// <summary>
        /// Write results to file
        /// </summary>
        private void AddResultToFile()
        {
            StreamWriter sw = new StreamWriter("../../record.csv", false);
            foreach (Tuple<int, String> t in this._results)
            {
                sw.WriteLine(t.Item2 + ";" + t.Item1);
            }
            sw.Close();
        }
    }
}


