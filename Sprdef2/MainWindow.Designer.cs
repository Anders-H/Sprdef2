﻿namespace Sprdef2
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lvSpriteList = new System.Windows.Forms.ListView();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.btnScrollUp = new System.Windows.Forms.ToolStripButton();
            this.btnScrollRight = new System.Windows.Forms.ToolStripButton();
            this.btnScrollDown = new System.Windows.Forms.ToolStripButton();
            this.btnScrollLeft = new System.Windows.Forms.ToolStripButton();
            this.scrollUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scrollRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scrollDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scrollLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flipLeftrightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flipTopdownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFlipLeftRight = new System.Windows.Forms.ToolStripButton();
            this.btnFlipTopDown = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.spriteToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(941, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(118, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.exitToolStripMenuItem.Text = "Quit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // spriteToolStripMenuItem
            // 
            this.spriteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSpriteToolStripMenuItem});
            this.spriteToolStripMenuItem.Name = "spriteToolStripMenuItem";
            this.spriteToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.spriteToolStripMenuItem.Text = "&Sprite";
            // 
            // addSpriteToolStripMenuItem
            // 
            this.addSpriteToolStripMenuItem.Name = "addSpriteToolStripMenuItem";
            this.addSpriteToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.addSpriteToolStripMenuItem.Text = "Add sprite...";
            this.addSpriteToolStripMenuItem.Click += new System.EventHandler(this.addSpriteToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scrollUpToolStripMenuItem,
            this.scrollRightToolStripMenuItem,
            this.scrollDownToolStripMenuItem,
            this.scrollLeftToolStripMenuItem,
            this.toolStripMenuItem2,
            this.propertiesToolStripMenuItem,
            this.toolStripMenuItem3,
            this.flipLeftrightToolStripMenuItem,
            this.flipTopdownToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.propertiesToolStripMenuItem.Text = "Properties...";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnScrollUp,
            this.btnScrollRight,
            this.btnScrollDown,
            this.btnScrollLeft,
            this.toolStripSeparator1,
            this.btnFlipLeftRight,
            this.btnFlipTopDown});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(941, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 592);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(941, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lvSpriteList
            // 
            this.lvSpriteList.Dock = System.Windows.Forms.DockStyle.Left;
            this.lvSpriteList.FullRowSelect = true;
            this.lvSpriteList.HideSelection = false;
            this.lvSpriteList.Location = new System.Drawing.Point(0, 49);
            this.lvSpriteList.MultiSelect = false;
            this.lvSpriteList.Name = "lvSpriteList";
            this.lvSpriteList.Size = new System.Drawing.Size(121, 543);
            this.lvSpriteList.TabIndex = 5;
            this.lvSpriteList.UseCompatibleStateImageBehavior = false;
            this.lvSpriteList.View = System.Windows.Forms.View.List;
            this.lvSpriteList.SelectedIndexChanged += new System.EventHandler(this.lvSpriteList_SelectedIndexChanged);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 6);
            // 
            // picPreview
            // 
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Right;
            this.picPreview.Location = new System.Drawing.Point(688, 49);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(253, 543);
            this.picPreview.TabIndex = 6;
            this.picPreview.TabStop = false;
            // 
            // btnScrollUp
            // 
            this.btnScrollUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnScrollUp.Image = global::Sprdef2.Properties.Resources._112_UpArrowLong_Blue_16x16_72;
            this.btnScrollUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnScrollUp.Name = "btnScrollUp";
            this.btnScrollUp.Size = new System.Drawing.Size(23, 22);
            this.btnScrollUp.Text = "Scroll up";
            this.btnScrollUp.Click += new System.EventHandler(this.btnScrollUp_Click);
            // 
            // btnScrollRight
            // 
            this.btnScrollRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnScrollRight.Image = global::Sprdef2.Properties.Resources._112_RightArrowLong_Blue_16x16_72;
            this.btnScrollRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnScrollRight.Name = "btnScrollRight";
            this.btnScrollRight.Size = new System.Drawing.Size(23, 22);
            this.btnScrollRight.Text = "toolStripButton1";
            this.btnScrollRight.Click += new System.EventHandler(this.btnScrollRight_Click);
            // 
            // btnScrollDown
            // 
            this.btnScrollDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnScrollDown.Image = global::Sprdef2.Properties.Resources._112_DownArrowLong_Blue_16x16_72;
            this.btnScrollDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnScrollDown.Name = "btnScrollDown";
            this.btnScrollDown.Size = new System.Drawing.Size(23, 22);
            this.btnScrollDown.Text = "toolStripButton1";
            this.btnScrollDown.Click += new System.EventHandler(this.btnScrollDown_Click);
            // 
            // btnScrollLeft
            // 
            this.btnScrollLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnScrollLeft.Image = global::Sprdef2.Properties.Resources._112_LeftArrowLong_Blue_16x16_72;
            this.btnScrollLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnScrollLeft.Name = "btnScrollLeft";
            this.btnScrollLeft.Size = new System.Drawing.Size(23, 22);
            this.btnScrollLeft.Text = "toolStripButton1";
            this.btnScrollLeft.Click += new System.EventHandler(this.btnScrollLeft_Click);
            // 
            // scrollUpToolStripMenuItem
            // 
            this.scrollUpToolStripMenuItem.Image = global::Sprdef2.Properties.Resources._112_UpArrowLong_Blue_16x16_72;
            this.scrollUpToolStripMenuItem.Name = "scrollUpToolStripMenuItem";
            this.scrollUpToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.scrollUpToolStripMenuItem.Text = "Scroll up";
            this.scrollUpToolStripMenuItem.Click += new System.EventHandler(this.scrollUpToolStripMenuItem_Click);
            // 
            // scrollRightToolStripMenuItem
            // 
            this.scrollRightToolStripMenuItem.Image = global::Sprdef2.Properties.Resources._112_RightArrowLong_Blue_16x16_72;
            this.scrollRightToolStripMenuItem.Name = "scrollRightToolStripMenuItem";
            this.scrollRightToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.scrollRightToolStripMenuItem.Text = "Scroll right";
            this.scrollRightToolStripMenuItem.Click += new System.EventHandler(this.scrollRightToolStripMenuItem_Click);
            // 
            // scrollDownToolStripMenuItem
            // 
            this.scrollDownToolStripMenuItem.Image = global::Sprdef2.Properties.Resources._112_DownArrowLong_Blue_16x16_72;
            this.scrollDownToolStripMenuItem.Name = "scrollDownToolStripMenuItem";
            this.scrollDownToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.scrollDownToolStripMenuItem.Text = "Scroll down";
            this.scrollDownToolStripMenuItem.Click += new System.EventHandler(this.scrollDownToolStripMenuItem_Click);
            // 
            // scrollLeftToolStripMenuItem
            // 
            this.scrollLeftToolStripMenuItem.Image = global::Sprdef2.Properties.Resources._112_LeftArrowLong_Blue_16x16_72;
            this.scrollLeftToolStripMenuItem.Name = "scrollLeftToolStripMenuItem";
            this.scrollLeftToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.scrollLeftToolStripMenuItem.Text = "Scroll left";
            this.scrollLeftToolStripMenuItem.Click += new System.EventHandler(this.scrollLeftToolStripMenuItem_Click);
            // 
            // flipLeftrightToolStripMenuItem
            // 
            this.flipLeftrightToolStripMenuItem.Image = global::Sprdef2.Properties.Resources.FlipHorizontalHS;
            this.flipLeftrightToolStripMenuItem.Name = "flipLeftrightToolStripMenuItem";
            this.flipLeftrightToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.flipLeftrightToolStripMenuItem.Text = "Flip left-right";
            this.flipLeftrightToolStripMenuItem.Click += new System.EventHandler(this.flipLeftrightToolStripMenuItem_Click);
            // 
            // flipTopdownToolStripMenuItem
            // 
            this.flipTopdownToolStripMenuItem.Image = global::Sprdef2.Properties.Resources.FlipVerticalHS;
            this.flipTopdownToolStripMenuItem.Name = "flipTopdownToolStripMenuItem";
            this.flipTopdownToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.flipTopdownToolStripMenuItem.Text = "Flip top-down";
            this.flipTopdownToolStripMenuItem.Click += new System.EventHandler(this.flipTopdownToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFlipLeftRight
            // 
            this.btnFlipLeftRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFlipLeftRight.Image = global::Sprdef2.Properties.Resources.FlipHorizontalHS;
            this.btnFlipLeftRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFlipLeftRight.Name = "btnFlipLeftRight";
            this.btnFlipLeftRight.Size = new System.Drawing.Size(23, 22);
            this.btnFlipLeftRight.Text = "Flip left-right";
            this.btnFlipLeftRight.Click += new System.EventHandler(this.btnFlipLeftRight_Click);
            // 
            // btnFlipTopDown
            // 
            this.btnFlipTopDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFlipTopDown.Image = global::Sprdef2.Properties.Resources.FlipVerticalHS;
            this.btnFlipTopDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFlipTopDown.Name = "btnFlipTopDown";
            this.btnFlipTopDown.Size = new System.Drawing.Size(23, 22);
            this.btnFlipTopDown.Text = "Flip top-down";
            this.btnFlipTopDown.Click += new System.EventHandler(this.btnFlipTopDown_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 614);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.lvSpriteList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Sprdef 2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem spriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSpriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ListView lvSpriteList;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scrollUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnScrollUp;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem scrollRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scrollDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scrollLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnScrollRight;
        private System.Windows.Forms.ToolStripButton btnScrollDown;
        private System.Windows.Forms.ToolStripButton btnScrollLeft;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem flipLeftrightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flipTopdownToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFlipLeftRight;
        private System.Windows.Forms.ToolStripButton btnFlipTopDown;
    }
}

