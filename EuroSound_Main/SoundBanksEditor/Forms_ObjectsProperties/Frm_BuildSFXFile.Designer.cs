
namespace EuroSound_Application
{
    partial class Frm_BuildSFXFile
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
            this.Label_EngineX = new System.Windows.Forms.Label();
            this.ProgressBar_Total = new System.Windows.Forms.ProgressBar();
            this.Panel_Container = new System.Windows.Forms.Panel();
            this.Button_Abort = new System.Windows.Forms.Button();
            this.Label_CurrentTask = new System.Windows.Forms.Label();
            this.ProgressBar_CurrentTask = new System.Windows.Forms.ProgressBar();
            this.BackgroundWorker_BuildSFX = new System.ComponentModel.BackgroundWorker();
            this.Panel_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_EngineX
            // 
            this.Label_EngineX.AutoSize = true;
            this.Label_EngineX.Location = new System.Drawing.Point(10, 7);
            this.Label_EngineX.Name = "Label_EngineX";
            this.Label_EngineX.Size = new System.Drawing.Size(140, 13);
            this.Label_EngineX.TabIndex = 0;
            this.Label_EngineX.Text = "EngineX Output: Sphinx: PC";
            // 
            // ProgressBar_Total
            // 
            this.ProgressBar_Total.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar_Total.Location = new System.Drawing.Point(13, 23);
            this.ProgressBar_Total.Name = "ProgressBar_Total";
            this.ProgressBar_Total.Size = new System.Drawing.Size(323, 29);
            this.ProgressBar_Total.TabIndex = 1;
            // 
            // Panel_Container
            // 
            this.Panel_Container.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel_Container.Controls.Add(this.Button_Abort);
            this.Panel_Container.Controls.Add(this.Label_CurrentTask);
            this.Panel_Container.Controls.Add(this.ProgressBar_CurrentTask);
            this.Panel_Container.Controls.Add(this.ProgressBar_Total);
            this.Panel_Container.Controls.Add(this.Label_EngineX);
            this.Panel_Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Container.Location = new System.Drawing.Point(0, 0);
            this.Panel_Container.Name = "Panel_Container";
            this.Panel_Container.Size = new System.Drawing.Size(350, 171);
            this.Panel_Container.TabIndex = 2;
            // 
            // Button_Abort
            // 
            this.Button_Abort.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Button_Abort.Location = new System.Drawing.Point(131, 134);
            this.Button_Abort.Name = "Button_Abort";
            this.Button_Abort.Size = new System.Drawing.Size(75, 23);
            this.Button_Abort.TabIndex = 4;
            this.Button_Abort.Text = "Abort";
            this.Button_Abort.UseVisualStyleBackColor = true;
            this.Button_Abort.Click += new System.EventHandler(this.Button_Abort_Click);
            // 
            // Label_CurrentTask
            // 
            this.Label_CurrentTask.AutoSize = true;
            this.Label_CurrentTask.Location = new System.Drawing.Point(10, 90);
            this.Label_CurrentTask.Name = "Label_CurrentTask";
            this.Label_CurrentTask.Size = new System.Drawing.Size(65, 13);
            this.Label_CurrentTask.TabIndex = 3;
            this.Label_CurrentTask.Text = "CurrentTask";
            // 
            // ProgressBar_CurrentTask
            // 
            this.ProgressBar_CurrentTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar_CurrentTask.Location = new System.Drawing.Point(13, 58);
            this.ProgressBar_CurrentTask.Name = "ProgressBar_CurrentTask";
            this.ProgressBar_CurrentTask.Size = new System.Drawing.Size(323, 29);
            this.ProgressBar_CurrentTask.TabIndex = 2;
            // 
            // BackgroundWorker_BuildSFX
            // 
            this.BackgroundWorker_BuildSFX.WorkerReportsProgress = true;
            this.BackgroundWorker_BuildSFX.WorkerSupportsCancellation = true;
            this.BackgroundWorker_BuildSFX.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_BuildSFX_DoWork);
            this.BackgroundWorker_BuildSFX.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_BuildSFX_ProgressChanged);
            this.BackgroundWorker_BuildSFX.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_BuildSFX_RunWorkerCompleted);
            // 
            // Frm_BuildSFXFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 171);
            this.Controls.Add(this.Panel_Container);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_BuildSFXFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_BuildSFXFile";
            this.Load += new System.EventHandler(this.Frm_BuildSFXFile_Load);
            this.Panel_Container.ResumeLayout(false);
            this.Panel_Container.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label_EngineX;
        private System.Windows.Forms.ProgressBar ProgressBar_Total;
        private System.Windows.Forms.Panel Panel_Container;
        private System.Windows.Forms.Button Button_Abort;
        private System.Windows.Forms.Label Label_CurrentTask;
        private System.Windows.Forms.ProgressBar ProgressBar_CurrentTask;
        private System.ComponentModel.BackgroundWorker BackgroundWorker_BuildSFX;
    }
}