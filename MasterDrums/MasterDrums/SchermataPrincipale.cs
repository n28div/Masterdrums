using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterDrums
{
    public partial class SchermataPrincipale : Form
    {

        // nome giocatore
        private String _nomeGiocatore;

        public SchermataPrincipale()
        {
            InitializeComponent();
        }

        // caricamento iniziale della finestra
        private void SchermataPrincipale_Load(object sender, EventArgs e)
        {
            // definizione grandezza minima della finestra
            this.MinimumSize = new System.Drawing.Size(800, 600);

            // visualizzazione lblNomeGiocatore
            lblNomeGiocatore.Visible = true;
            lblNomeGiocatore.TextAlign = ContentAlignment.MiddleCenter;
            lblNomeGiocatore.BackColor = Color.Red;

            // bottone nuova partita
            btnNuovaPartita.Size = new Size((this.Width / 7), (this.Height / 7));
            btnNuovaPartita.Location = new Point((this.Width / 2 - btnNuovaPartita.Width / 2), (this.Height / 2 - btnNuovaPartita.Height / 2) - (this.Height / 10));
            btnNuovaPartita.Font = new Font(btnNuovaPartita.Font.FontFamily, (this.Width / 100));

            // label nome giocatore
            lblNomeGiocatore.Size = new Size((this.Width / 6), (this.Height / 6));
            lblNomeGiocatore.Location = new Point((this.Width / 2 - lblNomeGiocatore.Width / 2), (this.Height / 2 - lblNomeGiocatore.Height / 2) + (this.Height / 10));
            lblNomeGiocatore.Font = new Font(btnNuovaPartita.Font.FontFamily, 2 * (this.Width / 100));
        }

        // resize automatico dei componenti nel momento in cui viene effettuato un ridimensionamento della finestra
        private void SchermataPrincipale_Resize(object sender, EventArgs e)
        {

                // bottone nuova partita
                btnNuovaPartita.Size = new Size((this.Width / 6), (this.Height / 7));
                btnNuovaPartita.Location = new Point((this.Width / 2 - btnNuovaPartita.Width / 2), (this.Height / 2 - btnNuovaPartita.Height / 2) - (this.Height / 10));
                btnNuovaPartita.Font = new Font(lblNomeGiocatore.Font.FontFamily, (this.Width / 100));

                // label nome giocatore
                lblNomeGiocatore.Size = new Size((this.Width / 6), (this.Height / 6));
                lblNomeGiocatore.Location = new Point((this.Width / 2 - lblNomeGiocatore.Width / 2), (this.Height / 2 - lblNomeGiocatore.Height / 2) + (this.Height / 10));
                lblNomeGiocatore.Font = new Font(lblNomeGiocatore.Font.FontFamily, 2 * (this.Width / 100));
            
        }

    }
}
