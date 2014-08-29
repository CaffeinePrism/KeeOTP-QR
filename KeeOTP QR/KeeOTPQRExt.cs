using KeePass.Plugins;
using KeePassLib;
using System;
using System.Windows.Forms;

namespace KeeOTPQR
{
    public sealed class KeeOTPQRExt : Plugin
    {
        private IPluginHost host = null;
        private ToolStripItem otpDialogToolStripItem;

        public override bool Initialize(IPluginHost host)
        {
            this.host = host;

            this.otpDialogToolStripItem = host.MainWindow.EntryContextMenu.Items.Add("QR",
                null,
                otpDialogToolStripItem_Click);

            return true;
        }
        void otpDialogToolStripItem_Click(object sender, EventArgs e)
        {
            PwEntry entry;
            if (GetSelectedSingleEntry(out entry))
            {
                ShowQR form = new ShowQR(entry, host);
                form.ShowDialog();
            }
        }
        private bool GetSelectedSingleEntry(out PwEntry entry)
        {
            entry = null;

            var entries = this.host.MainWindow.GetSelectedEntries();
            if (entries == null || entries.Length == 0)
            {
                MessageBox.Show("Please select an entry");
                return false;
            }
            else if (entries.Length > 1)
            {
                MessageBox.Show("Please select only one entry");
                return false;
            }
            else
            {
                // grab the entry that we care about
                entry = entries[0];
                return true;
            }
        }
        public override void Terminate()
        {
            // Remove all of our menu items
            ToolStripItemCollection menu = host.MainWindow.EntryContextMenu.Items;
            menu.Remove(otpDialogToolStripItem);
        }

    }
}
