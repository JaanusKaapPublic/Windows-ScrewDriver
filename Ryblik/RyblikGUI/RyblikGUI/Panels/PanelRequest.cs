using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Be.Windows.Forms;
using static RyblikGUI.Fuzz;
using static RyblikGUI.RBLK;
using RyblikGUI.Exceptions;
using RyblikGUI.Analysis;
using System.Threading;

namespace RyblikGUI
{
    public partial class PanelRequest : UserControl
    {
        Form1 parent;
        private HexBox hexBoxIn;
        private HexBox hexBoxOut;
        private PanelFuzzConf fuzzConf;
        SaveFileDialog saveFileDialog = new SaveFileDialog();


        public PanelRequest(Form1 parentIn, String device, byte[] data)
        {
            InitializeComponent();
            createHexBox();
            parent = parentIn;
            if (device != null && device.Length > 0)
                txtDevice.Text = device;
            if (data != null)
                hexBoxIn.ByteProvider = new DynamicByteProvider(data);
        }
        public PanelRequest(Form1 parentIn, String device, uint code, byte[] data, uint outSize)
        {
            InitializeComponent();
            createHexBox();
            parent = parentIn;
            if (device != null && device.Length > 0)
                txtDevice.Text = device;
            if (data != null)
                hexBoxIn.ByteProvider = new DynamicByteProvider(data);
            txtCtrlCode.Text = "0x" + code.ToString("X");
            txtMaxOut.Text = "0x" + outSize.ToString("X");

            uint access = (code >> 14) & 0x3;
            if ((access & 0x1) > 0)
                chkRead.Checked = true;
            if ((access & 0x2) > 0)
                chkWrite.Checked = true;
        }

        private void createHexBox()
        {
            hexBoxIn = new HexBox()
            {
                Top = 0,
                Width = panelHexIn.Width,
                Height = panelHexIn.Height,
                Left = 0,
                Visible = true,
                UseFixedBytesPerLine = true,
                BytesPerLine = 16,
                ColumnInfoVisible = true,
                LineInfoVisible = true,
                StringViewVisible = true,
                VScrollBarVisible = true
            };
            panelHexIn.Controls.Add(hexBoxIn);


            hexBoxOut = new HexBox()
            {
                Top = 0,
                Width = panelHexOut.Width,
                Height = panelHexOut.Height,
                Left = 0,
                Visible = false,
                UseFixedBytesPerLine = true,
                BytesPerLine = 16,
                ColumnInfoVisible = true,
                LineInfoVisible = true,
                StringViewVisible = true,
                VScrollBarVisible = true,
                ReadOnly = true
            };
            panelHexOut.Controls.Add(hexBoxOut);

            fuzzConf = new PanelFuzzConf();
            panelFuzz.Controls.Add(fuzzConf);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            uint code = Conversions.convert2uint(txtCtrlCode.Text);
            uint outSize = Conversions.convert2uint(txtMaxOut.Text);
            uint access = 0;

            if (chkRead.Checked)
                access |= 1;
            if (chkWrite.Checked)
                access |= 2;

            byte[] outputBuffer;
            byte[] inputBuffer = new byte[hexBoxIn.ByteProvider.Length];
            for (int x = 0; x < inputBuffer.Length; x++)
                inputBuffer[x] = hexBoxIn.ByteProvider.ReadByte(x);

            uint err = Driver.devCtrlReq(txtDevice.Text, access, code, inputBuffer, out outputBuffer, outSize);
            if(err == 0)
            {
                hexBoxOut.Visible = true;
                if(outputBuffer != null)
                    hexBoxOut.ByteProvider = new DynamicByteProvider(outputBuffer);
                txtErr.Text = "";
                txtErrMsg.Text = "";
            }
            else
            {
                hexBoxOut.Visible = false;
                txtErr.Text = "0x" + err.ToString("X");
                txtErrMsg.Text = Conversions.errorNames(err);
            }
        }

        private void btnFuzz_Click(object sender, EventArgs e)
        {
            FUZZ_CONF[] confSets = fuzzConf.getConf();
            btnFuzz.Enabled = false;

            uint code = Conversions.convert2uint(txtCtrlCode.Text);
            uint outSize = Conversions.convert2uint(txtMaxOut.Text);
            uint access = 0;

            if (chkRead.Checked)
                access |= 1;
            if (chkWrite.Checked)
                access |= 2;

            byte[] inputBuffer = new byte[hexBoxIn.ByteProvider.Length];
            for (int x = 0; x < inputBuffer.Length; x++)
                inputBuffer[x] = hexBoxIn.ByteProvider.ReadByte(x);

            Fuzz fuzz = new Fuzz();
            fuzz.setLogFile(fuzzConf.getLogFilename());
            Task.Run(() => 
            {
                fuzz.fuzz(txtDevice.Text, access, code, inputBuffer, outSize, confSets); 
                btnFuzz.Enabled = true; 
            });            
        }

        private void btnGeneratePython_Click(object sender, EventArgs e)
        {

            uint code = Conversions.convert2uint(txtCtrlCode.Text);
            uint outSize = Conversions.convert2uint(txtMaxOut.Text);
            uint access = 0;

            if (chkRead.Checked)
                access |= 1;
            if (chkWrite.Checked)
                access |= 2;

            byte[] inputBuffer = new byte[hexBoxIn.ByteProvider.Length];
            for (int x = 0; x < inputBuffer.Length; x++)
                inputBuffer[x] = hexBoxIn.ByteProvider.ReadByte(x);

            String pythonCode = ExportPython.exportSingle(txtDevice.Text, access, code, inputBuffer, access);

            saveFileDialog.Filter = "Python file|*.py";
            saveFileDialog.Title = "Save an code File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                byte[] bytes = Encoding.UTF8.GetBytes(pythonCode);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
        }

