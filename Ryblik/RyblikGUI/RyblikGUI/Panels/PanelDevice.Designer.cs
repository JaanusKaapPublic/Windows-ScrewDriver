namespace RyblikGUI
{
    partial class PanelDevice
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
            this.btnUnhook = new System.Windows.Forms.Button();
            this.btnHook = new System.Windows.Forms.Button();
            this.btnEnableDbgBreak = new System.Windows.Forms.Button();
            this.btnDisableDbgBreak = new System.Windows.Forms.Button();
            this.btnDisableDbgLog = new System.Windows.Forms.Button();
            this.btnEnableDbgLog = new System.Windows.Forms.Button();
            this.btnDisableFileLog = new System.Windows.Forms.Button();
            this.btnEnableFileLog = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtPID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBreakPID = new System.Windows.Forms.Button();
            this.btnBreakCode = new System.Windows.Forms.Button();
            this.btnBreakThisPID = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUnhook
            // 
            this.btnUnhook.Location = new System.Drawing.Point(110, 40);
            this.btnUnhook.Name = "btnUnhook";
            this.btnUnhook.Size = new System.Drawing.Size(94, 28);
            this.btnUnhook.TabIndex = 7;
            this.btnUnhook.Text = "UNHOOK";
            this.btnUnhook.UseVisualStyleBackColor = true;
            this.btnUnhook.Click += new System.EventHandler(this.btnUnhook_Click);
            // 
            // btnHook
            // 
            this.btnHook.Location = new System.Drawing.Point(10, 40);
            this.btnHook.Name = "btnHook";
            this.btnHook.Size = new System.Drawing.Size(94, 28);
            this.btnHook.TabIndex = 6;
            this.btnHook.Text = "HOOK";
            this.btnHook.UseVisualStyleBackColor = true;
            this.btnHook.Click += new System.EventHandler(this.btnHook_Click);
            // 
            // btnEnableDbgBreak
            // 
            this.btnEnableDbgBreak.Enabled = false;
            this.btnEnableDbgBreak.Location = new System.Drawing.Point(10, 86);
            this.btnEnableDbgBreak.Name = "btnEnableDbgBreak";
            this.btnEnableDbgBreak.Size = new System.Drawing.Size(194, 28);
            this.btnEnableDbgBreak.TabIndex = 8;
            this.btnEnableDbgBreak.Text = "ENABLE DEBUGGER BREAK";
            this.btnEnableDbgBreak.UseVisualStyleBackColor = true;
            this.btnEnableDbgBreak.Click += new System.EventHandler(this.btnEnableDbgBreak_Click);
            // 
            // btnDisableDbgBreak
            // 
            this.btnDisableDbgBreak.Enabled = false;
            this.btnDisableDbgBreak.Location = new System.Drawing.Point(210, 86);
            this.btnDisableDbgBreak.Name = "btnDisableDbgBreak";
            this.btnDisableDbgBreak.Size = new System.Drawing.Size(194, 28);
            this.btnDisableDbgBreak.TabIndex = 9;
            this.btnDisableDbgBreak.Text = "DISABLE DEBUGGER BREAK";
            this.btnDisableDbgBreak.UseVisualStyleBackColor = true;
            this.btnDisableDbgBreak.Click += new System.EventHandler(this.btnDisableDbgBreak_Click);
            // 
            // btnDisableDbgLog
            // 
            this.btnDisableDbgLog.Enabled = false;
            this.btnDisableDbgLog.Location = new System.Drawing.Point(210, 120);
            this.btnDisableDbgLog.Name = "btnDisableDbgLog";
            this.btnDisableDbgLog.Size = new System.Drawing.Size(194, 28);
            this.btnDisableDbgLog.TabIndex = 11;
            this.btnDisableDbgLog.Text = "DISABLE DEBUGGER LOG";
            this.btnDisableDbgLog.UseVisualStyleBackColor = true;
            this.btnDisableDbgLog.Click += new System.EventHandler(this.btnDisableDbgLog_Click);
            // 
            // btnEnableDbgLog
            // 
            this.btnEnableDbgLog.Enabled = false;
            this.btnEnableDbgLog.Location = new System.Drawing.Point(10, 120);
            this.btnEnableDbgLog.Name = "btnEnableDbgLog";
            this.btnEnableDbgLog.Size = new System.Drawing.Size(194, 28);
            this.btnEnableDbgLog.TabIndex = 10;
            this.btnEnableDbgLog.Text = "ENABLE DEBUGGER LOG";
            this.btnEnableDbgLog.UseVisualStyleBackColor = true;
            this.btnEnableDbgLog.Click += new System.EventHandler(this.btnEnableDbgLog_Click);
            // 
            // btnDisableFileLog
            // 
            this.btnDisableFileLog.Enabled = false;
            this.btnDisableFileLog.Location = new System.Drawing.Point(210, 154);
            this.btnDisableFileLog.Name = "btnDisableFileLog";
            this.btnDisableFileLog.Size = new System.Drawing.Size(194, 28);
            this.btnDisableFileLog.TabIndex = 13;
            this.btnDisableFileLog.Text = "DISABLE FILE LOGGING";
            this.btnDisableFileLog.UseVisualStyleBackColor = true;
            this.btnDisableFileLog.Click += new System.EventHandler(this.btnDisableFileLog_Click);
            // 
            // btnEnableFileLog
            // 
            this.btnEnableFileLog.Enabled = false;
            this.btnEnableFileLog.Location = new System.Drawing.Point(10, 154);
            this.btnEnableFileLog.Name = "btnEnableFileLog";
            this.btnEnableFileLog.Size = new System.Drawing.Size(194, 28);
            this.btnEnableFileLog.TabIndex = 12;
            this.btnEnableFileLog.Text = "ENABLE FILE LOGGING";
            this.btnEnableFileLog.UseVisualStyleBackColor = true;
            this.btnEnableFileLog.Click += new System.EventHandler(this.btnEnableFileLog_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblTitle.Location = new System.Drawing.Point(7, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(70, 25);
            this.lblTitle.TabIndex = 14;
            this.lblTitle.Text = "label1";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(137, 222);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 23;
            // 
            // txtPID
            // 
            this.txtPID.Location = new System.Drawing.Point(137, 192);
            this.txtPID.Name = "txtPID";
            this.txtPID.Size = new System.Drawing.Size(100, 20);
            this.txtPID.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "IO ctrl code to break on:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "PID to break on:";
            // 
            // btnBreakPID
            // 
            this.btnBreakPID.Location = new System.Drawing.Point(243, 190);
            this.btnBreakPID.Name = "btnBreakPID";
            this.btnBreakPID.Size = new System.Drawing.Size(41, 23);
            this.btnBreakPID.TabIndex = 24;
            this.btnBreakPID.Text = "SET";
            this.btnBreakPID.UseVisualStyleBackColor = true;
            this.btnBreakPID.Click += new System.EventHandler(this.btnBreakPID_Click);
            // 
            // btnBreakCode
            // 
            this.btnBreakCode.Location = new System.Drawing.Point(243, 220);
            this.btnBreakCode.Name = "btnBreakCode";
            this.btnBreakCode.Size = new System.Drawing.Size(41, 23);
            this.btnBreakCode.TabIndex = 25;
            this.btnBreakCode.Text = "SET";
            this.btnBreakCode.UseVisualStyleBackColor = true;
            this.btnBreakCode.Click += new System.EventHandler(this.btnBreakCode_Click);
            // 
            // btnBreakThisPID
            // 
            this.btnBreakThisPID.Location = new System.Drawing.Point(290, 190);
            this.btnBreakThisPID.Name = "btnBreakThisPID";
            this.btnBreakThisPID.Size = new System.Drawing.Size(112, 23);
            this.btnBreakThisPID.TabIndex = 26;
            this.btnBreakThisPID.Text = "THIS PROCESS";
            this.btnBreakThisPID.UseVisualStyleBackColor = true;
            this.btnBreakThisPID.Click += new System.EventHandler(this.btnBreakThisPID_Click);
            // 
            // PanelDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBreakThisPID);
            this.Controls.Add(this.btnBreakCode);
            this.Controls.Add(this.btnBreakPID);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtPID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnDisableFileLog);
            this.Controls.Add(this.btnEnableFileLog);
            this.Controls.Add(this.btnDisableDbgLog);
            this.Controls.Add(this.btnEnableDbgLog);
            this.Controls.Add(this.btnDisableDbgBreak);
            this.Controls.Add(this.btnEnableDbgBreak);
            this.Controls.Add(this.btnUnhook);
            this.Controls.Add(this.btnHook);
            this.Name = "PanelDevice";
            this.Size = new System.Drawing.Size(625, 536);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUnhook;
        private System.Windows.Forms.Button btnHook;
        private System.Windows.Forms.Button btnEnableDbgBreak;
        private System.Windows.Forms.Button btnDisableDbgBreak;
        private System.Windows.Forms.Button btnDisableDbgLog;
        private System.Windows.Forms.Button btnEnableDbgLog;
        private System.Windows.Forms.Button btnDisableFileLog;
        private System.Windows.Forms.Button btnEnableFileLog;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtPID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBreakPID;
        private System.Windows.Forms.Button btnBreakCode;
        private System.Windows.Forms.Button btnBreakThisPID;
    }
}
