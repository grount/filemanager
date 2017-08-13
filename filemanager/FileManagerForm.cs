using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Renci.SshNet;

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
                dataGridView.Rows.Add(stringDrive);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            setStacks();
            eFileType fileType = FileManagerUtils.GetCurrentPathType(m_currentPath);

            if (fileType == eFileType.Folder || fileType == eFileType.LogicalDrive)
            {
                fillDataGridView(Directory.GetFileSystemEntries(m_currentPath, "*", SearchOption.TopDirectoryOnly));
            }
            else if (fileType == eFileType.File)
            {
                System.Diagnostics.Process.Start(m_currentPath);
            }
        }

        private void setStacks()
        {
            if (dataGridView.Rows.Count <= 0) return;

            m_currentPath = dataGridView.CurrentCell.Value.ToString();
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
            redoUndoHelper();
        }

        private void redoToolStripButton_Click(object sender, EventArgs e)
        {
            if (r_nowStack.Count <= 1) return;

            r_pastStack.Push(r_nowStack.Pop());
            redoUndoHelper();
        }

        private void redoUndoHelper()
        {
            pathTextBox.Text = r_nowStack.Peek().Item1;
            m_currentPath = r_nowStack.Peek().Item1;
            fillDataGridView(r_nowStack.Peek().Item2);
        }

        private void fillDataGridView(IEnumerable<string> files)
        {
            dataGridView.Rows.Clear();

            if (m_currentPath != null)
            {
                foreach (var file in files)
                {
                    dataGridView.Rows.Add(file);
                }
            }
        }

        private void pathTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string enteredPath = pathTextBox.Text;
            eFileType fileType = FileManagerUtils.GetCurrentPathType(enteredPath);

            if (fileType == eFileType.Folder)
            {
                m_currentPath = enteredPath;
                fillDataGridView(Directory.GetFileSystemEntries(enteredPath, "*", SearchOption.TopDirectoryOnly));
            }
            else if (fileType == eFileType.File)
            {
                System.Diagnostics.Process.Start(enteredPath);
            }
            else // TODO web site should work also from url bar.
            {
                MessageBox.Show("Windows can't find '" + enteredPath + "'. Check the spelling and try again.",
                    "File Explorer",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string getDataGridViewCellValue()
        {
            int selectedRowIndex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView.Rows[selectedRowIndex];

            return Convert.ToString(selectedRow.Cells["Path"].Value);
        }

        private void operationEventHandler(eActionType i_ActionType)
        {
            string message = string.Empty;

            FileManagerUtils.messagePreperations(ref message, i_ActionType);

            if (!checkDataGridViewBounds(message)) return;

            string path = getDataGridViewCellValue();

            if (i_ActionType != eActionType.Delete && showFolderBrowseDialog(path, message))
            {
                operationEventHandlerHelper(i_ActionType, path);
            }
            else if (i_ActionType == eActionType.Delete)
            {
                operationEventHandlerHelper(i_ActionType, path); // todo add delete functionality
            }
        }

        private void operationEventHandlerHelper(eActionType i_ActionType, string i_Path)
        {
            string destinationFolder = folderBrowserDialog.SelectedPath;

            ActionUtils.Perform(i_ActionType, i_Path, destinationFolder);

            if (i_ActionType != eActionType.Copy)
            {
                removePathFromDataGridView(i_Path);
            }
        }

        private bool checkDataGridViewBounds(string i_Message)
        {
            if (dataGridView.SelectedCells.Count <= 0)
            {
                MessageBox.Show("You need to select an item in order to " + i_Message + " it.", "Windows Explorer",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                return false;
            }

            return true;
        }

        private bool showFolderBrowseDialog(string i_Path, string i_Message)
        {
            folderBrowserDialog.SelectedPath = System.IO.Path.GetDirectoryName(i_Path);

            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("You need to select destination folder in order to " + i_Message + " it.",
                    "Windows Explorer",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                return true;
            }

            return false;
        }

        private void removePathFromDataGridView(string i_Path)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells["Path"].Value.Equals(i_Path))
                {
                    dataGridView.Rows.Remove(row);
                }
            }
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            operationEventHandler(eActionType.Copy);
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            operationEventHandler(eActionType.Delete);
        }

        private void moveToolStripButton_Click(object sender, EventArgs e)
        {
            operationEventHandler(eActionType.Move);
        }

        private void connectToolStripButton_Click(object sender, EventArgs e)
        {
        }
    }
}