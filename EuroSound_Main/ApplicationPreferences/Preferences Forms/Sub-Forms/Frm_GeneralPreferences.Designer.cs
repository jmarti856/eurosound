
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
            this.Groupbox_Waves = new System.Windows.Forms.GroupBox();
            this.Button_WavesBackColor = new System.Windows.Forms.Button();
            this.Label_ColorBackground = new System.Windows.Forms.Label();
            this.Button_WavesColorControl = new System.Windows.Forms.Button();
            this.Label_ColorWaves = new System.Windows.Forms.Label();
            this.GroupBox_LoadingOptions = new System.Windows.Forms.GroupBox();
            this.CheckBox_UseThreading = new System.Windows.Forms.CheckBox();
            this.CheckBox_IgnoreLookTree = new System.Windows.Forms.CheckBox();
            this.CheckBox_ReloadLastESF = new System.Windows.Forms.CheckBox();
            this.Panel_Title.SuspendLayout();
            this.Groupbox_Waves.SuspendLayout();
            this.GroupBox_LoadingOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Title
            // 
            this.Panel_Title.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Panel_Title.Controls.Add(this.Label_Title);
            this.Panel_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Title.Location = new System.Drawing.Point(0, 0);
            this.Panel_Title.Name = "Panel_Title";
            this.Panel_Title.Size = new System.Drawing.Size(513, 23);
            this.Panel_Title.TabIndex = 1;
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Title.Location = new System.Drawing.Point(177, 5);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(137, 16);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "General Configuration";
            // 
            // Groupbox_Waves
            // 
            this.Groupbox_Waves.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_Waves.Controls.Add(this.Button_WavesBackColor);
            this.Groupbox_Waves.Controls.Add(this.Label_ColorBackground);
            this.Groupbox_Waves.Controls.Add(this.Button_WavesColorControl);
            this.Groupbox_Waves.Controls.Add(this.Label_ColorWaves);
            this.Groupbox_Waves.Location = new System.Drawing.Point(12, 187);
            this.Groupbox_Waves.Name = "Groupbox_Waves";
            this.Groupbox_Waves.Size = new System.Drawing.Size(489, 103);
            this.Groupbox_Waves.TabIndex = 3;
            this.Groupbox_Waves.TabStop = false;
            this.Groupbox_Waves.Text = "Audio Waves Viewer:";
            // 
            // Button_WavesBackColor
            // 
            this.Button_WavesBackColor.FlatAppearance.BorderSize = 0;
            this.Button_WavesBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Button_WavesBackColor.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Button_WavesBackColor.Location = new System.Drawing.Point(306, 32);
            this.Button_WavesBackColor.Name = "Button_WavesBackColor";
            this.Button_WavesBackColor.Size = new System.Drawing.Size(50, 29);
            this.Button_WavesBackColor.TabIndex = 3;
            this.Button_WavesBackColor.UseVisualStyleBackColor = true;
            this.Button_WavesBackColor.Click += new System.EventHandler(this.Button_WavesBackColor_Click);
            this.Button_WavesBackColor.Paint += new System.Windows.Forms.PaintEventHandler(this.Button_WavesBackColor_Paint);
            this.Button_WavesBackColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_WavesBackColor_MouseDown);
            this.Button_WavesBackColor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_WavesBackColor_MouseUp);
            // 
            // Label_ColorBackground
            // 
            this.Label_ColorBackground.AutoSize = true;
            this.Label_ColorBackground.Location = new System.Drawing.Point(206, 40);
            this.Label_ColorBackground.Name = "Label_ColorBackground";
            this.Label_ColorBackground.Size = new System.Drawing.Size(94, 13);
            this.Label_ColorBackground.TabIndex = 2;
            this.Label_ColorBackground.Text = "Background color:";
            // 
            // Button_WavesColorControl
            // 
            this.Button_WavesColorControl.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.Button_WavesColorControl.FlatAppearance.BorderSize = 0;
            this.Button_WavesColorControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Button_WavesColorControl.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Button_WavesColorControl.Location = new System.Drawing.Point(149, 32);
            this.Button_WavesColorControl.Name = "Button_WavesColorControl";
            this.Button_WavesColorControl.Size = new System.Drawing.Size(50, 29);
            this.Button_WavesColorControl.TabIndex = 1;
            this.Button_WavesColorControl.UseVisualStyleBackColor = true;
            this.Button_WavesColorControl.Click += new System.EventHandler(this.Button_WavesColorControl_Click);
            this.Button_WavesColorControl.Paint += new System.Windows.Forms.PaintEventHandler(this.Button_WavesColorControl_Paint);
            this.Button_WavesColorControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_WavesColorControl_MouseDown);
            this.Button_WavesColorControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_WavesColorControl_MouseUp);
            // 
            // Label_ColorWaves
            // 
            this.Label_ColorWaves.AutoSize = true;
            this.Label_ColorWaves.Location = new System.Drawing.Point(73, 40);
            this.Label_ColorWaves.Name = "Label_ColorWaves";
            this.Label_ColorWaves.Size = new System.Drawing.Size(70, 13);
            this.Label_ColorWaves.TabIndex = 0;
            this.Label_ColorWaves.Text = "Waves color:";
            // 
            // GroupBox_LoadingOptions
            // 
            this.GroupBox_LoadingOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_LoadingOptions.Controls.Add(this.CheckBox_UseThreading);
            this.GroupBox_LoadingOptions.Controls.Add(this.CheckBox_IgnoreLookTree);
            this.GroupBox_LoadingOptions.Controls.Add(this.CheckBox_ReloadLastESF);
            this.GroupBox_LoadingOptions.Location = new System.Drawing.Point(12, 88);
            this.GroupBox_LoadingOptions.Name = "GroupBox_LoadingOptions";
            this.GroupBox_LoadingOptions.Size = new System.Drawing.Size(489, 93);
            this.GroupBox_LoadingOptions.TabIndex = 4;
            this.GroupBox_LoadingOptions.TabStop = false;
            this.GroupBox_LoadingOptions.Text = "Loading Options:";
            // 
            // CheckBox_UseThreading
            // 
            this.CheckBox_UseThreading.AutoSize = true;
            this.CheckBox_UseThreading.Location = new System.Drawing.Point(19, 65);
            this.CheckBox_UseThreading.Name = "CheckBox_UseThreading";
            this.CheckBox_UseThreading.Size = new System.Drawing.Size(438, 17);
            this.CheckBox_UseThreading.TabIndex = 2;
            this.CheckBox_UseThreading.Text = "Use threading when loading ESF files (recommended on computers with low resources" +
    ").";
            this.CheckBox_UseThreading.UseVisualStyleBackColor = true;
            // 
            // CheckBox_IgnoreLookTree
            // 
            this.CheckBox_IgnoreLookTree.AutoSize = true;
            this.CheckBox_IgnoreLookTree.Location = new System.Drawing.Point(19, 42);
            this.CheckBox_IgnoreLookTree.Name = "CheckBox_IgnoreLookTree";
            this.CheckBox_IgnoreLookTree.Size = new System.Drawing.Size(357, 17);
            this.CheckBox_IgnoreLookTree.TabIndex = 1;
            this.CheckBox_IgnoreLookTree.Text = "Ignore data stored in the ESF describing the look and state of the tree.";
            this.CheckBox_IgnoreLookTree.UseVisualStyleBackColor = true;
            // 
            // CheckBox_ReloadLastESF
            // 
            this.CheckBox_ReloadLastESF.AutoSize = true;
            this.CheckBox_ReloadLastESF.Location = new System.Drawing.Point(19, 19);
            this.CheckBox_ReloadLastESF.Name = "CheckBox_ReloadLastESF";
            this.CheckBox_ReloadLastESF.Size = new System.Drawing.Size(181, 17);
            this.CheckBox_ReloadLastESF.TabIndex = 0;
            this.CheckBox_ReloadLastESF.Text = "Automatically reload last ESF file.";
            this.CheckBox_ReloadLastESF.UseVisualStyleBackColor = true;
            // 
            // Frm_General
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 395);
            this.Controls.Add(this.GroupBox_LoadingOptions);
            this.Controls.Add(this.Groupbox_Waves);
            this.Controls.Add(this.Panel_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_General";
            this.Text = "Frm_TreeViewPrefs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_TreeViewPrefs_FormClosing);
            this.Load += new System.EventHandler(this.Frm_TreeViewPrefs_Load);
            this.Panel_Title.ResumeLayout(false);
            this.Panel_Title.PerformLayout();
            this.Groupbox_Waves.ResumeLayout(false);
            this.Groupbox_Waves.PerformLayout();
            this.GroupBox_LoadingOptions.ResumeLayout(false);
            this.GroupBox_LoadingOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Title;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.FontDialog FontDialogTreeView;
        private System.Windows.Forms.GroupBox Groupbox_Waves;
        private System.Windows.Forms.Label Label_ColorWaves;
        private System.Windows.Forms.Button Button_WavesColorControl;
        private System.Windows.Forms.Button Button_WavesBackColor;
        private System.Windows.Forms.Label Label_ColorBackground;
        private System.Windows.Forms.GroupBox GroupBox_LoadingOptions;
        private System.Windows.Forms.CheckBox CheckBox_ReloadLastESF;
        private System.Windows.Forms.CheckBox CheckBox_IgnoreLookTree;
        private System.Windows.Forms.CheckBox CheckBox_UseThreading;
    }
}