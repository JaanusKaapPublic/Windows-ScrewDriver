
namespace RyblikGUI.Forms
{
    partial class FormResultText
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
            this.lblComment = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(12, 31);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(343, 13);
            this.lblComment.TabIndex = 7;
            this.lblComment.Text = "????????????????????????????????????????????????????????";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(15, 60);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(340, 354);
            this.txtResult.TabIndex = 6;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(89, 9);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(132, 13);
            this.lblProgress.TabIndex = 5;
            this.lblProgress.Text = "?????????? / ?????????";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "PROGRESS:";
            // 
            // FormResultText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 426);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.label1);
            this.Name = "FormResultText";
            this.Text = "Result";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label1;
    }
}