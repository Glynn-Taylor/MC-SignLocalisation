namespace MCGT_SignTranslator.GTaylor.Forms.Modal
{
    partial class ResourcePackDialog
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
            this.CancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ExternalPackButton = new System.Windows.Forms.Button();
            this.BundledPackButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.BackColor = System.Drawing.Color.Brown;
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.ForeColor = System.Drawing.SystemColors.Control;
            this.CancelButton.Location = new System.Drawing.Point(12, 125);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 0;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(58, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 84);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose external resource pack\r\nor\r\nuse pack bundled with map?\r\n(autocreates if no" +
    "n-existent)\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ExternalPackButton
            // 
            this.ExternalPackButton.BackColor = System.Drawing.Color.Gray;
            this.ExternalPackButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ExternalPackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExternalPackButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExternalPackButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ExternalPackButton.Location = new System.Drawing.Point(93, 125);
            this.ExternalPackButton.Name = "ExternalPackButton";
            this.ExternalPackButton.Size = new System.Drawing.Size(115, 23);
            this.ExternalPackButton.TabIndex = 2;
            this.ExternalPackButton.Text = "External Pack";
            this.ExternalPackButton.UseVisualStyleBackColor = false;
            this.ExternalPackButton.Click += new System.EventHandler(this.ExternalPackButton_Click);
            // 
            // BundledPackButton
            // 
            this.BundledPackButton.BackColor = System.Drawing.Color.Gray;
            this.BundledPackButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BundledPackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BundledPackButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BundledPackButton.Location = new System.Drawing.Point(218, 125);
            this.BundledPackButton.Name = "BundledPackButton";
            this.BundledPackButton.Size = new System.Drawing.Size(115, 23);
            this.BundledPackButton.TabIndex = 3;
            this.BundledPackButton.Text = "Bundled Pack";
            this.BundledPackButton.UseVisualStyleBackColor = false;
            this.BundledPackButton.Click += new System.EventHandler(this.BundledPackButton_Click);
            // 
            // ResourcePackDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CancelButton = this.CancelButton;
            this.ClientSize = new System.Drawing.Size(345, 160);
            this.Controls.Add(this.ExternalPackButton);
            this.Controls.Add(this.BundledPackButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResourcePackDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ResourcePackDialog";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ExternalPackButton;
        private System.Windows.Forms.Button BundledPackButton;
    }
}