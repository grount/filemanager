namespace filemanager
{
    partial class FileManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileManagerForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.undoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.redoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripButton,
            this.redoToolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(701, 45);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // undoToolStripButton
            // 
            this.undoToolStripButton.AutoSize = false;
            this.undoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripButton.Image")));
            this.undoToolStripButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.undoToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.undoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoToolStripButton.Name = "undoToolStripButton";
            this.undoToolStripButton.Size = new System.Drawing.Size(42, 42);
            this.undoToolStripButton.Text = "Undo";
            this.undoToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.undoToolStripButton.Click += new System.EventHandler(this.undoToolStripButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(0, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(701, 20);
            this.textBox1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Path});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 65);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(701, 294);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // Path
            // 
            this.Path.HeaderText = "Path";
            this.Path.Name = "Path";
            this.Path.ReadOnly = true;
            this.Path.Width = 300;
            // 
            // redoToolStripButton
            // 
            this.redoToolStripButton.AutoSize = false;
            this.redoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripButton.Image")));
            this.redoToolStripButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.redoToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.redoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoToolStripButton.Name = "redoToolStripButton";
            this.redoToolStripButton.Size = new System.Drawing.Size(42, 42);
            this.redoToolStripButton.Text = "Redo";
            this.redoToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.redoToolStripButton.Click += new System.EventHandler(this.redoToolStripButton_Click);
            // 
            // FileManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 359);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.toolStrip);
            this.Name = "FileManagerForm";
            this.Text = "File Manager";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.ToolStripButton undoToolStripButton;
        private System.Windows.Forms.ToolStripButton redoToolStripButton;
    }
}

