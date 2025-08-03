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
            this.SuspendLayout();
            // 
            // spriteEditorControl1
            // 
            this.spriteEditorControl1.Location = new System.Drawing.Point(4, 4);
            this.spriteEditorControl1.Name = "spriteEditorControl1";
            this.spriteEditorControl1.Size = new System.Drawing.Size(359, 314);
            this.spriteEditorControl1.TabIndex = 4;
            this.spriteEditorControl1.Text = "spriteEditorControl1";
            this.spriteEditorControl1.Zoom = 15;
            this.spriteEditorControl1.SpriteChanged += new EditStateSprite.SpriteChangedDelegate(this.spriteEditorControl1_SpriteChanged);
            this.spriteEditorControl1.ZoomChanged += new EditStateSprite.ZoomChangedDelegate(this.spriteEditorControl1_ZoomChanged);
            // 
            // SpriteEditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(3, 3);
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(475, 349);
            this.Controls.Add(this.spriteEditorControl1);
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
    }
}