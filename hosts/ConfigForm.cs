namespace XNMD
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public class ConfigForm : Form
    {
        private Button btnCanel;
        private Button btnSave;
        private IContainer components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private RadioButton radAddComment;
        private RadioButton radDelComment;
        private RadioButton radGB2312;
        private RadioButton radNoComment;
        private RadioButton radNoQuick;
        private RadioButton radQuickUse;
        private RadioButton radSaveComment;
        private RadioButton radUTF8;

        public ConfigForm()
        {
            this.InitializeComponent();
            if (HostsDal.Encode == Encoding.UTF8)
            {
                this.radUTF8.Checked = true;
            }
            if (!HostsDal.LinkQuickUse)
            {
                this.radNoQuick.Checked = true;
            }
            if (!HostsDal.AddMicroComment)
            {
                this.radNoComment.Checked = true;
            }
            if (!HostsDal.SaveComment)
            {
                this.radDelComment.Checked = true;
            }
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string encode = this.radUTF8.Checked ? "UTF-8" : "GB2312";
            string linkQuickUse = this.radNoQuick.Checked ? "0" : "1";
            string addMicroComment = this.radNoComment.Checked ? "0" : "1";
            string saveComment = this.radDelComment.Checked ? "0" : "1";
            HostsDal.SaveConfig(encode, linkQuickUse, addMicroComment, saveComment);
            base.Close();
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
            this.groupBox1 = new GroupBox();
            this.radUTF8 = new RadioButton();
            this.radGB2312 = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.radNoQuick = new RadioButton();
            this.radQuickUse = new RadioButton();
            this.groupBox3 = new GroupBox();
            this.radNoComment = new RadioButton();
            this.radAddComment = new RadioButton();
            this.btnSave = new Button();
            this.btnCanel = new Button();
            this.groupBox4 = new GroupBox();
            this.radDelComment = new RadioButton();
            this.radSaveComment = new RadioButton();
//            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.radUTF8);
            this.groupBox1.Controls.Add(this.radGB2312);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x114, 0x2f);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "hosts文件编码";
            this.radUTF8.AutoSize = true;
            this.radUTF8.Location = new Point(0x6d, 20);
            this.radUTF8.Name = "radUTF8";
            this.radUTF8.Size = new Size(0x35, 0x10);
            this.radUTF8.TabIndex = 1;
            this.radUTF8.Text = "UTF-8";
            this.radUTF8.UseVisualStyleBackColor = true;
            this.radGB2312.AutoSize = true;
            this.radGB2312.Checked = true;
            this.radGB2312.Location = new Point(6, 20);
            this.radGB2312.Name = "radGB2312";
            this.radGB2312.Size = new Size(0x3b, 0x10);
            this.radGB2312.TabIndex = 0;
            this.radGB2312.TabStop = true;
            this.radGB2312.Text = "GB2312";
            this.radGB2312.UseVisualStyleBackColor = true;
            this.groupBox2.Controls.Add(this.radNoQuick);
            this.groupBox2.Controls.Add(this.radQuickUse);
            this.groupBox2.Location = new Point(12, 0x4b);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x114, 0x2f);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择快捷方式时，立即应用";
            this.radNoQuick.AutoSize = true;
            this.radNoQuick.Location = new Point(0x6d, 20);
            this.radNoQuick.Name = "radNoQuick";
            this.radNoQuick.Size = new Size(0x9b, 0x10);
            this.radNoQuick.TabIndex = 1;
            this.radNoQuick.Text = "通过点击菜单的立即应用";
            this.radNoQuick.UseVisualStyleBackColor = true;
            this.radQuickUse.AutoSize = true;
            this.radQuickUse.Checked = true;
            this.radQuickUse.Location = new Point(6, 20);
            this.radQuickUse.Name = "radQuickUse";
            this.radQuickUse.Size = new Size(0x47, 0x10);
            this.radQuickUse.TabIndex = 0;
            this.radQuickUse.TabStop = true;
            this.radQuickUse.Text = "立即应用";
            this.radQuickUse.UseVisualStyleBackColor = true;
            this.groupBox3.Controls.Add(this.radNoComment);
            this.groupBox3.Controls.Add(this.radAddComment);
            this.groupBox3.Location = new Point(12, 0xd9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x114, 0x2f);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "保存时，是否添加微软标准注释";
            this.radNoComment.AutoSize = true;
            this.radNoComment.Location = new Point(0x6d, 20);
            this.radNoComment.Name = "radNoComment";
            this.radNoComment.Size = new Size(0x3b, 0x10);
            this.radNoComment.TabIndex = 1;
            this.radNoComment.Text = "不添加";
            this.radNoComment.UseVisualStyleBackColor = true;
            this.radAddComment.AutoSize = true;
            this.radAddComment.Checked = true;
            this.radAddComment.Location = new Point(6, 20);
            this.radAddComment.Name = "radAddComment";
            this.radAddComment.Size = new Size(0x2f, 0x10);
            this.radAddComment.TabIndex = 0;
            this.radAddComment.TabStop = true;
            this.radAddComment.Text = "添加";
            this.radAddComment.UseVisualStyleBackColor = true;
            this.btnSave.Location = new Point(0x1f, 280);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(0x4b, 0x17);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            this.btnCanel.Location = new Point(0xb1, 280);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new Size(0x4b, 0x17);
            this.btnCanel.TabIndex = 1;
            this.btnCanel.Text = "取消";
            this.btnCanel.UseVisualStyleBackColor = true;
            this.btnCanel.Click += new EventHandler(this.btnCanel_Click);
            this.groupBox4.Controls.Add(this.radDelComment);
            this.groupBox4.Controls.Add(this.radSaveComment);
            this.groupBox4.Location = new Point(12, 0x90);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x114, 0x2f);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "是否保存注释的host记录";
            this.radDelComment.AutoSize = true;
            this.radDelComment.Location = new Point(0x6d, 20);
            this.radDelComment.Name = "radDelComment";
            this.radDelComment.Size = new Size(0x2f, 0x10);
            this.radDelComment.TabIndex = 1;
            this.radDelComment.Text = "删除";
            this.radDelComment.UseVisualStyleBackColor = true;
            this.radSaveComment.AutoSize = true;
            this.radSaveComment.Checked = true;
            this.radSaveComment.Location = new Point(6, 20);
            this.radSaveComment.Name = "radSaveComment";
            this.radSaveComment.Size = new Size(0x2f, 0x10);
            this.radSaveComment.TabIndex = 0;
            this.radSaveComment.TabStop = true;
            this.radSaveComment.Text = "保存";
            this.radSaveComment.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(300, 0x150);
            base.Controls.Add(this.btnCanel);
            base.Controls.Add(this.btnSave);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
//            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ConfigForm";
            this.Text = "配置-beinet.cn";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            base.ResumeLayout(false);
        }
    }
}

