/*
 *  frmColorPicker.cs
 *  
 *  Copyright (c) 2007-2010, OpenPainter.org, and based on the work of
 *                2005 Danny Blanchard (scrabcakes@gmail.com)
 *  
 *  The contents of this file are subject to the Mozilla Public License
 *  Version 1.1 (the "License"); you may not use this file except in
 *  compliance with the License. You may obtain a copy of the License at
 *  
 *  http://www.mozilla.org/MPL/
 *  
 *  Software distributed under the License is distributed on an "AS IS"
 *  basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See
 *  the License for the specific language governing rights and limitations
 *  under the License.
 *  
 *  The Original Code is OpenPainter.
 *  
 *  The Initial Developer of the Original Code is OpenPainter.org.
 *  All Rights Reserved.
 */

/******************************************************************/
/*****                                                        *****/
/*****     Project:           Adobe Color Picker Clone 1      *****/
/*****     Filename:          frmColorPicker.cs               *****/
/*****     Original Author:   Danny Blanchard                 *****/
/*****                        - scrabcakes@gmail.com          *****/
/*****     Updates:	                                          *****/
/*****      3/28/2005 - Initial Version : Danny Blanchard     *****/
/*****      July 2010 - Updated by OpenPainter.org            *****/
/*****                                                        *****/
/******************************************************************/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenPainter.ColorPicker
{
    /// <summary>
    /// Summary description for frmColorPicker.
    /// </summary>
    public class FrmColorPicker : Form
    {
        public FrmColorPicker(Color starting_color)
        {
            InitializeComponent();

            _rgb = starting_color;
            _hsl = AdobeColors.RGB_to_HSB(_rgb);
            _cmyk = AdobeColors.RGB_to_CMYK(_rgb);

            UpdateTextBoxes();

            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;

            m_lbl_Primary_Color.BackColor = starting_color;
            m_lbl_Secondary_Color.BackColor = starting_color;

            m_rbtn_Hue.Checked = true;
        }

        private AdobeColors.HSB _hsl;
        private Color _rgb;
        private AdobeColors.CMYK _cmyk;

        public Color PrimaryColor
        {
            get
            {
                return _rgb;
            }
            set
            {
                _rgb = value;
                _hsl = AdobeColors.RGB_to_HSB(_rgb);

                UpdateTextBoxes();

                m_ctrl_BigBox.HSB = _hsl;
                m_ctrl_ThinBox.HSB = _hsl;

                m_lbl_Primary_Color.BackColor = _rgb;
            }
        }

        public ColorComponent DrawStyle
        {
            get
            {
                if (m_rbtn_Hue.Checked)
                {
                    return ColorComponent.Hue;
                }
                else if (m_rbtn_Sat.Checked)
                {
                    return ColorComponent.Saturation;
                }
                else if (m_rbtn_Black.Checked)
                {
                    return ColorComponent.Brightness;
                }
                else if (m_rbtn_Red.Checked)
                {
                    return ColorComponent.Red;
                }
                else if (m_rbtn_Green.Checked)
                {
                    return ColorComponent.Green;
                }
                else if (m_rbtn_Blue.Checked)
                {
                    return ColorComponent.Blue;
                }
                else
                {
                    return ColorComponent.Hue;
                }
            }
            set
            {
                switch (value)
                {
                    case ColorComponent.Hue:
                        m_rbtn_Hue.Checked = true;
                        break;
                    case ColorComponent.Saturation:
                        m_rbtn_Sat.Checked = true;
                        break;
                    case ColorComponent.Brightness:
                        m_rbtn_Black.Checked = true;
                        break;
                    case ColorComponent.Red:
                        m_rbtn_Red.Checked = true;
                        break;
                    case ColorComponent.Green:
                        m_rbtn_Green.Checked = true;
                        break;
                    case ColorComponent.Blue:
                        m_rbtn_Blue.Checked = true;
                        break;
                    default:
                        m_rbtn_Hue.Checked = true;
                        break;
                }
            }
        }

        #region Designer Generated Variables

        private System.Windows.Forms.PictureBox m_pbx_BlankBox;
        private System.Windows.Forms.Button m_cmd_OK;
        private System.Windows.Forms.Button m_cmd_Cancel;
        private System.Windows.Forms.TextBox m_txt_Hue;
        private System.Windows.Forms.TextBox m_txt_Sat;
        private System.Windows.Forms.TextBox m_txt_Black;
        private System.Windows.Forms.TextBox m_txt_Red;
        private System.Windows.Forms.TextBox m_txt_Green;
        private System.Windows.Forms.TextBox m_txt_Blue;
        private System.Windows.Forms.TextBox m_txt_Lum;
        private System.Windows.Forms.TextBox m_txt_a;
        private System.Windows.Forms.TextBox m_txt_b;
        private System.Windows.Forms.TextBox m_txt_Cyan;
        private System.Windows.Forms.TextBox m_txt_Magenta;
        private System.Windows.Forms.TextBox m_txt_Yellow;
        private System.Windows.Forms.TextBox m_txt_K;
        private System.Windows.Forms.TextBox m_txt_Hex;
        private System.Windows.Forms.RadioButton m_rbtn_Hue;
        private System.Windows.Forms.RadioButton m_rbtn_Sat;
        private System.Windows.Forms.RadioButton m_rbtn_Black;
        private System.Windows.Forms.RadioButton m_rbtn_Red;
        private System.Windows.Forms.RadioButton m_rbtn_Green;
        private System.Windows.Forms.RadioButton m_rbtn_Blue;
        private System.Windows.Forms.CheckBox chkWebColorsOnly;
        private System.Windows.Forms.Label m_lbl_HexPound;
        private System.Windows.Forms.RadioButton m_rbtn_L;
        private System.Windows.Forms.RadioButton m_rbtn_a;
        private System.Windows.Forms.RadioButton m_rbtn_b;
        private System.Windows.Forms.Label m_lbl_Cyan;
        private System.Windows.Forms.Label m_lbl_Magenta;
        private System.Windows.Forms.Label m_lbl_Yellow;
        private System.Windows.Forms.Label m_lbl_K;
        private System.Windows.Forms.Label m_lbl_Primary_Color;
        private System.Windows.Forms.Label m_lbl_Secondary_Color;
        private CtrlVerticalColorSlider m_ctrl_ThinBox;
        private Ctrl2DColorBox m_ctrl_BigBox;
        private System.Windows.Forms.Label m_lbl_Hue_Symbol;
        private System.Windows.Forms.Label m_lbl_Saturation_Symbol;
        private System.Windows.Forms.Label m_lbl_Black_Symbol;
        private System.Windows.Forms.Label m_lbl_Cyan_Symbol;
        private System.Windows.Forms.Label m_lbl_Magenta_Symbol;
        private System.Windows.Forms.Label m_lbl_Yellow_Symbol;
        private Label label1;
        private Label label2;
        private Label label3;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            OpenPainter.ColorPicker.AdobeColors.HSB hsb1 = new OpenPainter.ColorPicker.AdobeColors.HSB();
            OpenPainter.ColorPicker.AdobeColors.HSB hsb2 = new OpenPainter.ColorPicker.AdobeColors.HSB();
            m_pbx_BlankBox = new System.Windows.Forms.PictureBox();
            m_cmd_OK = new System.Windows.Forms.Button();
            m_cmd_Cancel = new System.Windows.Forms.Button();
            m_txt_Hue = new System.Windows.Forms.TextBox();
            m_txt_Sat = new System.Windows.Forms.TextBox();
            m_txt_Black = new System.Windows.Forms.TextBox();
            m_txt_Red = new System.Windows.Forms.TextBox();
            m_txt_Green = new System.Windows.Forms.TextBox();
            m_txt_Blue = new System.Windows.Forms.TextBox();
            m_txt_Lum = new System.Windows.Forms.TextBox();
            m_txt_a = new System.Windows.Forms.TextBox();
            m_txt_b = new System.Windows.Forms.TextBox();
            m_txt_Cyan = new System.Windows.Forms.TextBox();
            m_txt_Magenta = new System.Windows.Forms.TextBox();
            m_txt_Yellow = new System.Windows.Forms.TextBox();
            m_txt_K = new System.Windows.Forms.TextBox();
            m_txt_Hex = new System.Windows.Forms.TextBox();
            m_rbtn_Hue = new System.Windows.Forms.RadioButton();
            m_rbtn_Sat = new System.Windows.Forms.RadioButton();
            m_rbtn_Black = new System.Windows.Forms.RadioButton();
            m_rbtn_Red = new System.Windows.Forms.RadioButton();
            m_rbtn_Green = new System.Windows.Forms.RadioButton();
            m_rbtn_Blue = new System.Windows.Forms.RadioButton();
            chkWebColorsOnly = new System.Windows.Forms.CheckBox();
            m_lbl_HexPound = new System.Windows.Forms.Label();
            m_rbtn_L = new System.Windows.Forms.RadioButton();
            m_rbtn_a = new System.Windows.Forms.RadioButton();
            m_rbtn_b = new System.Windows.Forms.RadioButton();
            m_lbl_Cyan = new System.Windows.Forms.Label();
            m_lbl_Magenta = new System.Windows.Forms.Label();
            m_lbl_Yellow = new System.Windows.Forms.Label();
            m_lbl_K = new System.Windows.Forms.Label();
            m_lbl_Primary_Color = new System.Windows.Forms.Label();
            m_lbl_Secondary_Color = new System.Windows.Forms.Label();
            m_lbl_Hue_Symbol = new System.Windows.Forms.Label();
            m_lbl_Saturation_Symbol = new System.Windows.Forms.Label();
            m_lbl_Black_Symbol = new System.Windows.Forms.Label();
            m_lbl_Cyan_Symbol = new System.Windows.Forms.Label();
            m_lbl_Magenta_Symbol = new System.Windows.Forms.Label();
            m_lbl_Yellow_Symbol = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            m_ctrl_BigBox = new OpenPainter.ColorPicker.Ctrl2DColorBox();
            m_ctrl_ThinBox = new OpenPainter.ColorPicker.CtrlVerticalColorSlider();
            ((System.ComponentModel.ISupportInitialize)(m_pbx_BlankBox)).BeginInit();
            SuspendLayout();
            // 
            // m_pbx_BlankBox
            // 
            m_pbx_BlankBox.BackColor = System.Drawing.Color.Black;
            m_pbx_BlankBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            m_pbx_BlankBox.Location = new System.Drawing.Point(313, 45);
            m_pbx_BlankBox.Name = "m_pbx_BlankBox";
            m_pbx_BlankBox.Size = new System.Drawing.Size(63, 71);
            m_pbx_BlankBox.TabIndex = 3;
            m_pbx_BlankBox.TabStop = false;
            // 
            // m_cmd_OK
            // 
            m_cmd_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            m_cmd_OK.Location = new System.Drawing.Point(415, 11);
            m_cmd_OK.Name = "m_cmd_OK";
            m_cmd_OK.Size = new System.Drawing.Size(112, 23);
            m_cmd_OK.TabIndex = 4;
            m_cmd_OK.Text = "OK";
            m_cmd_OK.Click += new System.EventHandler(Cmd_OK_Click);
            // 
            // m_cmd_Cancel
            // 
            m_cmd_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            m_cmd_Cancel.Location = new System.Drawing.Point(415, 41);
            m_cmd_Cancel.Name = "m_cmd_Cancel";
            m_cmd_Cancel.Size = new System.Drawing.Size(112, 23);
            m_cmd_Cancel.TabIndex = 5;
            m_cmd_Cancel.Text = "Cancel";
            m_cmd_Cancel.Click += new System.EventHandler(Cmd_Cancel_Click);
            // 
            // m_txt_Hue
            // 
            m_txt_Hue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_Hue.Location = new System.Drawing.Point(355, 164);
            m_txt_Hue.Name = "m_txt_Hue";
            m_txt_Hue.Size = new System.Drawing.Size(33, 20);
            m_txt_Hue.TabIndex = 6;
            m_txt_Hue.Leave += new System.EventHandler(Txt_Hue_Leave);
            // 
            // m_txt_Sat
            // 
            m_txt_Sat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_Sat.Location = new System.Drawing.Point(355, 189);
            m_txt_Sat.Name = "m_txt_Sat";
            m_txt_Sat.Size = new System.Drawing.Size(33, 20);
            m_txt_Sat.TabIndex = 7;
            m_txt_Sat.Leave += new System.EventHandler(Txt_Sat_Leave);
            // 
            // m_txt_Black
            // 
            m_txt_Black.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_Black.Location = new System.Drawing.Point(355, 214);
            m_txt_Black.Name = "m_txt_Black";
            m_txt_Black.Size = new System.Drawing.Size(33, 20);
            m_txt_Black.TabIndex = 8;
            m_txt_Black.Leave += new System.EventHandler(Txt_Black_Leave);
            // 
            // m_txt_Red
            // 
            m_txt_Red.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_Red.Location = new System.Drawing.Point(355, 244);
            m_txt_Red.Name = "m_txt_Red";
            m_txt_Red.Size = new System.Drawing.Size(33, 20);
            m_txt_Red.TabIndex = 9;
            m_txt_Red.Leave += new System.EventHandler(Txt_Red_Leave);
            // 
            // m_txt_Green
            // 
            m_txt_Green.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_Green.Location = new System.Drawing.Point(355, 269);
            m_txt_Green.Name = "m_txt_Green";
            m_txt_Green.Size = new System.Drawing.Size(33, 20);
            m_txt_Green.TabIndex = 10;
            m_txt_Green.Leave += new System.EventHandler(Txt_Green_Leave);
            // 
            // m_txt_Blue
            // 
            m_txt_Blue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_Blue.Location = new System.Drawing.Point(355, 294);
            m_txt_Blue.Name = "m_txt_Blue";
            m_txt_Blue.Size = new System.Drawing.Size(33, 20);
            m_txt_Blue.TabIndex = 11;
            m_txt_Blue.Leave += new System.EventHandler(Txt_Blue_Leave);
            // 
            // m_txt_Lum
            // 
            m_txt_Lum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_Lum.Enabled = false;
            m_txt_Lum.Location = new System.Drawing.Point(467, 164);
            m_txt_Lum.Name = "m_txt_Lum";
            m_txt_Lum.Size = new System.Drawing.Size(42, 20);
            m_txt_Lum.TabIndex = 12;
            // 
            // m_txt_a
            // 
            m_txt_a.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_a.Enabled = false;
            m_txt_a.Location = new System.Drawing.Point(467, 189);
            m_txt_a.Name = "m_txt_a";
            m_txt_a.Size = new System.Drawing.Size(42, 20);
            m_txt_a.TabIndex = 13;
            // 
            // m_txt_b
            // 
            m_txt_b.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_b.Enabled = false;
            m_txt_b.Location = new System.Drawing.Point(467, 214);
            m_txt_b.Name = "m_txt_b";
            m_txt_b.Size = new System.Drawing.Size(42, 20);
            m_txt_b.TabIndex = 14;
            // 
            // m_txt_Cyan
            // 
            m_txt_Cyan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_Cyan.Location = new System.Drawing.Point(467, 244);
            m_txt_Cyan.Name = "m_txt_Cyan";
            m_txt_Cyan.Size = new System.Drawing.Size(33, 20);
            m_txt_Cyan.TabIndex = 15;
            m_txt_Cyan.Leave += new System.EventHandler(Txt_Cyan_Leave);
            // 
            // m_txt_Magenta
            // 
            m_txt_Magenta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_Magenta.Location = new System.Drawing.Point(467, 269);
            m_txt_Magenta.Name = "m_txt_Magenta";
            m_txt_Magenta.Size = new System.Drawing.Size(33, 20);
            m_txt_Magenta.TabIndex = 16;
            m_txt_Magenta.Leave += new System.EventHandler(Txt_Magenta_Leave);
            // 
            // m_txt_Yellow
            // 
            m_txt_Yellow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_Yellow.Location = new System.Drawing.Point(467, 294);
            m_txt_Yellow.Name = "m_txt_Yellow";
            m_txt_Yellow.Size = new System.Drawing.Size(33, 20);
            m_txt_Yellow.TabIndex = 17;
            m_txt_Yellow.Leave += new System.EventHandler(Txt_Yellow_Leave);
            // 
            // m_txt_K
            // 
            m_txt_K.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_K.Location = new System.Drawing.Point(467, 319);
            m_txt_K.Name = "m_txt_K";
            m_txt_K.Size = new System.Drawing.Size(33, 20);
            m_txt_K.TabIndex = 18;
            m_txt_K.Leave += new System.EventHandler(Txt_K_Leave);
            // 
            // m_txt_Hex
            // 
            m_txt_Hex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            m_txt_Hex.Location = new System.Drawing.Point(327, 324);
            m_txt_Hex.Name = "m_txt_Hex";
            m_txt_Hex.Size = new System.Drawing.Size(61, 20);
            m_txt_Hex.TabIndex = 19;
            m_txt_Hex.Leave += new System.EventHandler(Txt_Hex_Leave);
            // 
            // m_rbtn_Hue
            // 
            m_rbtn_Hue.AutoSize = true;
            m_rbtn_Hue.Location = new System.Drawing.Point(314, 164);
            m_rbtn_Hue.Name = "m_rbtn_Hue";
            m_rbtn_Hue.Size = new System.Drawing.Size(36, 17);
            m_rbtn_Hue.TabIndex = 20;
            m_rbtn_Hue.Text = "H:";
            m_rbtn_Hue.CheckedChanged += new System.EventHandler(Rbtn_Hue_CheckedChanged);
            // 
            // m_rbtn_Sat
            // 
            m_rbtn_Sat.AutoSize = true;
            m_rbtn_Sat.Location = new System.Drawing.Point(314, 189);
            m_rbtn_Sat.Name = "m_rbtn_Sat";
            m_rbtn_Sat.Size = new System.Drawing.Size(35, 17);
            m_rbtn_Sat.TabIndex = 21;
            m_rbtn_Sat.Text = "S:";
            m_rbtn_Sat.CheckedChanged += new System.EventHandler(Rbtn_Sat_CheckedChanged);
            // 
            // m_rbtn_Black
            // 
            m_rbtn_Black.AutoSize = true;
            m_rbtn_Black.Location = new System.Drawing.Point(314, 214);
            m_rbtn_Black.Name = "m_rbtn_Black";
            m_rbtn_Black.Size = new System.Drawing.Size(35, 17);
            m_rbtn_Black.TabIndex = 22;
            m_rbtn_Black.Text = "B:";
            m_rbtn_Black.CheckedChanged += new System.EventHandler(Rbtn_Black_CheckedChanged);
            // 
            // m_rbtn_Red
            // 
            m_rbtn_Red.AutoSize = true;
            m_rbtn_Red.Location = new System.Drawing.Point(314, 244);
            m_rbtn_Red.Name = "m_rbtn_Red";
            m_rbtn_Red.Size = new System.Drawing.Size(36, 17);
            m_rbtn_Red.TabIndex = 23;
            m_rbtn_Red.Text = "R:";
            m_rbtn_Red.CheckedChanged += new System.EventHandler(Rbtn_Red_CheckedChanged);
            // 
            // m_rbtn_Green
            // 
            m_rbtn_Green.AutoSize = true;
            m_rbtn_Green.Location = new System.Drawing.Point(314, 269);
            m_rbtn_Green.Name = "m_rbtn_Green";
            m_rbtn_Green.Size = new System.Drawing.Size(36, 17);
            m_rbtn_Green.TabIndex = 24;
            m_rbtn_Green.Text = "G:";
            m_rbtn_Green.CheckedChanged += new System.EventHandler(Rbtn_Green_CheckedChanged);
            // 
            // m_rbtn_Blue
            // 
            m_rbtn_Blue.AutoSize = true;
            m_rbtn_Blue.Location = new System.Drawing.Point(314, 294);
            m_rbtn_Blue.Name = "m_rbtn_Blue";
            m_rbtn_Blue.Size = new System.Drawing.Size(35, 17);
            m_rbtn_Blue.TabIndex = 25;
            m_rbtn_Blue.Text = "B:";
            m_rbtn_Blue.CheckedChanged += new System.EventHandler(Rbtn_Blue_CheckedChanged);
            // 
            // chkWebColorsOnly
            // 
            chkWebColorsOnly.AutoSize = true;
            chkWebColorsOnly.Location = new System.Drawing.Point(8, 298);
            chkWebColorsOnly.Name = "chkWebColorsOnly";
            chkWebColorsOnly.Size = new System.Drawing.Size(106, 17);
            chkWebColorsOnly.TabIndex = 26;
            chkWebColorsOnly.Text = "Only Web Colors";
            chkWebColorsOnly.CheckedChanged += new System.EventHandler(ChkWebColorsOnly_CheckedChanged);
            // 
            // m_lbl_HexPound
            // 
            m_lbl_HexPound.Location = new System.Drawing.Point(313, 326);
            m_lbl_HexPound.Name = "m_lbl_HexPound";
            m_lbl_HexPound.Size = new System.Drawing.Size(19, 15);
            m_lbl_HexPound.TabIndex = 27;
            m_lbl_HexPound.Text = "#";
            // 
            // m_rbtn_L
            // 
            m_rbtn_L.AutoSize = true;
            m_rbtn_L.Enabled = false;
            m_rbtn_L.Location = new System.Drawing.Point(427, 164);
            m_rbtn_L.Name = "m_rbtn_L";
            m_rbtn_L.Size = new System.Drawing.Size(34, 17);
            m_rbtn_L.TabIndex = 28;
            m_rbtn_L.Text = "L:";
            // 
            // m_rbtn_a
            // 
            m_rbtn_a.AutoSize = true;
            m_rbtn_a.Enabled = false;
            m_rbtn_a.Location = new System.Drawing.Point(427, 189);
            m_rbtn_a.Name = "m_rbtn_a";
            m_rbtn_a.Size = new System.Drawing.Size(35, 17);
            m_rbtn_a.TabIndex = 29;
            m_rbtn_a.Text = "a:";
            // 
            // m_rbtn_b
            // 
            m_rbtn_b.AutoSize = true;
            m_rbtn_b.Enabled = false;
            m_rbtn_b.Location = new System.Drawing.Point(427, 214);
            m_rbtn_b.Name = "m_rbtn_b";
            m_rbtn_b.Size = new System.Drawing.Size(35, 17);
            m_rbtn_b.TabIndex = 30;
            m_rbtn_b.Text = "b:";
            // 
            // m_lbl_Cyan
            // 
            m_lbl_Cyan.Location = new System.Drawing.Point(435, 249);
            m_lbl_Cyan.Name = "m_lbl_Cyan";
            m_lbl_Cyan.Size = new System.Drawing.Size(30, 18);
            m_lbl_Cyan.TabIndex = 31;
            m_lbl_Cyan.Text = "C:";
            m_lbl_Cyan.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lbl_Magenta
            // 
            m_lbl_Magenta.Location = new System.Drawing.Point(435, 273);
            m_lbl_Magenta.Name = "m_lbl_Magenta";
            m_lbl_Magenta.Size = new System.Drawing.Size(30, 18);
            m_lbl_Magenta.TabIndex = 32;
            m_lbl_Magenta.Text = "M:";
            m_lbl_Magenta.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lbl_Yellow
            // 
            m_lbl_Yellow.Location = new System.Drawing.Point(435, 297);
            m_lbl_Yellow.Name = "m_lbl_Yellow";
            m_lbl_Yellow.Size = new System.Drawing.Size(30, 18);
            m_lbl_Yellow.TabIndex = 33;
            m_lbl_Yellow.Text = "Y:";
            m_lbl_Yellow.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lbl_K
            // 
            m_lbl_K.Location = new System.Drawing.Point(435, 321);
            m_lbl_K.Name = "m_lbl_K";
            m_lbl_K.Size = new System.Drawing.Size(30, 18);
            m_lbl_K.TabIndex = 34;
            m_lbl_K.Text = "K:";
            m_lbl_K.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lbl_Primary_Color
            // 
            m_lbl_Primary_Color.BackColor = System.Drawing.Color.White;
            m_lbl_Primary_Color.Location = new System.Drawing.Point(315, 47);
            m_lbl_Primary_Color.Name = "m_lbl_Primary_Color";
            m_lbl_Primary_Color.Size = new System.Drawing.Size(58, 33);
            m_lbl_Primary_Color.TabIndex = 36;
            m_lbl_Primary_Color.Click += new System.EventHandler(Lbl_Primary_Color_Click);
            // 
            // m_lbl_Secondary_Color
            // 
            m_lbl_Secondary_Color.BackColor = System.Drawing.Color.Silver;
            m_lbl_Secondary_Color.Location = new System.Drawing.Point(315, 80);
            m_lbl_Secondary_Color.Name = "m_lbl_Secondary_Color";
            m_lbl_Secondary_Color.Size = new System.Drawing.Size(58, 33);
            m_lbl_Secondary_Color.TabIndex = 37;
            m_lbl_Secondary_Color.Click += new System.EventHandler(Lbl_Secondary_Color_Click);
            // 
            // m_lbl_Hue_Symbol
            // 
            m_lbl_Hue_Symbol.AutoSize = true;
            m_lbl_Hue_Symbol.Location = new System.Drawing.Point(387, 166);
            m_lbl_Hue_Symbol.Name = "m_lbl_Hue_Symbol";
            m_lbl_Hue_Symbol.Size = new System.Drawing.Size(12, 13);
            m_lbl_Hue_Symbol.TabIndex = 40;
            m_lbl_Hue_Symbol.Text = "°";
            // 
            // m_lbl_Saturation_Symbol
            // 
            m_lbl_Saturation_Symbol.AutoSize = true;
            m_lbl_Saturation_Symbol.Location = new System.Drawing.Point(387, 191);
            m_lbl_Saturation_Symbol.Name = "m_lbl_Saturation_Symbol";
            m_lbl_Saturation_Symbol.Size = new System.Drawing.Size(18, 13);
            m_lbl_Saturation_Symbol.TabIndex = 41;
            m_lbl_Saturation_Symbol.Text = "%";
            // 
            // m_lbl_Black_Symbol
            // 
            m_lbl_Black_Symbol.AutoSize = true;
            m_lbl_Black_Symbol.Location = new System.Drawing.Point(387, 216);
            m_lbl_Black_Symbol.Name = "m_lbl_Black_Symbol";
            m_lbl_Black_Symbol.Size = new System.Drawing.Size(18, 13);
            m_lbl_Black_Symbol.TabIndex = 42;
            m_lbl_Black_Symbol.Text = "%";
            // 
            // m_lbl_Cyan_Symbol
            // 
            m_lbl_Cyan_Symbol.AutoSize = true;
            m_lbl_Cyan_Symbol.Location = new System.Drawing.Point(499, 246);
            m_lbl_Cyan_Symbol.Name = "m_lbl_Cyan_Symbol";
            m_lbl_Cyan_Symbol.Size = new System.Drawing.Size(18, 13);
            m_lbl_Cyan_Symbol.TabIndex = 43;
            m_lbl_Cyan_Symbol.Text = "%";
            // 
            // m_lbl_Magenta_Symbol
            // 
            m_lbl_Magenta_Symbol.AutoSize = true;
            m_lbl_Magenta_Symbol.Location = new System.Drawing.Point(499, 271);
            m_lbl_Magenta_Symbol.Name = "m_lbl_Magenta_Symbol";
            m_lbl_Magenta_Symbol.Size = new System.Drawing.Size(18, 13);
            m_lbl_Magenta_Symbol.TabIndex = 44;
            m_lbl_Magenta_Symbol.Text = "%";
            // 
            // m_lbl_Yellow_Symbol
            // 
            m_lbl_Yellow_Symbol.AutoSize = true;
            m_lbl_Yellow_Symbol.Location = new System.Drawing.Point(499, 296);
            m_lbl_Yellow_Symbol.Name = "m_lbl_Yellow_Symbol";
            m_lbl_Yellow_Symbol.Size = new System.Drawing.Size(18, 13);
            m_lbl_Yellow_Symbol.TabIndex = 45;
            m_lbl_Yellow_Symbol.Text = "%";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(499, 321);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(18, 13);
            label1.TabIndex = 45;
            label1.Text = "%";
            // 
            // label2
            // 
            label2.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            label2.Location = new System.Drawing.Point(313, 29);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(63, 13);
            label2.TabIndex = 46;
            label2.Text = "new";
            label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            label3.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            label3.Location = new System.Drawing.Point(313, 117);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(63, 13);
            label3.TabIndex = 46;
            label3.Text = "current";
            label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // m_ctrl_BigBox
            // 
            m_ctrl_BigBox.BaseColorComponent = OpenPainter.ColorPicker.ColorComponent.Hue;
            hsb1.B = 1D;
            hsb1.H = 0D;
            hsb1.S = 1D;
            m_ctrl_BigBox.HSB = hsb1;
            m_ctrl_BigBox.Location = new System.Drawing.Point(8, 32);
            m_ctrl_BigBox.Name = "m_ctrl_BigBox";
            m_ctrl_BigBox.RGB = System.Drawing.Color.FromArgb(255, 0, 0);
            m_ctrl_BigBox.Size = new System.Drawing.Size(260, 260);
            m_ctrl_BigBox.TabIndex = 39;
            m_ctrl_BigBox.WebSafeColorsOnly = false;
            m_ctrl_BigBox.SelectionChanged += new System.EventHandler(Ctrl_BigBox_SelectionChanged);
            // 
            // m_ctrl_ThinBox
            // 
            m_ctrl_ThinBox.BaseColorComponent = OpenPainter.ColorPicker.ColorComponent.Hue;
            hsb2.B = 1D;
            hsb2.H = 0D;
            hsb2.S = 1D;
            m_ctrl_ThinBox.HSB = hsb2;
            m_ctrl_ThinBox.Location = new System.Drawing.Point(269, 30);
            m_ctrl_ThinBox.Name = "m_ctrl_ThinBox";
            m_ctrl_ThinBox.RGB = System.Drawing.Color.Red;
            m_ctrl_ThinBox.Size = new System.Drawing.Size(39, 264);
            m_ctrl_ThinBox.TabIndex = 38;
            m_ctrl_ThinBox.WebSafeColorsOnly = false;
            m_ctrl_ThinBox.SelectionChanged += new System.EventHandler(Ctrl_ThinBox_SelectionChanged);
            // 
            // FrmColorPicker
            // 
            AcceptButton = m_cmd_OK;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            CancelButton = m_cmd_Cancel;
            ClientSize = new System.Drawing.Size(539, 357);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(m_txt_Hex);
            Controls.Add(m_ctrl_BigBox);
            Controls.Add(m_ctrl_ThinBox);
            Controls.Add(m_lbl_Secondary_Color);
            Controls.Add(m_lbl_Primary_Color);
            Controls.Add(m_lbl_K);
            Controls.Add(m_lbl_Yellow);
            Controls.Add(m_lbl_Magenta);
            Controls.Add(m_lbl_Cyan);
            Controls.Add(m_rbtn_b);
            Controls.Add(m_rbtn_a);
            Controls.Add(m_rbtn_L);
            Controls.Add(m_lbl_HexPound);
            Controls.Add(chkWebColorsOnly);
            Controls.Add(m_rbtn_Blue);
            Controls.Add(m_rbtn_Green);
            Controls.Add(m_rbtn_Red);
            Controls.Add(m_rbtn_Black);
            Controls.Add(m_rbtn_Sat);
            Controls.Add(m_rbtn_Hue);
            Controls.Add(m_txt_K);
            Controls.Add(m_txt_Yellow);
            Controls.Add(m_txt_Magenta);
            Controls.Add(m_txt_Cyan);
            Controls.Add(m_txt_b);
            Controls.Add(m_txt_a);
            Controls.Add(m_txt_Lum);
            Controls.Add(m_txt_Blue);
            Controls.Add(m_txt_Green);
            Controls.Add(m_txt_Red);
            Controls.Add(m_txt_Black);
            Controls.Add(m_txt_Sat);
            Controls.Add(m_txt_Hue);
            Controls.Add(m_cmd_Cancel);
            Controls.Add(m_cmd_OK);
            Controls.Add(m_pbx_BlankBox);
            Controls.Add(m_lbl_Black_Symbol);
            Controls.Add(m_lbl_Saturation_Symbol);
            Controls.Add(m_lbl_Hue_Symbol);
            Controls.Add(label1);
            Controls.Add(m_lbl_Yellow_Symbol);
            Controls.Add(m_lbl_Magenta_Symbol);
            Controls.Add(m_lbl_Cyan_Symbol);
            Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmColorPicker";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Color Picker";
            Load += new System.EventHandler(FrmColorPicker_Load);
            ((System.ComponentModel.ISupportInitialize)(m_pbx_BlankBox)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }


        #endregion

        #region Events Handlers

        #region General Events

        private void FrmColorPicker_Load(object sender, EventArgs e)
        {

        }


        private void Cmd_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }


        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        #endregion

        #region Primary Picture Box (m_ctrl_BigBox)

        private void Ctrl_BigBox_SelectionChanged(object sender, EventArgs e)
        {
            _hsl = m_ctrl_BigBox.HSB;
            _rgb = AdobeColors.HSB_to_RGB(_hsl);
            _cmyk = AdobeColors.RGB_to_CMYK(_rgb);

            UpdateTextBoxes();

            m_ctrl_ThinBox.HSB = _hsl;

            m_lbl_Primary_Color.BackColor = _rgb;
            m_lbl_Primary_Color.Update();
        }

        #endregion

        #region Secondary Picture Box (m_ctrl_ThinBox)

        private void Ctrl_ThinBox_SelectionChanged(object sender, System.EventArgs e)
        {
            _hsl = m_ctrl_ThinBox.HSB;
            _rgb = AdobeColors.HSB_to_RGB(_hsl);
            _cmyk = AdobeColors.RGB_to_CMYK(_rgb);

            UpdateTextBoxes();

            m_ctrl_BigBox.HSB = _hsl;

            m_lbl_Primary_Color.BackColor = _rgb;
            m_lbl_Primary_Color.Update();
        }

        #endregion

        #region Hex Box (m_txt_Hex)

        private void Txt_Hex_Leave(object sender, System.EventArgs e)
        {
            string text = m_txt_Hex.Text.ToUpper();
            bool has_illegal_chars = false;

            if (text.Length <= 0)
                has_illegal_chars = true;
            foreach (char letter in text)
            {
                if (!char.IsNumber(letter))
                {
                    if (letter >= 'A' && letter <= 'F')
                        continue;
                    has_illegal_chars = true;
                    break;
                }
            }

            if (has_illegal_chars)
            {
                MessageBox.Show("Hex must be a hex value between 0x000000 and 0xFFFFFF");
                WriteHexData(_rgb);
                return;
            }

            _rgb = ParseHexData(text);
            _hsl = AdobeColors.RGB_to_HSB(_rgb);
            _cmyk = AdobeColors.RGB_to_CMYK(_rgb);

            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;
            m_lbl_Primary_Color.BackColor = _rgb;

            UpdateTextBoxes();
        }


        #endregion

        #region Color Boxes

        private void Lbl_Primary_Color_Click(object sender, EventArgs e)
        {
            _rgb = m_lbl_Primary_Color.BackColor;
            _hsl = AdobeColors.RGB_to_HSB(_rgb);

            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;

            _cmyk = AdobeColors.RGB_to_CMYK(_rgb);

            UpdateTextBoxes();
        }

        private void Lbl_Secondary_Color_Click(object sender, EventArgs e)
        {
            _rgb = m_lbl_Secondary_Color.BackColor;
            _hsl = AdobeColors.RGB_to_HSB(_rgb);

            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;

            m_lbl_Primary_Color.BackColor = _rgb;
            m_lbl_Primary_Color.Update();

            _cmyk = AdobeColors.RGB_to_CMYK(_rgb);

            UpdateTextBoxes();
        }

        #endregion

        #region Radio Buttons

        private void Rbtn_Hue_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rbtn_Hue.Checked)
            {
                m_ctrl_ThinBox.BaseColorComponent = ColorComponent.Hue;
                m_ctrl_BigBox.BaseColorComponent = ColorComponent.Hue;
            }
        }

        private void Rbtn_Sat_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rbtn_Sat.Checked)
            {
                m_ctrl_ThinBox.BaseColorComponent = ColorComponent.Saturation;
                m_ctrl_BigBox.BaseColorComponent = ColorComponent.Saturation;
            }
        }

        private void Rbtn_Black_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rbtn_Black.Checked)
            {
                m_ctrl_ThinBox.BaseColorComponent = ColorComponent.Brightness;
                m_ctrl_BigBox.BaseColorComponent = ColorComponent.Brightness;
            }
        }

        private void Rbtn_Red_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rbtn_Red.Checked)
            {
                m_ctrl_ThinBox.BaseColorComponent = ColorComponent.Red;
                m_ctrl_BigBox.BaseColorComponent = ColorComponent.Red;
            }
        }

        private void Rbtn_Green_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_rbtn_Green.Checked)
            {
                m_ctrl_ThinBox.BaseColorComponent = ColorComponent.Green;
                m_ctrl_BigBox.BaseColorComponent = ColorComponent.Green;
            }
        }

        private void Rbtn_Blue_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_rbtn_Blue.Checked)
            {
                m_ctrl_ThinBox.BaseColorComponent = ColorComponent.Blue;
                m_ctrl_BigBox.BaseColorComponent = ColorComponent.Blue;
            }
        }

        #endregion

        #region Text Boxes

        private void Txt_Hue_Leave(object sender, EventArgs e)
        {
            string text = m_txt_Hue.Text;
            bool has_illegal_chars = false;

            if (text.Length <= 0)
            {
                has_illegal_chars = true;
            }
            else
                foreach (char letter in text)
                {
                    if (!char.IsNumber(letter))
                    {
                        has_illegal_chars = true;
                        break;
                    }
                }

            if (has_illegal_chars)
            {
                MessageBox.Show("Hue must be a number value between 0 and 360");
                UpdateTextBoxes();
                return;
            }

            int hue = int.Parse(text);

            if (hue < 0)
            {
                MessageBox.Show("An integer between 0 and 360 is required.\nClosest value inserted.");
                m_txt_Hue.Text = "0";
                _hsl.H = 0.0;
            }
            else if (hue > 360)
            {
                MessageBox.Show("An integer between 0 and 360 is required.\nClosest value inserted.");
                m_txt_Hue.Text = "360";
                _hsl.H = 1.0;
            }
            else
            {
                _hsl.H = (double)hue / 360;
            }

            _rgb = AdobeColors.HSB_to_RGB(_hsl);
            _cmyk = AdobeColors.RGB_to_CMYK(_rgb);
            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;
            m_lbl_Primary_Color.BackColor = _rgb;

            UpdateTextBoxes();
        }

        private void Txt_Sat_Leave(object sender, System.EventArgs e)
        {
            string text = m_txt_Sat.Text;
            bool has_illegal_chars = false;

            if (text.Length <= 0)
                has_illegal_chars = true;
            else
                foreach (char letter in text)
                {
                    if (!char.IsNumber(letter))
                    {
                        has_illegal_chars = true;
                        break;
                    }
                }

            if (has_illegal_chars)
            {
                MessageBox.Show("Saturation must be a number value between 0 and 100");
                UpdateTextBoxes();
                return;
            }

            int sat = int.Parse(text);

            if (sat < 0)
            {
                MessageBox.Show("An integer between 0 and 100 is required.\nClosest value inserted.");
                m_txt_Sat.Text = "0";
                _hsl.S = 0.0;
            }
            else if (sat > 100)
            {
                MessageBox.Show("An integer between 0 and 100 is required.\nClosest value inserted.");
                m_txt_Sat.Text = "100";
                _hsl.S = 1.0;
            }
            else
            {
                _hsl.S = (double)sat / 100;
            }

            _rgb = AdobeColors.HSB_to_RGB(_hsl);
            _cmyk = AdobeColors.RGB_to_CMYK(_rgb);
            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;
            m_lbl_Primary_Color.BackColor = _rgb;

            UpdateTextBoxes();
        }

        private void Txt_Black_Leave(object sender, System.EventArgs e)
        {
            string text = m_txt_Black.Text;
            bool has_illegal_chars = false;

            if (text.Length <= 0)
                has_illegal_chars = true;
            else
                foreach (char letter in text)
                {
                    if (!char.IsNumber(letter))
                    {
                        has_illegal_chars = true;
                        break;
                    }
                }

            if (has_illegal_chars)
            {
                MessageBox.Show("Brightness must be a number value between 0 and 100.");
                UpdateTextBoxes();
                return;
            }

            int lum = int.Parse(text);

            if (lum < 0)
            {
                MessageBox.Show("An integer between 0 and 100 is required.\nClosest value inserted.");
                m_txt_Black.Text = "0";
                _hsl.B = 0.0;
            }
            else if (lum > 100)
            {
                MessageBox.Show("An integer between 0 and 100 is required.\nClosest value inserted.");
                m_txt_Black.Text = "100";
                _hsl.B = 1.0;
            }
            else
            {
                _hsl.B = (double)lum / 100;
            }

            _rgb = AdobeColors.HSB_to_RGB(_hsl);
            _cmyk = AdobeColors.RGB_to_CMYK(_rgb);
            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;
            m_lbl_Primary_Color.BackColor = _rgb;

            UpdateTextBoxes();
        }

        private void Txt_Red_Leave(object sender, System.EventArgs e)
        {
            string text = m_txt_Red.Text;
            bool has_illegal_chars = false;

            if (text.Length <= 0)
                has_illegal_chars = true;
            else
                foreach (char letter in text)
                {
                    if (!char.IsNumber(letter))
                    {
                        has_illegal_chars = true;
                        break;
                    }
                }

            if (has_illegal_chars)
            {
                MessageBox.Show("Red must be a number value between 0 and 255");
                UpdateTextBoxes();
                return;
            }

            int red = int.Parse(text);

            if (red < 0)
            {
                MessageBox.Show("An integer between 0 and 255 is required.\nClosest value inserted.");
                m_txt_Sat.Text = "0";
                _rgb = Color.FromArgb(0, _rgb.G, _rgb.B);
            }
            else if (red > 255)
            {
                MessageBox.Show("An integer between 0 and 255 is required.\nClosest value inserted.");
                m_txt_Sat.Text = "255";
                _rgb = Color.FromArgb(255, _rgb.G, _rgb.B);
            }
            else
            {
                _rgb = Color.FromArgb(red, _rgb.G, _rgb.B);
            }

            _hsl = AdobeColors.RGB_to_HSB(_rgb);
            _cmyk = AdobeColors.RGB_to_CMYK(_rgb);
            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;
            m_lbl_Primary_Color.BackColor = _rgb;

            UpdateTextBoxes();
        }

        private void Txt_Green_Leave(object sender, System.EventArgs e)
        {
            string text = m_txt_Green.Text;
            bool has_illegal_chars = false;

            if (text.Length <= 0)
                has_illegal_chars = true;
            else
                foreach (char letter in text)
                {
                    if (!char.IsNumber(letter))
                    {
                        has_illegal_chars = true;
                        break;
                    }
                }

            if (has_illegal_chars)
            {
                MessageBox.Show("Green must be a number value between 0 and 255");
                UpdateTextBoxes();
                return;
            }

            int green = int.Parse(text);

            if (green < 0)
            {
                MessageBox.Show("An integer between 0 and 255 is required.\nClosest value inserted.");
                m_txt_Green.Text = "0";
                _rgb = Color.FromArgb(_rgb.R, 0, _rgb.B);
            }
            else if (green > 255)
            {
                MessageBox.Show("An integer between 0 and 255 is required.\nClosest value inserted.");
                m_txt_Green.Text = "255";
                _rgb = Color.FromArgb(_rgb.R, 255, _rgb.B);
            }
            else
            {
                _rgb = Color.FromArgb(_rgb.R, green, _rgb.B);
            }

            _hsl = AdobeColors.RGB_to_HSB(_rgb);
            _cmyk = AdobeColors.RGB_to_CMYK(_rgb);
            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;
            m_lbl_Primary_Color.BackColor = _rgb;

            UpdateTextBoxes();
        }

        private void Txt_Blue_Leave(object sender, EventArgs e)
        {
            string text = m_txt_Blue.Text;
            bool has_illegal_chars = false;

            if (text.Length <= 0)
                has_illegal_chars = true;
            else
                foreach (char letter in text)
                {
                    if (!char.IsNumber(letter))
                    {
                        has_illegal_chars = true;
                        break;
                    }
                }

            if (has_illegal_chars)
            {
                MessageBox.Show("Blue must be a number value between 0 and 255");
                UpdateTextBoxes();
                return;
            }

            int blue = int.Parse(text);

            if (blue < 0)
            {
                MessageBox.Show("An integer between 0 and 255 is required.\nClosest value inserted.");
                m_txt_Blue.Text = "0";
                _rgb = Color.FromArgb(_rgb.R, _rgb.G, 0);
            }
            else if (blue > 255)
            {
                MessageBox.Show("An integer between 0 and 255 is required.\nClosest value inserted.");
                m_txt_Blue.Text = "255";
                _rgb = Color.FromArgb(_rgb.R, _rgb.G, 255);
            }
            else
            {
                _rgb = Color.FromArgb(_rgb.R, _rgb.G, blue);
            }

            _hsl = AdobeColors.RGB_to_HSB(_rgb);
            _cmyk = AdobeColors.RGB_to_CMYK(_rgb);
            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;
            m_lbl_Primary_Color.BackColor = _rgb;

            UpdateTextBoxes();
        }

        private void Txt_Cyan_Leave(object sender, System.EventArgs e)
        {
            string text = m_txt_Cyan.Text;
            bool has_illegal_chars = false;

            if (text.Length <= 0)
                has_illegal_chars = true;
            else
                foreach (char letter in text)
                {
                    if (!char.IsNumber(letter))
                    {
                        has_illegal_chars = true;
                        break;
                    }
                }

            if (has_illegal_chars)
            {
                MessageBox.Show("Cyan must be a number value between 0 and 100");
                UpdateTextBoxes();
                return;
            }

            int cyan = int.Parse(text);

            if (cyan < 0)
            {
                MessageBox.Show("An integer between 0 and 100 is required.\nClosest value inserted.");
                _cmyk.C = 0.0;
            }
            else if (cyan > 100)
            {
                MessageBox.Show("An integer between 0 and 100 is required.\nClosest value inserted.");
                _cmyk.C = 1.0;
            }
            else
            {
                _cmyk.C = (double)cyan / 100;
            }

            _rgb = AdobeColors.CmykToRgb(_cmyk);
            _hsl = AdobeColors.RGB_to_HSB(_rgb);
            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;
            m_lbl_Primary_Color.BackColor = _rgb;

            UpdateTextBoxes();
        }

        private void Txt_Magenta_Leave(object sender, System.EventArgs e)
        {
            string text = m_txt_Magenta.Text;
            bool has_illegal_chars = false;

            if (text.Length <= 0)
                has_illegal_chars = true;
            else
                foreach (char letter in text)
                {
                    if (!char.IsNumber(letter))
                    {
                        has_illegal_chars = true;
                        break;
                    }
                }

            if (has_illegal_chars)
            {
                MessageBox.Show("Magenta must be a number value between 0 and 100");
                UpdateTextBoxes();
                return;
            }

            int magenta = int.Parse(text);

            if (magenta < 0)
            {
                MessageBox.Show("An integer between 0 and 100 is required.\nClosest value inserted.");
                m_txt_Magenta.Text = "0";
                _cmyk.M = 0.0;
            }
            else if (magenta > 100)
            {
                MessageBox.Show("An integer between 0 and 100 is required.\nClosest value inserted.");
                m_txt_Magenta.Text = "100";
                _cmyk.M = 1.0;
            }
            else
            {
                _cmyk.M = (double)magenta / 100;
            }

            _rgb = AdobeColors.CmykToRgb(_cmyk);
            _hsl = AdobeColors.RGB_to_HSB(_rgb);
            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;
            m_lbl_Primary_Color.BackColor = _rgb;

            UpdateTextBoxes();
        }

        private void Txt_Yellow_Leave(object sender, System.EventArgs e)
        {
            string text = m_txt_Yellow.Text;
            bool has_illegal_chars = false;

            if (text.Length <= 0)
                has_illegal_chars = true;
            else
                foreach (char letter in text)
                {
                    if (!char.IsNumber(letter))
                    {
                        has_illegal_chars = true;
                        break;
                    }
                }

            if (has_illegal_chars)
            {
                MessageBox.Show("Yellow must be a number value between 0 and 100");
                UpdateTextBoxes();
                return;
            }

            int yellow = int.Parse(text);

            if (yellow < 0)
            {
                MessageBox.Show("An integer between 0 and 100 is required.\nClosest value inserted.");
                m_txt_Yellow.Text = "0";
                _cmyk.Y = 0.0;
            }
            else if (yellow > 100)
            {
                MessageBox.Show("An integer between 0 and 100 is required.\nClosest value inserted.");
                m_txt_Yellow.Text = "100";
                _cmyk.Y = 1.0;
            }
            else
            {
                _cmyk.Y = (double)yellow / 100;
            }

            _rgb = AdobeColors.CmykToRgb(_cmyk);
            _hsl = AdobeColors.RGB_to_HSB(_rgb);
            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;
            m_lbl_Primary_Color.BackColor = _rgb;

            UpdateTextBoxes();
        }

        private void Txt_K_Leave(object sender, System.EventArgs e)
        {
            string text = m_txt_K.Text;
            bool has_illegal_chars = false;

            if (text.Length <= 0)
                has_illegal_chars = true;
            else
                foreach (char letter in text)
                {
                    if (!char.IsNumber(letter))
                    {
                        has_illegal_chars = true;
                        break;
                    }
                }

            if (has_illegal_chars)
            {
                MessageBox.Show("Key must be a number value between 0 and 100");
                UpdateTextBoxes();
                return;
            }

            int key = int.Parse(text);

            if (key < 0)
            {
                MessageBox.Show("An integer between 0 and 100 is required.\nClosest value inserted.");
                m_txt_K.Text = "0";
                _cmyk.K = 0.0;
            }
            else if (key > 100)
            {
                MessageBox.Show("An integer between 0 and 100 is required.\nClosest value inserted.");
                m_txt_K.Text = "100";
                _cmyk.K = 1.0;
            }
            else
            {
                _cmyk.K = (double)key / 100;
            }

            _rgb = AdobeColors.CmykToRgb(_cmyk);
            _hsl = AdobeColors.RGB_to_HSB(_rgb);
            m_ctrl_BigBox.HSB = _hsl;
            m_ctrl_ThinBox.HSB = _hsl;
            m_lbl_Primary_Color.BackColor = _rgb;

            UpdateTextBoxes();
        }

        #endregion

        private void ChkWebColorsOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWebColorsOnly.Checked)
            {
                _rgb = AdobeColors.GetNearestWebSafeColor(_rgb);
                _hsl = AdobeColors.RGB_to_HSB(_rgb);
                _cmyk = AdobeColors.RGB_to_CMYK(_rgb);

                m_ctrl_BigBox.HSB = _hsl;
                m_ctrl_ThinBox.HSB = _hsl;
                m_lbl_Primary_Color.BackColor = _rgb;

                UpdateTextBoxes();
            }

            m_ctrl_BigBox.WebSafeColorsOnly = chkWebColorsOnly.Checked;
            m_ctrl_ThinBox.WebSafeColorsOnly = chkWebColorsOnly.Checked;
        }

        #endregion

        #region Private Functions

        private void WriteHexData(Color rgb)
        {
            string red = Convert.ToString(rgb.R, 16);
            if (red.Length < 2) red = "0" + red;
            string green = Convert.ToString(rgb.G, 16);
            if (green.Length < 2) green = "0" + green;
            string blue = Convert.ToString(rgb.B, 16);
            if (blue.Length < 2) blue = "0" + blue;

            m_txt_Hex.Text = red.ToUpper() + green.ToUpper() + blue.ToUpper();
            m_txt_Hex.Update();
        }

        private Color ParseHexData(string hex_data)
        {
            hex_data = "000000" + hex_data;
            hex_data = hex_data.Remove(0, hex_data.Length - 6);

            string r_text, g_text, b_text;
            int r, g, b;

            r_text = hex_data.Substring(0, 2);
            g_text = hex_data.Substring(2, 2);
            b_text = hex_data.Substring(4, 2);

            r = int.Parse(r_text, System.Globalization.NumberStyles.HexNumber);
            g = int.Parse(g_text, System.Globalization.NumberStyles.HexNumber);
            b = int.Parse(b_text, System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(r, g, b);
        }

        private void UpdateTextBoxes()
        {
            m_txt_Hue.Text = ((int)Math.Round(_hsl.H * 360)).ToString();
            m_txt_Sat.Text = ((int)Math.Round(_hsl.S * 100)).ToString();
            m_txt_Black.Text = ((int)Math.Round(_hsl.B * 100)).ToString();

            m_txt_Red.Text = _rgb.R.ToString();
            m_txt_Green.Text = _rgb.G.ToString();
            m_txt_Blue.Text = _rgb.B.ToString();

            m_txt_Cyan.Text = ((int)Math.Round(_cmyk.C * 100)).ToString();
            m_txt_Magenta.Text = ((int)Math.Round(_cmyk.M * 100)).ToString();
            m_txt_Yellow.Text = ((int)Math.Round(_cmyk.Y * 100)).ToString();
            m_txt_K.Text = ((int)Math.Round(_cmyk.K * 100)).ToString();

            m_txt_Hue.Update();
            m_txt_Sat.Update();
            m_txt_Black.Update();

            m_txt_Red.Update();
            m_txt_Green.Update();
            m_txt_Blue.Update();

            m_txt_Cyan.Update();
            m_txt_Magenta.Update();
            m_txt_Yellow.Update();
            m_txt_K.Update();

            WriteHexData(_rgb);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

    }
}
