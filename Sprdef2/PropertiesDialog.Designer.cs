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
            this.txtPreviewX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPreviewY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboBehaviourDuringAnimation = new System.Windows.Forms.ComboBox();
            this.btnEditorBackgroundColor = new System.Windows.Forms.Button();
            this.txtBytes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chkMulticolor
            // 
            this.chkMulticolor.AutoSize = true;
            this.chkMulticolor.Location = new System.Drawing.Point(8, 56);
            this.chkMulticolor.Name = "chkMulticolor";
            this.chkMulticolor.Size = new System.Drawing.Size(71, 17);
            this.chkMulticolor.TabIndex = 2;
            this.chkMulticolor.Text = "Multicolor";
            this.chkMulticolor.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(448, 356);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 13;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(528, 356);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkExpandX
            // 
            this.chkExpandX.AutoSize = true;
            this.chkExpandX.Location = new System.Drawing.Point(8, 84);
            this.chkExpandX.Name = "chkExpandX";
            this.chkExpandX.Size = new System.Drawing.Size(72, 17);
            this.chkExpandX.TabIndex = 3;
            this.chkExpandX.Text = "Expand X";
            this.chkExpandX.UseVisualStyleBackColor = true;
            // 
            // chkExpandY
            // 
            this.chkExpandY.AutoSize = true;
            this.chkExpandY.Location = new System.Drawing.Point(8, 112);
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
            this.txtSpriteName.Location = new System.Drawing.Point(8, 28);
            this.txtSpriteName.MaxLength = 400;
            this.txtSpriteName.Name = "txtSpriteName";
            this.txtSpriteName.Size = new System.Drawing.Size(288, 20);
            this.txtSpriteName.TabIndex = 1;
            this.txtSpriteName.Validated += new System.EventHandler(this.txtSpriteName_Validated);
            // 
            // txtPreviewX
            // 
            this.txtPreviewX.Location = new System.Drawing.Point(80, 136);
            this.txtPreviewX.MaxLength = 4;
            this.txtPreviewX.Name = "txtPreviewX";
            this.txtPreviewX.Size = new System.Drawing.Size(72, 20);
            this.txtPreviewX.TabIndex = 6;
            this.txtPreviewX.Validated += new System.EventHandler(this.txtPreviewX_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Preview X:";
            // 
            // txtPreviewY
            // 
            this.txtPreviewY.Location = new System.Drawing.Point(80, 164);
            this.txtPreviewY.MaxLength = 4;
            this.txtPreviewY.Name = "txtPreviewY";
            this.txtPreviewY.Size = new System.Drawing.Size(72, 20);
            this.txtPreviewY.TabIndex = 8;
            this.txtPreviewY.Validated += new System.EventHandler(this.txtPreviewY_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Preview Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 316);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Behaviour during animation:";
            // 
            // cboBehaviourDuringAnimation
            // 
            this.cboBehaviourDuringAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBehaviourDuringAnimation.FormattingEnabled = true;
            this.cboBehaviourDuringAnimation.Location = new System.Drawing.Point(8, 332);
            this.cboBehaviourDuringAnimation.Name = "cboBehaviourDuringAnimation";
            this.cboBehaviourDuringAnimation.Size = new System.Drawing.Size(288, 21);
            this.cboBehaviourDuringAnimation.TabIndex = 11;
            // 
            // btnEditorBackgroundColor
            // 
            this.btnEditorBackgroundColor.Location = new System.Drawing.Point(4, 196);
            this.btnEditorBackgroundColor.Name = "btnEditorBackgroundColor";
            this.btnEditorBackgroundColor.Size = new System.Drawing.Size(148, 23);
            this.btnEditorBackgroundColor.TabIndex = 9;
            this.btnEditorBackgroundColor.Text = "Editor background color...";
            this.btnEditorBackgroundColor.UseVisualStyleBackColor = true;
            this.btnEditorBackgroundColor.Click += new System.EventHandler(this.btnEditorBackgroundColor_Click);
            // 
            // txtBytes
            // 
            this.txtBytes.Location = new System.Drawing.Point(300, 8);
            this.txtBytes.Multiline = true;
            this.txtBytes.Name = "txtBytes";
            this.txtBytes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBytes.Size = new System.Drawing.Size(304, 344);
            this.txtBytes.TabIndex = 12;
            this.txtBytes.WordWrap = false;
            // 
            // PropertiesDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(609, 386);
            this.Controls.Add(this.txtBytes);
            this.Controls.Add(this.btnEditorBackgroundColor);
            this.Controls.Add(this.cboBehaviourDuringAnimation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPreviewY);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPreviewX);
            this.Controls.Add(this.label2);
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
        private System.Windows.Forms.TextBox txtPreviewX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPreviewY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboBehaviourDuringAnimation;
        private System.Windows.Forms.Button btnEditorBackgroundColor;
        private System.Windows.Forms.TextBox txtBytes;
    }
}