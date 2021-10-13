namespace RyblikGUI
{
    partial class FormLogData
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
            this.tree = new System.Windows.Forms.TreeView();
            this.table = new System.Windows.Forms.DataGridView();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.driver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inputSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outputSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.open = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.SuspendLayout();
            // 
            // tree
            // 
            this.tree.Location = new System.Drawing.Point(12, 12);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(345, 545);
            this.tree.TabIndex = 0;
            this.tree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_NodeMouseClick);
            this.tree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_NodeMouseDoubleClick);
            // 
            // table
            // 
            this.table.AllowUserToAddRows = false;
            this.table.AllowUserToDeleteRows = false;
            this.table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index,
            this.driver,
            this.device,
            this.ctrlCode,
            this.inputSize,
            this.outputSize,
            this.open});
            this.table.Location = new System.Drawing.Point(364, 13);
            this.table.Name = "table";
            this.table.ReadOnly = true;
            this.table.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.table.Size = new System.Drawing.Size(903, 296);
            this.table.TabIndex = 1;
            this.table.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.table_CellClick);
            this.table.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.table_CellContentClick);
            this.table.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.table_ColumnHeaderMouseClick);
            // 
            // index
            // 
            this.index.HeaderText = "Index";
            this.index.Name = "index";
            this.index.ReadOnly = true;
            this.index.Width = 60;
            // 
            // driver
            // 
            this.driver.HeaderText = "Driver";
            this.driver.Name = "driver";
            this.driver.ReadOnly = true;
            this.driver.Width = 160;
            // 
            // device
            // 
            this.device.HeaderText = "Device";
            this.device.Name = "device";
            this.device.ReadOnly = true;
            this.device.Width = 160;
            // 
            // ctrlCode
            // 
            this.ctrlCode.FillWeight = 90F;
            this.ctrlCode.HeaderText = "Ctrl code";
            this.ctrlCode.Name = "ctrlCode";
            this.ctrlCode.ReadOnly = true;
            this.ctrlCode.Width = 90;
            // 
            // inputSize
            // 
            this.inputSize.FillWeight = 80F;
            this.inputSize.HeaderText = "Input size";
            this.inputSize.Name = "inputSize";
            this.inputSize.ReadOnly = true;
            this.inputSize.Width = 80;
            // 
            // outputSize
            // 
            this.outputSize.FillWeight = 90F;
            this.outputSize.HeaderText = "Output size";
            this.outputSize.Name = "outputSize";
            this.outputSize.ReadOnly = true;
            this.outputSize.Width = 90;
            // 
            // open
            // 
            this.open.HeaderText = "Open";
            this.open.Name = "open";
            this.open.ReadOnly = true;
            this.open.Width = 60;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(12, 563);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(345, 22);
            this.btnSelectAll.TabIndex = 19;
            this.btnSelectAll.Text = "Select all";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(1172, 315);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(95, 23);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // FormLogData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1279, 597);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.table);
            this.Controls.Add(this.tree);
            this.MaximizeBox = false;
            this.Name = "FormLogData";
            this.Text = "Stored data";
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.DataGridView table;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewTextBoxColumn driver;
        private System.Windows.Forms.DataGridViewTextBoxColumn device;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctrlCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn inputSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn outputSize;
        private System.Windows.Forms.DataGridViewButtonColumn open;
    }
}