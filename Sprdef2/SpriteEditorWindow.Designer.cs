namespace Sprdef2
{
    partial class SpriteEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteEditorWindow));
            this.spriteEditorControl1 = new EditStateSprite.SpriteEditorControl();
            this.optColor0 = new System.Windows.Forms.RadioButton();
            this.optColor1 = new System.Windows.Forms.RadioButton();
            this.optColor2 = new System.Windows.Forms.RadioButton();
            this.optColor3 = new System.Windows.Forms.RadioButton();
            this.btnProperties = new System.Windows.Forms.Button();
            this.btnPalette = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // spriteEditorControl1
            // 
            this.spriteEditorControl1.Location = new System.Drawing.Point(4, 32);
            this.spriteEditorControl1.Name = "spriteEditorControl1";
            this.spriteEditorControl1.Size = new System.Drawing.Size(359, 314);
            this.spriteEditorControl1.TabIndex = 0;
            this.spriteEditorControl1.Text = "spriteEditorControl1";
            this.spriteEditorControl1.SpriteChanged += new EditStateSprite.SpriteChangedDelegate(this.spriteEditorControl1_SpriteChanged);
            // 
            // optColor0
            // 
            this.optColor0.Appearance = System.Windows.Forms.Appearance.Button;
            this.optColor0.Checked = true;
            this.optColor0.Location = new System.Drawing.Point(4, 4);
            this.optColor0.Name = "optColor0";
            this.optColor0.Size = new System.Drawing.Size(44, 24);
            this.optColor0.TabIndex = 1;
            this.optColor0.TabStop = true;
            this.optColor0.Text = "0";
            this.optColor0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optColor0.UseVisualStyleBackColor = true;
            this.optColor0.CheckedChanged += new System.EventHandler(this.optColor0_CheckedChanged);
            // 
            // optColor1
            // 
            this.optColor1.Appearance = System.Windows.Forms.Appearance.Button;
            this.optColor1.Location = new System.Drawing.Point(52, 4);
            this.optColor1.Name = "optColor1";
            this.optColor1.Size = new System.Drawing.Size(44, 24);
            this.optColor1.TabIndex = 2;
            this.optColor1.Text = "1";
            this.optColor1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optColor1.UseVisualStyleBackColor = true;
            this.optColor1.CheckedChanged += new System.EventHandler(this.optColor1_CheckedChanged);
            // 
            // optColor2
            // 
            this.optColor2.Appearance = System.Windows.Forms.Appearance.Button;
            this.optColor2.Location = new System.Drawing.Point(100, 4);
            this.optColor2.Name = "optColor2";
            this.optColor2.Size = new System.Drawing.Size(44, 24);
            this.optColor2.TabIndex = 3;
            this.optColor2.Text = "2";
            this.optColor2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optColor2.UseVisualStyleBackColor = true;
            this.optColor2.CheckedChanged += new System.EventHandler(this.optColor2_CheckedChanged);
            // 
            // optColor3
            // 
            this.optColor3.Appearance = System.Windows.Forms.Appearance.Button;
            this.optColor3.Location = new System.Drawing.Point(148, 4);
            this.optColor3.Name = "optColor3";
            this.optColor3.Size = new System.Drawing.Size(44, 24);
            this.optColor3.TabIndex = 4;
            this.optColor3.Text = "3";
            this.optColor3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optColor3.UseVisualStyleBackColor = true;
            this.optColor3.CheckedChanged += new System.EventHandler(this.optColor3_CheckedChanged);
            // 
            // btnProperties
            // 
            this.btnProperties.Location = new System.Drawing.Point(196, 4);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(84, 23);
            this.btnProperties.TabIndex = 5;
            this.btnProperties.Text = "Properties...";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
            // 
            // btnPalette
            // 
            this.btnPalette.Location = new System.Drawing.Point(284, 4);
            this.btnPalette.Name = "btnPalette";
            this.btnPalette.Size = new System.Drawing.Size(80, 23);
            this.btnPalette.TabIndex = 6;
            this.btnPalette.Text = "Palette...";
            this.btnPalette.UseVisualStyleBackColor = true;
            this.btnPalette.Click += new System.EventHandler(this.btnPalette_Click);
            // 
            // SpriteEditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 349);
            this.Controls.Add(this.btnPalette);
            this.Controls.Add(this.btnProperties);
            this.Controls.Add(this.optColor3);
            this.Controls.Add(this.optColor2);
            this.Controls.Add(this.optColor1);
            this.Controls.Add(this.optColor0);
            this.Controls.Add(this.spriteEditorControl1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SpriteEditorWindow";
            this.Text = "Sprite";
            this.Activated += new System.EventHandler(this.SpriteEditorWindow_Activated);
            this.Load += new System.EventHandler(this.SpriteEditorWindow_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SpriteEditorWindow_Paint);
            this.Enter += new System.EventHandler(this.SpriteEditorWindow_Enter);
            this.ResumeLayout(false);

        }

        #endregion

        private EditStateSprite.SpriteEditorControl spriteEditorControl1;
        private System.Windows.Forms.RadioButton optColor0;
        private System.Windows.Forms.RadioButton optColor1;
        private System.Windows.Forms.RadioButton optColor2;
        private System.Windows.Forms.RadioButton optColor3;
        private System.Windows.Forms.Button btnProperties;
        private System.Windows.Forms.Button btnPalette;
    }
}