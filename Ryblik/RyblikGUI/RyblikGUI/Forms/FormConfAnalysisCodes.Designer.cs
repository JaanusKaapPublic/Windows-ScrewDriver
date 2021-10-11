﻿
namespace RyblikGUI.Forms
{
    partial class FormConfAnalysisCodes
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
            this.txtStart = new System.Windows.Forms.TextBox();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start:";
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(54, 10);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(100, 20);
            this.txtStart.TabIndex = 1;
            this.txtStart.Text = "0x0";
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(54, 36);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(100, 20);
            this.txtEnd.TabIndex = 3;
            this.txtEnd.Text = "0x7FFFFFFF";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "End:";
            // 
            // txtCodes
            // 
            this.txtCodes.Location = new System.Drawing.Point(16, 81);
            this.txtCodes.Multiline = true;
            this.txtCodes.Name = "txtCodes";
            this.txtCodes.Size = new System.Drawing.Size(345, 184);
            this.txtCodes.TabIndex = 5;
            this.txtCodes.Text = "#ERROR_INVALID_FUNCTION\r\n0x1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Codes to ignore:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(16, 276);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(345, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // FormConfAnalysisCodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 311);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtCodes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.label1);
            this.Name = "FormConfAnalysisCodes";
            this.Text = "Analysis conf";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStart;
    }
}