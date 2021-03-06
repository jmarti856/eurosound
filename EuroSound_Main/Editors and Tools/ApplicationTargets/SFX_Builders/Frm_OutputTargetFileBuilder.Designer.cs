﻿
namespace EuroSound_Application.Editors_and_Tools.ApplicationTargets
{
    partial class Frm_OutputTargetFileBuilder
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
            this.Panel_Container = new System.Windows.Forms.Panel();
            this.Label_ProjectAndTarget = new System.Windows.Forms.Label();
            this.Button_Abort = new System.Windows.Forms.Button();
            this.Label_CurrentTask = new System.Windows.Forms.Label();
            this.ProgressBar_CurrentTask = new System.Windows.Forms.ProgressBar();
            this.ProgressBar_Total = new System.Windows.Forms.ProgressBar();
            this.Label_EngineX = new System.Windows.Forms.Label();
            this.BackgroundWorker_BuildSFX = new System.ComponentModel.BackgroundWorker();
            this.Panel_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Container
            // 
            this.Panel_Container.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel_Container.Controls.Add(this.Label_ProjectAndTarget);
            this.Panel_Container.Controls.Add(this.Button_Abort);
            this.Panel_Container.Controls.Add(this.Label_CurrentTask);
            this.Panel_Container.Controls.Add(this.ProgressBar_CurrentTask);
            this.Panel_Container.Controls.Add(this.ProgressBar_Total);
            this.Panel_Container.Controls.Add(this.Label_EngineX);
            this.Panel_Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Container.Location = new System.Drawing.Point(0, 0);
            this.Panel_Container.Name = "Panel_Container";
            this.Panel_Container.Size = new System.Drawing.Size(350, 171);
            this.Panel_Container.TabIndex = 3;
            // 
            // Label_ProjectAndTarget
            // 
            this.Label_ProjectAndTarget.AutoSize = true;
            this.Label_ProjectAndTarget.Location = new System.Drawing.Point(92, 7);
            this.Label_ProjectAndTarget.Name = "Label_ProjectAndTarget";
            this.Label_ProjectAndTarget.Size = new System.Drawing.Size(105, 13);
            this.Label_ProjectAndTarget.TabIndex = 1;
            this.Label_ProjectAndTarget.Text = "XXXXXXXXXXXXXX";
            // 
            // Button_Abort
            // 
            this.Button_Abort.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Button_Abort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Abort.Location = new System.Drawing.Point(131, 134);
            this.Button_Abort.Name = "Button_Abort";
            this.Button_Abort.Size = new System.Drawing.Size(75, 23);
            this.Button_Abort.TabIndex = 5;
            this.Button_Abort.Text = "Abort";
            this.Button_Abort.UseVisualStyleBackColor = true;
            this.Button_Abort.Click += new System.EventHandler(this.Button_Abort_Click);
            // 
            // Label_CurrentTask
            // 
            this.Label_CurrentTask.AutoSize = true;
            this.Label_CurrentTask.Location = new System.Drawing.Point(10, 90);
            this.Label_CurrentTask.Name = "Label_CurrentTask";
            this.Label_CurrentTask.Size = new System.Drawing.Size(91, 13);
            this.Label_CurrentTask.TabIndex = 4;
            this.Label_CurrentTask.Text = "XXXXXXXXXXXX";
            // 
            // ProgressBar_CurrentTask
            // 
            this.ProgressBar_CurrentTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar_CurrentTask.Location = new System.Drawing.Point(13, 58);
            this.ProgressBar_CurrentTask.Name = "ProgressBar_CurrentTask";
            this.ProgressBar_CurrentTask.Size = new System.Drawing.Size(323, 29);
            this.ProgressBar_CurrentTask.TabIndex = 3;
            // 
            // ProgressBar_Total
            // 
            this.ProgressBar_Total.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar_Total.Location = new System.Drawing.Point(13, 23);
            this.ProgressBar_Total.Name = "ProgressBar_Total";
            this.ProgressBar_Total.Size = new System.Drawing.Size(323, 29);
            this.ProgressBar_Total.TabIndex = 2;
            // 
            // Label_EngineX
            // 
            this.Label_EngineX.AutoSize = true;
            this.Label_EngineX.Location = new System.Drawing.Point(10, 7);
            this.Label_EngineX.Name = "Label_EngineX";
            this.Label_EngineX.Size = new System.Drawing.Size(85, 13);
            this.Label_EngineX.TabIndex = 0;
            this.Label_EngineX.Text = "EngineX Output:";
            // 
            // BackgroundWorker_BuildSFX
            // 
            this.BackgroundWorker_BuildSFX.WorkerReportsProgress = true;
            this.BackgroundWorker_BuildSFX.WorkerSupportsCancellation = true;
            this.BackgroundWorker_BuildSFX.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_BuildSFX_DoWork);
            this.BackgroundWorker_BuildSFX.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_BuildSFX_ProgressChanged);
            this.BackgroundWorker_BuildSFX.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_BuildSFX_RunWorkerCompleted);
            // 
            // Frm_OutputTargetFileBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 171);
            this.Controls.Add(this.Panel_Container);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_OutputTargetFileBuilder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Output Target";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_OutputTargetFileBuilder_FormClosing);
            this.Load += new System.EventHandler(this.Frm_OutputTargetFileBuilder_Load);
            this.Panel_Container.ResumeLayout(false);
            this.Panel_Container.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Container;
        private System.Windows.Forms.Button Button_Abort;
        private System.Windows.Forms.Label Label_CurrentTask;
        private System.Windows.Forms.ProgressBar ProgressBar_CurrentTask;
        private System.Windows.Forms.ProgressBar ProgressBar_Total;
        private System.Windows.Forms.Label Label_EngineX;
        private System.ComponentModel.BackgroundWorker BackgroundWorker_BuildSFX;
        private System.Windows.Forms.Label Label_ProjectAndTarget;
    }
}