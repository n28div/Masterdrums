namespace MasterDrums
{
    partial class SchermataPrincipale
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNuovaPartita = new System.Windows.Forms.Button();
            this.lblNomeGiocatore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnNuovaPartita
            // 
            this.btnNuovaPartita.Location = new System.Drawing.Point(340, 181);
            this.btnNuovaPartita.Name = "btnNuovaPartita";
            this.btnNuovaPartita.Size = new System.Drawing.Size(75, 23);
            this.btnNuovaPartita.TabIndex = 0;
            this.btnNuovaPartita.Text = "NUOVA PARTITA";
            this.btnNuovaPartita.UseVisualStyleBackColor = true;
            // 
            // lblNomeGiocatore
            // 
            this.lblNomeGiocatore.AutoSize = true;
            this.lblNomeGiocatore.Location = new System.Drawing.Point(312, 286);
            this.lblNomeGiocatore.Name = "lblNomeGiocatore";
            this.lblNomeGiocatore.Size = new System.Drawing.Size(132, 17);
            this.lblNomeGiocatore.TabIndex = 1;
            this.lblNomeGiocatore.Text = "Inserisci il tuo nome";
            // 
            // SchermataPrincipale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblNomeGiocatore);
            this.Controls.Add(this.btnNuovaPartita);
            this.Name = "SchermataPrincipale";
            this.Text = "MasterDrums";
            this.Load += new System.EventHandler(this.SchermataPrincipale_Load);
            this.Resize += new System.EventHandler(this.SchermataPrincipale_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNuovaPartita;
        private System.Windows.Forms.Label lblNomeGiocatore;
    }
}

