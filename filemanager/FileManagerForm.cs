using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace filemanager
{
    public partial class FileManagerForm : Form
    {
        readonly Stack<Tuple<string, string[]>> r_undoStack = new Stack<Tuple<string, string[]>>();
        string m_currentPath = string.Empty;

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
            if (dataGridView1.Rows.Count <= 0) return;

            m_currentPath = dataGridView1.CurrentCell.Value.ToString();
            r_undoStack.Push(new Tuple<string, string[]>(m_currentPath, getDataGridViewRowStrings()));

            if (isCurrentPathAFolder())
            {
                fillDataGridView(Directory.GetFileSystemEntries(m_currentPath, "*", SearchOption.TopDirectoryOnly));
            }
            else
            {
                System.Diagnostics.Process.Start(m_currentPath);
            }

        }

        private string[] getDataGridViewRowStrings()
        {
            int i = 0;
            string[] dataGridViewStrings = new string[dataGridView1.RowCount];

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                dataGridViewStrings[i] = row.Cells[0].Value.ToString();
                i++;
            }

            return dataGridViewStrings;
        }

        private void undoToolStripButton_Click(object sender, EventArgs e)
        {
            if (r_undoStack.Count != 0)
            {
                m_currentPath = r_undoStack.Peek().Item1;
                fillDataGridView(r_undoStack.Peek().Item2);
                r_undoStack.Pop();
            }
        }

        private void fillDataGridView(IEnumerable<string> files)
        {
            dataGridView1.Rows.Clear();

            if (m_currentPath != null)
            {
                foreach (var file in files)
                {
                    dataGridView1.Rows.Add(file);
                }
            }
        }

        private bool isCurrentPathAFolder()
        {
            FileAttributes attributes = File.GetAttributes(m_currentPath);

            return attributes.HasFlag(FileAttributes.Directory);
        }
    }
}