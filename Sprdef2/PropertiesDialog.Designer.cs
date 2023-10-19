namespace Sprdef2
{
    partial class PropertiesDialog
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
            this.chkMulticolor = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkExpandX = new System.Windows.Forms.CheckBox();
            this.chkExpandY = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSpriteName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chkMulticolor
            // 
            this.chkMulticolor.AutoSize = true;
            this.chkMulticolor.Location = new System.Drawing.Point(8, 36);
            this.chkMulticolor.Name = "chkMulticolor";
            this.chkMulticolor.Size = new System.Drawing.Size(71, 17);
            this.chkMulticolor.TabIndex = 2;
            this.chkMulticolor.Text = "Multicolor";
            this.chkMulticolor.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(140, 160);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(220, 160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkExpandX
            // 
            this.chkExpandX.AutoSize = true;
            this.chkExpandX.Location = new System.Drawing.Point(8, 60);
            this.chkExpandX.Name = "chkExpandX";
            this.chkExpandX.Size = new System.Drawing.Size(72, 17);
            this.chkExpandX.TabIndex = 3;
            this.chkExpandX.Text = "Expand X";
            this.chkExpandX.UseVisualStyleBackColor = true;
            // 
            // chkExpandY
            // 
            this.chkExpandY.AutoSize = true;
            this.chkExpandY.Location = new System.Drawing.Point(8, 84);
            this.chkExpandY.Name = "chkExpandY";
            this.chkExpandY.Size = new System.Drawing.Size(72, 17);
            this.chkExpandY.TabIndex = 4;
            this.chkExpandY.Text = "Expand Y";
            this.chkExpandY.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sprite name:";
            // 
            // txtSpriteName
            // 
            this.txtSpriteName.Location = new System.Drawing.Point(80, 8);
            this.txtSpriteName.MaxLength = 400;
            this.txtSpriteName.Name = "txtSpriteName";
            this.txtSpriteName.Size = new System.Drawing.Size(216, 20);
            this.txtSpriteName.TabIndex = 1;
            // 
            // PropertiesDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(304, 190);
            this.Controls.Add(this.txtSpriteName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkExpandY);
            this.Controls.Add(this.chkExpandX);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkMulticolor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertiesDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sprite properties";
            this.Load += new System.EventHandler(this.PropertiesDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkMulticolor;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkExpandX;
        private System.Windows.Forms.CheckBox chkExpandY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSpriteName;
    }
}