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
            this.components = new System.ComponentModel.Container();
            this.btnNuovaPartita = new System.Windows.Forms.Button();
            this.btnIstruzioni = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.pnlNuovaPartita = new System.Windows.Forms.Panel();
            this.pnlPausaGioco = new System.Windows.Forms.Panel();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.pnlRecord = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnNuovaPartita
            // 
            this.btnNuovaPartita.Location = new System.Drawing.Point(0, 0);
            this.btnNuovaPartita.Name = "btnNuovaPartita";
            this.btnNuovaPartita.Size = new System.Drawing.Size(75, 23);
            this.btnNuovaPartita.TabIndex = 0;
            // 
            // btnIstruzioni
            // 
            this.btnIstruzioni.Location = new System.Drawing.Point(0, 0);
            this.btnIstruzioni.Name = "btnIstruzioni";
            this.btnIstruzioni.Size = new System.Drawing.Size(75, 23);
            this.btnIstruzioni.TabIndex = 0;
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(0, 0);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(75, 23);
            this.btnRecord.TabIndex = 0;
            // 
            // pnlNuovaPartita
            // 
            this.pnlNuovaPartita.Location = new System.Drawing.Point(12, 12);
            this.pnlNuovaPartita.Name = "pnlNuovaPartita";
            this.pnlNuovaPartita.Size = new System.Drawing.Size(258, 217);
            this.pnlNuovaPartita.TabIndex = 0;
            this.pnlNuovaPartita.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlNuovaPartita_Paint);
            // 
            // pnlPausaGioco
            // 
            this.pnlPausaGioco.Location = new System.Drawing.Point(579, 241);
            this.pnlPausaGioco.Name = "pnlPausaGioco";
            this.pnlPausaGioco.Size = new System.Drawing.Size(209, 186);
            this.pnlPausaGioco.TabIndex = 1;
            this.pnlPausaGioco.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPausaGioco_Paint);
            // 
            // pnlRecord
            // 
            this.pnlRecord.Location = new System.Drawing.Point(579, 44);
            this.pnlRecord.Name = "pnlRecord";
            this.pnlRecord.Size = new System.Drawing.Size(209, 171);
            this.pnlRecord.TabIndex = 2;
            this.pnlRecord.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlRecord_Paint);
            // 
            // SchermataPrincipale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlRecord);
            this.Controls.Add(this.pnlPausaGioco);
            this.Controls.Add(this.pnlNuovaPartita);
            this.Name = "SchermataPrincipale";
            this.Text = "MasterDrums";
            this.Load += new System.EventHandler(this.SchermataPrincipale_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SchermataPrincipale_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNuovaPartita;
        private System.Windows.Forms.Button btnIstruzioni;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Panel pnlNuovaPartita;
        private System.Windows.Forms.Panel pnlPausaGioco;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.Panel pnlRecord;
    }
}

