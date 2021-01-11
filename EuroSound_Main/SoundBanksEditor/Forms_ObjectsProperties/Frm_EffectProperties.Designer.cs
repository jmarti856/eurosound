namespace EuroSound_Application.SoundBanksEditor
{
    partial class Frm_EffectProperties
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_EffectProperties));
            this.groupbox_params = new System.Windows.Forms.GroupBox();
            this.Textbox_OuterRadius = new System.Windows.Forms.TextBox();
            this.Textbox_InnerRadius = new System.Windows.Forms.TextBox();
            this.numeric_mastervolume = new System.Windows.Forms.NumericUpDown();
            this.label_mastervolume = new System.Windows.Forms.Label();
            this.textbox_flags = new System.Windows.Forms.TextBox();
            this.label_flags = new System.Windows.Forms.Label();
            this.numeric_ducker = new System.Windows.Forms.NumericUpDown();
            this.label_ducker = new System.Windows.Forms.Label();
            this.numeric_priority = new System.Windows.Forms.NumericUpDown();
            this.label_priority = new System.Windows.Forms.Label();
            this.cbx_trackingtype = new System.Windows.Forms.ComboBox();
            this.numeric_maxvoices = new System.Windows.Forms.NumericUpDown();
            this.label_maxvoices = new System.Windows.Forms.Label();
            this.label_trackingtype = new System.Windows.Forms.Label();
            this.numeric_reverbsend = new System.Windows.Forms.NumericUpDown();
            this.label_reverbsend = new System.Windows.Forms.Label();
            this.label_outerradiusreal = new System.Windows.Forms.Label();
            this.label_innerradiusreal = new System.Windows.Forms.Label();
            this.numeric_maxdelay = new System.Windows.Forms.NumericUpDown();
            this.label_maxdelay = new System.Windows.Forms.Label();
            this.numeric_mindelay = new System.Windows.Forms.NumericUpDown();
            this.label_mindelay = new System.Windows.Forms.Label();
            this.numeric_duckerlength = new System.Windows.Forms.NumericUpDown();
            this.label_duckerlenght = new System.Windows.Forms.Label();
            this.groupbox_samples = new System.Windows.Forms.GroupBox();
            this.List_Samples = new System.Windows.Forms.ListView();
            this.SamplesImageList = new System.Windows.Forms.ImageList(this.components);
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.grbx_hashcode = new System.Windows.Forms.GroupBox();
            this.cbx_hashcode = new System.Windows.Forms.ComboBox();
            this.label_hashcode = new System.Windows.Forms.Label();
            this.Checkbox_OutputThisSound = new System.Windows.Forms.CheckBox();
            this.groupbox_params.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_mastervolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_ducker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_priority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_maxvoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_reverbsend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_maxdelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_mindelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_duckerlength)).BeginInit();
            this.groupbox_samples.SuspendLayout();
            this.grbx_hashcode.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupbox_params
            // 
            this.groupbox_params.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupbox_params.Controls.Add(this.Textbox_OuterRadius);
            this.groupbox_params.Controls.Add(this.Textbox_InnerRadius);
            this.groupbox_params.Controls.Add(this.numeric_mastervolume);
            this.groupbox_params.Controls.Add(this.label_mastervolume);
            this.groupbox_params.Controls.Add(this.textbox_flags);
            this.groupbox_params.Controls.Add(this.label_flags);
            this.groupbox_params.Controls.Add(this.numeric_ducker);
            this.groupbox_params.Controls.Add(this.label_ducker);
            this.groupbox_params.Controls.Add(this.numeric_priority);
            this.groupbox_params.Controls.Add(this.label_priority);
            this.groupbox_params.Controls.Add(this.cbx_trackingtype);
            this.groupbox_params.Controls.Add(this.numeric_maxvoices);
            this.groupbox_params.Controls.Add(this.label_maxvoices);
            this.groupbox_params.Controls.Add(this.label_trackingtype);
            this.groupbox_params.Controls.Add(this.numeric_reverbsend);
            this.groupbox_params.Controls.Add(this.label_reverbsend);
            this.groupbox_params.Controls.Add(this.label_outerradiusreal);
            this.groupbox_params.Controls.Add(this.label_innerradiusreal);
            this.groupbox_params.Controls.Add(this.numeric_maxdelay);
            this.groupbox_params.Controls.Add(this.label_maxdelay);
            this.groupbox_params.Controls.Add(this.numeric_mindelay);
            this.groupbox_params.Controls.Add(this.label_mindelay);
            this.groupbox_params.Controls.Add(this.numeric_duckerlength);
            this.groupbox_params.Controls.Add(this.label_duckerlenght);
            this.groupbox_params.Location = new System.Drawing.Point(12, 75);
            this.groupbox_params.Name = "groupbox_params";
            this.groupbox_params.Size = new System.Drawing.Size(524, 180);
            this.groupbox_params.TabIndex = 1;
            this.groupbox_params.TabStop = false;
            this.groupbox_params.Text = "Params:";
            // 
            // Textbox_OuterRadius
            // 
            this.Textbox_OuterRadius.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_OuterRadius.Location = new System.Drawing.Point(107, 120);
            this.Textbox_OuterRadius.Name = "Textbox_OuterRadius";
            this.Textbox_OuterRadius.ReadOnly = true;
            this.Textbox_OuterRadius.Size = new System.Drawing.Size(134, 20);
            this.Textbox_OuterRadius.TabIndex = 25;
            // 
            // Textbox_InnerRadius
            // 
            this.Textbox_InnerRadius.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_InnerRadius.Location = new System.Drawing.Point(107, 94);
            this.Textbox_InnerRadius.Name = "Textbox_InnerRadius";
            this.Textbox_InnerRadius.ReadOnly = true;
            this.Textbox_InnerRadius.Size = new System.Drawing.Size(134, 20);
            this.Textbox_InnerRadius.TabIndex = 24;
            // 
            // numeric_mastervolume
            // 
            this.numeric_mastervolume.Location = new System.Drawing.Point(365, 121);
            this.numeric_mastervolume.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.numeric_mastervolume.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.numeric_mastervolume.Name = "numeric_mastervolume";
            this.numeric_mastervolume.Size = new System.Drawing.Size(152, 20);
            this.numeric_mastervolume.TabIndex = 21;
            // 
            // label_mastervolume
            // 
            this.label_mastervolume.AutoSize = true;
            this.label_mastervolume.Location = new System.Drawing.Point(279, 123);
            this.label_mastervolume.Name = "label_mastervolume";
            this.label_mastervolume.Size = new System.Drawing.Size(80, 13);
            this.label_mastervolume.TabIndex = 20;
            this.label_mastervolume.Text = "Master Volume:";
            // 
            // textbox_flags
            // 
            this.textbox_flags.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textbox_flags.Location = new System.Drawing.Point(365, 149);
            this.textbox_flags.Name = "textbox_flags";
            this.textbox_flags.ReadOnly = true;
            this.textbox_flags.Size = new System.Drawing.Size(152, 20);
            this.textbox_flags.TabIndex = 23;
            this.textbox_flags.Click += new System.EventHandler(this.Textbox_flags_Click);
            // 
            // label_flags
            // 
            this.label_flags.AutoSize = true;
            this.label_flags.Location = new System.Drawing.Point(324, 152);
            this.label_flags.Name = "label_flags";
            this.label_flags.Size = new System.Drawing.Size(35, 13);
            this.label_flags.TabIndex = 22;
            this.label_flags.Text = "Flags:";
            // 
            // numeric_ducker
            // 
            this.numeric_ducker.Location = new System.Drawing.Point(365, 95);
            this.numeric_ducker.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.numeric_ducker.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.numeric_ducker.Name = "numeric_ducker";
            this.numeric_ducker.Size = new System.Drawing.Size(152, 20);
            this.numeric_ducker.TabIndex = 19;
            // 
            // label_ducker
            // 
            this.label_ducker.AutoSize = true;
            this.label_ducker.Location = new System.Drawing.Point(314, 97);
            this.label_ducker.Name = "label_ducker";
            this.label_ducker.Size = new System.Drawing.Size(45, 13);
            this.label_ducker.TabIndex = 18;
            this.label_ducker.Text = "Ducker:";
            // 
            // numeric_priority
            // 
            this.numeric_priority.Location = new System.Drawing.Point(365, 69);
            this.numeric_priority.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.numeric_priority.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.numeric_priority.Name = "numeric_priority";
            this.numeric_priority.Size = new System.Drawing.Size(152, 20);
            this.numeric_priority.TabIndex = 17;
            // 
            // label_priority
            // 
            this.label_priority.AutoSize = true;
            this.label_priority.Location = new System.Drawing.Point(318, 71);
            this.label_priority.Name = "label_priority";
            this.label_priority.Size = new System.Drawing.Size(41, 13);
            this.label_priority.TabIndex = 16;
            this.label_priority.Text = "Priority:";
            // 
            // cbx_trackingtype
            // 
            this.cbx_trackingtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_trackingtype.FormattingEnabled = true;
            this.cbx_trackingtype.Items.AddRange(new object[] {
            "2D",
            "Amb",
            "3D",
            "3D_Rnd_Pos",
            "2D_PL2"});
            this.cbx_trackingtype.Location = new System.Drawing.Point(365, 16);
            this.cbx_trackingtype.Name = "cbx_trackingtype";
            this.cbx_trackingtype.Size = new System.Drawing.Size(152, 21);
            this.cbx_trackingtype.TabIndex = 13;
            // 
            // numeric_maxvoices
            // 
            this.numeric_maxvoices.Location = new System.Drawing.Point(365, 43);
            this.numeric_maxvoices.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.numeric_maxvoices.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.numeric_maxvoices.Name = "numeric_maxvoices";
            this.numeric_maxvoices.Size = new System.Drawing.Size(152, 20);
            this.numeric_maxvoices.TabIndex = 15;
            // 
            // label_maxvoices
            // 
            this.label_maxvoices.AutoSize = true;
            this.label_maxvoices.Location = new System.Drawing.Point(294, 45);
            this.label_maxvoices.Name = "label_maxvoices";
            this.label_maxvoices.Size = new System.Drawing.Size(65, 13);
            this.label_maxvoices.TabIndex = 14;
            this.label_maxvoices.Text = "Max Voices:";
            // 
            // label_trackingtype
            // 
            this.label_trackingtype.AutoSize = true;
            this.label_trackingtype.Location = new System.Drawing.Point(280, 19);
            this.label_trackingtype.Name = "label_trackingtype";
            this.label_trackingtype.Size = new System.Drawing.Size(79, 13);
            this.label_trackingtype.TabIndex = 12;
            this.label_trackingtype.Text = "Tracking Type:";
            // 
            // numeric_reverbsend
            // 
            this.numeric_reverbsend.Location = new System.Drawing.Point(107, 147);
            this.numeric_reverbsend.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.numeric_reverbsend.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.numeric_reverbsend.Name = "numeric_reverbsend";
            this.numeric_reverbsend.Size = new System.Drawing.Size(134, 20);
            this.numeric_reverbsend.TabIndex = 11;
            // 
            // label_reverbsend
            // 
            this.label_reverbsend.AutoSize = true;
            this.label_reverbsend.Location = new System.Drawing.Point(28, 149);
            this.label_reverbsend.Name = "label_reverbsend";
            this.label_reverbsend.Size = new System.Drawing.Size(73, 13);
            this.label_reverbsend.TabIndex = 10;
            this.label_reverbsend.Text = "Reverb Send:";
            // 
            // label_outerradiusreal
            // 
            this.label_outerradiusreal.AutoSize = true;
            this.label_outerradiusreal.Location = new System.Drawing.Point(4, 123);
            this.label_outerradiusreal.Name = "label_outerradiusreal";
            this.label_outerradiusreal.Size = new System.Drawing.Size(97, 13);
            this.label_outerradiusreal.TabIndex = 8;
            this.label_outerradiusreal.Text = "Outer Radius Real:";
            // 
            // label_innerradiusreal
            // 
            this.label_innerradiusreal.AutoSize = true;
            this.label_innerradiusreal.Location = new System.Drawing.Point(6, 97);
            this.label_innerradiusreal.Name = "label_innerradiusreal";
            this.label_innerradiusreal.Size = new System.Drawing.Size(95, 13);
            this.label_innerradiusreal.TabIndex = 6;
            this.label_innerradiusreal.Text = "Inner Radius Real:";
            // 
            // numeric_maxdelay
            // 
            this.numeric_maxdelay.Location = new System.Drawing.Point(107, 69);
            this.numeric_maxdelay.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numeric_maxdelay.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numeric_maxdelay.Name = "numeric_maxdelay";
            this.numeric_maxdelay.Size = new System.Drawing.Size(134, 20);
            this.numeric_maxdelay.TabIndex = 5;
            // 
            // label_maxdelay
            // 
            this.label_maxdelay.AutoSize = true;
            this.label_maxdelay.Location = new System.Drawing.Point(41, 71);
            this.label_maxdelay.Name = "label_maxdelay";
            this.label_maxdelay.Size = new System.Drawing.Size(60, 13);
            this.label_maxdelay.TabIndex = 4;
            this.label_maxdelay.Text = "Max Delay:";
            // 
            // numeric_mindelay
            // 
            this.numeric_mindelay.Location = new System.Drawing.Point(107, 43);
            this.numeric_mindelay.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numeric_mindelay.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numeric_mindelay.Name = "numeric_mindelay";
            this.numeric_mindelay.Size = new System.Drawing.Size(134, 20);
            this.numeric_mindelay.TabIndex = 3;
            // 
            // label_mindelay
            // 
            this.label_mindelay.AutoSize = true;
            this.label_mindelay.Location = new System.Drawing.Point(44, 45);
            this.label_mindelay.Name = "label_mindelay";
            this.label_mindelay.Size = new System.Drawing.Size(57, 13);
            this.label_mindelay.TabIndex = 2;
            this.label_mindelay.Text = "Min Delay:";
            // 
            // numeric_duckerlength
            // 
            this.numeric_duckerlength.Location = new System.Drawing.Point(107, 17);
            this.numeric_duckerlength.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numeric_duckerlength.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numeric_duckerlength.Name = "numeric_duckerlength";
            this.numeric_duckerlength.Size = new System.Drawing.Size(134, 20);
            this.numeric_duckerlength.TabIndex = 1;
            // 
            // label_duckerlenght
            // 
            this.label_duckerlenght.AutoSize = true;
            this.label_duckerlenght.Location = new System.Drawing.Point(20, 19);
            this.label_duckerlenght.Name = "label_duckerlenght";
            this.label_duckerlenght.Size = new System.Drawing.Size(81, 13);
            this.label_duckerlenght.TabIndex = 0;
            this.label_duckerlenght.Text = "Ducker Lenght:";
            // 
            // groupbox_samples
            // 
            this.groupbox_samples.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupbox_samples.Controls.Add(this.List_Samples);
            this.groupbox_samples.Location = new System.Drawing.Point(12, 261);
            this.groupbox_samples.Name = "groupbox_samples";
            this.groupbox_samples.Size = new System.Drawing.Size(524, 293);
            this.groupbox_samples.TabIndex = 2;
            this.groupbox_samples.TabStop = false;
            this.groupbox_samples.Text = "Associated Samples";
            // 
            // List_Samples
            // 
            this.List_Samples.Dock = System.Windows.Forms.DockStyle.Fill;
            this.List_Samples.FullRowSelect = true;
            this.List_Samples.GridLines = true;
            this.List_Samples.HideSelection = false;
            this.List_Samples.Location = new System.Drawing.Point(3, 16);
            this.List_Samples.Name = "List_Samples";
            this.List_Samples.Size = new System.Drawing.Size(518, 274);
            this.List_Samples.SmallImageList = this.SamplesImageList;
            this.List_Samples.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.List_Samples.TabIndex = 0;
            this.List_Samples.UseCompatibleStateImageBehavior = false;
            this.List_Samples.View = System.Windows.Forms.View.List;
            this.List_Samples.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.List_Samples_MouseDoubleClick);
            // 
            // SamplesImageList
            // 
            this.SamplesImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SamplesImageList.ImageStream")));
            this.SamplesImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.SamplesImageList.Images.SetKeyName(0, "audio_compression-1.png");
            // 
            // button_ok
            // 
            this.button_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ok.Location = new System.Drawing.Point(380, 560);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 3;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.Button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(461, 560);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 4;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.Button_cancel_Click);
            // 
            // grbx_hashcode
            // 
            this.grbx_hashcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbx_hashcode.Controls.Add(this.cbx_hashcode);
            this.grbx_hashcode.Controls.Add(this.label_hashcode);
            this.grbx_hashcode.Location = new System.Drawing.Point(12, 12);
            this.grbx_hashcode.Name = "grbx_hashcode";
            this.grbx_hashcode.Size = new System.Drawing.Size(524, 57);
            this.grbx_hashcode.TabIndex = 0;
            this.grbx_hashcode.TabStop = false;
            this.grbx_hashcode.Text = "Hashcode";
            // 
            // cbx_hashcode
            // 
            this.cbx_hashcode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_hashcode.FormattingEnabled = true;
            this.cbx_hashcode.Location = new System.Drawing.Point(71, 19);
            this.cbx_hashcode.Name = "cbx_hashcode";
            this.cbx_hashcode.Size = new System.Drawing.Size(447, 21);
            this.cbx_hashcode.TabIndex = 1;
            this.cbx_hashcode.SelectionChangeCommitted += new System.EventHandler(this.Cbx_hashcode_SelectionChangeCommitted);
            this.cbx_hashcode.Click += new System.EventHandler(this.Cbx_hashcode_Click);
            // 
            // label_hashcode
            // 
            this.label_hashcode.AutoSize = true;
            this.label_hashcode.Location = new System.Drawing.Point(6, 22);
            this.label_hashcode.Name = "label_hashcode";
            this.label_hashcode.Size = new System.Drawing.Size(59, 13);
            this.label_hashcode.TabIndex = 0;
            this.label_hashcode.Text = "Hashcode:";
            // 
            // Checkbox_OutputThisSound
            // 
            this.Checkbox_OutputThisSound.AutoSize = true;
            this.Checkbox_OutputThisSound.Location = new System.Drawing.Point(12, 560);
            this.Checkbox_OutputThisSound.Name = "Checkbox_OutputThisSound";
            this.Checkbox_OutputThisSound.Size = new System.Drawing.Size(115, 17);
            this.Checkbox_OutputThisSound.TabIndex = 5;
            this.Checkbox_OutputThisSound.Text = "Output This Sound";
            this.Checkbox_OutputThisSound.UseVisualStyleBackColor = true;
            // 
            // Frm_EffectProperties
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(548, 595);
            this.Controls.Add(this.Checkbox_OutputThisSound);
            this.Controls.Add(this.grbx_hashcode);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.groupbox_samples);
            this.Controls.Add(this.groupbox_params);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_EffectProperties";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_EffectProperties";
            this.Load += new System.EventHandler(this.Frm_EffectProperties_Load);
            this.Shown += new System.EventHandler(this.Frm_EffectProperties_Shown);
            this.groupbox_params.ResumeLayout(false);
            this.groupbox_params.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_mastervolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_ducker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_priority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_maxvoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_reverbsend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_maxdelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_mindelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_duckerlength)).EndInit();
            this.groupbox_samples.ResumeLayout(false);
            this.grbx_hashcode.ResumeLayout(false);
            this.grbx_hashcode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupbox_params;
        private System.Windows.Forms.NumericUpDown numeric_reverbsend;
        private System.Windows.Forms.Label label_reverbsend;
        private System.Windows.Forms.Label label_outerradiusreal;
        private System.Windows.Forms.Label label_innerradiusreal;
        private System.Windows.Forms.NumericUpDown numeric_maxdelay;
        private System.Windows.Forms.Label label_maxdelay;
        private System.Windows.Forms.NumericUpDown numeric_mindelay;
        private System.Windows.Forms.Label label_mindelay;
        private System.Windows.Forms.NumericUpDown numeric_duckerlength;
        private System.Windows.Forms.Label label_duckerlenght;
        private System.Windows.Forms.NumericUpDown numeric_priority;
        private System.Windows.Forms.Label label_priority;
        private System.Windows.Forms.ComboBox cbx_trackingtype;
        private System.Windows.Forms.NumericUpDown numeric_maxvoices;
        private System.Windows.Forms.Label label_maxvoices;
        private System.Windows.Forms.Label label_trackingtype;
        private System.Windows.Forms.NumericUpDown numeric_ducker;
        private System.Windows.Forms.Label label_ducker;
        private System.Windows.Forms.TextBox textbox_flags;
        private System.Windows.Forms.Label label_flags;
        private System.Windows.Forms.GroupBox groupbox_samples;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.GroupBox grbx_hashcode;
        private System.Windows.Forms.ComboBox cbx_hashcode;
        private System.Windows.Forms.Label label_hashcode;
        private System.Windows.Forms.NumericUpDown numeric_mastervolume;
        private System.Windows.Forms.Label label_mastervolume;
        private System.Windows.Forms.CheckBox Checkbox_OutputThisSound;
        private System.Windows.Forms.TextBox Textbox_OuterRadius;
        private System.Windows.Forms.TextBox Textbox_InnerRadius;
        private System.Windows.Forms.ListView List_Samples;
        private System.Windows.Forms.ImageList SamplesImageList;
    }
}