using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace filemanager
{
    public partial class FileManagerForm : Form
    {
        readonly Stack<Tuple<string, string[]>> r_nowStack = new Stack<Tuple<string, string[]>>();
        readonly Stack<Tuple<string, string[]>> r_pastStack = new Stack<Tuple<string, string[]>>();

        string m_currentPath = string.Empty;

        public FileManagerForm()
        {
            InitializeComponent();
            InitializeListBoxItems();
        }

        private void InitializeListBoxItems()
        {
            string[] stringDrives = Environment.GetLogicalDrives();

            r_nowStack.Push(new Tuple<string, string[]>("My PC", stringDrives));

            foreach (var stringDrive in stringDrives)
            {
                dataGridView1.Rows.Add(stringDrive);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            setStacks();

            if (isCurrentPathAFolder())
            {
                fillDataGridView(Directory.GetFileSystemEntries(m_currentPath, "*", SearchOption.TopDirectoryOnly));
            }
            else
            {
                System.Diagnostics.Process.Start(m_currentPath);
            }
        }

        private void setStacks()
        {
            if (dataGridView1.Rows.Count <= 0) return;

            m_currentPath = dataGridView1.CurrentCell.Value.ToString();
            pathTextBox.Text = m_currentPath;

            if (r_nowStack.Count > 0)
            {
                r_pastStack.Push(r_nowStack.Pop());
                r_nowStack.Clear();
            }

            r_nowStack.Push(new Tuple<string, string[]>(m_currentPath,
                Directory.GetFileSystemEntries(m_currentPath, "*", SearchOption.TopDirectoryOnly)));
        }

        private void undoToolStripButton_Click(object sender, EventArgs e)
        {
            if (r_pastStack.Count <= 0) return;

            r_nowStack.Push(r_pastStack.Pop());
            pathTextBox.Text = r_nowStack.Peek().Item1;
            m_currentPath = r_nowStack.Peek().Item1;
            fillDataGridView(r_nowStack.Peek().Item2);
        }

        private void redoToolStripButton_Click(object sender, EventArgs e)
        {
            if (r_nowStack.Count <= 1) return;

            r_pastStack.Push(r_nowStack.Pop());
            pathTextBox.Text = r_nowStack.Peek().Item1;
            m_currentPath = r_nowStack.Peek().Item1;
            fillDataGridView(r_nowStack.Peek().Item2);
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