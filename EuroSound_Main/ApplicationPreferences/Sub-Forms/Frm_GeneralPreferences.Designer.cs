
namespace EuroSound_Application
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
            this.Button_Choose = new System.Windows.Forms.Button();
            this.Textbox_OutputSelectedPath = new System.Windows.Forms.TextBox();
            this.Label_OutputPath = new System.Windows.Forms.Label();
            this.Groupbox_Shell = new System.Windows.Forms.GroupBox();
            this.ButtonRegister_FileTypes = new System.Windows.Forms.Button();
            this.Label_Expl = new System.Windows.Forms.Label();
            this.FolderBrowser_OutputPath = new System.Windows.Forms.FolderBrowserDialog();
            this.Panel_Title.SuspendLayout();
            this.GroupBox_Output.SuspendLayout();
            this.Groupbox_Shell.SuspendLayout();
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
            this.GroupBox_Output.Controls.Add(this.Button_Choose);
            this.GroupBox_Output.Controls.Add(this.Textbox_OutputSelectedPath);
            this.GroupBox_Output.Controls.Add(this.Label_OutputPath);
            this.GroupBox_Output.Location = new System.Drawing.Point(12, 82);
            this.GroupBox_Output.Name = "GroupBox_Output";
            this.GroupBox_Output.Size = new System.Drawing.Size(467, 72);
            this.GroupBox_Output.TabIndex = 2;
            this.GroupBox_Output.TabStop = false;
            this.GroupBox_Output.Text = "SFX Output Directory";
            // 
            // Button_Choose
            // 
            this.Button_Choose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Choose.Location = new System.Drawing.Point(437, 30);
            this.Button_Choose.Name = "Button_Choose";
            this.Button_Choose.Size = new System.Drawing.Size(24, 20);
            this.Button_Choose.TabIndex = 2;
            this.Button_Choose.Text = "...";
            this.Button_Choose.UseVisualStyleBackColor = true;
            this.Button_Choose.Click += new System.EventHandler(this.Button_Choose_Click);
            // 
            // Textbox_OutputSelectedPath
            // 
            this.Textbox_OutputSelectedPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_OutputSelectedPath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_OutputSelectedPath.Location = new System.Drawing.Point(79, 30);
            this.Textbox_OutputSelectedPath.Name = "Textbox_OutputSelectedPath";
            this.Textbox_OutputSelectedPath.ReadOnly = true;
            this.Textbox_OutputSelectedPath.Size = new System.Drawing.Size(352, 20);
            this.Textbox_OutputSelectedPath.TabIndex = 1;
            // 
            // Label_OutputPath
            // 
            this.Label_OutputPath.AutoSize = true;
            this.Label_OutputPath.Location = new System.Drawing.Point(6, 33);
            this.Label_OutputPath.Name = "Label_OutputPath";
            this.Label_OutputPath.Size = new System.Drawing.Size(67, 13);
            this.Label_OutputPath.TabIndex = 0;
            this.Label_OutputPath.Text = "Output Path:";
            // 
            // Groupbox_Shell
            // 
            this.Groupbox_Shell.Controls.Add(this.ButtonRegister_FileTypes);
            this.Groupbox_Shell.Controls.Add(this.Label_Expl);
            this.Groupbox_Shell.Location = new System.Drawing.Point(12, 160);
            this.Groupbox_Shell.Name = "Groupbox_Shell";
            this.Groupbox_Shell.Size = new System.Drawing.Size(467, 92);
            this.Groupbox_Shell.TabIndex = 3;
            this.Groupbox_Shell.TabStop = false;
            this.Groupbox_Shell.Text = "Shell";
            // 
            // ButtonRegister_FileTypes
            // 
            this.ButtonRegister_FileTypes.Location = new System.Drawing.Point(141, 63);
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
            // Frm_General
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 339);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Title;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.FontDialog FontDialogTreeView;
        private System.Windows.Forms.GroupBox GroupBox_Output;
        private System.Windows.Forms.Button Button_Choose;
        private System.Windows.Forms.TextBox Textbox_OutputSelectedPath;
        private System.Windows.Forms.Label Label_OutputPath;
        private System.Windows.Forms.GroupBox Groupbox_Shell;
        private System.Windows.Forms.Label Label_Expl;
        private System.Windows.Forms.Button ButtonRegister_FileTypes;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser_OutputPath;
    }
}