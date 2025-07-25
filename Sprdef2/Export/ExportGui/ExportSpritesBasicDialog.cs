﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EditStateSprite;
using Sprdef2.Export.ExportLogic;

namespace Sprdef2.Export.ExportGui;

public partial class ExportSpritesBasicDialog : Form
{
    public SpriteList? Sprites { get; set; }
    public List<SpriteRoot>? SelectedSprites { get; set; }
    public ExportFormat SelectedExportFormat { get; private set; }

    public ExportSpritesBasicDialog()
    {
        InitializeComponent();
    }

    private void ExportSpritesBasicDialog_Load(object sender, EventArgs e)
    {
        if (Sprites == null)
            throw new InvalidOperationException("Sprites property must be set before loading the dialog.");

        spritePickerControl1.SetSprites(Sprites);
        spritePickerControl2.SetSprites(Sprites);
        spritePickerControl3.SetSprites(Sprites);
        spritePickerControl4.SetSprites(Sprites);
        spritePickerControl5.SetSprites(Sprites);
        spritePickerControl6.SetSprites(Sprites);
        spritePickerControl7.SetSprites(Sprites);
        spritePickerControl8.SetSprites(Sprites);

        foreach (var i in new ExportFormatComboItemList())
        {
            cboExportFormat.Items.Add(i);
        }

        cboExportFormat.SelectedIndex = 0;
        SelectedExportFormat = ExportFormat.CommodoreBasic20;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        spritePickerControl1.StoreLocation();
        spritePickerControl2.StoreLocation();
        spritePickerControl3.StoreLocation();
        spritePickerControl4.StoreLocation();
        spritePickerControl5.StoreLocation();
        spritePickerControl6.StoreLocation();
        spritePickerControl7.StoreLocation();
        spritePickerControl8.StoreLocation();

        if (SelectedExportFormat == ExportFormat.CommodoreBasic20)
        {
            if (spritePickerControl1.Sprite == null
                && spritePickerControl2.Sprite == null
                && spritePickerControl3.Sprite == null
                && spritePickerControl4.Sprite == null
                && spritePickerControl5.Sprite == null
                && spritePickerControl6.Sprite == null
                && spritePickerControl7.Sprite == null
                && spritePickerControl8.Sprite == null)
            {
                MessageBox.Show(this, @"You have not selected any sprites to export.", Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (ColorConflict())
                MessageBox.Show(this, @"Some of the multi color sprites differ in the last two colors, and can not be displayed correctly at the same time.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            SelectedSprites = [];

            if (spritePickerControl1.Sprite != null)
                SelectedSprites.Add(spritePickerControl1.Sprite);

            if (spritePickerControl2.Sprite != null)
                SelectedSprites.Add(spritePickerControl2.Sprite);

            if (spritePickerControl3.Sprite != null)
                SelectedSprites.Add(spritePickerControl3.Sprite);

            if (spritePickerControl4.Sprite != null)
                SelectedSprites.Add(spritePickerControl4.Sprite);

            if (spritePickerControl5.Sprite != null)
                SelectedSprites.Add(spritePickerControl5.Sprite);

            if (spritePickerControl6.Sprite != null)
                SelectedSprites.Add(spritePickerControl6.Sprite);

            if (spritePickerControl7.Sprite != null)
                SelectedSprites.Add(spritePickerControl7.Sprite);

            if (spritePickerControl8.Sprite != null)
                SelectedSprites.Add(spritePickerControl8.Sprite);
        }

        DialogResult = DialogResult.OK;
    }

    private bool ColorConflict()
    {
        var multicolorSprites = new List<SpriteRoot>();

        if (spritePickerControl1.IsMulticolor && spritePickerControl1.Sprite != null)
            multicolorSprites.Add(spritePickerControl1.Sprite);

        if (spritePickerControl2.IsMulticolor && spritePickerControl2.Sprite != null)
            multicolorSprites.Add(spritePickerControl2.Sprite);

        if (spritePickerControl3.IsMulticolor && spritePickerControl3.Sprite != null)
            multicolorSprites.Add(spritePickerControl3.Sprite);

        if (spritePickerControl4.IsMulticolor && spritePickerControl4.Sprite != null)
            multicolorSprites.Add(spritePickerControl4.Sprite);

        if (spritePickerControl5.IsMulticolor && spritePickerControl5.Sprite != null)
            multicolorSprites.Add(spritePickerControl5.Sprite);

        if (spritePickerControl6.IsMulticolor && spritePickerControl6.Sprite != null)
            multicolorSprites.Add(spritePickerControl6.Sprite);

        if (spritePickerControl7.IsMulticolor && spritePickerControl7.Sprite != null)
            multicolorSprites.Add(spritePickerControl7.Sprite);

        if (spritePickerControl8.IsMulticolor && spritePickerControl8.Sprite != null)
            multicolorSprites.Add(spritePickerControl8.Sprite);

        if (multicolorSprites.Count < 2)
            return false;

        var color3 = multicolorSprites.First().SpriteColorPalette[2];
        var color4 = multicolorSprites.First().SpriteColorPalette[3];

        foreach (var multicolorSprite in multicolorSprites)
        {
            if (multicolorSprite.SpriteColorPalette[2] != color3)
                return true;

            if (multicolorSprite.SpriteColorPalette[3] != color4)
                return true;
        }

        return false;
    }

    private void cboExportFormat_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectedExportFormat = ((ExportFormatComboItem)cboExportFormat.SelectedItem).ExportFormat;

        switch (SelectedExportFormat)
        {
            case ExportFormat.CommodoreBasic20:
                spritePickerControl1.Enabled = true;
                spritePickerControl2.Enabled = true;
                spritePickerControl3.Enabled = true;
                spritePickerControl4.Enabled = true;
                spritePickerControl5.Enabled = true;
                spritePickerControl6.Enabled = true;
                spritePickerControl7.Enabled = true;
                spritePickerControl8.Enabled = true;
                Text = @"Export sprites to Commodore BASIC 2.0 (Commodore 64)";
                break;
            //case ExportFormat.DataStatements:
            //    spritePickerControl1.Enabled = false;
            //    spritePickerControl2.Enabled = false;
            //    spritePickerControl3.Enabled = false;
            //    spritePickerControl4.Enabled = false;
            //    spritePickerControl5.Enabled = false;
            //    spritePickerControl6.Enabled = false;
            //    spritePickerControl7.Enabled = false;
            //    spritePickerControl8.Enabled = false;
            //    Text = @"Export sprites to DATA statements (Commodore 64/128)";
            //    break;
            //case ExportFormat.DataOnlyPrg:
            //    spritePickerControl1.Enabled = false;
            //    spritePickerControl2.Enabled = false;
            //    spritePickerControl3.Enabled = false;
            //    spritePickerControl4.Enabled = false;
            //    spritePickerControl5.Enabled = false;
            //    spritePickerControl6.Enabled = false;
            //    spritePickerControl7.Enabled = false;
            //    spritePickerControl8.Enabled = false;
            //    Text = @"Export sprites as PRG file";
            //    break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}