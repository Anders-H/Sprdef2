using System.Windows.Forms;

namespace Sprdef2.Export.ExportGui
{
    public partial class MemoryLocationDialog : Form
    {
        private static int LastPositionNumber { get; set; }

        static MemoryLocationDialog()
        {
            LastPositionNumber = 128;
        }

        public MemoryLocationDialog()
        {
            InitializeComponent();
        }

        private void MemoryLocationDialog_Load(object sender, System.EventArgs e)
        {
            txtPositionNumber.Text = LastPositionNumber.ToString();
        }
    }
}