        public void save()
        {
            saveFileDialog.Filter = "Ryblik file|*.rblk";
            saveFileDialog.Title = "Save an Ryblik File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                uint code = Conversions.convert2uint(txtCtrlCode.Text);
                uint outSize = Conversions.convert2uint(txtMaxOut.Text);
                uint access = 0;

                if (chkRead.Checked)
                    access |= 1;
                if (chkWrite.Checked)
                    access |= 2;

                byte[] inputBuffer = new byte[hexBoxIn.ByteProvider.Length];
                for (int x = 0; x < inputBuffer.Length; x++)
                    inputBuffer[x] = hexBoxIn.ByteProvider.ReadByte(x);

                List<RblkMessageStruct> all = new List<RblkMessageStruct>();
                RblkMessageStruct call;
                call.code = code;
                call.driverName = "Unknown";
                call.deviceName = txtDevice.Text;
                call.input = inputBuffer;
                call.outputSize = outSize;
                all.Add(call);
                RBLK.save(saveFileDialog.FileName, all);
            }
        }

        private void btnSetAccess_Click(object sender, EventArgs e)
        {
            try
            {
                uint code = Conversions.convert2uint(txtCtrlCode.Text);
                bool accessRead = (code & 0x4000) > 0;
                bool accessWrite = (code & 0x8000) > 0;
                chkRead.Checked = accessRead;
                chkWrite.Checked = accessWrite;
            }
            catch 
            {
                MessageBox.Show("Error parsing IOCTL code");
            }
        }

        public void log(String msg)
        {
            txtLog.Text = msg + "\r\n" + txtLog.Text;
        }

        private void btnFindCodes_Click(object sender, EventArgs e)
        {
            AnalysisCodeDetection codeDetection = new AnalysisCodeDetection();
            if (!codeDetection.getconf())
                return;
            Task.Run(() => codeDetection.start(txtDevice.Text));
        }

        private void btnFindLength_Click(object sender, EventArgs e)
        {
            AnalysisLengthDetection codeDetection = new AnalysisLengthDetection();
            uint code = Conversions.convert2uint(txtCtrlCode.Text);
            uint outSize = Conversions.convert2uint(txtMaxOut.Text);
            uint access = 0;
            if (chkRead.Checked)
                access |= 1;
            if (chkWrite.Checked)
                access |= 2;

            if (!codeDetection.getconf())
                return;
            Task.Run(() => codeDetection.start(txtDevice.Text, code, access, outSize));
        }

        private void btnLoopBytes_Click(object sender, EventArgs e)
        {
            AnalysisLoopBytes codeDetection = new AnalysisLoopBytes();
            uint code = Conversions.convert2uint(txtCtrlCode.Text);
            uint outSize = Conversions.convert2uint(txtMaxOut.Text);
            uint access = 0;
            if (chkRead.Checked)
                access |= 1;
            if (chkWrite.Checked)
                access |= 2;
            byte[] inputBuffer = new byte[hexBoxIn.ByteProvider.Length];
            for (int x = 0; x < inputBuffer.Length; x++)
                inputBuffer[x] = hexBoxIn.ByteProvider.ReadByte(x);

            if (!codeDetection.getconf(inputBuffer.Length))
                return;
            Task.Run(() => codeDetection.start(txtDevice.Text, inputBuffer, code, access, outSize));
        }

        private void btnLoopWords_Click(object sender, EventArgs e)
        {
            AnalysisLoopWords codeDetection = new AnalysisLoopWords();
            uint code = Conversions.convert2uint(txtCtrlCode.Text);
            uint outSize = Conversions.convert2uint(txtMaxOut.Text);
            uint access = 0;
            if (chkRead.Checked)
                access |= 1;
            if (chkWrite.Checked)
                access |= 2;
            byte[] inputBuffer = new byte[hexBoxIn.ByteProvider.Length];
            for (int x = 0; x < inputBuffer.Length; x++)
                inputBuffer[x] = hexBoxIn.ByteProvider.ReadByte(x);

            if (!codeDetection.getconf(inputBuffer.Length))
                return;
            Task.Run(() => codeDetection.start(txtDevice.Text, inputBuffer, code, access, outSize));
        }

        private void btnDetectLeak_Click(object sender, EventArgs e)
        {
            byte[] outputBuffer = new byte[hexBoxOut.ByteProvider.Length];
            for (int x = 0; x < outputBuffer.Length; x++)
                outputBuffer[x] = hexBoxOut.ByteProvider.ReadByte(x);


            int pos = 0;
            if (hexBoxOut.SelectionStart >= 0 && hexBoxOut.SelectionLength > 0)
                pos = (int)hexBoxOut.SelectionStart + 1;

            for (;pos < outputBuffer.Length-8; pos++)
            {
                if (outputBuffer[pos] != (byte)0xFF || outputBuffer[pos + 1] != 0xFF)
                    continue;
                hexBoxOut.SelectionStart = pos;
                hexBoxOut.SelectionLength = 8;
                return;
            }
            log("No suitable address found");
        }

        private void btnGenMemory_Click(object sender, EventArgs e)
        {

        }
    }
}
