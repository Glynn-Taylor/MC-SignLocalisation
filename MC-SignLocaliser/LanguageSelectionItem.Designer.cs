namespace MCGT_SignTranslator
{
    partial class LanguageSelectionItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.languageName = new System.Windows.Forms.Label();
            this.languageText = new System.Windows.Forms.Label();
            this.languageImage = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.languageImage)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.languageImage);
            this.flowLayoutPanel1.Controls.Add(this.languageName);
            this.flowLayoutPanel1.Controls.Add(this.languageText);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(348, 72);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClick);
            // 
            // languageName
            // 
            this.languageName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.languageName.AutoSize = true;
            this.languageName.Location = new System.Drawing.Point(73, 28);
            this.languageName.MaximumSize = new System.Drawing.Size(60, 0);
            this.languageName.MinimumSize = new System.Drawing.Size(120, 0);
            this.languageName.Name = "languageName";
            this.languageName.Size = new System.Drawing.Size(120, 0);
            this.languageName.TabIndex = 1;
            this.languageName.Text = "label1";
            // 
            // languageText
            // 
            this.languageText.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.languageText.AutoSize = true;
            this.languageText.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.languageText.Location = new System.Drawing.Point(199, 28);
            this.languageText.Name = "languageText";
            this.languageText.Size = new System.Drawing.Size(35, 13);
            this.languageText.TabIndex = 2;
            this.languageText.Text = "label2";
            this.languageText.Visible = false;
            // 
            // languageImage
            // 
            this.languageImage.Location = new System.Drawing.Point(0, 0);
            this.languageImage.Margin = new System.Windows.Forms.Padding(0);
            this.languageImage.Name = "languageImage";
            this.languageImage.Size = new System.Drawing.Size(70, 70);
            this.languageImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.languageImage.TabIndex = 0;
            this.languageImage.TabStop = false;
            // 
            // LanguageSelectionItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "LanguageSelectionItem";
            this.Size = new System.Drawing.Size(348, 72);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.languageImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox languageImage;
        private System.Windows.Forms.Label languageName;
        private System.Windows.Forms.Label languageText;
    }
}
