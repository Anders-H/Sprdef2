namespace Sprdef2
{
    partial class CbmPrgStudioDataDialog
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbmOk = new System.Windows.Forms.Button();
            this.cbmCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(400, 196);
            this.textBox1.TabIndex = 0;
            // 
            // cbmOk
            // 
            this.cbmOk.Location = new System.Drawing.Point(248, 204);
            this.cbmOk.Name = "cbmOk";
            this.cbmOk.Size = new System.Drawing.Size(75, 23);
            this.cbmOk.TabIndex = 1;
            this.cbmOk.Text = "OK";
            this.cbmOk.UseVisualStyleBackColor = true;
            this.cbmOk.Click += new System.EventHandler(this.cbmOk_Click);
            // 
            // cbmCancel
            // 
            this.cbmCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cbmCancel.Location = new System.Drawing.Point(328, 204);
            this.cbmCancel.Name = "cbmCancel";
            this.cbmCancel.Size = new System.Drawing.Size(75, 23);
            this.cbmCancel.TabIndex = 2;
            this.cbmCancel.Text = "Cancel";
            this.cbmCancel.UseVisualStyleBackColor = true;
            // 
            // CbmPrgStudioDataDialog
            // 
            this.AcceptButton = this.cbmOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cbmCancel;
            this.ClientSize = new System.Drawing.Size(408, 231);
            this.Controls.Add(this.cbmCancel);
            this.Controls.Add(this.cbmOk);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CbmPrgStudioDataDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CBM Prg Studio assembler DATA statements";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button cbmOk;
        private System.Windows.Forms.Button cbmCancel;
    }
}