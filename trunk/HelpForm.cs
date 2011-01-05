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
            this.labHelp.Text = "\r\n新增：在最下方的空行里输入，即可新增行\r\n\r\n修改：双击需要修改的内容，即可修改\r\n\r\n删除：单击行左侧的灰色方格，按Del键\r\n\r\n是否应用：勾选时表示应用，取消勾选时表示这行是注释\r\n\r\n加空行：保存时在该条记录下方添加一个空行";
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
            this.btnCanel = new Button();
            this.labHelp = new Label();
            this.labTitle = new Label();
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.button1 = new Button();
            base.SuspendLayout();
            this.btnCanel.Location = new Point(0xef, 0x14d);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new Size(0x8a, 0x17);
            this.btnCanel.TabIndex = 1;
            this.btnCanel.Text = "关闭";
            this.btnCanel.UseVisualStyleBackColor = true;
            this.btnCanel.Click += new EventHandler(this.btnCanel_Click);
            this.labHelp.AutoSize = true;
            this.labHelp.Location = new Point(12, 0x27);
            this.labHelp.Name = "labHelp";
            this.labHelp.Size = new Size(0x17, 12);
            this.labHelp.TabIndex = 3;
            this.labHelp.Text = "111";
            this.labTitle.AutoSize = true;
            this.labTitle.Location = new Point(0x62, 9);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new Size(0x65, 12);
            this.labTitle.TabIndex = 4;
            this.labTitle.Text = "hosts修改工具1.0";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0xb2);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x11f, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "工具里的“修改注册表使hosts修改立即生效”说明：";
            this.textBox1.Location = new Point(14, 0xc1);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x1db, 0x7f);
            this.textBox1.TabIndex = 5;
            this.button1.Location = new Point(0x2f, 0x14d);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x8f, 0x17);
            this.button1.TabIndex = 1;
            this.button1.Text = "前往secsay.com";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1f5, 0x170);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.labTitle);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.labHelp);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.btnCanel);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "HelpForm";
            this.Text = "帮助";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

