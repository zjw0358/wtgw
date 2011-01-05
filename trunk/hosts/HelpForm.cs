namespace Capture
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class HelpForm : Form
    {
        private Button btnCanel;
        private Button button1;
        private IContainer components = null;
        private Label label1;
        private Label labHelp;
        private Label labTitle;
        private TextBox textBox1;

        public HelpForm()
        {
            this.InitializeComponent();
            this.labHelp.Text = "\r\n新增：在最下方的空行里输入，即可新增行\r\n\r\n修改：双击需要修改的内容，即可修改\r\n\r\n删除：单击行左侧的灰色方格，按Del键\r\n\r\n是否应用：勾选时表示应用，取消勾选时表示这行是注释\r\n\r\n加空行：保存时在该条记录下方添加一个空行\r\n";
            this.textBox1.Text = "该功能相当于把下方的信息导入注册表，你也可以直接把下方的文字保存为reg文件，然后双击导入：\r\nWindows Registry Editor Version 5.00\r\n\r\n[HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings]\r\n\"DnsCacheEnabled\"=dword:00000000\r\n\"DnsCacheTimeout\"=dword:00000000\r\n\"ServerInfoTimeOut\"=dword:00000000\r\n";
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("http://secsay.com");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnCanel = new System.Windows.Forms.Button();
            this.labHelp = new System.Windows.Forms.Label();
            this.labTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCanel
            // 
            this.btnCanel.Location = new System.Drawing.Point(263, 333);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(138, 23);
            this.btnCanel.TabIndex = 1;
            this.btnCanel.Text = "关闭";
            this.btnCanel.UseVisualStyleBackColor = true;
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // labHelp
            // 
            this.labHelp.AutoSize = true;
            this.labHelp.Location = new System.Drawing.Point(12, 39);
            this.labHelp.Name = "labHelp";
            this.labHelp.Size = new System.Drawing.Size(29, 12);
            this.labHelp.TabIndex = 3;
            this.labHelp.Text = "test";
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Location = new System.Drawing.Point(185, 9);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(83, 12);
            this.labTitle.TabIndex = 4;
            this.labTitle.Text = "hosts修改帮助";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "工具里的“修改注册表使hosts修改立即生效”说明：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 193);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(475, 127);
            this.textBox1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 333);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "前往secsay.com";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HelpForm
            // 
            this.ClientSize = new System.Drawing.Size(501, 368);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labTitle);
//            this.Controls.Add(this.label1);
            this.Controls.Add(this.labHelp);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpForm";
            this.Text = "帮助";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

