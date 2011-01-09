namespace Capture
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Web;
    using System.Windows.Forms;

    public class frmTextWizard : Form
    {
        private CheckBox cbShowHex;
        private IContainer components;
        private Panel pnlBottom;
        private Panel pnlToFrom;
        private RadioButton rdoFromBin64;
        private RadioButton rdoFromDeflatedSAML;
        private RadioButton rdoFromUTF7;
        private RadioButton rdoHexEncode;
        private RadioButton rdoHTMLDecode;
        private RadioButton rdoHTMLEncode;
        private RadioButton rdoJScriptDecode;
        private RadioButton rdoJScriptEncode;
        private RadioButton rdoToBin64;
        private RadioButton rdoToDeflatedSAML;
        private RadioButton rdoToUTF7;
        private RadioButton rdoURLDecode;
        private RadioButton rdoURLEncode;
        private Splitter splitter1;
        private ToolTip toolTip1;
        private TextBox txtInputString;
        private TextBox txtOutputString;

        internal frmTextWizard()
        {
            this.InitializeComponent();
            this.txtOutputString.BackColor = System.Drawing.Color.AliceBlue;
        }

        private void actDoRecalc(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                this.Recalc();
            }
        }

        private void cbShowHex_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbShowHex.Checked)
            {
                this.txtOutputString.Font = new Font("Lucida Console", 8.25f);
            }
            else
            {
                this.txtOutputString.Font = new Font("Arial Unicode MS", 8.25f);
            }
            this.Recalc();
        }

        private static string DecodeJSString(string s)
        {
            if (string.IsNullOrEmpty(s) || !s.Contains(@"\"))
            {
                return s;
            }
            StringBuilder builder = new StringBuilder();
            int length = s.Length;
            for (int i = 0; i < length; i++)
            {
                char ch = s[i];
                if (ch == '\\')
                {
                    if ((i < (length - 5)) && (s[i + 1] == 'u'))
                    {
                        int num3 = HexToInt(s[i + 2]);
                        int num4 = HexToInt(s[i + 3]);
                        int num5 = HexToInt(s[i + 4]);
                        int num6 = HexToInt(s[i + 5]);
                        if (((num3 < 0) || (num4 < 0)) || ((num5 < 0) || (num6 < 0)))
                        {
                            goto Label_0171;
                        }
                        ch = (char) ((((num3 << 12) | (num4 << 8)) | (num5 << 4)) | num6);
                        i += 5;
                        builder.Append(ch);
                        continue;
                    }
                    if ((i < (length - 3)) && (s[i + 1] == 'x'))
                    {
                        int num7 = HexToInt(s[i + 2]);
                        int num8 = HexToInt(s[i + 3]);
                        if ((num7 < 0) || (num8 < 0))
                        {
                            goto Label_0171;
                        }
                        ch = (char) ((num7 << 4) | num8);
                        i += 3;
                        builder.Append(ch);
                        continue;
                    }
                    if (i < (length - 1))
                    {
                        if (s[i + 1] == 'n')
                        {
                            builder.Append("\n");
                            i++;
                            continue;
                        }
                        if (s[i + 1] == '\\')
                        {
                            builder.Append(@"\");
                            i++;
                            continue;
                        }
                    }
                }
            Label_0171:
                builder.Append(ch);
            }
            return builder.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string EncodeJSString(string sInput)
        {
            StringBuilder builder = new StringBuilder(sInput);
            builder.Replace(@"\", @"\\");
            builder.Replace("\r", @"\r");
            builder.Replace("\n", @"\n");
            builder.Replace("\"", "\\\"");
            sInput = builder.ToString();
            builder = new StringBuilder();
            foreach (char ch in sInput)
            {
                if ('\x007f' < ch)
                {
                    builder.AppendFormat(@"\u{0:X4}", (int) ch);
                }
                else
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        private void frmTextWizard_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                base.Close();
            }
        }

        private void frmTextWizard_Load(object sender, EventArgs e)
        {
            this.txtInputString.Font = new Font(this.txtInputString.Font.FontFamily, 10);
            this.txtOutputString.Font = new Font(this.txtOutputString.Font.FontFamily, 10);
            if (Clipboard.ContainsText() && (Clipboard.GetText().Length < 0x8000))
            {
                this.txtInputString.Paste();
            }
        }

        private static int HexToInt(char h)
        {
            if ((h >= '0') && (h <= '9'))
            {
                return (h - '0');
            }
            if ((h >= 'a') && (h <= 'f'))
            {
                return ((h - 'a') + 10);
            }
            if ((h >= 'A') && (h <= 'F'))
            {
                return ((h - 'A') + 10);
            }
            return -1;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtInputString = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.txtOutputString = new System.Windows.Forms.TextBox();
            this.pnlToFrom = new System.Windows.Forms.Panel();
            this.rdoToDeflatedSAML = new System.Windows.Forms.RadioButton();
            this.rdoFromDeflatedSAML = new System.Windows.Forms.RadioButton();
            this.rdoJScriptDecode = new System.Windows.Forms.RadioButton();
            this.rdoFromUTF7 = new System.Windows.Forms.RadioButton();
            this.rdoToUTF7 = new System.Windows.Forms.RadioButton();
            this.rdoHTMLDecode = new System.Windows.Forms.RadioButton();
            this.rdoHTMLEncode = new System.Windows.Forms.RadioButton();
            this.rdoJScriptEncode = new System.Windows.Forms.RadioButton();
            this.cbShowHex = new System.Windows.Forms.CheckBox();
            this.rdoHexEncode = new System.Windows.Forms.RadioButton();
            this.rdoURLDecode = new System.Windows.Forms.RadioButton();
            this.rdoURLEncode = new System.Windows.Forms.RadioButton();
            this.rdoFromBin64 = new System.Windows.Forms.RadioButton();
            this.rdoToBin64 = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlBottom.SuspendLayout();
            this.pnlToFrom.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInputString
            // 
            this.txtInputString.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtInputString.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInputString.Location = new System.Drawing.Point(0, 0);
            this.txtInputString.MaxLength = 1000000;
            this.txtInputString.Multiline = true;
            this.txtInputString.Name = "txtInputString";
            this.txtInputString.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInputString.Size = new System.Drawing.Size(760, 152);
            this.txtInputString.TabIndex = 0;
            this.txtInputString.TextChanged += new System.EventHandler(this.txtInputString_TextChanged);
            this.txtInputString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInputString_KeyDown);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 152);
            this.splitter1.MinExtra = 50;
            this.splitter1.MinSize = 50;
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(760, 3);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.txtOutputString);
            this.pnlBottom.Controls.Add(this.pnlToFrom);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(0, 155);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(760, 381);
            this.pnlBottom.TabIndex = 9;
            // 
            // txtOutputString
            // 
            this.txtOutputString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutputString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutputString.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F);
            this.txtOutputString.Location = new System.Drawing.Point(129, 0);
            this.txtOutputString.MaxLength = 1000000;
            this.txtOutputString.Multiline = true;
            this.txtOutputString.Name = "txtOutputString";
            this.txtOutputString.ReadOnly = true;
            this.txtOutputString.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutputString.Size = new System.Drawing.Size(631, 381);
            this.txtOutputString.TabIndex = 1;
            this.txtOutputString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOutputString_KeyDown);
            // 
            // pnlToFrom
            // 
            this.pnlToFrom.Controls.Add(this.rdoToDeflatedSAML);
            this.pnlToFrom.Controls.Add(this.rdoFromDeflatedSAML);
            this.pnlToFrom.Controls.Add(this.rdoJScriptDecode);
            this.pnlToFrom.Controls.Add(this.rdoFromUTF7);
            this.pnlToFrom.Controls.Add(this.rdoToUTF7);
            this.pnlToFrom.Controls.Add(this.rdoHTMLDecode);
            this.pnlToFrom.Controls.Add(this.rdoHTMLEncode);
            this.pnlToFrom.Controls.Add(this.rdoJScriptEncode);
            this.pnlToFrom.Controls.Add(this.cbShowHex);
            this.pnlToFrom.Controls.Add(this.rdoHexEncode);
            this.pnlToFrom.Controls.Add(this.rdoURLDecode);
            this.pnlToFrom.Controls.Add(this.rdoURLEncode);
            this.pnlToFrom.Controls.Add(this.rdoFromBin64);
            this.pnlToFrom.Controls.Add(this.rdoToBin64);
            this.pnlToFrom.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlToFrom.Location = new System.Drawing.Point(0, 0);
            this.pnlToFrom.Name = "pnlToFrom";
            this.pnlToFrom.Size = new System.Drawing.Size(129, 381);
            this.pnlToFrom.TabIndex = 0;
            // 
            // rdoToDeflatedSAML
            // 
            this.rdoToDeflatedSAML.Location = new System.Drawing.Point(3, 270);
            this.rdoToDeflatedSAML.Name = "rdoToDeflatedSAML";
            this.rdoToDeflatedSAML.Size = new System.Drawing.Size(121, 24);
            this.rdoToDeflatedSAML.TabIndex = 11;
            this.rdoToDeflatedSAML.Text = "To DeflatedSAML";
            this.toolTip1.SetToolTip(this.rdoToDeflatedSAML, "URLEncode(Base64Encode(Deflate(input))))");
            this.rdoToDeflatedSAML.CheckedChanged += new System.EventHandler(this.actDoRecalc);
            // 
            // rdoFromDeflatedSAML
            // 
            this.rdoFromDeflatedSAML.Location = new System.Drawing.Point(2, 294);
            this.rdoFromDeflatedSAML.Name = "rdoFromDeflatedSAML";
            this.rdoFromDeflatedSAML.Size = new System.Drawing.Size(121, 24);
            this.rdoFromDeflatedSAML.TabIndex = 12;
            this.rdoFromDeflatedSAML.Text = "From DeflatedSAML";
            this.toolTip1.SetToolTip(this.rdoFromDeflatedSAML, "Inflate(Base64Decode(URLDecode(input))))");
            this.rdoFromDeflatedSAML.CheckedChanged += new System.EventHandler(this.actDoRecalc);
            // 
            // rdoJScriptDecode
            // 
            this.rdoJScriptDecode.Location = new System.Drawing.Point(3, 150);
            this.rdoJScriptDecode.Name = "rdoJScriptDecode";
            this.rdoJScriptDecode.Size = new System.Drawing.Size(96, 24);
            this.rdoJScriptDecode.TabIndex = 6;
            this.rdoJScriptDecode.Text = "From &JS string";
            this.toolTip1.SetToolTip(this.rdoJScriptDecode, "Decode bytes using JScript rules");
            this.rdoJScriptDecode.CheckedChanged += new System.EventHandler(this.actDoRecalc);
            // 
            // rdoFromUTF7
            // 
            this.rdoFromUTF7.Location = new System.Drawing.Point(3, 246);
            this.rdoFromUTF7.Name = "rdoFromUTF7";
            this.rdoFromUTF7.Size = new System.Drawing.Size(96, 24);
            this.rdoFromUTF7.TabIndex = 10;
            this.rdoFromUTF7.Text = "From UTF-7";
            this.toolTip1.SetToolTip(this.rdoFromUTF7, "Decode From UTF-7");
            this.rdoFromUTF7.CheckedChanged += new System.EventHandler(this.actDoRecalc);
            // 
            // rdoToUTF7
            // 
            this.rdoToUTF7.Location = new System.Drawing.Point(3, 222);
            this.rdoToUTF7.Name = "rdoToUTF7";
            this.rdoToUTF7.Size = new System.Drawing.Size(96, 24);
            this.rdoToUTF7.TabIndex = 9;
            this.rdoToUTF7.Text = "To UTF-7";
            this.toolTip1.SetToolTip(this.rdoToUTF7, "UTF-7 Encode the string");
            this.rdoToUTF7.CheckedChanged += new System.EventHandler(this.actDoRecalc);
            // 
            // rdoHTMLDecode
            // 
            this.rdoHTMLDecode.Location = new System.Drawing.Point(3, 198);
            this.rdoHTMLDecode.Name = "rdoHTMLDecode";
            this.rdoHTMLDecode.Size = new System.Drawing.Size(96, 24);
            this.rdoHTMLDecode.TabIndex = 8;
            this.rdoHTMLDecode.Text = "HTM&L Decode";
            this.toolTip1.SetToolTip(this.rdoHTMLDecode, "HTML Decode");
            this.rdoHTMLDecode.CheckedChanged += new System.EventHandler(this.actDoRecalc);
            // 
            // rdoHTMLEncode
            // 
            this.rdoHTMLEncode.Location = new System.Drawing.Point(3, 174);
            this.rdoHTMLEncode.Name = "rdoHTMLEncode";
            this.rdoHTMLEncode.Size = new System.Drawing.Size(96, 24);
            this.rdoHTMLEncode.TabIndex = 7;
            this.rdoHTMLEncode.Text = "HT&ML Encode";
            this.toolTip1.SetToolTip(this.rdoHTMLEncode, "HTML Encode");
            this.rdoHTMLEncode.CheckedChanged += new System.EventHandler(this.actDoRecalc);
            // 
            // rdoJScriptEncode
            // 
            this.rdoJScriptEncode.Location = new System.Drawing.Point(3, 126);
            this.rdoJScriptEncode.Name = "rdoJScriptEncode";
            this.rdoJScriptEncode.Size = new System.Drawing.Size(96, 24);
            this.rdoJScriptEncode.TabIndex = 5;
            this.rdoJScriptEncode.Text = "To &JS string";
            this.toolTip1.SetToolTip(this.rdoJScriptEncode, "Encode high bytes (above 0x127) using JScript rules");
            this.rdoJScriptEncode.CheckedChanged += new System.EventHandler(this.actDoRecalc);
            // 
            // cbShowHex
            // 
            this.cbShowHex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbShowHex.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbShowHex.Location = new System.Drawing.Point(2, 356);
            this.cbShowHex.Name = "cbShowHex";
            this.cbShowHex.Size = new System.Drawing.Size(96, 24);
            this.cbShowHex.TabIndex = 13;
            this.cbShowHex.Text = "&View bytes";
            this.cbShowHex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbShowHex.CheckedChanged += new System.EventHandler(this.cbShowHex_CheckedChanged);
            // 
            // rdoHexEncode
            // 
            this.rdoHexEncode.Location = new System.Drawing.Point(3, 102);
            this.rdoHexEncode.Name = "rdoHexEncode";
            this.rdoHexEncode.Size = new System.Drawing.Size(96, 24);
            this.rdoHexEncode.TabIndex = 4;
            this.rdoHexEncode.Text = "&HexEncode";
            this.toolTip1.SetToolTip(this.rdoHexEncode, "Encode each character as a Hex-escaped value");
            this.rdoHexEncode.CheckedChanged += new System.EventHandler(this.actDoRecalc);
            // 
            // rdoURLDecode
            // 
            this.rdoURLDecode.Checked = true;
            this.rdoURLDecode.Location = new System.Drawing.Point(3, 78);
            this.rdoURLDecode.Name = "rdoURLDecode";
            this.rdoURLDecode.Size = new System.Drawing.Size(96, 24);
            this.rdoURLDecode.TabIndex = 3;
            this.rdoURLDecode.TabStop = true;
            this.rdoURLDecode.Text = "URL&Decode";
            this.toolTip1.SetToolTip(this.rdoURLDecode, "Decode using URLDecode()");
            this.rdoURLDecode.CheckedChanged += new System.EventHandler(this.actDoRecalc);
            // 
            // rdoURLEncode
            // 
            this.rdoURLEncode.Location = new System.Drawing.Point(3, 54);
            this.rdoURLEncode.Name = "rdoURLEncode";
            this.rdoURLEncode.Size = new System.Drawing.Size(96, 24);
            this.rdoURLEncode.TabIndex = 2;
            this.rdoURLEncode.Text = "&URLEncode";
            this.toolTip1.SetToolTip(this.rdoURLEncode, "Encode using URLEncoding rules used by webforms");
            this.rdoURLEncode.CheckedChanged += new System.EventHandler(this.actDoRecalc);
            // 
            // rdoFromBin64
            // 
            this.rdoFromBin64.Location = new System.Drawing.Point(3, 30);
            this.rdoFromBin64.Name = "rdoFromBin64";
            this.rdoFromBin64.Size = new System.Drawing.Size(96, 24);
            this.rdoFromBin64.TabIndex = 1;
            this.rdoFromBin64.Text = "&From Base64";
            this.toolTip1.SetToolTip(this.rdoFromBin64, "Decode from Base64; note that \"View bytes\" may be required to see the response co" +
                    "rrectly.");
            this.rdoFromBin64.CheckedChanged += new System.EventHandler(this.actDoRecalc);
            // 
            // rdoToBin64
            // 
            this.rdoToBin64.Location = new System.Drawing.Point(3, 6);
            this.rdoToBin64.Name = "rdoToBin64";
            this.rdoToBin64.Size = new System.Drawing.Size(96, 24);
            this.rdoToBin64.TabIndex = 0;
            this.rdoToBin64.Text = "&To Base64";
            this.toolTip1.SetToolTip(this.rdoToBin64, "Base64-encode the string");
            this.rdoToBin64.CheckedChanged += new System.EventHandler(this.actDoRecalc);
            // 
            // frmTextWizard
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(760, 536);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.txtInputString);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(150, 150);
            this.Name = "frmTextWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "±àÂë×ª»»";
            this.Load += new System.EventHandler(this.frmTextWizard_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmTextWizard_KeyUp);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlToFrom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Recalc()
        {
            string text = this.txtInputString.Text;
            string s = string.Empty;
            try
            {
                byte[] inArr = null;
                if (this.rdoFromUTF7.Checked)
                {
                    s = Encoding.UTF7.GetString(Encoding.UTF8.GetBytes(text));
                }
                else if (this.rdoToUTF7.Checked)
                {
                    s = Encoding.ASCII.GetString(Encoding.UTF7.GetBytes(((char) 0xfeff) + text));
                }
                else if (this.rdoToBin64.Checked)
                {
                    s = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
                }
                else if (this.rdoHexEncode.Checked)
                {
                    StringBuilder builder = new StringBuilder(0x80);
                    foreach (char ch in text)
                    {
                        builder.AppendFormat("%{0:x}", (int) ch);
                    }
                    s = builder.ToString();
                }
                else if (this.rdoFromBin64.Checked)
                {
                    inArr = Convert.FromBase64String(text);
                    s = Encoding.UTF8.GetString(Convert.FromBase64String(text));
                }
                else if (this.rdoURLEncode.Checked)
                {
                    s = HttpUtility.UrlEncode(text, Encoding.UTF8);
                }
                else if (this.rdoURLDecode.Checked)
                {
                    inArr = HttpUtility.UrlDecodeToBytes(text, Encoding.UTF8);
                    s = HttpUtility.UrlDecode(text, Encoding.UTF8);
                }
                else if (this.rdoJScriptEncode.Checked)
                {
                    s = this.EncodeJSString(text);
                }
                else if (this.rdoJScriptDecode.Checked)
                {
                    s = DecodeJSString(text);
                }
                else if (this.rdoHTMLEncode.Checked)
                {
                    s = Utilities.HtmlEncode(text);
                }
                else if (this.rdoHTMLDecode.Checked)
                {
                    s = HttpUtility.HtmlDecode(text);
                }
                else if (this.rdoFromDeflatedSAML.Checked)
                {
                    byte[] compressedData = Convert.FromBase64String(HttpUtility.UrlDecode(text, Encoding.UTF8));
                    s = Encoding.UTF8.GetString(Utilities.DeflaterExpand(compressedData));
                }
                else if (this.rdoToDeflatedSAML.Checked)
                {
                    s = HttpUtility.UrlEncode(Convert.ToBase64String(Utilities.DeflaterCompress(Encoding.UTF8.GetBytes(text))), Encoding.UTF8);
                }
                this.Text = "TextWizard [" + this.txtInputString.TextLength.ToString() + " => " + s.Length.ToString() + " chars]";
                if (this.cbShowHex.Checked)
                {
                    if (inArr != null)
                    {
                        s = Utilities.ByteArrayToHexView(inArr, 20);
                        string[] strArray2 = new string[] { "TextWizard [", this.txtInputString.TextLength.ToString(), " => ", inArr.Length.ToString(), " bytes]" };
                        this.Text = string.Concat(strArray2);
                    }
                    else
                    {
                        s = Utilities.ByteArrayToHexView(Encoding.UTF8.GetBytes(s), 20);
                    }
                }
                else
                {
                    s = s.Replace("\r\n", "\n").Replace("\n", "\r\n");//.Replace('\0', 0xfffd);
                }
            }
            catch (Exception exception)
            {
                s = "Error: " + exception.Message;
                this.Text = "TextWizard [Invalid Conversion]";
            }
            this.txtOutputString.WordWrap = !this.cbShowHex.Checked;
            this.txtOutputString.Text = s;
        }

        private void txtInputString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.A))
            {
                this.txtInputString.SelectAll();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtInputString_TextChanged(object sender, EventArgs e)
        {
            this.Recalc();
        }

        private void txtOutputString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.A))
            {
                this.txtOutputString.SelectAll();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        [STAThread]
        public static void Main(string[] args)
        {
            frmTextWizard wizard = new frmTextWizard();
            Application.Run(wizard);
        }
    }
}
