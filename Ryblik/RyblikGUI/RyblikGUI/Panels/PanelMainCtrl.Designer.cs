namespace RyblikGUI
{
    partial class PanelMainCtrl
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
            this.tree = new System.Windows.Forms.TreeView();
            this.chkHideNoAccess = new System.Windows.Forms.CheckBox();
            this.chkHideNoDevices = new System.Windows.Forms.CheckBox();
            this.chkRW = new System.Windows.Forms.CheckBox();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLogFile = new System.Windows.Forms.TextBox();
            this.btnLogging = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tree
            // 
            this.tree.Location = new System.Drawing.Point(0, 31);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(356, 536);
            this.tree.TabIndex = 1;
            this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterSelect);
            this.tree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_NodeMouseDoubleClick);
            // 
            // chkHideNoAccess
            // 
            this.chkHideNoAccess.AutoSize = true;
            this.chkHideNoAccess.Location = new System.Drawing.Point(4, 4);
            this.chkHideNoAccess.Name = "chkHideNoAccess";
            this.chkHideNoAccess.Size = new System.Drawing.Size(119, 17);
            this.chkHideNoAccess.TabIndex = 2;
            this.chkHideNoAccess.Text = "Hide not accessible";
            this.chkHideNoAccess.UseVisualStyleBackColor = true;
            this.chkHideNoAccess.CheckedChanged += new System.EventHandler(this.chkHideNoAccess_CheckedChanged);
            // 
            // chkHideNoDevices
            // 
            this.chkHideNoDevices.AutoSize = true;
            this.chkHideNoDevices.Location = new System.Drawing.Point(129, 3);
            this.chkHideNoDevices.Name = "chkHideNoDevices";
            this.chkHideNoDevices.Size = new System.Drawing.Size(103, 17);
            this.chkHideNoDevices.TabIndex = 3;
            this.chkHideNoDevices.Text = "Hide no devices";
            this.chkHideNoDevices.UseVisualStyleBackColor = true;
            this.chkHideNoDevices.CheckedChanged += new System.EventHandler(this.chkHideNoAccess_CheckedChanged);
            // 
            // chkRW
            // 
            this.chkRW.AutoSize = true;
            this.chkRW.Location = new System.Drawing.Point(238, 4);
            this.chkRW.Name = "chkRW";
            this.chkRW.Size = new System.Drawing.Size(69, 17);
            this.chkRW.TabIndex = 4;
            this.chkRW.Text = "Only R&W";
            this.chkRW.UseVisualStyleBackColor = true;
            this.chkRW.CheckedChanged += new System.EventHandler(this.chkHideNoAccess_CheckedChanged);
            // 
            // panelDetails
            // 
            this.panelDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelDetails.Location = new System.Drawing.Point(362, 31);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(625, 536);
            this.panelDetails.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(359, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Log file:";
            // 
            // txtLogFile
            // 
            this.txtLogFile.Location = new System.Drawing.Point(409, 2);
            this.txtLogFile.Name = "txtLogFile";
            this.txtLogFile.Size = new System.Drawing.Size(438, 20);
            this.txtLogFile.TabIndex = 7;
            // 
            // btnLogging
            // 
            this.btnLogging.Location = new System.Drawing.Point(853, 0);
            this.btnLogging.Name = "btnLogging";
            this.btnLogging.Size = new System.Drawing.Size(133, 23);
            this.btnLogging.TabIndex = 8;
            this.btnLogging.Text = "Start logging";
            this.btnLogging.UseVisualStyleBackColor = true;
            this.btnLogging.Click += new System.EventHandler(this.btnLogging_Click);
            // 
            // PanelMainCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLogging);
            this.Controls.Add(this.txtLogFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelDetails);
            this.Controls.Add(this.chkRW);
            this.Controls.Add(this.chkHideNoDevices);
            this.Controls.Add(this.chkHideNoAccess);
            this.Controls.Add(this.tree);
            this.Name = "PanelMainCtrl";
            this.Size = new System.Drawing.Size(990, 570);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.CheckBox chkHideNoAccess;
        private System.Windows.Forms.CheckBox chkHideNoDevices;
        private System.Windows.Forms.CheckBox chkRW;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLogFile;
        private System.Windows.Forms.Button btnLogging;
    }
}
