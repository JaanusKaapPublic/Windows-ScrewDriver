
namespace RyblikGUI.Forms
{
    partial class FormResultList
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.treeResults = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "PROGRESS:";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(90, 13);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(132, 13);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "?????????? / ?????????";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(345, 52);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(257, 354);
            this.txtResult.TabIndex = 2;
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(14, 35);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(343, 13);
            this.lblComment.TabIndex = 3;
            this.lblComment.Text = "????????????????????????????????????????????????????????";
            // 
            // treeResults
            // 
            this.treeResults.Location = new System.Drawing.Point(17, 52);
            this.treeResults.Name = "treeResults";
            this.treeResults.Size = new System.Drawing.Size(322, 354);
            this.treeResults.TabIndex = 4;
            // 
            // FormResultList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 420);
            this.Controls.Add(this.treeResults);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.label1);
            this.Name = "FormResultList";
            this.Text = "Results";
            this.Load += new System.EventHandler(this.FormResultList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TreeView treeResults;
    }
}