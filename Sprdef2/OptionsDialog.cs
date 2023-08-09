using System;
using System.Windows.Forms;

namespace Sprdef2
{
    public partial class OptionsDialog : Form
    {
        public OptionsDialog()
        {
            InitializeComponent();
        }

        private void OptionsDialog_Load(object sender, EventArgs e)
        {
            chkDoubleSizePreview.Checked = MainWindow.PreviewZoom;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            MainWindow.PreviewZoom = chkDoubleSizePreview.Checked;
            DialogResult = DialogResult.OK;
        }
    }
}