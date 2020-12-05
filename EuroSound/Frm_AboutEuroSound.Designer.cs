namespace EuroSound_SB_Editor
{
    partial class Frm_AboutEuroSound
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Textbox_About = new System.Windows.Forms.TextBox();
            this.Button_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Textbox_About
            // 
            this.Textbox_About.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_About.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Textbox_About.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Textbox_About.CausesValidation = false;
            this.Textbox_About.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Textbox_About.Location = new System.Drawing.Point(12, 12);
            this.Textbox_About.Multiline = true;
            this.Textbox_About.Name = "Textbox_About";
            this.Textbox_About.ReadOnly = true;
            this.Textbox_About.Size = new System.Drawing.Size(406, 247);
            this.Textbox_About.TabIndex = 0;
            this.Textbox_About.TabStop = false;
            this.Textbox_About.Text = "\r\nEuroSound Soundbank\r\nEditor\r\n\r\n(c) 2020\r\n\r\nby Ismael Ferreras and\r\nJordi Martín" +
    "ez";
            this.Textbox_About.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Textbox_About.WordWrap = false;
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_OK.Location = new System.Drawing.Point(180, 267);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 1;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Frm_AboutEuroSound
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 302);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Textbox_About);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_AboutEuroSound";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About EuroSound";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Textbox_About;
        private System.Windows.Forms.Button Button_OK;
    }
}
