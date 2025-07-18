using System.Globalization;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Sprdef2.MainWindowControllers;

public static class HelpController
{
    public static void Help(IWin32Window owner, string text)
    {
        const string url = @"https://github.com/Anders-H/Sprdef2";
        var prompt = $@"Sprdef2 version {MainWindow.ApplicationVersion.ToString("n1", CultureInfo.InvariantCulture)}

Do you want to visit {url}?";

        if (MessageBox.Show(owner, prompt, text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) !=
            DialogResult.Yes)
            return;

        try
        {
            System.Diagnostics.Process.Start(url);
        }
        catch
        {
            MessageBox.Show(owner, $@"Failed to open {url}.", text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void ReportAnIssue(IWin32Window owner, string text)
    {
        const string url = @"https://github.com/Anders-H/Sprdef2/issues";
        const string prompt = $@"Open URL {url}?";

        if (MessageBox.Show(owner, prompt, $@"Sprdef2 version {MainWindow.ApplicationVersion.ToString("n1", CultureInfo.InvariantCulture)}: Report an issue", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
            return;

        try
        {
            System.Diagnostics.Process.Start(url);
        }
        catch
        {
            MessageBox.Show(owner, $@"Failed to open {url}.", text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}