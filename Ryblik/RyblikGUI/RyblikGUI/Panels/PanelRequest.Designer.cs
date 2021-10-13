namespace RyblikGUI
{
    partial class PanelRequest
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
            this.txtDevice = new System.Windows.Forms.TextBox();
            this.panelHexIn = new System.Windows.Forms.Panel();
            this.panelHexOut = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCtrlCode = new System.Windows.Forms.TextBox();
            this.txtMaxOut = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtErr = new System.Windows.Forms.TextBox();
            this.chkRead = new System.Windows.Forms.CheckBox();
            this.chkWrite = new System.Windows.Forms.CheckBox();
            this.btnGeneratePython = new System.Windows.Forms.Button();
            this.txtErrMsg = new System.Windows.Forms.TextBox();
            this.btnSetAccess = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnFindCodes = new System.Windows.Forms.Button();
            this.btnFindLength = new System.Windows.Forms.Button();
            this.btnLoopBytes = new System.Windows.Forms.Button();
            this.btnLoopWords = new System.Windows.Forms.Button();
            this.btnDetectLeak = new System.Windows.Forms.Button();
            this.btnGenMemory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Device:";
            // 
            // txtDevice
            // 
            this.txtDevice.Location = new System.Drawing.Point(54, 6);
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.Size = new System.Drawing.Size(448, 20);
            this.txtDevice.TabIndex = 1;
            // 
            // panelHexIn
            // 
            this.panelHexIn.Location = new System.Drawing.Point(7, 33);
            this.panelHexIn.Name = "panelHexIn";
            this.panelHexIn.Size = new System.Drawing.Size(626, 182);
            this.panelHexIn.TabIndex = 3;
            // 
            // panelHexOut
            // 
            this.panelHexOut.Location = new System.Drawing.Point(7, 249);
            this.panelHexOut.Name = "panelHexOut";
            this.panelHexOut.Size = new System.Drawing.Size(626, 206);
            this.panelHexOut.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Output";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(639, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Control code:";
            // 
            // txtCtrlCode
            // 
            this.txtCtrlCode.Location = new System.Drawing.Point(737, 30);
            this.txtCtrlCode.Name = "txtCtrlCode";
            this.txtCtrlCode.Size = new System.Drawing.Size(93, 20);
            this.txtCtrlCode.TabIndex = 7;
            this.txtCtrlCode.Text = "0x12345678";
            // 
            // txtMaxOut
            // 
            this.txtMaxOut.Location = new System.Drawing.Point(737, 56);
            this.txtMaxOut.Name = "txtMaxOut";
            this.txtMaxOut.Size = new System.Drawing.Size(93, 20);
            this.txtMaxOut.TabIndex = 9;
            this.txtMaxOut.Text = "0x1000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(639, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Max output:";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(642, 124);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(328, 23);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Error";
            // 
            // txtErr
            // 
            this.txtErr.Location = new System.Drawing.Point(122, 223);
            this.txtErr.Name = "txtErr";
            this.txtErr.ReadOnly = true;
            this.txtErr.Size = new System.Drawing.Size(91, 20);
            this.txtErr.TabIndex = 12;
            this.txtErr.Text = "0x1000";
            // 
            // chkRead
            // 
            this.chkRead.AutoSize = true;
            this.chkRead.Location = new System.Drawing.Point(642, 87);
            this.chkRead.Name = "chkRead";
            this.chkRead.Size = new System.Drawing.Size(56, 17);
            this.chkRead.TabIndex = 13;
            this.chkRead.Text = "READ";
            this.chkRead.UseVisualStyleBackColor = true;
            // 
            // chkWrite
            // 
            this.chkWrite.AutoSize = true;
            this.chkWrite.Location = new System.Drawing.Point(908, 87);
            this.chkWrite.Name = "chkWrite";
            this.chkWrite.Size = new System.Drawing.Size(62, 17);
            this.chkWrite.TabIndex = 14;
            this.chkWrite.Text = "WRITE";
            this.chkWrite.UseVisualStyleBackColor = true;
            // 
            // btnGeneratePython
            // 
            this.btnGeneratePython.Location = new System.Drawing.Point(642, 163);
            this.btnGeneratePython.Name = "btnGeneratePython";
            this.btnGeneratePython.Size = new System.Drawing.Size(328, 23);
            this.btnGeneratePython.TabIndex = 17;
            this.btnGeneratePython.Text = "Export as python";
            this.btnGeneratePython.UseVisualStyleBackColor = true;
            this.btnGeneratePython.Click += new System.EventHandler(this.btnGeneratePython_Click);
            // 
            // txtErrMsg
            // 
            this.txtErrMsg.Location = new System.Drawing.Point(219, 223);
            this.txtErrMsg.Name = "txtErrMsg";
            this.txtErrMsg.ReadOnly = true;
            this.txtErrMsg.Size = new System.Drawing.Size(414, 20);
            this.txtErrMsg.TabIndex = 18;
            this.txtErrMsg.Text = "0x1000";
            // 
            // btnSetAccess
            // 
            this.btnSetAccess.Location = new System.Drawing.Point(836, 28);
            this.btnSetAccess.Name = "btnSetAccess";
            this.btnSetAccess.Size = new System.Drawing.Size(134, 23);
            this.btnSetAccess.TabIndex = 19;
            this.btnSetAccess.Text = "Set access";
            this.btnSetAccess.UseVisualStyleBackColor = true;
            this.btnSetAccess.Click += new System.EventHandler(this.btnSetAccess_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(7, 492);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(626, 75);
            this.txtLog.TabIndex = 20;
            // 
            // btnFindCodes
            // 
            this.btnFindCodes.Location = new System.Drawing.Point(642, 192);
            this.btnFindCodes.Name = "btnFindCodes";
            this.btnFindCodes.Size = new System.Drawing.Size(109, 23);
            this.btnFindCodes.TabIndex = 21;
            this.btnFindCodes.Text = "Find control codes";
            this.btnFindCodes.UseVisualStyleBackColor = true;
            this.btnFindCodes.Click += new System.EventHandler(this.btnFindCodes_Click);
            // 
            // btnFindLength
            // 
            this.btnFindLength.Location = new System.Drawing.Point(757, 192);
            this.btnFindLength.Name = "btnFindLength";
            this.btnFindLength.Size = new System.Drawing.Size(104, 23);
            this.btnFindLength.TabIndex = 22;
            this.btnFindLength.Text = "Find length";
            this.btnFindLength.UseVisualStyleBackColor = true;
            this.btnFindLength.Click += new System.EventHandler(this.btnFindLength_Click);
            // 
            // btnLoopBytes
            // 
            this.btnLoopBytes.Location = new System.Drawing.Point(642, 221);
            this.btnLoopBytes.Name = "btnLoopBytes";
            this.btnLoopBytes.Size = new System.Drawing.Size(80, 23);
            this.btnLoopBytes.TabIndex = 23;
            this.btnLoopBytes.Text = "Loop bytes";
            this.btnLoopBytes.UseVisualStyleBackColor = true;
            this.btnLoopBytes.Click += new System.EventHandler(this.btnLoopBytes_Click);
            // 
            // btnLoopWords
            // 
            this.btnLoopWords.Location = new System.Drawing.Point(728, 221);
            this.btnLoopWords.Name = "btnLoopWords";
            this.btnLoopWords.Size = new System.Drawing.Size(80, 23);
            this.btnLoopWords.TabIndex = 24;
            this.btnLoopWords.Text = "Loop words";
            this.btnLoopWords.UseVisualStyleBackColor = true;
            this.btnLoopWords.Click += new System.EventHandler(this.btnLoopWords_Click);
            // 
            // btnDetectLeak
            // 
            this.btnDetectLeak.Location = new System.Drawing.Point(7, 461);
            this.btnDetectLeak.Name = "btnDetectLeak";
            this.btnDetectLeak.Size = new System.Drawing.Size(80, 23);
            this.btnDetectLeak.TabIndex = 25;
            this.btnDetectLeak.Text = "Detect leak";
            this.btnDetectLeak.UseVisualStyleBackColor = true;
            this.btnDetectLeak.Click += new System.EventHandler(this.btnDetectLeak_Click);
            // 
            // btnGenMemory
            // 
            this.btnGenMemory.Location = new System.Drawing.Point(508, 6);
            this.btnGenMemory.Name = "btnGenMemory";
            this.btnGenMemory.Size = new System.Drawing.Size(125, 20);
            this.btnGenMemory.TabIndex = 26;
            this.btnGenMemory.Text = "Allocate memory";
            this.btnGenMemory.UseVisualStyleBackColor = true;
            this.btnGenMemory.Click += new System.EventHandler(this.btnGenMemory_Click);
            // 
            // PanelRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGenMemory);
            this.Controls.Add(this.btnDetectLeak);
            this.Controls.Add(this.btnLoopWords);
            this.Controls.Add(this.btnLoopBytes);
            this.Controls.Add(this.btnFindCodes);
            this.Controls.Add(this.btnFindLength);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnSetAccess);
            this.Controls.Add(this.txtErrMsg);
            this.Controls.Add(this.btnGeneratePython);
            this.Controls.Add(this.chkWrite);
            this.Controls.Add(this.chkRead);
            this.Controls.Add(this.txtErr);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMaxOut);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCtrlCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelHexOut);
            this.Controls.Add(this.panelHexIn);
            this.Controls.Add(this.txtDevice);
            this.Controls.Add(this.label1);
            this.Name = "PanelRequest";
            this.Size = new System.Drawing.Size(990, 570);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDevice;
        private System.Windows.Forms.Panel panelHexIn;
        private System.Windows.Forms.Panel panelHexOut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCtrlCode;
        private System.Windows.Forms.TextBox txtMaxOut;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtErr;
        private System.Windows.Forms.CheckBox chkRead;
        private System.Windows.Forms.CheckBox chkWrite;
        private System.Windows.Forms.Button btnGeneratePython;
        private System.Windows.Forms.TextBox txtErrMsg;
        private System.Windows.Forms.Button btnSetAccess;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnFindCodes;
        private System.Windows.Forms.Button btnFindLength;
        private System.Windows.Forms.Button btnLoopBytes;
        private System.Windows.Forms.Button btnLoopWords;
        private System.Windows.Forms.Button btnDetectLeak;
        private System.Windows.Forms.Button btnGenMemory;
    }
}
