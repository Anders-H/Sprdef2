﻿#nullable enable
using System;
using System.Windows.Forms;

namespace Sprdef2;

public partial class OptionsDialog : Form
{
    public OptionsDialog()
    {
        InitializeComponent();
    }

    private void OptionsDialog_Load(object sender, EventArgs e)
    {
        cboMulticolor.Items.Add("No");
        cboMulticolor.Items.Add("Yes");
        cboMulticolor.Items.Add("Ask");
        cboMulticolor.SelectedIndex = MainWindow.NewSpriteIsMulticolor;
        chkDoubleSizePreview.Checked = MainWindow.PreviewZoom;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        MainWindow.NewSpriteIsMulticolor = cboMulticolor.SelectedIndex;
        MainWindow.PreviewZoom = chkDoubleSizePreview.Checked;
        DialogResult = DialogResult.OK;
    }
}