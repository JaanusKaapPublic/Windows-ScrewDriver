namespace RyblikGUI
{
    partial class PanelFuzzConf
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
            this.txtConf = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSeed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLogFile = new System.Windows.Forms.TextBox();
            this.btnLogFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtConf
            // 
            this.txtConf.Location = new System.Drawing.Point(4, 47);
            this.txtConf.Multiline = true;
            this.txtConf.Name = "txtConf";
            this.txtConf.Size = new System.Drawing.Size(321, 178);
            this.txtConf.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "SEED: ";
            // 
            // txtSeed
            // 
            this.txtSeed.Location = new System.Drawing.Point(70, 1);
            this.txtSeed.Name = "txtSeed";
            this.txtSeed.Size = new System.Drawing.Size(218, 20);
            this.txtSeed.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "LOG FILE: ";
            // 
            // txtLogFile
            // 
            this.txtLogFile.Location = new System.Drawing.Point(70, 21);
            this.txtLogFile.Name = "txtLogFile";
            this.txtLogFile.Size = new System.Drawing.Size(218, 20);
            this.txtLogFile.TabIndex = 4;
            // 
            // btnLogFile
            // 
            this.btnLogFile.Location = new System.Drawing.Point(294, 21);
            this.btnLogFile.Name = "btnLogFile";
            this.btnLogFile.Size = new System.Drawing.Size(30, 20);
            this.btnLogFile.TabIndex = 5;
            this.btnLogFile.Text = "...";
            this.btnLogFile.UseVisualStyleBackColor = true;
            this.btnLogFile.Click += new System.EventHandler(this.btnLogFile_Click);
            // 
            // PanelFuzzConf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLogFile);
            this.Controls.Add(this.txtLogFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSeed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtConf);
            this.Name = "PanelFuzzConf";
            this.Size = new System.Drawing.Size(328, 232);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLogFile;
        private System.Windows.Forms.Button btnLogFile;
    }
}
