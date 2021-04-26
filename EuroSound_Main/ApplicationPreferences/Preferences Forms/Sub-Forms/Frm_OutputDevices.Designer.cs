
namespace EuroSound_Application.ApplicationPreferencesForms
{
    partial class Frm_OutputDevices
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
            this.GroupboxAudioDevices = new System.Windows.Forms.GroupBox();
            this.Combobox_Driver = new System.Windows.Forms.ComboBox();
            this.Label_Driver = new System.Windows.Forms.Label();
            this.Combobox_AvailableDevices = new System.Windows.Forms.ComboBox();
            this.Label_AudioDevice = new System.Windows.Forms.Label();
            this.Panel_Title.SuspendLayout();
            this.GroupboxAudioDevices.SuspendLayout();
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
            this.Label_Title.Location = new System.Drawing.Point(199, 4);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(92, 16);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "Output Device";
            // 
            // GroupboxAudioDevices
            // 
            this.GroupboxAudioDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupboxAudioDevices.Controls.Add(this.Combobox_Driver);
            this.GroupboxAudioDevices.Controls.Add(this.Label_Driver);
            this.GroupboxAudioDevices.Controls.Add(this.Combobox_AvailableDevices);
            this.GroupboxAudioDevices.Controls.Add(this.Label_AudioDevice);
            this.GroupboxAudioDevices.Location = new System.Drawing.Point(12, 127);
            this.GroupboxAudioDevices.Name = "GroupboxAudioDevices";
            this.GroupboxAudioDevices.Size = new System.Drawing.Size(489, 163);
            this.GroupboxAudioDevices.TabIndex = 2;
            this.GroupboxAudioDevices.TabStop = false;
            this.GroupboxAudioDevices.Text = "Available Devices:";
            // 
            // Combobox_Driver
            // 
            this.Combobox_Driver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_Driver.Enabled = false;
            this.Combobox_Driver.FormattingEnabled = true;
            this.Combobox_Driver.Items.AddRange(new object[] {
            "MME"});
            this.Combobox_Driver.Location = new System.Drawing.Point(56, 60);
            this.Combobox_Driver.Name = "Combobox_Driver";
            this.Combobox_Driver.Size = new System.Drawing.Size(121, 21);
            this.Combobox_Driver.TabIndex = 3;
            // 
            // Label_Driver
            // 
            this.Label_Driver.AutoSize = true;
            this.Label_Driver.Location = new System.Drawing.Point(12, 63);
            this.Label_Driver.Name = "Label_Driver";
            this.Label_Driver.Size = new System.Drawing.Size(38, 13);
            this.Label_Driver.TabIndex = 2;
            this.Label_Driver.Text = "Driver:";
            // 
            // Combobox_AvailableDevices
            // 
            this.Combobox_AvailableDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Combobox_AvailableDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_AvailableDevices.FormattingEnabled = true;
            this.Combobox_AvailableDevices.Location = new System.Drawing.Point(56, 33);
            this.Combobox_AvailableDevices.Name = "Combobox_AvailableDevices";
            this.Combobox_AvailableDevices.Size = new System.Drawing.Size(427, 21);
            this.Combobox_AvailableDevices.TabIndex = 1;
            // 
            // Label_AudioDevice
            // 
            this.Label_AudioDevice.AutoSize = true;
            this.Label_AudioDevice.Location = new System.Drawing.Point(6, 36);
            this.Label_AudioDevice.Name = "Label_AudioDevice";
            this.Label_AudioDevice.Size = new System.Drawing.Size(44, 13);
            this.Label_AudioDevice.TabIndex = 0;
            this.Label_AudioDevice.Text = "Device:";
            // 
            // Frm_OutputDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 395);
            this.Controls.Add(this.GroupboxAudioDevices);
            this.Controls.Add(this.Panel_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_OutputDevices";
            this.Text = "Frm_OutputDevicecs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_OutputDevicecs_FormClosing);
            this.Load += new System.EventHandler(this.Frm_OutputDevicecs_Load);
            this.Panel_Title.ResumeLayout(false);
            this.Panel_Title.PerformLayout();
            this.GroupboxAudioDevices.ResumeLayout(false);
            this.GroupboxAudioDevices.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Title;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.GroupBox GroupboxAudioDevices;
        private System.Windows.Forms.ComboBox Combobox_AvailableDevices;
        private System.Windows.Forms.Label Label_AudioDevice;
        private System.Windows.Forms.ComboBox Combobox_Driver;
        private System.Windows.Forms.Label Label_Driver;
    }
}