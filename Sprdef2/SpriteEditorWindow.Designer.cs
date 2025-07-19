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
            this.btnProperties = new System.Windows.Forms.Button();
            this.colorPicker1 = new C64ColorControls.ColorPicker();
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
            // btnProperties
            // 
            this.btnProperties.Location = new System.Drawing.Point(272, 4);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(92, 25);
            this.btnProperties.TabIndex = 2;
            this.btnProperties.Text = "Properties...";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
            // 
            // colorPicker1
            // 
            this.colorPicker1.Location = new System.Drawing.Point(0, 4);
            this.colorPicker1.MultiColor = false;
            this.colorPicker1.Name = "colorPicker1";
            this.colorPicker1.Size = new System.Drawing.Size(268, 28);
            this.colorPicker1.TabIndex = 1;
            this.colorPicker1.SelectedColorChanged += new C64ColorControls.SelectedColorChangedDelegate(this.colorPicker1_SelectedColorChanged);
            this.colorPicker1.PaletteChanged += new C64ColorControls.PaletteChangedDelegate(this.colorPicker1_PaletteChanged);
            // 
            // SpriteEditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(475, 349);
            this.Controls.Add(this.btnProperties);
            this.Controls.Add(this.spriteEditorControl1);
            this.Controls.Add(this.colorPicker1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SpriteEditorWindow";
            this.Text = "Sprite";
            this.Activated += new System.EventHandler(this.SpriteEditorWindow_Activated);
            this.Load += new System.EventHandler(this.SpriteEditorWindow_Load);
            this.Shown += new System.EventHandler(this.SpriteEditorWindow_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SpriteEditorWindow_Paint);
            this.Enter += new System.EventHandler(this.SpriteEditorWindow_Enter);
            this.ResumeLayout(false);

        }

        #endregion

        private EditStateSprite.SpriteEditorControl spriteEditorControl1;
        private System.Windows.Forms.Button btnProperties;
        private C64ColorControls.ColorPicker colorPicker1;
    }
}