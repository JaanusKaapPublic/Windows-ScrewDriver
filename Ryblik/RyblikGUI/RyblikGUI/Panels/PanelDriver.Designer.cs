namespace RyblikGUI
{
    partial class PanelDriver
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tableFuncs = new System.Windows.Forms.DataGridView();
            this.majorFunc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pointer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.tableDevices = new System.Windows.Forms.DataGridView();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.access = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnHook = new System.Windows.Forms.Button();
            this.btnUnhook = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tableFuncs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableDevices)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Major functions";
            // 
            // tableFuncs
            // 
            this.tableFuncs.AllowUserToAddRows = false;
            this.tableFuncs.AllowUserToDeleteRows = false;
            this.tableFuncs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableFuncs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.majorFunc,
            this.pointer});
            this.tableFuncs.Location = new System.Drawing.Point(6, 61);
            this.tableFuncs.Name = "tableFuncs";
            this.tableFuncs.Size = new System.Drawing.Size(604, 190);
            this.tableFuncs.TabIndex = 1;
            // 
            // majorFunc
            // 
            this.majorFunc.HeaderText = "Major function";
            this.majorFunc.Name = "majorFunc";
            this.majorFunc.ReadOnly = true;
            this.majorFunc.Width = 240;
            // 
            // pointer
            // 
            this.pointer.HeaderText = "Pointer";
            this.pointer.Name = "pointer";
            this.pointer.Width = 300;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 254);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Devices";
            // 
            // tableDevices
            // 
            this.tableDevices.AllowUserToAddRows = false;
            this.tableDevices.AllowUserToDeleteRows = false;
            this.tableDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableDevices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.address,
            this.name,
            this.access});
            this.tableDevices.Location = new System.Drawing.Point(6, 270);
            this.tableDevices.Name = "tableDevices";
            this.tableDevices.Size = new System.Drawing.Size(604, 150);
            this.tableDevices.TabIndex = 3;
            // 
            // address
            // 
            this.address.HeaderText = "Address";
            this.address.Name = "address";
            this.address.ReadOnly = true;
            this.address.Width = 200;
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 200;
            // 
            // access
            // 
            this.access.HeaderText = "Access";
            this.access.Name = "access";
            this.access.ReadOnly = true;
            this.access.Width = 140;
            // 
            // btnHook
            // 
            this.btnHook.Location = new System.Drawing.Point(6, 482);
            this.btnHook.Name = "btnHook";
            this.btnHook.Size = new System.Drawing.Size(94, 28);
            this.btnHook.TabIndex = 4;
            this.btnHook.Text = "HOOK";
            this.btnHook.UseVisualStyleBackColor = true;
            this.btnHook.Click += new System.EventHandler(this.btnHook_Click);
            // 
            // btnUnhook
            // 
            this.btnUnhook.Location = new System.Drawing.Point(106, 482);
            this.btnUnhook.Name = "btnUnhook";
            this.btnUnhook.Size = new System.Drawing.Size(94, 28);
            this.btnUnhook.TabIndex = 5;
            this.btnUnhook.Text = "UNHOOK";
            this.btnUnhook.UseVisualStyleBackColor = true;
            this.btnUnhook.Click += new System.EventHandler(this.btnUnhook_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(70, 25);
            this.lblTitle.TabIndex = 15;
            this.lblTitle.Text = "label1";
            // 
            // PanelDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnUnhook);
            this.Controls.Add(this.btnHook);
            this.Controls.Add(this.tableDevices);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tableFuncs);
            this.Controls.Add(this.label1);
            this.Name = "PanelDriver";
            this.Size = new System.Drawing.Size(625, 536);
            ((System.ComponentModel.ISupportInitialize)(this.tableFuncs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableDevices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView tableFuncs;
        private System.Windows.Forms.DataGridViewTextBoxColumn majorFunc;
        private System.Windows.Forms.DataGridViewTextBoxColumn pointer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView tableDevices;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn access;
        private System.Windows.Forms.Button btnHook;
        private System.Windows.Forms.Button btnUnhook;
        private System.Windows.Forms.Label lblTitle;
    }
}
