namespace Sprdef2.Export.ExportGui
{
    partial class SpritePickerControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboSprite = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.picDelete = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sprite:";
            // 
            // cboSprite
            // 
            this.cboSprite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSprite.FormattingEnabled = true;
            this.cboSprite.Location = new System.Drawing.Point(0, 16);
            this.cboSprite.Name = "cboSprite";
            this.cboSprite.Size = new System.Drawing.Size(256, 21);
            this.cboSprite.TabIndex = 1;
            this.cboSprite.SelectedIndexChanged += new System.EventHandler(this.cboSprite_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(288, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "X (0-511):";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(284, 16);
            this.txtX.MaxLength = 5;
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(68, 20);
            this.txtX.TabIndex = 3;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(356, 16);
            this.txtY.MaxLength = 5;
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(68, 20);
            this.txtY.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(360, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y (0-255):";
            // 
            // picDelete
            // 
            this.picDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDelete.Image = global::Sprdef2.Properties.Resources.DeleteRed;
            this.picDelete.Location = new System.Drawing.Point(262, 19);
            this.picDelete.Name = "picDelete";
            this.picDelete.Size = new System.Drawing.Size(16, 16);
            this.picDelete.TabIndex = 6;
            this.picDelete.TabStop = false;
            this.picDelete.Click += new System.EventHandler(this.picDelete_Click);
            // 
            // SpritePickerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picDelete);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboSprite);
            this.Controls.Add(this.label1);
            this.Name = "SpritePickerControl";
            this.Size = new System.Drawing.Size(429, 38);
            ((System.ComponentModel.ISupportInitialize)(this.picDelete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboSprite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picDelete;
    }
}
