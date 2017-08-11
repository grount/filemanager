using System;
using System.IO;
using System.Windows.Forms;

namespace filemanager
{
    public partial class FileManagerForm : Form
    {
        string currentPath = string.Empty;

        public FileManagerForm()
        {
            InitializeComponent();
            InitializeListBoxItems();
        }

        private void InitializeListBoxItems()
        {
            string[] stringDrives = Environment.GetLogicalDrives();

            foreach (var stringDrive in stringDrives)
            {
                dataGridView1.Rows.Add(stringDrive);
            }

        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                currentPath = dataGridView1.CurrentCell.Value.ToString();
                dataGridView1.Rows.Clear();

                if (currentPath != null)
                {
                    string[] files = Directory.GetFileSystemEntries(currentPath, "*", SearchOption.TopDirectoryOnly); // use stack to save old files

                    foreach (var file in files)
                    {
                        dataGridView1.Rows.Add(file);
                    }
                }

            }
        }
    }
}
