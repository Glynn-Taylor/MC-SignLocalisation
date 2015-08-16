namespace MCGT_SignTranslator.GTaylor.Forms.Modal
{
    partial class LanguageSelectDialog
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
            this.LanguageBox = new System.Windows.Forms.ComboBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LanguageBox
            // 
            this.LanguageBox.FormattingEnabled = true;
            this.LanguageBox.Items.AddRange(new object[] {
            "frikaans",
            "اللغة العربية",
            "Asturianu",
            "Български",
            "Català",
            "Čeština",
            "Cymraeg",
            "Dansk",
            "Deutsch",
            "Ελληνικά",
            "Australian English",
            "Canadian English",
            "English (UK)",
            "Pirate Speak (PIRATE)",
            "English (US)",
            "Esperanto (Mondo)",
            "Español (Ar)",
            "Español (Es)",
            "Español (Me)",
            "Español (Ur)",
            "Español (Ve)",
            "Eesti",
            "Euskara",
            "Suomi",
            "Français (Fr)",
            "Français (Ca)",
            "Gaeilge",
            "Galego",
            "עברית",
            "हिन्दी",
            "Hrvatski",
            "Magyar",
            "Հայերեն",
            "Bahasa Indonesia",
            "Íslenska",
            "Italiano",
            "日本語",
            "ქართული",
            "한국어",
            "Kernowek",
            "Lingua Latina",
            "Lëtzebuergesch",
            "Lietuvių",
            "Latviešu",
            "Bahasa Melayu",
            "Malti",
            "Nederlands",
            "Norsk Nynorsk",
            "Norsk",
            "Occitan",
            "Polski",
            "Português (Br)",
            "Portuguese (Po)",
            "Quenya",
            "Română",
            "Русский",
            "Slovenčina",
            "Slovenščina",
            "Српски",
            "Svenska",
            "ภาษาไทย",
            "tlhIngan Hol",
            "Türkçe",
            "Українська",
            "Valencià",
            "Tiếng Việt",
            "简体中文",
            "繁體中文"});
            this.LanguageBox.Location = new System.Drawing.Point(147, 63);
            this.LanguageBox.Name = "LanguageBox";
            this.LanguageBox.Size = new System.Drawing.Size(183, 21);
            this.LanguageBox.TabIndex = 0;
            // 
            // OkButton
            // 
            this.OkButton.BackColor = System.Drawing.Color.Gray;
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton.ForeColor = System.Drawing.SystemColors.Control;
            this.OkButton.Location = new System.Drawing.Point(263, 107);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(67, 23);
            this.OkButton.TabIndex = 4;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = false;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MCGT_SignTranslator.Properties.Resources.world1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 118);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // LanguageSelectDialog
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(342, 142);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.LanguageBox);
            this.Name = "LanguageSelectDialog";
            this.ShowIcon = false;
            this.Text = "LanguageSelectDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox LanguageBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}