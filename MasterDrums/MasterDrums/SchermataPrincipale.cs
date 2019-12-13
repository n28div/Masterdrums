using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MasterDrums
{
    public partial class SchermataPrincipale : Form
    {
        private Label lblNomeGiocatore;
        private TextBox txtNomeGiocatore;
        private Button btnInizioPartita;

        private Boolean partitaInCorso;

        public SchermataPrincipale()
        {
            this.partitaInCorso = false;
            InitializeComponent();
        }

        private void SchermataPrincipale_Load(object sender, EventArgs e)
        {
            // creazione della finestra di gioco e posizionamento di essa al centro dello schermo
            this.Size = new Size(1200, 700);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2), ((Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2));

            // permette di catturare il tasto premuto in ogni momento dell'esecuzione del programma
            this.KeyPreview = true;

            // impostazione dei parametri del panel della nuova partita
            // all'inizio dell'esecuzione, il panel dovrà essere visibile per permettere all'utente di selezionare
            // l'azione che vorrà effettuare
            pnlNuovaPartita.Size = new Size(500, 300);
            pnlNuovaPartita.Location = new Point(((this.Width - pnlNuovaPartita.Width) / 2), ((this.Height - pnlNuovaPartita.Height) / 2));
            pnlNuovaPartita.BackColor = Color.Red;
            pnlNuovaPartita.Visible = true;

            // impostazione dei parametri del panel della nuova partita
            // all'inizio dell'esecuzione, il panel non dovrà essere visibile, lo diventerà quando l'utente premerà esc durante l'esecuzione
            // del gioco
            pnlPausaGioco.Size = new Size(500, 300);
            pnlPausaGioco.Location = new Point(((this.Width - pnlPausaGioco.Width) / 2), ((this.Height - pnlPausaGioco.Height) / 2));
            pnlPausaGioco.BackColor = Color.Blue;
            pnlPausaGioco.Visible = false;

            // impostazione dei parametri del panel della grafica dei risultati
            // all'inizio dell'esecuzione, il panel non dovrà essere visibile, ma sarà visibile quando l'utente andrà a schiacciare 
            // l'opportuno tasto nel menù iniziale
            pnlRecord.Size = new Size(this.Width, this.Height);
            pnlRecord.Location = new Point((this.Width / 2 - pnlRecord.Width / 2), (this.Height / 2 - pnlRecord.Height / 2));
            pnlRecord.BackColor = Color.AntiqueWhite;
            pnlRecord.Visible = false;

            // creazione label per inserire il nome del giocatore
            this.lblNomeGiocatore = new Label();
            this.lblNomeGiocatore.Size = new Size(400, 30);
            this.lblNomeGiocatore.AutoSize = false;
            this.lblNomeGiocatore.Text = "Inserisci il tuo nome per iniziare la partita";
            this.lblNomeGiocatore.Location = new Point(((this.Width - this.lblNomeGiocatore.Width) / 2), ((this.Height - this.lblNomeGiocatore.Height) / 2 - 50));
            this.lblNomeGiocatore.BackColor = Color.YellowGreen;
            // inizialmente il label non sarà visibile, verrà mostrato solamente nel momento in cui l'utente vorrà
            // iniziare una nuova partita
            this.lblNomeGiocatore.Visible = false;

            // creazione TextBox per inserire il nome del giocatore
            this.txtNomeGiocatore = new TextBox();
            this.txtNomeGiocatore.Size = new Size(400, 30);
            this.txtNomeGiocatore.AutoSize = false;
            this.txtNomeGiocatore.Location = new Point(((this.Width - this.lblNomeGiocatore.Width) / 2), ((this.Height - this.lblNomeGiocatore.Height) / 2));
            this.txtNomeGiocatore.BackColor = Color.White;
            // inizialmente la TextBox non sarà visibile, verrà mostrato solamente nel momento in cui l'utente vorrà
            // iniziare una nuova partita
            this.txtNomeGiocatore.Visible = false;

            // creazione bottone per iniziare la partita dopo aver inserito il nome del giocatore
            this.btnInizioPartita = new Button();
            this.btnInizioPartita.Size = new Size(200, 30);
            this.btnInizioPartita.AutoSize = false;
            this.btnInizioPartita.Text = "Inizia la partita!";
            this.btnInizioPartita.Location = new Point(((this.Width - this.btnInizioPartita.Width) / 2), ((this.Height - this.btnInizioPartita.Height) / 2 + 40));
            this.btnInizioPartita.BackColor = Color.Gold;
            this.btnInizioPartita.Click += Button_Click;
            // inizialmente il bottone non sarà visibile, verrà mostrato solamente nel momento in cui l'utente vorrà
            // iniziare una nuova partita
            this.btnInizioPartita.Visible = false;


            // aggiunta elementi alla finestra
            this.Controls.Add(pnlNuovaPartita);
            this.Controls.Add(pnlPausaGioco);
            this.Controls.Add(pnlRecord);
            this.Controls.Add(this.lblNomeGiocatore);
            this.Controls.Add(this.txtNomeGiocatore);
            this.Controls.Add(this.btnInizioPartita);
        }

        private void pnlNuovaPartita_Paint(object sender, PaintEventArgs e)
        {
            Button btnNuovaPartita = new Button();
            btnNuovaPartita.Text = "Nuova partita";
            btnNuovaPartita.Size = new Size(200, 30);
            btnNuovaPartita.Location = new Point(((pnlNuovaPartita.Width - btnNuovaPartita.Width) / 2), ((pnlNuovaPartita.Height - btnNuovaPartita.Height) / 2 - 50));
            btnNuovaPartita.BackColor = Color.White;
            btnNuovaPartita.Click += Button_Click;

            Button btnIstruzioni = new Button();
            btnIstruzioni.Text = "Istruzioni del gioco";
            btnIstruzioni.Size = new Size(200, 30);
            btnIstruzioni.Location = new Point(((pnlNuovaPartita.Width - btnNuovaPartita.Width) / 2), ((pnlNuovaPartita.Height - btnNuovaPartita.Height) / 2));
            btnIstruzioni.BackColor = Color.White;
            btnIstruzioni.Click += Button_Click;

            Button btnRecord = new Button();
            btnRecord.Text = "Migliori punteggi";
            btnRecord.Size = new Size(200, 30);
            btnRecord.Location = new Point(((pnlNuovaPartita.Width - btnNuovaPartita.Width) / 2), ((pnlNuovaPartita.Height - btnNuovaPartita.Height) / 2 + 50));
            btnRecord.BackColor = Color.White;
            btnRecord.Click += Button_Click;

            // aggiunta dei bottoni al pannello
            pnlNuovaPartita.Controls.Add(btnNuovaPartita);
            pnlNuovaPartita.Controls.Add(btnIstruzioni);
            pnlNuovaPartita.Controls.Add(btnRecord);
        }

        private void pnlPausaGioco_Paint(object sender, PaintEventArgs e)
        {
            Button btnRiprendi = new Button();
            btnRiprendi.Text = "Riprendi";
            btnRiprendi.Size = new Size(200, 30);
            btnRiprendi.Location = new Point(((pnlPausaGioco.Width - btnRiprendi.Width) / 2), ((pnlPausaGioco.Height - btnRiprendi.Height) / 2 - 50));
            btnRiprendi.BackColor = Color.White;
            btnRiprendi.Click += Button_Click;

            Button btnAbbandona = new Button();
            btnAbbandona.Text = "Abbandona";
            btnAbbandona.Size = new Size(200, 30);
            btnAbbandona.Location = new Point(((pnlPausaGioco.Width - btnAbbandona.Width) / 2), ((pnlPausaGioco.Height - btnAbbandona.Height) / 2));
            btnAbbandona.BackColor = Color.White;
            btnAbbandona.Click += Button_Click;

            // aggiunta dei bottoni al pannello
            pnlPausaGioco.Controls.Add(btnRiprendi);
            pnlPausaGioco.Controls.Add(btnAbbandona);
        }

        // funzione che gestirà i click effettuati sui bottoni
        private void Button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == "Nuova partita")
            {
                //MessageBox.Show("Premuto il tasto: "+b.Text);
                pnlNuovaPartita.Visible = false;
                this.lblNomeGiocatore.Visible = true;
                this.txtNomeGiocatore.Visible = true;
                this.txtNomeGiocatore.Text = "";
                this.btnInizioPartita.Visible = true;
            }
            else if (b.Text == "Istruzioni del gioco")
            {
                //
            }
            else if (b.Text == "Migliori punteggi")
            {
                pnlRecord.Visible = true;
                pnlNuovaPartita.Visible = false;
            }
            else if (b.Text == "Inizia la partita!")
            {
                if(this.txtNomeGiocatore.Text != "")
                {
                    this.lblNomeGiocatore.Visible = false;
                    this.txtNomeGiocatore.Visible = false;
                    this.btnInizioPartita.Visible = false;
                    this.partitaInCorso = true;
                    
                }
                else
                {
                    MessageBox.Show("Inserisci il tuo nome prima di iniziare la partita!");
                }

            }
            else if (b.Text == "Riprendi")
            {
                pnlPausaGioco.Visible = false;
                this.partitaInCorso = true;
            }
            else if (b.Text == "Abbandona")
            {
                pnlNuovaPartita.Visible = true;
                pnlPausaGioco.Visible = false;
            }
        }

        

        private void SchermataPrincipale_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (this.partitaInCorso == true)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    pnlPausaGioco.Visible = true;
                    this.partitaInCorso = false;
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                if (pnlRecord.Visible == true)
                {
                    pnlRecord.Visible = false;
                    pnlNuovaPartita.Visible = true;
                }
                if (this.txtNomeGiocatore.Visible == true)
                {
                    this.lblNomeGiocatore.Visible = false;
                    this.txtNomeGiocatore.Visible = false;
                    this.btnInizioPartita.Visible = false;
                    pnlNuovaPartita.Visible = true;
                }
            }
        }

        private void pnlRecord_Paint(object sender, PaintEventArgs e)
        {

            ListView record = new ListView();
            record.View = View.Details;


            record.Columns.Add("Username", 498, HorizontalAlignment.Left);
            record.Columns.Add("Punteggio", 498, HorizontalAlignment.Left);
            record.Size = new Size(1000, 400);
            record.Location = new Point((pnlRecord.Width - record.Width) / 2, (pnlRecord.Height - record.Height) / 2);

            List<ListViewItem> items = new List<ListViewItem>();
            items = ottieniPunteggi();

            for(int i = 0; i < items.Count; i++)
            {
                record.Items.Add(items[i]);
            }
            pnlRecord.Controls.Add(record);

        }

        private List<ListViewItem> ottieniPunteggi()
        {
            List<ListViewItem> items = new List<ListViewItem>();

            try
            {
                StreamReader reader = new StreamReader("../../record.txt");
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');
                    if (!string.IsNullOrEmpty(line))
                    {
                        items.Add(new ListViewItem(new[] {values[0], values[1]}));   
                    }
                }
            }
            catch(System.Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return items;
        }
    }
}
