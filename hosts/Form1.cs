namespace XNMD
{
    using Microsoft.VisualBasic;
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class Form1 : Form
    {
        private IContainer components;
        private ToolStripMenuItem ConfigToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private DataGridView dataGridView1;
        private ToolStripMenuItem EditHostsToolStripMenuItem;
        private ToolStripMenuItem EditRegToolStripMenuItem;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem HelpToolStripMenuItem;
        private ToolStripMenuItem LinksToolStripMenuItem;
        private ToolStripMenuItem menuAdd;
        private ToolStripMenuItem menuConfig;
        private ToolStripMenuItem menuDel;
        private ToolStripMenuItem menuSave;
        private ToolStripMenuItem menuSaveas;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem OpenSysDirToolStripMenuItem;
        private ToolStripMenuItem SaveAsToolStripMenuItem;
        private ToolStripMenuItem SaveHistoryToolStripMenuItem;
        private ToolStripMenuItem SaveToolStripMenuItem;
        private ToolStripMenuItem ToolsHostsToolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private DataGridViewTextBoxColumn colIP;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colDesc;
        private DataGridViewCheckBoxColumn colUse;
        private DataGridViewCheckBoxColumn colSpace;
        private ToolStripMenuItem 帮助ToolStripMenuItem; 
        private ToolStripSeparator toolStripSeparator3;

        public Form1()
        {
            this.InitializeComponent();
            HostsDal.BackupHosts();
            this.BindHosts(string.Empty);
            this.BindHistory();
            this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void BindHistory()
        {
            this.LinksToolStripMenuItem.DropDownItems.Clear();
            ToolStripMenuItem item = new ToolStripMenuItem("当前应用");
            item.Click += new EventHandler(this.LinkItemStripMenuItem_Click);
            this.LinksToolStripMenuItem.DropDownItems.Add(item);
            foreach (string str in HostsDal.GetHostHistory())
            {
                item = new ToolStripMenuItem(str);
                item.Click += new EventHandler(this.LinkItemStripMenuItem_Click);
                this.LinksToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void BindHosts(string name)
        {
            this.dataGridView1.Rows.Clear();
            List<HostItem> hosts = HostsDal.GetHosts(name);
            
            if (hosts != null)
            {
                foreach (HostItem item in hosts)
                {
                    
                    this.dataGridView1.Rows.Add(new object[] { item.IP, item.Name, item.Description, item.IsUsing, item.AddLine });
                }
            }
        }

        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ConfigForm().ShowDialog(this);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (((e.RowIndex >= 0) && (e.RowIndex < this.dataGridView1.Rows.Count)) && ((e.ColumnIndex == 3) || (e.ColumnIndex == 4)))
            {
                object obj2 = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (obj2 == null)
                {
                    obj2 = false;
                }
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !((bool) obj2);
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (e.RowIndex >= 0))
            {
                this.dataGridView1.ClearSelection();
                int num = (e.ColumnIndex >= 0) ? e.ColumnIndex : 0;
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[num];
                this.dataGridView1.Rows[e.RowIndex].Cells[num].Selected = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void EditHostsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", HostsDal.HostsPath);
        }

        private void EditRegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";
            using (RegistryKey key = Registry.CurrentUser)
            {
                using (RegistryKey key2 = key.OpenSubKey(name, true))
                {
                    key2.SetValue("DnsCacheEnabled", 0, RegistryValueKind.DWord);
                    key2.SetValue("DnsCacheTimeout", 0, RegistryValueKind.DWord);
                    key2.SetValue("ServerInfoTimeOut", 0, RegistryValueKind.DWord);
                }
            }
            MessageBox.Show("操作成功");
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private List<HostItem> GetGridHost()
        {
            List<HostItem> list = new List<HostItem>();
            foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
            {
                string str = Convert.ToString(row.Cells[0].Value).Trim();
                string name = Convert.ToString(row.Cells[1].Value).Trim();
                string str2 = Convert.ToString(row.Cells[2].Value).Trim();
                bool use = Convert.ToBoolean(row.Cells[3].Value);
                bool addLine = Convert.ToBoolean(row.Cells[4].Value);
                if ((!string.IsNullOrEmpty(str) || !string.IsNullOrEmpty(name)) || !string.IsNullOrEmpty(str2))
                {
                    if (use && (list.Find(delegate (HostItem i) {
                        return i.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && i.IsUsing;
                    }) != null))
                    {
                        MessageBox.Show("域名：" + name + "存在重复配置，请检查后再重新保存");
                        return null;
                    }
                    HostItem item = new HostItem(str, name, str2, use, addLine);
                    list.Add(item);
                }
            }
            return list;
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new HelpForm().ShowDialog(this);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSpace = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuDel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSaveas = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LinksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsHostsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenSysDirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditHostsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditRegToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIP,
            this.colName,
            this.colDesc,
            this.colUse,
            this.colSpace});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(673, 256);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            // 
            // colIP
            // 
            this.colIP.HeaderText = "IP";
            this.colIP.Name = "colIP";
            this.colIP.Width = 120;
            // 
            // colName
            // 
            this.colName.HeaderText = "对应域名";
            this.colName.Name = "colName";
            this.colName.Width = 200;
            // 
            // colDesc
            // 
            this.colDesc.HeaderText = "说明";
            this.colDesc.Name = "colDesc";
            this.colDesc.Width = 200;
            // 
            // colUse
            // 
            this.colUse.FalseValue = "false";
            this.colUse.HeaderText = "是否应用";
            this.colUse.Name = "colUse";
            this.colUse.ToolTipText = "取消表示用#注释该行";
            this.colUse.TrueValue = "true";
            this.colUse.Width = 60;
            // 
            // colSpace
            // 
            this.colSpace.HeaderText = "加空行";
            this.colSpace.Name = "colSpace";
            this.colSpace.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSpace.ToolTipText = "在行下方添加空行";
            this.colSpace.Width = 50;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDel,
            this.menuAdd,
            this.toolStripSeparator1,
            this.menuSaveas,
            this.toolStripSeparator2,
            this.menuConfig,
            this.toolStripSeparator3,
            this.menuSave,
            this.帮助ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(170, 176);
            // 
            // menuDel
            // 
            this.menuDel.Name = "menuDel";
            this.menuDel.Size = new System.Drawing.Size(169, 22);
            this.menuDel.Text = "删除行";
            this.menuDel.Click += new System.EventHandler(this.menuDel_Click);
            // 
            // menuAdd
            // 
            this.menuAdd.Name = "menuAdd";
            this.menuAdd.Size = new System.Drawing.Size(169, 22);
            this.menuAdd.Text = "新增行";
            this.menuAdd.Click += new System.EventHandler(this.menuAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // menuSaveas
            // 
            this.menuSaveas.Name = "menuSaveas";
            this.menuSaveas.Size = new System.Drawing.Size(169, 22);
            this.menuSaveas.Text = "另存为快捷方式...";
            this.menuSaveas.Click += new System.EventHandler(this.menuSaveas_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
            // 
            // menuConfig
            // 
            this.menuConfig.Name = "menuConfig";
            this.menuConfig.Size = new System.Drawing.Size(169, 22);
            this.menuConfig.Text = "配置...";
            this.menuConfig.Click += new System.EventHandler(this.menuConfig_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(166, 6);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(169, 22);
            this.menuSave.Text = "立即应用";
            this.menuSave.Click += new System.EventHandler(this.menuSave_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.LinksToolStripMenuItem,
            this.HelpToolStripMenuItem,
            this.toolStripMenuItem3,
            this.SaveToolStripMenuItem,
            this.ToolsHostsToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(673, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveHistoryToolStripMenuItem,
            this.SaveAsToolStripMenuItem,
            this.ConfigToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.FileToolStripMenuItem.Text = "文件";
            // 
            // SaveHistoryToolStripMenuItem
            // 
            this.SaveHistoryToolStripMenuItem.Name = "SaveHistoryToolStripMenuItem";
            this.SaveHistoryToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.SaveHistoryToolStripMenuItem.Text = "保存为";
            this.SaveHistoryToolStripMenuItem.Visible = false;
            this.SaveHistoryToolStripMenuItem.Click += new System.EventHandler(this.SaveHistoryToolStripMenuItem_Click);
            // 
            // SaveAsToolStripMenuItem
            // 
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.SaveAsToolStripMenuItem.Text = "另存为快捷方式...";
            this.SaveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // ConfigToolStripMenuItem
            // 
            this.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            this.ConfigToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.ConfigToolStripMenuItem.Text = "配置...";
            this.ConfigToolStripMenuItem.Click += new System.EventHandler(this.ConfigToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // LinksToolStripMenuItem
            // 
            this.LinksToolStripMenuItem.Name = "LinksToolStripMenuItem";
            this.LinksToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.LinksToolStripMenuItem.Text = "快捷方式列表";
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.HelpToolStripMenuItem.Text = "帮助";
            this.HelpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(12, 21);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.SaveToolStripMenuItem.Text = "立即应用";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // ToolsHostsToolStripMenuItem1
            // 
            this.ToolsHostsToolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ToolsHostsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenSysDirToolStripMenuItem,
            this.EditHostsToolStripMenuItem,
            this.EditRegToolStripMenuItem});
            this.ToolsHostsToolStripMenuItem1.Name = "ToolsHostsToolStripMenuItem1";
            this.ToolsHostsToolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.ToolsHostsToolStripMenuItem1.Text = "工具";
            // 
            // OpenSysDirToolStripMenuItem
            // 
            this.OpenSysDirToolStripMenuItem.Name = "OpenSysDirToolStripMenuItem";
            this.OpenSysDirToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.OpenSysDirToolStripMenuItem.Text = "打开Hosts目录";
            this.OpenSysDirToolStripMenuItem.Click += new System.EventHandler(this.OpenSysDirToolStripMenuItem_Click);
            // 
            // EditHostsToolStripMenuItem
            // 
            this.EditHostsToolStripMenuItem.Name = "EditHostsToolStripMenuItem";
            this.EditHostsToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.EditHostsToolStripMenuItem.Text = "手动编辑Hosts";
            this.EditHostsToolStripMenuItem.Click += new System.EventHandler(this.EditHostsToolStripMenuItem_Click);
            // 
            // EditRegToolStripMenuItem
            // 
            this.EditRegToolStripMenuItem.Name = "EditRegToolStripMenuItem";
            this.EditRegToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.EditRegToolStripMenuItem.Text = "修改注册表使hosts立即生效";
            this.EditRegToolStripMenuItem.Click += new System.EventHandler(this.EditRegToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // openFileDialog1
            // 
            
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(673, 281);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Hosts修改";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void LinkItemStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = ((ToolStripMenuItem) sender).Text;
            if (text == "当前应用")
            {
                text = string.Empty;
                this.SaveHistoryToolStripMenuItem.Visible = false;
            }
            else
            {
                this.SaveHistoryToolStripMenuItem.Visible = true;
                this.SaveHistoryToolStripMenuItem.Text = "保存 " + text;
                if (HostsDal.LinkQuickUse && File.Exists(HostsDal.GetFileName(text)))
                {
                    File.Copy(HostsDal.GetFileName(text), HostsDal.HostsPath, true);
                    MessageBox.Show("应用成功");
                }
            }
            this.BindHosts(text);
        }

        private void menuAdd_Click(object sender, EventArgs e)
        {
            this.dataGridView1.ClearSelection();
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[0];
            this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[0].Selected = true;
            this.dataGridView1.BeginEdit(true);
        }

        private void menuConfig_Click(object sender, EventArgs e)
        {
            this.ConfigToolStripMenuItem_Click(null, null);
        }

        private void menuDel_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentCell.OwningRow);
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            this.SaveToolStripMenuItem_Click(null, null);
        }

        private void menuSaveas_Click(object sender, EventArgs e)
        {
            this.SaveAsToolStripMenuItem_Click(null, null);
        }

        private void OpenSysDirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(HostsDal.SysDir);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = Interaction.InputBox("请输入快捷方式名", "请输入快捷方式名", "", 100, 100);
            if (!string.IsNullOrEmpty(str) && (!File.Exists(HostsDal.GetFileName(str)) || (MessageBox.Show("快捷方式文件已存在，是否覆盖？", "文件已存在", MessageBoxButtons.YesNo) == DialogResult.Yes)))
            {
                List<HostItem> gridHost = this.GetGridHost();
                if (gridHost != null)
                {
                    HostsDal.SaveHosts(str, gridHost);
                    this.BindHistory();
                }
            }
        }

        private void SaveHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = ((ToolStripMenuItem) sender).Text.Split(new char[] { ' ' })[1];
            List<HostItem> gridHost = this.GetGridHost();
            if (gridHost != null)
            {
                HostsDal.SaveHosts(name, gridHost);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<HostItem> gridHost = this.GetGridHost();
            if (gridHost != null)
            {
                HostsDal.SaveHosts(string.Empty, gridHost);
                this.BindHosts(string.Empty);
                MessageBox.Show("应用成功");
            }
        }


      
    }
}

