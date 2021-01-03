
namespace EuroSound_Application.ApplicationPreferencesForms
{
    partial class Frm_General
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Panel_Title = new System.Windows.Forms.Panel();
            this.Label_Title = new System.Windows.Forms.Label();
            this.FontDialogTreeView = new System.Windows.Forms.FontDialog();
            this.GroupBox_Output = new System.Windows.Forms.GroupBox();
            this.Button_ChooseSFX_OutputPath = new System.Windows.Forms.Button();
            this.Textbox_SFX_OutputPath = new System.Windows.Forms.TextBox();
            this.Label_SFXOutputPath = new System.Windows.Forms.Label();
            this.Groupbox_Shell = new System.Windows.Forms.GroupBox();
            this.ButtonRegister_FileTypes = new System.Windows.Forms.Button();
            this.Label_Expl = new System.Windows.Forms.Label();
            this.FolderBrowser_OutputPath = new System.Windows.Forms.FolderBrowserDialog();
            this.GroupBox_MusicOutputPath = new System.Windows.Forms.GroupBox();
            this.Button_MusicOutputPath = new System.Windows.Forms.Button();
            this.Textbox_MusicOutputPath = new System.Windows.Forms.TextBox();
            this.Label_MusicOutputPath = new System.Windows.Forms.Label();
            this.Groupbox_Waves = new System.Windows.Forms.GroupBox();
            this.Button_WavesBackColor = new System.Windows.Forms.Button();
            this.Label_ColorBackground = new System.Windows.Forms.Label();
            this.Button_WavesColorControl = new System.Windows.Forms.Button();
            this.Label_ColorWaves = new System.Windows.Forms.Label();
            this.Panel_Title.SuspendLayout();
            this.GroupBox_Output.SuspendLayout();
            this.Groupbox_Shell.SuspendLayout();
            this.GroupBox_MusicOutputPath.SuspendLayout();
            this.Groupbox_Waves.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Title
            // 
            this.Panel_Title.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Panel_Title.Controls.Add(this.Label_Title);
            this.Panel_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Title.Location = new System.Drawing.Point(0, 0);
            this.Panel_Title.Name = "Panel_Title";
            this.Panel_Title.Size = new System.Drawing.Size(491, 23);
            this.Panel_Title.TabIndex = 1;
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Title.Location = new System.Drawing.Point(174, 5);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(137, 16);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "General Configuration";
            // 
            // GroupBox_Output
            // 
            this.GroupBox_Output.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Output.Controls.Add(this.Button_ChooseSFX_OutputPath);
            this.GroupBox_Output.Controls.Add(this.Textbox_SFX_OutputPath);
            this.GroupBox_Output.Controls.Add(this.Label_SFXOutputPath);
            this.GroupBox_Output.Location = new System.Drawing.Point(12, 99);
            this.GroupBox_Output.Name = "GroupBox_Output";
            this.GroupBox_Output.Size = new System.Drawing.Size(467, 52);
            this.GroupBox_Output.TabIndex = 1;
            this.GroupBox_Output.TabStop = false;
            this.GroupBox_Output.Text = "SFX Output Directory";
            // 
            // Button_ChooseSFX_OutputPath
            // 
            this.Button_ChooseSFX_OutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_ChooseSFX_OutputPath.Location = new System.Drawing.Point(437, 19);
            this.Button_ChooseSFX_OutputPath.Name = "Button_ChooseSFX_OutputPath";
            this.Button_ChooseSFX_OutputPath.Size = new System.Drawing.Size(24, 20);
            this.Button_ChooseSFX_OutputPath.TabIndex = 2;
            this.Button_ChooseSFX_OutputPath.Text = "...";
            this.Button_ChooseSFX_OutputPath.UseVisualStyleBackColor = true;
            this.Button_ChooseSFX_OutputPath.Click += new System.EventHandler(this.Button_ChooseSFX_OutputPath_Click);
            // 
            // Textbox_SFX_OutputPath
            // 
            this.Textbox_SFX_OutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_SFX_OutputPath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_SFX_OutputPath.Location = new System.Drawing.Point(79, 19);
            this.Textbox_SFX_OutputPath.Name = "Textbox_SFX_OutputPath";
            this.Textbox_SFX_OutputPath.ReadOnly = true;
            this.Textbox_SFX_OutputPath.Size = new System.Drawing.Size(352, 20);
            this.Textbox_SFX_OutputPath.TabIndex = 1;
            // 
            // Label_SFXOutputPath
            // 
            this.Label_SFXOutputPath.AutoSize = true;
            this.Label_SFXOutputPath.Location = new System.Drawing.Point(6, 22);
            this.Label_SFXOutputPath.Name = "Label_SFXOutputPath";
            this.Label_SFXOutputPath.Size = new System.Drawing.Size(67, 13);
            this.Label_SFXOutputPath.TabIndex = 0;
            this.Label_SFXOutputPath.Text = "Output Path:";
            // 
            // Groupbox_Shell
            // 
            this.Groupbox_Shell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_Shell.Controls.Add(this.ButtonRegister_FileTypes);
            this.Groupbox_Shell.Controls.Add(this.Label_Expl);
            this.Groupbox_Shell.Location = new System.Drawing.Point(12, 217);
            this.Groupbox_Shell.Name = "Groupbox_Shell";
            this.Groupbox_Shell.Size = new System.Drawing.Size(467, 92);
            this.Groupbox_Shell.TabIndex = 2;
            this.Groupbox_Shell.TabStop = false;
            this.Groupbox_Shell.Text = "Shell";
            // 
            // ButtonRegister_FileTypes
            // 
            this.ButtonRegister_FileTypes.Location = new System.Drawing.Point(142, 63);
            this.ButtonRegister_FileTypes.Name = "ButtonRegister_FileTypes";
            this.ButtonRegister_FileTypes.Size = new System.Drawing.Size(207, 23);
            this.ButtonRegister_FileTypes.TabIndex = 1;
            this.ButtonRegister_FileTypes.Text = "Register Shell File-Types";
            this.ButtonRegister_FileTypes.UseVisualStyleBackColor = true;
            this.ButtonRegister_FileTypes.Click += new System.EventHandler(this.ButtonRegister_FileTypes_Click);
            // 
            // Label_Expl
            // 
            this.Label_Expl.AutoSize = true;
            this.Label_Expl.Location = new System.Drawing.Point(6, 16);
            this.Label_Expl.Name = "Label_Expl";
            this.Label_Expl.Size = new System.Drawing.Size(452, 26);
            this.Label_Expl.TabIndex = 0;
            this.Label_Expl.Text = "ESFs can be loaded into a running EuroSound by double-clicking on them. If this d" +
    "oesn\'t work,\r\nclick on the button below to register the files that EuroSound use" +
    "s with the system.";
            // 
            // GroupBox_MusicOutputPath
            // 
            this.GroupBox_MusicOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_MusicOutputPath.Controls.Add(this.Button_MusicOutputPath);
            this.GroupBox_MusicOutputPath.Controls.Add(this.Textbox_MusicOutputPath);
            this.GroupBox_MusicOutputPath.Controls.Add(this.Label_MusicOutputPath);
            this.GroupBox_MusicOutputPath.Location = new System.Drawing.Point(12, 41);
            this.GroupBox_MusicOutputPath.Name = "GroupBox_MusicOutputPath";
            this.GroupBox_MusicOutputPath.Size = new System.Drawing.Size(467, 52);
            this.GroupBox_MusicOutputPath.TabIndex = 0;
            this.GroupBox_MusicOutputPath.TabStop = false;
            this.GroupBox_MusicOutputPath.Text = "Music Output Directory";
            // 
            // Button_MusicOutputPath
            // 
            this.Button_MusicOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_MusicOutputPath.Location = new System.Drawing.Point(437, 18);
            this.Button_MusicOutputPath.Name = "Button_MusicOutputPath";
            this.Button_MusicOutputPath.Size = new System.Drawing.Size(24, 20);
            this.Button_MusicOutputPath.TabIndex = 2;
            this.Button_MusicOutputPath.Text = "...";
            this.Button_MusicOutputPath.UseVisualStyleBackColor = true;
            this.Button_MusicOutputPath.Click += new System.EventHandler(this.Button_MusicOutputPath_Click);
            // 
            // Textbox_MusicOutputPath
            // 
            this.Textbox_MusicOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_MusicOutputPath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_MusicOutputPath.Location = new System.Drawing.Point(79, 19);
            this.Textbox_MusicOutputPath.Name = "Textbox_MusicOutputPath";
            this.Textbox_MusicOutputPath.ReadOnly = true;
            this.Textbox_MusicOutputPath.Size = new System.Drawing.Size(352, 20);
            this.Textbox_MusicOutputPath.TabIndex = 1;
            // 
            // Label_MusicOutputPath
            // 
            this.Label_MusicOutputPath.AutoSize = true;
            this.Label_MusicOutputPath.Location = new System.Drawing.Point(6, 21);
            this.Label_MusicOutputPath.Name = "Label_MusicOutputPath";
            this.Label_MusicOutputPath.Size = new System.Drawing.Size(67, 13);
            this.Label_MusicOutputPath.TabIndex = 0;
            this.Label_MusicOutputPath.Text = "Output Path:";
            // 
            // Groupbox_Waves
            // 
            this.Groupbox_Waves.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_Waves.Controls.Add(this.Button_WavesBackColor);
            this.Groupbox_Waves.Controls.Add(this.Label_ColorBackground);
            this.Groupbox_Waves.Controls.Add(this.Button_WavesColorControl);
            this.Groupbox_Waves.Controls.Add(this.Label_ColorWaves);
            this.Groupbox_Waves.Location = new System.Drawing.Point(12, 157);
            this.Groupbox_Waves.Name = "Groupbox_Waves";
            this.Groupbox_Waves.Size = new System.Drawing.Size(467, 54);
            this.Groupbox_Waves.TabIndex = 3;
            this.Groupbox_Waves.TabStop = false;
            this.Groupbox_Waves.Text = "Audio Waves Viewer";
            // 
            // Button_WavesBackColor
            // 
            this.Button_WavesBackColor.FlatAppearance.BorderSize = 2;
            this.Button_WavesBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_WavesBackColor.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Button_WavesBackColor.Location = new System.Drawing.Point(239, 17);
            this.Button_WavesBackColor.Name = "Button_WavesBackColor";
            this.Button_WavesBackColor.Size = new System.Drawing.Size(50, 29);
            this.Button_WavesBackColor.TabIndex = 3;
            this.Button_WavesBackColor.UseVisualStyleBackColor = true;
            this.Button_WavesBackColor.Click += new System.EventHandler(this.Button_WavesBackColor_Click);
            // 
            // Label_ColorBackground
            // 
            this.Label_ColorBackground.AutoSize = true;
            this.Label_ColorBackground.Location = new System.Drawing.Point(139, 25);
            this.Label_ColorBackground.Name = "Label_ColorBackground";
            this.Label_ColorBackground.Size = new System.Drawing.Size(94, 13);
            this.Label_ColorBackground.TabIndex = 2;
            this.Label_ColorBackground.Text = "Background color:";
            // 
            // Button_WavesColorControl
            // 
            this.Button_WavesColorControl.FlatAppearance.BorderSize = 2;
            this.Button_WavesColorControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_WavesColorControl.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Button_WavesColorControl.Location = new System.Drawing.Point(83, 17);
            this.Button_WavesColorControl.Name = "Button_WavesColorControl";
            this.Button_WavesColorControl.Size = new System.Drawing.Size(50, 29);
            this.Button_WavesColorControl.TabIndex = 1;
            this.Button_WavesColorControl.UseVisualStyleBackColor = true;
            this.Button_WavesColorControl.Click += new System.EventHandler(this.Button_WavesColorControl_Click);
            // 
            // Label_ColorWaves
            // 
            this.Label_ColorWaves.AutoSize = true;
            this.Label_ColorWaves.Location = new System.Drawing.Point(6, 25);
            this.Label_ColorWaves.Name = "Label_ColorWaves";
            this.Label_ColorWaves.Size = new System.Drawing.Size(70, 13);
            this.Label_ColorWaves.TabIndex = 0;
            this.Label_ColorWaves.Text = "Waves color:";
            // 
            // Frm_General
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 339);
            this.Controls.Add(this.Groupbox_Waves);
            this.Controls.Add(this.GroupBox_MusicOutputPath);
            this.Controls.Add(this.Groupbox_Shell);
            this.Controls.Add(this.GroupBox_Output);
            this.Controls.Add(this.Panel_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_General";
            this.Text = "Frm_TreeViewPrefs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_TreeViewPrefs_FormClosing);
            this.Load += new System.EventHandler(this.Frm_TreeViewPrefs_Load);
            this.Panel_Title.ResumeLayout(false);
            this.Panel_Title.PerformLayout();
            this.GroupBox_Output.ResumeLayout(false);
            this.GroupBox_Output.PerformLayout();
            this.Groupbox_Shell.ResumeLayout(false);
            this.Groupbox_Shell.PerformLayout();
            this.GroupBox_MusicOutputPath.ResumeLayout(false);
            this.GroupBox_MusicOutputPath.PerformLayout();
            this.Groupbox_Waves.ResumeLayout(false);
            this.Groupbox_Waves.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Title;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.FontDialog FontDialogTreeView;
        private System.Windows.Forms.GroupBox GroupBox_Output;
        private System.Windows.Forms.Button Button_ChooseSFX_OutputPath;
        private System.Windows.Forms.TextBox Textbox_SFX_OutputPath;
        private System.Windows.Forms.Label Label_SFXOutputPath;
        private System.Windows.Forms.GroupBox Groupbox_Shell;
        private System.Windows.Forms.Label Label_Expl;
        private System.Windows.Forms.Button ButtonRegister_FileTypes;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser_OutputPath;
        private System.Windows.Forms.GroupBox GroupBox_MusicOutputPath;
        private System.Windows.Forms.Button Button_MusicOutputPath;
        private System.Windows.Forms.TextBox Textbox_MusicOutputPath;
        private System.Windows.Forms.Label Label_MusicOutputPath;
        private System.Windows.Forms.GroupBox Groupbox_Waves;
        private System.Windows.Forms.Label Label_ColorWaves;
        private System.Windows.Forms.Button Button_WavesColorControl;
        private System.Windows.Forms.Button Button_WavesBackColor;
        private System.Windows.Forms.Label Label_ColorBackground;
    }
}