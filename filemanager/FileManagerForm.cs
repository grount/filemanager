using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace filemanager
{
    public partial class FileManagerForm : Form
    {
        readonly Stack<Tuple<string, string[]>> r_pathStack = new Stack<Tuple<string, string[]>>();
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
            r_pathStack.Push(new Tuple<string, string[]>(m_currentPath, getDataGridViewRowStrings()));

            if (isCurrentPathFolder())
            {
                string[] files = Directory.GetFileSystemEntries(m_currentPath, "*", SearchOption.TopDirectoryOnly);
                dataGridView1.Rows.Clear();

                foreach (var file in files)
                {
                    dataGridView1.Rows.Add(file);
                }
            }
            else
            {
                File.Open(m_currentPath, FileMode.Open);
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
            if (r_pathStack != null)
            {
                m_currentPath = r_pathStack.Peek().Item1;
                dataGridView1.Rows.Clear();

                if (m_currentPath != null)
                {
                    string[] files = r_pathStack.Peek().Item2;

                    foreach (var file in files)
                    {
                        dataGridView1.Rows.Add(file);
                    }
                }
            }
        }

        private void fillDataGridView(string[] files)
        {
            if (m_currentPath != null)
            {
                foreach (var file in files)
                {
                    dataGridView1.Rows.Add(file);
                }
            }
        }

        private bool isCurrentPathFolder()
        {
            FileAttributes attributes = File.GetAttributes(m_currentPath);

            return attributes.HasFlag(FileAttributes.Directory);
        }
    }
}