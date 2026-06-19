#nullable enable
using System;
using System.Windows.Forms;

namespace Sprdef2;

public class RecentFiles
{
    private string[] _filenames;

    public RecentFiles()
    {
        _filenames = new string[10];
    }

    public void UpdateGui(ToolStripSeparator separator, params ToolStripMenuItem[] i)
    {
        if (i.Length != 10)
            throw new System.ArgumentException("Exactly 10 menu items must be provided.");

        var visibleCount = 0;

        for (var j = 0; j < 10; j++)
        {
            var empty = string.IsNullOrWhiteSpace(_filenames[j]);

            if (empty)
            {
                i[j].Visible = false;
                i[j].Text = "";
                i[j].Tag = null;
            }
            else
            {
                visibleCount++;
                i[j].Text = _filenames[j];
                i[j].Tag = _filenames[j];
                i[j].Visible = true;
            }
        }

        separator.Visible = visibleCount > 0;
    }

    public void Push(string filename)
    {
        var existingIndex = Array.IndexOf(_filenames, filename);
        var shiftTo = existingIndex >= 0 ? existingIndex : _filenames.Length - 1;

        for (var i = shiftTo; i > 0; i--)
            _filenames[i] = _filenames[i - 1];

        _filenames[0] = filename;
    }
}