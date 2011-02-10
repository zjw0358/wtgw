
partial class UAUserInterface {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private Secsay.UAEngine xa;
    
    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UAUserInterface));
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.chkbEnabled = new System.Windows.Forms.CheckBox();
        this.chkbEnableDomainFilter = new System.Windows.Forms.CheckBox();
        this.chkbFilterReq = new System.Windows.Forms.CheckBox();
        this.chkbFilterRes = new System.Windows.Forms.CheckBox();
        this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
        this.lbDelayPeriod = new System.Windows.Forms.Label();
        this.lbBatchSize = new System.Windows.Forms.Label();
        this.tbDelayPeriod = new System.Windows.Forms.TextBox();
        this.tbBatchSize = new System.Windows.Forms.TextBox();
        this.chkbEnableResultFiltering = new System.Windows.Forms.CheckBox();
        this.chkbInjectQueryParam = new System.Windows.Forms.CheckBox();
        this.chkbAutoInjectPost = new System.Windows.Forms.CheckBox();
        this.chkbCheckRequestCanary = new System.Windows.Forms.CheckBox();
        this.tbCanary = new System.Windows.Forms.TextBox();
        this.resultsTP = new System.Windows.Forms.TabPage();
        this.splitContainer1 = new System.Windows.Forms.SplitContainer();
        this.panelResultsGrid = new System.Windows.Forms.Panel();
        this.panelResultButtons = new System.Windows.Forms.Panel();
        this.clearBtn = new System.Windows.Forms.Button();
        this.btnEasyFilter = new System.Windows.Forms.Button();
        this.ResultsDataGridView = new System.Windows.Forms.DataGridView();
        this.contextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.transformationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.CodePoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.responseResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
        this.richTextBox1 = new System.Windows.Forms.RichTextBox();
        this.ConfigTP = new System.Windows.Forms.TabPage();
        this.gbGeneralOptions = new System.Windows.Forms.GroupBox();
        this.chkbAutoCheckForUpdates = new System.Windows.Forms.CheckBox();
        this.CheckLatestButton = new System.Windows.Forms.Button();
        this.injectionOptionsGroupBox = new System.Windows.Forms.GroupBox();
        this.chkbThrottleRequests = new System.Windows.Forms.CheckBox();
        this.encodingOptionsGroupBox = new System.Windows.Forms.GroupBox();
        this.chkbEncodeQueryStringParam = new System.Windows.Forms.CheckBox();
        this.chkbUrlEncodeBodyMatches = new System.Windows.Forms.CheckBox();
        this.chkbUrlEncodeHeaderMatches = new System.Windows.Forms.CheckBox();
        this.gbResultFilterOpt = new System.Windows.Forms.GroupBox();
        this.lblCanary = new System.Windows.Forms.Label();
        this.domainFilteringGroupBox = new System.Windows.Forms.GroupBox();
        this.lbDomainFilters = new System.Windows.Forms.ListBox();
        this.lblFilteredDomains = new System.Windows.Forms.Label();
        this.btnAddToDomainFilterList = new System.Windows.Forms.Button();
        this.btnRemoveDomainFilter = new System.Windows.Forms.Button();
        this.label3 = new System.Windows.Forms.Label();
        this.tbDomain = new System.Windows.Forms.TextBox();
        this.lblFilteredDomainsAdd = new System.Windows.Forms.Label();
        this.tabControl1 = new System.Windows.Forms.TabControl();
        this.tpChrConfiguration = new System.Windows.Forms.TabPage();
        this.lblTargetCodePointText = new System.Windows.Forms.Label();
        this.lblSourceCodePointText = new System.Windows.Forms.Label();
        this.lblTargetCodePoint = new System.Windows.Forms.Label();
        this.lblSourceCodePoint = new System.Windows.Forms.Label();
        this.lblChrNameValue = new System.Windows.Forms.Label();
        this.lblChrDescription = new System.Windows.Forms.Label();
        this.lblChrName = new System.Windows.Forms.Label();
        this.lblFilter = new System.Windows.Forms.Label();
        this.cbFilter = new System.Windows.Forms.ComboBox();
        this.dgUnicodeTestMappings = new System.Windows.Forms.DataGridView();
        this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        this.sourcePointDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.targetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.UAUnicodeCharMappingBindingSource = new System.Windows.Forms.BindingSource(this.components);
        this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
        this.richTextBox2 = new System.Windows.Forms.RichTextBox();
        this.resultsTP.SuspendLayout();
        this.splitContainer1.Panel1.SuspendLayout();
        this.splitContainer1.Panel2.SuspendLayout();
        this.splitContainer1.SuspendLayout();
        this.panelResultsGrid.SuspendLayout();
        this.panelResultButtons.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.ResultsDataGridView)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.responseResultBindingSource)).BeginInit();
        this.ConfigTP.SuspendLayout();
        this.gbGeneralOptions.SuspendLayout();
        this.injectionOptionsGroupBox.SuspendLayout();
        this.encodingOptionsGroupBox.SuspendLayout();
        this.gbResultFilterOpt.SuspendLayout();
        this.domainFilteringGroupBox.SuspendLayout();
        this.tabControl1.SuspendLayout();
        this.tpChrConfiguration.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgUnicodeTestMappings)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.UAUnicodeCharMappingBindingSource)).BeginInit();
        this.SuspendLayout();
        // 
        // chkbEnabled
        // 
        this.chkbEnabled.AutoSize = true;
        this.chkbEnabled.Location = new System.Drawing.Point(6, 6);
        this.chkbEnabled.Name = "chkbEnabled";
        this.chkbEnabled.Size = new System.Drawing.Size(60, 16);
        this.chkbEnabled.TabIndex = 1;
        this.chkbEnabled.Text = "Enable";
        this.toolTip1.SetToolTip(this.chkbEnabled, "Check to enable the XSS automation testing.");
        this.chkbEnabled.UseVisualStyleBackColor = true;
        this.chkbEnabled.CheckedChanged += new System.EventHandler(this.enabledChkBox_CheckedChanged);
        // 
        // chkbEnableDomainFilter
        // 
        this.chkbEnableDomainFilter.AutoSize = true;
        this.chkbEnableDomainFilter.Enabled = false;
        this.chkbEnableDomainFilter.Location = new System.Drawing.Point(6, 61);
        this.chkbEnableDomainFilter.Name = "chkbEnableDomainFilter";
        this.chkbEnableDomainFilter.Size = new System.Drawing.Size(192, 16);
        this.chkbEnableDomainFilter.TabIndex = 22;
        this.chkbEnableDomainFilter.Text = "Enable Domain Name Targeting";
        this.toolTip1.SetToolTip(this.chkbEnableDomainFilter, "Enable targeted analysis of domains.");
        this.chkbEnableDomainFilter.UseVisualStyleBackColor = true;
        this.chkbEnableDomainFilter.CheckedChanged += new System.EventHandler(this.chkbEnableDomainFilter_CheckedChanged);
        // 
        // chkbFilterReq
        // 
        this.chkbFilterReq.AutoSize = true;
        this.chkbFilterReq.Enabled = false;
        this.chkbFilterReq.Location = new System.Drawing.Point(90, 21);
        this.chkbFilterReq.Name = "chkbFilterReq";
        this.chkbFilterReq.Size = new System.Drawing.Size(72, 16);
        this.chkbFilterReq.TabIndex = 27;
        this.chkbFilterReq.Text = "Requests";
        this.toolTip1.SetToolTip(this.chkbFilterReq, "If this box is checked requests will be filtered.");
        this.chkbFilterReq.UseVisualStyleBackColor = true;
        this.chkbFilterReq.CheckedChanged += new System.EventHandler(this.chkbFilterReq_CheckedChanged);
        // 
        // chkbFilterRes
        // 
        this.chkbFilterRes.AutoSize = true;
        this.chkbFilterRes.Enabled = false;
        this.chkbFilterRes.Location = new System.Drawing.Point(90, 44);
        this.chkbFilterRes.Name = "chkbFilterRes";
        this.chkbFilterRes.Size = new System.Drawing.Size(78, 16);
        this.chkbFilterRes.TabIndex = 28;
        this.chkbFilterRes.Text = "Responses";
        this.toolTip1.SetToolTip(this.chkbFilterRes, "If this box is checked responses will be filtered. ");
        this.chkbFilterRes.UseVisualStyleBackColor = true;
        this.chkbFilterRes.CheckedChanged += new System.EventHandler(this.chkbFilterRes_CheckedChanged);
        // 
        // checkedListBox1
        // 
        this.checkedListBox1.CheckOnClick = true;
        this.checkedListBox1.Enabled = false;
        this.checkedListBox1.FormattingEnabled = true;
        this.checkedListBox1.Items.AddRange(new object[] {
            "None",
            "Replacement",
            "Transformed",
            "Encoded",
            "ShortestForm",
            "Unknown"});
        this.checkedListBox1.Location = new System.Drawing.Point(8, 22);
        this.checkedListBox1.Name = "checkedListBox1";
        this.checkedListBox1.Size = new System.Drawing.Size(461, 52);
        this.checkedListBox1.TabIndex = 20;
        this.toolTip1.SetToolTip(this.checkedListBox1, "Filter the results view.");
        this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
        // 
        // lbDelayPeriod
        // 
        this.lbDelayPeriod.AutoSize = true;
        this.lbDelayPeriod.Location = new System.Drawing.Point(5, 71);
        this.lbDelayPeriod.Name = "lbDelayPeriod";
        this.lbDelayPeriod.Size = new System.Drawing.Size(113, 12);
        this.lbDelayPeriod.TabIndex = 34;
        this.lbDelayPeriod.Text = "Delay period (ms):";
        this.toolTip1.SetToolTip(this.lbDelayPeriod, "Number of milliseconds to delay prior to sending each batch of generated requests" +
                ".");
        // 
        // lbBatchSize
        // 
        this.lbBatchSize.AutoSize = true;
        this.lbBatchSize.Location = new System.Drawing.Point(5, 93);
        this.lbBatchSize.Name = "lbBatchSize";
        this.lbBatchSize.Size = new System.Drawing.Size(71, 12);
        this.lbBatchSize.TabIndex = 35;
        this.lbBatchSize.Text = "Batch size:";
        this.toolTip1.SetToolTip(this.lbBatchSize, "Maximum number of requests to send between delay periods.");
        // 
        // tbDelayPeriod
        // 
        this.tbDelayPeriod.Location = new System.Drawing.Point(106, 68);
        this.tbDelayPeriod.Name = "tbDelayPeriod";
        this.tbDelayPeriod.Size = new System.Drawing.Size(100, 21);
        this.tbDelayPeriod.TabIndex = 36;
        this.toolTip1.SetToolTip(this.tbDelayPeriod, "Number of milliseconds to delay prior to sending each batch of generated requests" +
                ".");
        this.tbDelayPeriod.TextChanged += new System.EventHandler(this.tbDelayPeriod_TextChanged);
        // 
        // tbBatchSize
        // 
        this.tbBatchSize.Location = new System.Drawing.Point(106, 90);
        this.tbBatchSize.Name = "tbBatchSize";
        this.tbBatchSize.Size = new System.Drawing.Size(100, 21);
        this.tbBatchSize.TabIndex = 37;
        this.toolTip1.SetToolTip(this.tbBatchSize, "Maximum number of requests to send between delay periods.");
        this.tbBatchSize.TextChanged += new System.EventHandler(this.tbBatchSize_TextChanged);
        // 
        // chkbEnableResultFiltering
        // 
        this.chkbEnableResultFiltering.AutoSize = true;
        this.chkbEnableResultFiltering.Enabled = false;
        this.chkbEnableResultFiltering.Location = new System.Drawing.Point(6, 246);
        this.chkbEnableResultFiltering.Name = "chkbEnableResultFiltering";
        this.chkbEnableResultFiltering.Size = new System.Drawing.Size(168, 16);
        this.chkbEnableResultFiltering.TabIndex = 0;
        this.chkbEnableResultFiltering.Text = "Enable Results Filtering";
        this.toolTip1.SetToolTip(this.chkbEnableResultFiltering, "Enable this for more granular control over the results data.");
        this.chkbEnableResultFiltering.UseVisualStyleBackColor = true;
        this.chkbEnableResultFiltering.CheckedChanged += new System.EventHandler(this.chkbAdvanceFilter_CheckedChanged);
        // 
        // chkbInjectQueryParam
        // 
        this.chkbInjectQueryParam.AutoSize = true;
        this.chkbInjectQueryParam.Location = new System.Drawing.Point(8, 19);
        this.chkbInjectQueryParam.Name = "chkbInjectQueryParam";
        this.chkbInjectQueryParam.Size = new System.Drawing.Size(198, 16);
        this.chkbInjectQueryParam.TabIndex = 19;
        this.chkbInjectQueryParam.Text = "Inject GET request parameters";
        this.toolTip1.SetToolTip(this.chkbInjectQueryParam, "All GET request query-string parameters will be parsed and tested.");
        this.chkbInjectQueryParam.UseVisualStyleBackColor = true;
        this.chkbInjectQueryParam.CheckedChanged += new System.EventHandler(this.chkbInjectQueryParam_CheckedChanged);
        // 
        // chkbAutoInjectPost
        // 
        this.chkbAutoInjectPost.AutoSize = true;
        this.chkbAutoInjectPost.Location = new System.Drawing.Point(246, 19);
        this.chkbAutoInjectPost.Name = "chkbAutoInjectPost";
        this.chkbAutoInjectPost.Size = new System.Drawing.Size(204, 16);
        this.chkbAutoInjectPost.TabIndex = 31;
        this.chkbAutoInjectPost.Text = "Inject POST request parameters";
        this.toolTip1.SetToolTip(this.chkbAutoInjectPost, "All POST request body parameters will be parsed and tested using the included par" +
                "sers.  Custom parsers can be added to x5s through code.");
        this.chkbAutoInjectPost.UseVisualStyleBackColor = true;
        this.chkbAutoInjectPost.CheckedChanged += new System.EventHandler(this.chkbAutoInjectPost_CheckedChanged);
        // 
        // chkbCheckRequestCanary
        // 
        this.chkbCheckRequestCanary.AutoSize = true;
        this.chkbCheckRequestCanary.Location = new System.Drawing.Point(246, 42);
        this.chkbCheckRequestCanary.Name = "chkbCheckRequestCanary";
        this.chkbCheckRequestCanary.Size = new System.Drawing.Size(174, 16);
        this.chkbCheckRequestCanary.TabIndex = 32;
        this.chkbCheckRequestCanary.Text = "Inject \"Other\" parameters";
        this.toolTip1.SetToolTip(this.chkbCheckRequestCanary, resources.GetString("chkbCheckRequestCanary.ToolTip"));
        this.chkbCheckRequestCanary.UseVisualStyleBackColor = true;
        this.chkbCheckRequestCanary.CheckedChanged += new System.EventHandler(this.chkbCheckRequestCanary_CheckedChanged);
        // 
        // tbCanary
        // 
        this.tbCanary.Enabled = false;
        this.tbCanary.Location = new System.Drawing.Point(66, 30);
        this.tbCanary.Name = "tbCanary";
        this.tbCanary.Size = new System.Drawing.Size(222, 21);
        this.tbCanary.TabIndex = 33;
        this.toolTip1.SetToolTip(this.tbCanary, "Enter a unique string like \'pqqz\' or \'test123\' that x5s will use to extract resul" +
                "ts from each response.");
        this.tbCanary.TextChanged += new System.EventHandler(this.tbCanary_TextChanged);
        // 
        // resultsTP
        // 
        this.resultsTP.AutoScroll = true;
        this.resultsTP.BackColor = System.Drawing.Color.Transparent;
        this.resultsTP.Controls.Add(this.splitContainer1);
        this.resultsTP.Location = new System.Drawing.Point(4, 22);
        this.resultsTP.Name = "resultsTP";
        this.resultsTP.Padding = new System.Windows.Forms.Padding(3);
        this.resultsTP.Size = new System.Drawing.Size(515, 643);
        this.resultsTP.TabIndex = 2;
        this.resultsTP.Text = "Results";
        this.resultsTP.UseVisualStyleBackColor = true;
        // 
        // splitContainer1
        // 
        this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitContainer1.Location = new System.Drawing.Point(3, 3);
        this.splitContainer1.Name = "splitContainer1";
        this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
        // 
        // splitContainer1.Panel1
        // 
        this.splitContainer1.Panel1.Controls.Add(this.panelResultsGrid);
        // 
        // splitContainer1.Panel2
        // 
        this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
        this.splitContainer1.Size = new System.Drawing.Size(509, 637);
        this.splitContainer1.SplitterDistance = 275;
        this.splitContainer1.TabIndex = 1;
        // 
        // panelResultsGrid
        // 
        this.panelResultsGrid.Controls.Add(this.panelResultButtons);
        this.panelResultsGrid.Controls.Add(this.ResultsDataGridView);
        this.panelResultsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        this.panelResultsGrid.Location = new System.Drawing.Point(0, 0);
        this.panelResultsGrid.Name = "panelResultsGrid";
        this.panelResultsGrid.Size = new System.Drawing.Size(509, 275);
        this.panelResultsGrid.TabIndex = 50;
        // 
        // panelResultButtons
        // 
        this.panelResultButtons.Controls.Add(this.clearBtn);
        this.panelResultButtons.Controls.Add(this.btnEasyFilter);
        this.panelResultButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.panelResultButtons.Location = new System.Drawing.Point(0, 246);
        this.panelResultButtons.Name = "panelResultButtons";
        this.panelResultButtons.Size = new System.Drawing.Size(509, 29);
        this.panelResultButtons.TabIndex = 51;
        // 
        // clearBtn
        // 
        this.clearBtn.Location = new System.Drawing.Point(113, 3);
        this.clearBtn.Name = "clearBtn";
        this.clearBtn.Size = new System.Drawing.Size(104, 26);
        this.clearBtn.TabIndex = 47;
        this.clearBtn.Text = "Clear Results";
        this.clearBtn.UseVisualStyleBackColor = true;
        this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
        // 
        // btnEasyFilter
        // 
        this.btnEasyFilter.Location = new System.Drawing.Point(3, 3);
        this.btnEasyFilter.Name = "btnEasyFilter";
        this.btnEasyFilter.Size = new System.Drawing.Size(104, 26);
        this.btnEasyFilter.TabIndex = 49;
        this.btnEasyFilter.Text = "Show Hotspots";
        this.btnEasyFilter.UseVisualStyleBackColor = true;
        this.btnEasyFilter.Click += new System.EventHandler(this.btnEasyFilter_Click);
        // 
        // ResultsDataGridView
        // 
        this.ResultsDataGridView.AllowUserToAddRows = false;
        this.ResultsDataGridView.AllowUserToOrderColumns = true;
        this.ResultsDataGridView.AutoGenerateColumns = false;
        this.ResultsDataGridView.CausesValidation = false;
        this.ResultsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.ResultsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.contextDataGridViewTextBoxColumn,
            this.transformationDataGridViewTextBoxColumn,
            this.CodePoint});
        this.ResultsDataGridView.DataSource = this.responseResultBindingSource;
        this.ResultsDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
        this.ResultsDataGridView.Location = new System.Drawing.Point(0, 0);
        this.ResultsDataGridView.Name = "ResultsDataGridView";
        this.ResultsDataGridView.ReadOnly = true;
        this.ResultsDataGridView.RowTemplate.Height = 23;
        this.ResultsDataGridView.Size = new System.Drawing.Size(509, 237);
        this.ResultsDataGridView.TabIndex = 3;
        // 
        // contextDataGridViewTextBoxColumn
        // 
        this.contextDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        this.contextDataGridViewTextBoxColumn.DataPropertyName = "Context";
        this.contextDataGridViewTextBoxColumn.HeaderText = "Context";
        this.contextDataGridViewTextBoxColumn.Name = "contextDataGridViewTextBoxColumn";
        this.contextDataGridViewTextBoxColumn.ReadOnly = true;
        // 
        // transformationDataGridViewTextBoxColumn
        // 
        this.transformationDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        this.transformationDataGridViewTextBoxColumn.DataPropertyName = "Transformation";
        this.transformationDataGridViewTextBoxColumn.HeaderText = "Transformation";
        this.transformationDataGridViewTextBoxColumn.Name = "transformationDataGridViewTextBoxColumn";
        this.transformationDataGridViewTextBoxColumn.ReadOnly = true;
        // 
        // CodePoint
        // 
        this.CodePoint.DataPropertyName = "CodePoint";
        this.CodePoint.HeaderText = "CodePoint";
        this.CodePoint.Name = "CodePoint";
        this.CodePoint.ReadOnly = true;
        // 
        // responseResultBindingSource
        // 
        this.responseResultBindingSource.DataSource = typeof(Secsay.ResponseResult);
        // 
        // richTextBox1
        // 
        this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
        this.richTextBox1.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.richTextBox1.Location = new System.Drawing.Point(0, 0);
        this.richTextBox1.Name = "richTextBox1";
        this.richTextBox1.Size = new System.Drawing.Size(509, 289);
        this.richTextBox1.TabIndex = 8;
        this.richTextBox1.Text = "";
        // 
        // ConfigTP
        // 
        this.ConfigTP.AutoScroll = true;
        this.ConfigTP.BackColor = System.Drawing.Color.Transparent;
        this.ConfigTP.Controls.Add(this.gbGeneralOptions);
        this.ConfigTP.Controls.Add(this.chkbEnableResultFiltering);
        this.ConfigTP.Controls.Add(this.injectionOptionsGroupBox);
        this.ConfigTP.Controls.Add(this.encodingOptionsGroupBox);
        this.ConfigTP.Controls.Add(this.gbResultFilterOpt);
        this.ConfigTP.Controls.Add(this.tbCanary);
        this.ConfigTP.Controls.Add(this.chkbEnableDomainFilter);
        this.ConfigTP.Controls.Add(this.lblCanary);
        this.ConfigTP.Controls.Add(this.chkbEnabled);
        this.ConfigTP.Controls.Add(this.domainFilteringGroupBox);
        this.ConfigTP.Location = new System.Drawing.Point(4, 22);
        this.ConfigTP.Name = "ConfigTP";
        this.ConfigTP.Padding = new System.Windows.Forms.Padding(3);
        this.ConfigTP.Size = new System.Drawing.Size(515, 643);
        this.ConfigTP.TabIndex = 0;
        this.ConfigTP.Text = "Configuration";
        this.ConfigTP.UseVisualStyleBackColor = true;
        // 
        // gbGeneralOptions
        // 
        this.gbGeneralOptions.Controls.Add(this.chkbAutoCheckForUpdates);
        this.gbGeneralOptions.Controls.Add(this.CheckLatestButton);
        this.gbGeneralOptions.Enabled = false;
        this.gbGeneralOptions.Location = new System.Drawing.Point(6, 580);
        this.gbGeneralOptions.Name = "gbGeneralOptions";
        this.gbGeneralOptions.Size = new System.Drawing.Size(482, 46);
        this.gbGeneralOptions.TabIndex = 46;
        this.gbGeneralOptions.TabStop = false;
        this.gbGeneralOptions.Text = "General Options";
        // 
        // chkbAutoCheckForUpdates
        // 
        this.chkbAutoCheckForUpdates.AutoSize = true;
        this.chkbAutoCheckForUpdates.Location = new System.Drawing.Point(8, 19);
        this.chkbAutoCheckForUpdates.Name = "chkbAutoCheckForUpdates";
        this.chkbAutoCheckForUpdates.Size = new System.Drawing.Size(264, 16);
        this.chkbAutoCheckForUpdates.TabIndex = 4;
        this.chkbAutoCheckForUpdates.Text = "Automatically check for updates on start";
        this.chkbAutoCheckForUpdates.UseVisualStyleBackColor = true;
        this.chkbAutoCheckForUpdates.CheckedChanged += new System.EventHandler(this.chkbAutoCheckForUpdates_CheckedChanged);
        // 
        // CheckLatestButton
        // 
        this.CheckLatestButton.AutoSize = true;
        this.CheckLatestButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.CheckLatestButton.Location = new System.Drawing.Point(338, 13);
        this.CheckLatestButton.Name = "CheckLatestButton";
        this.CheckLatestButton.Size = new System.Drawing.Size(138, 23);
        this.CheckLatestButton.TabIndex = 2;
        this.CheckLatestButton.Text = "Check for Updates";
        this.CheckLatestButton.UseVisualStyleBackColor = true;
        this.CheckLatestButton.Click += new System.EventHandler(this.CheckLatestButton_Click);
        // 
        // injectionOptionsGroupBox
        // 
        this.injectionOptionsGroupBox.Controls.Add(this.tbBatchSize);
        this.injectionOptionsGroupBox.Controls.Add(this.tbDelayPeriod);
        this.injectionOptionsGroupBox.Controls.Add(this.lbBatchSize);
        this.injectionOptionsGroupBox.Controls.Add(this.lbDelayPeriod);
        this.injectionOptionsGroupBox.Controls.Add(this.chkbThrottleRequests);
        this.injectionOptionsGroupBox.Controls.Add(this.chkbInjectQueryParam);
        this.injectionOptionsGroupBox.Controls.Add(this.chkbAutoInjectPost);
        this.injectionOptionsGroupBox.Controls.Add(this.chkbCheckRequestCanary);
        this.injectionOptionsGroupBox.Enabled = false;
        this.injectionOptionsGroupBox.Location = new System.Drawing.Point(6, 371);
        this.injectionOptionsGroupBox.Name = "injectionOptionsGroupBox";
        this.injectionOptionsGroupBox.Size = new System.Drawing.Size(482, 123);
        this.injectionOptionsGroupBox.TabIndex = 42;
        this.injectionOptionsGroupBox.TabStop = false;
        this.injectionOptionsGroupBox.Text = "Auto-Injection Options";
        // 
        // chkbThrottleRequests
        // 
        this.chkbThrottleRequests.AutoSize = true;
        this.chkbThrottleRequests.Location = new System.Drawing.Point(8, 42);
        this.chkbThrottleRequests.Name = "chkbThrottleRequests";
        this.chkbThrottleRequests.Size = new System.Drawing.Size(186, 16);
        this.chkbThrottleRequests.TabIndex = 33;
        this.chkbThrottleRequests.Text = "Throttle request generation";
        this.chkbThrottleRequests.UseVisualStyleBackColor = true;
        this.chkbThrottleRequests.CheckedChanged += new System.EventHandler(this.cbThrottleRequests_CheckedChanged);
        // 
        // encodingOptionsGroupBox
        // 
        this.encodingOptionsGroupBox.Controls.Add(this.chkbEncodeQueryStringParam);
        this.encodingOptionsGroupBox.Controls.Add(this.chkbUrlEncodeBodyMatches);
        this.encodingOptionsGroupBox.Controls.Add(this.chkbUrlEncodeHeaderMatches);
        this.encodingOptionsGroupBox.Enabled = false;
        this.encodingOptionsGroupBox.Location = new System.Drawing.Point(6, 504);
        this.encodingOptionsGroupBox.Name = "encodingOptionsGroupBox";
        this.encodingOptionsGroupBox.Size = new System.Drawing.Size(482, 65);
        this.encodingOptionsGroupBox.TabIndex = 43;
        this.encodingOptionsGroupBox.TabStop = false;
        this.encodingOptionsGroupBox.Text = "URI Encoding Options";
        // 
        // chkbEncodeQueryStringParam
        // 
        this.chkbEncodeQueryStringParam.AutoSize = true;
        this.chkbEncodeQueryStringParam.Location = new System.Drawing.Point(246, 19);
        this.chkbEncodeQueryStringParam.Name = "chkbEncodeQueryStringParam";
        this.chkbEncodeQueryStringParam.Size = new System.Drawing.Size(138, 16);
        this.chkbEncodeQueryStringParam.TabIndex = 30;
        this.chkbEncodeQueryStringParam.Text = "Encode Query String";
        this.chkbEncodeQueryStringParam.UseVisualStyleBackColor = true;
        this.chkbEncodeQueryStringParam.CheckedChanged += new System.EventHandler(this.chkbEncodeQueryStringParam_CheckedChanged);
        // 
        // chkbUrlEncodeBodyMatches
        // 
        this.chkbUrlEncodeBodyMatches.AutoSize = true;
        this.chkbUrlEncodeBodyMatches.Location = new System.Drawing.Point(8, 19);
        this.chkbUrlEncodeBodyMatches.Name = "chkbUrlEncodeBodyMatches";
        this.chkbUrlEncodeBodyMatches.Size = new System.Drawing.Size(150, 16);
        this.chkbUrlEncodeBodyMatches.TabIndex = 35;
        this.chkbUrlEncodeBodyMatches.Text = "Encode HTTP Body Data";
        this.chkbUrlEncodeBodyMatches.UseVisualStyleBackColor = true;
        this.chkbUrlEncodeBodyMatches.CheckedChanged += new System.EventHandler(this.chkbUrlEncodeBodyMatches_CheckedChanged);
        // 
        // chkbUrlEncodeHeaderMatches
        // 
        this.chkbUrlEncodeHeaderMatches.AutoSize = true;
        this.chkbUrlEncodeHeaderMatches.Location = new System.Drawing.Point(8, 42);
        this.chkbUrlEncodeHeaderMatches.Name = "chkbUrlEncodeHeaderMatches";
        this.chkbUrlEncodeHeaderMatches.Size = new System.Drawing.Size(138, 16);
        this.chkbUrlEncodeHeaderMatches.TabIndex = 34;
        this.chkbUrlEncodeHeaderMatches.Text = "Encode HTTP Headers";
        this.chkbUrlEncodeHeaderMatches.UseVisualStyleBackColor = true;
        this.chkbUrlEncodeHeaderMatches.CheckedChanged += new System.EventHandler(this.chkbUrlEncodeHeaderMatches_CheckedChanged);
        // 
        // gbResultFilterOpt
        // 
        this.gbResultFilterOpt.Controls.Add(this.checkedListBox1);
        this.gbResultFilterOpt.Enabled = false;
        this.gbResultFilterOpt.Location = new System.Drawing.Point(6, 269);
        this.gbResultFilterOpt.Name = "gbResultFilterOpt";
        this.gbResultFilterOpt.Size = new System.Drawing.Size(482, 92);
        this.gbResultFilterOpt.TabIndex = 44;
        this.gbResultFilterOpt.TabStop = false;
        this.gbResultFilterOpt.Text = "Results Filtering";
        // 
        // lblCanary
        // 
        this.lblCanary.AutoSize = true;
        this.lblCanary.Enabled = false;
        this.lblCanary.Location = new System.Drawing.Point(6, 33);
        this.lblCanary.Name = "lblCanary";
        this.lblCanary.Size = new System.Drawing.Size(59, 12);
        this.lblCanary.TabIndex = 2;
        this.lblCanary.Text = "Preamble:";
        // 
        // domainFilteringGroupBox
        // 
        this.domainFilteringGroupBox.Controls.Add(this.lbDomainFilters);
        this.domainFilteringGroupBox.Controls.Add(this.lblFilteredDomains);
        this.domainFilteringGroupBox.Controls.Add(this.btnAddToDomainFilterList);
        this.domainFilteringGroupBox.Controls.Add(this.btnRemoveDomainFilter);
        this.domainFilteringGroupBox.Controls.Add(this.chkbFilterReq);
        this.domainFilteringGroupBox.Controls.Add(this.chkbFilterRes);
        this.domainFilteringGroupBox.Controls.Add(this.label3);
        this.domainFilteringGroupBox.Controls.Add(this.tbDomain);
        this.domainFilteringGroupBox.Controls.Add(this.lblFilteredDomainsAdd);
        this.domainFilteringGroupBox.Enabled = false;
        this.domainFilteringGroupBox.Location = new System.Drawing.Point(9, 84);
        this.domainFilteringGroupBox.Name = "domainFilteringGroupBox";
        this.domainFilteringGroupBox.Size = new System.Drawing.Size(479, 149);
        this.domainFilteringGroupBox.TabIndex = 41;
        this.domainFilteringGroupBox.TabStop = false;
        this.domainFilteringGroupBox.Text = "Domain Targeting";
        // 
        // lbDomainFilters
        // 
        this.lbDomainFilters.Enabled = false;
        this.lbDomainFilters.FormattingEnabled = true;
        this.lbDomainFilters.ItemHeight = 12;
        this.lbDomainFilters.Location = new System.Drawing.Point(215, 40);
        this.lbDomainFilters.Name = "lbDomainFilters";
        this.lbDomainFilters.Size = new System.Drawing.Size(254, 64);
        this.lbDomainFilters.TabIndex = 21;
        // 
        // lblFilteredDomains
        // 
        this.lblFilteredDomains.AutoSize = true;
        this.lblFilteredDomains.Location = new System.Drawing.Point(212, 21);
        this.lblFilteredDomains.Name = "lblFilteredDomains";
        this.lblFilteredDomains.Size = new System.Drawing.Size(107, 12);
        this.lblFilteredDomains.TabIndex = 39;
        this.lblFilteredDomains.Text = "Targeted domains:";
        // 
        // btnAddToDomainFilterList
        // 
        this.btnAddToDomainFilterList.Enabled = false;
        this.btnAddToDomainFilterList.Location = new System.Drawing.Point(107, 115);
        this.btnAddToDomainFilterList.Name = "btnAddToDomainFilterList";
        this.btnAddToDomainFilterList.Size = new System.Drawing.Size(75, 23);
        this.btnAddToDomainFilterList.TabIndex = 24;
        this.btnAddToDomainFilterList.Text = "Add";
        this.btnAddToDomainFilterList.UseVisualStyleBackColor = true;
        this.btnAddToDomainFilterList.Click += new System.EventHandler(this.btnAddToDomainFilterList_Click);
        // 
        // btnRemoveDomainFilter
        // 
        this.btnRemoveDomainFilter.Enabled = false;
        this.btnRemoveDomainFilter.Location = new System.Drawing.Point(394, 115);
        this.btnRemoveDomainFilter.Name = "btnRemoveDomainFilter";
        this.btnRemoveDomainFilter.Size = new System.Drawing.Size(75, 23);
        this.btnRemoveDomainFilter.TabIndex = 25;
        this.btnRemoveDomainFilter.Text = "Remove";
        this.btnRemoveDomainFilter.UseVisualStyleBackColor = true;
        this.btnRemoveDomainFilter.Click += new System.EventHandler(this.btnRemoveDomainFilter_Click);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Enabled = false;
        this.label3.Location = new System.Drawing.Point(5, 21);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(71, 12);
        this.label3.TabIndex = 29;
        this.label3.Text = "Applies to:";
        // 
        // tbDomain
        // 
        this.tbDomain.Enabled = false;
        this.tbDomain.Location = new System.Drawing.Point(8, 89);
        this.tbDomain.Name = "tbDomain";
        this.tbDomain.Size = new System.Drawing.Size(174, 21);
        this.tbDomain.TabIndex = 23;
        // 
        // lblFilteredDomainsAdd
        // 
        this.lblFilteredDomainsAdd.AutoSize = true;
        this.lblFilteredDomainsAdd.Location = new System.Drawing.Point(5, 73);
        this.lblFilteredDomainsAdd.Name = "lblFilteredDomainsAdd";
        this.lblFilteredDomainsAdd.Size = new System.Drawing.Size(47, 12);
        this.lblFilteredDomainsAdd.TabIndex = 40;
        this.lblFilteredDomainsAdd.Text = "Domain:";
        // 
        // tabControl1
        // 
        this.tabControl1.Controls.Add(this.ConfigTP);
        this.tabControl1.Controls.Add(this.tpChrConfiguration);
        this.tabControl1.Controls.Add(this.resultsTP);
        this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tabControl1.Location = new System.Drawing.Point(0, 0);
        this.tabControl1.Name = "tabControl1";
        this.tabControl1.SelectedIndex = 0;
        this.tabControl1.Size = new System.Drawing.Size(523, 669);
        this.tabControl1.TabIndex = 14;
        // 
        // tpChrConfiguration
        // 
        this.tpChrConfiguration.AutoScroll = true;
        this.tpChrConfiguration.BackColor = System.Drawing.Color.Transparent;
        this.tpChrConfiguration.Controls.Add(this.lblTargetCodePointText);
        this.tpChrConfiguration.Controls.Add(this.lblSourceCodePointText);
        this.tpChrConfiguration.Controls.Add(this.lblTargetCodePoint);
        this.tpChrConfiguration.Controls.Add(this.lblSourceCodePoint);
        this.tpChrConfiguration.Controls.Add(this.richTextBox2);
        this.tpChrConfiguration.Controls.Add(this.lblChrNameValue);
        this.tpChrConfiguration.Controls.Add(this.lblChrDescription);
        this.tpChrConfiguration.Controls.Add(this.lblChrName);
        this.tpChrConfiguration.Controls.Add(this.lblFilter);
        this.tpChrConfiguration.Controls.Add(this.cbFilter);
        this.tpChrConfiguration.Controls.Add(this.dgUnicodeTestMappings);
        this.tpChrConfiguration.Location = new System.Drawing.Point(4, 22);
        this.tpChrConfiguration.Name = "tpChrConfiguration";
        this.tpChrConfiguration.Padding = new System.Windows.Forms.Padding(3);
        this.tpChrConfiguration.Size = new System.Drawing.Size(515, 643);
        this.tpChrConfiguration.TabIndex = 3;
        this.tpChrConfiguration.Text = "Test Case Configuration";
        this.tpChrConfiguration.UseVisualStyleBackColor = true;
        // 
        // lblTargetCodePointText
        // 
        this.lblTargetCodePointText.AutoSize = true;
        this.lblTargetCodePointText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblTargetCodePointText.Location = new System.Drawing.Point(129, 229);
        this.lblTargetCodePointText.Name = "lblTargetCodePointText";
        this.lblTargetCodePointText.Size = new System.Drawing.Size(108, 13);
        this.lblTargetCodePointText.TabIndex = 49;
        this.lblTargetCodePointText.Text = "TargetCodePointText";
        // 
        // lblSourceCodePointText
        // 
        this.lblSourceCodePointText.AutoSize = true;
        this.lblSourceCodePointText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblSourceCodePointText.Location = new System.Drawing.Point(129, 205);
        this.lblSourceCodePointText.Name = "lblSourceCodePointText";
        this.lblSourceCodePointText.Size = new System.Drawing.Size(111, 13);
        this.lblSourceCodePointText.TabIndex = 48;
        this.lblSourceCodePointText.Text = "SourceCodePointText";
        // 
        // lblTargetCodePoint
        // 
        this.lblTargetCodePoint.AutoSize = true;
        this.lblTargetCodePoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblTargetCodePoint.Location = new System.Drawing.Point(6, 229);
        this.lblTargetCodePoint.Name = "lblTargetCodePoint";
        this.lblTargetCodePoint.Size = new System.Drawing.Size(114, 13);
        this.lblTargetCodePoint.TabIndex = 47;
        this.lblTargetCodePoint.Text = "Target Code Point:";
        // 
        // lblSourceCodePoint
        // 
        this.lblSourceCodePoint.AutoSize = true;
        this.lblSourceCodePoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblSourceCodePoint.Location = new System.Drawing.Point(6, 205);
        this.lblSourceCodePoint.Name = "lblSourceCodePoint";
        this.lblSourceCodePoint.Size = new System.Drawing.Size(117, 13);
        this.lblSourceCodePoint.TabIndex = 46;
        this.lblSourceCodePoint.Text = "Source Code Point:";
        // 
        // lblChrNameValue
        // 
        this.lblChrNameValue.AutoSize = true;
        this.lblChrNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblChrNameValue.Location = new System.Drawing.Point(129, 183);
        this.lblChrNameValue.Name = "lblChrNameValue";
        this.lblChrNameValue.Size = new System.Drawing.Size(56, 13);
        this.lblChrNameValue.TabIndex = 44;
        this.lblChrNameValue.Text = "NameText";
        // 
        // lblChrDescription
        // 
        this.lblChrDescription.AutoSize = true;
        this.lblChrDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblChrDescription.Location = new System.Drawing.Point(6, 252);
        this.lblChrDescription.Name = "lblChrDescription";
        this.lblChrDescription.Size = new System.Drawing.Size(75, 13);
        this.lblChrDescription.TabIndex = 43;
        this.lblChrDescription.Text = "Description:";
        // 
        // lblChrName
        // 
        this.lblChrName.AutoSize = true;
        this.lblChrName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblChrName.Location = new System.Drawing.Point(6, 183);
        this.lblChrName.Name = "lblChrName";
        this.lblChrName.Size = new System.Drawing.Size(47, 13);
        this.lblChrName.TabIndex = 42;
        this.lblChrName.Text = "Name: ";
        // 
        // lblFilter
        // 
        this.lblFilter.AutoSize = true;
        this.lblFilter.Location = new System.Drawing.Point(3, 6);
        this.lblFilter.Name = "lblFilter";
        this.lblFilter.Size = new System.Drawing.Size(113, 12);
        this.lblFilter.TabIndex = 41;
        this.lblFilter.Text = "Character Filter: ";
        // 
        // cbFilter
        // 
        this.cbFilter.FormattingEnabled = true;
        this.cbFilter.Location = new System.Drawing.Point(132, 3);
        this.cbFilter.Name = "cbFilter";
        this.cbFilter.Size = new System.Drawing.Size(146, 20);
        this.cbFilter.TabIndex = 40;
        this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
        // 
        // dgUnicodeTestMappings
        // 
        this.dgUnicodeTestMappings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.dgUnicodeTestMappings.AutoGenerateColumns = false;
        this.dgUnicodeTestMappings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dgUnicodeTestMappings.BackgroundColor = System.Drawing.SystemColors.Control;
        this.dgUnicodeTestMappings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgUnicodeTestMappings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enabledDataGridViewCheckBoxColumn,
            this.sourcePointDataGridViewTextBoxColumn,
            this.targetDataGridViewTextBoxColumn});
        this.dgUnicodeTestMappings.DataSource = this.UAUnicodeCharMappingBindingSource;
        this.dgUnicodeTestMappings.Location = new System.Drawing.Point(0, 30);
        this.dgUnicodeTestMappings.Name = "dgUnicodeTestMappings";
        this.dgUnicodeTestMappings.RowTemplate.Height = 23;
        this.dgUnicodeTestMappings.Size = new System.Drawing.Size(463, 138);
        this.dgUnicodeTestMappings.TabIndex = 39;
        this.dgUnicodeTestMappings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgUnicodeTestMappings_CellContentClick);
        this.dgUnicodeTestMappings.SelectionChanged += new System.EventHandler(this.dgUnicodeTestMappings_SelectionChanged);
        // 
        // enabledDataGridViewCheckBoxColumn
        // 
        this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
        this.enabledDataGridViewCheckBoxColumn.HeaderText = "Enabled";
        this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
        // 
        // sourcePointDataGridViewTextBoxColumn
        // 
        this.sourcePointDataGridViewTextBoxColumn.DataPropertyName = "SourcePoint";
        this.sourcePointDataGridViewTextBoxColumn.HeaderText = "Source/Test-case";
        this.sourcePointDataGridViewTextBoxColumn.Name = "sourcePointDataGridViewTextBoxColumn";
        this.sourcePointDataGridViewTextBoxColumn.ReadOnly = true;
        // 
        // targetDataGridViewTextBoxColumn
        // 
        this.targetDataGridViewTextBoxColumn.DataPropertyName = "Target";
        this.targetDataGridViewTextBoxColumn.HeaderText = "Target";
        this.targetDataGridViewTextBoxColumn.Name = "targetDataGridViewTextBoxColumn";
        this.targetDataGridViewTextBoxColumn.ReadOnly = true;
        // 
        // UAUnicodeCharMappingBindingSource
        // 
        this.UAUnicodeCharMappingBindingSource.DataSource = typeof(Secsay.UnicodeTestCase);
        // 
        // richTextBox2
        // 
        this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.richTextBox2.BackColor = System.Drawing.SystemColors.Window;
        this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.richTextBox2.Location = new System.Drawing.Point(15, 268);
        this.richTextBox2.Name = "richTextBox2";
        this.richTextBox2.ReadOnly = true;
        this.richTextBox2.Size = new System.Drawing.Size(448, 304);
        this.richTextBox2.TabIndex = 50;
        this.richTextBox2.Text = "";
        // 
        // UAUserInterface
        // 
        this.ClientSize = new System.Drawing.Size(523, 669);
        this.Controls.Add(this.tabControl1);
        this.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Name = "UAUserInterface";
        this.Text = "XSS Fuzz";
        this.resultsTP.ResumeLayout(false);
        this.splitContainer1.Panel1.ResumeLayout(false);
        this.splitContainer1.Panel2.ResumeLayout(false);
        this.splitContainer1.ResumeLayout(false);
        this.panelResultsGrid.ResumeLayout(false);
        this.panelResultButtons.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.ResultsDataGridView)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.responseResultBindingSource)).EndInit();
        this.ConfigTP.ResumeLayout(false);
        this.ConfigTP.PerformLayout();
        this.gbGeneralOptions.ResumeLayout(false);
        this.gbGeneralOptions.PerformLayout();
        this.injectionOptionsGroupBox.ResumeLayout(false);
        this.injectionOptionsGroupBox.PerformLayout();
        this.encodingOptionsGroupBox.ResumeLayout(false);
        this.encodingOptionsGroupBox.PerformLayout();
        this.gbResultFilterOpt.ResumeLayout(false);
        this.domainFilteringGroupBox.ResumeLayout(false);
        this.domainFilteringGroupBox.PerformLayout();
        this.tabControl1.ResumeLayout(false);
        this.tpChrConfiguration.ResumeLayout(false);
        this.tpChrConfiguration.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgUnicodeTestMappings)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.UAUnicodeCharMappingBindingSource)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.BindingSource responseResultBindingSource;
    private System.Windows.Forms.TabPage resultsTP;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.DataGridView ResultsDataGridView;

    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.TabPage ConfigTP;
    private System.Windows.Forms.TextBox tbDomain;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnRemoveDomainFilter;
    private System.Windows.Forms.Button btnAddToDomainFilterList;
    private System.Windows.Forms.CheckBox chkbEnableDomainFilter;
    private System.Windows.Forms.ListBox lbDomainFilters;
    private System.Windows.Forms.Label lblCanary;
    private System.Windows.Forms.CheckBox chkbEnabled;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TextBox tbCanary;
    private System.Windows.Forms.DataGridViewTextBoxColumn contextDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn transformationDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn CodePoint;
    private System.Windows.Forms.BindingSource UAUnicodeCharMappingBindingSource;
    private System.Windows.Forms.TabPage tpChrConfiguration;
    private System.Windows.Forms.CheckBox chkbFilterRes;
    private System.Windows.Forms.CheckBox chkbFilterReq;
    private System.Windows.Forms.Label lblChrNameValue;
    private System.Windows.Forms.Label lblChrDescription;
    private System.Windows.Forms.Label lblChrName;
    private System.Windows.Forms.Label lblFilter;
    private System.Windows.Forms.ComboBox cbFilter;
    private System.Windows.Forms.DataGridView dgUnicodeTestMappings;
    private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn sourcePointDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn targetDataGridViewTextBoxColumn;
    private System.Windows.Forms.Label lblSourceCodePoint;
    private System.Windows.Forms.Label lblTargetCodePoint;
    private System.Windows.Forms.Label lblTargetCodePointText;
    private System.Windows.Forms.Label lblSourceCodePointText;
    private System.Windows.Forms.ToolTip toolTip2;
    private System.Windows.Forms.Label lblFilteredDomains;
    private System.Windows.Forms.Label lblFilteredDomainsAdd;
    public System.Windows.Forms.GroupBox domainFilteringGroupBox;
    private System.Windows.Forms.GroupBox gbResultFilterOpt;
    private System.Windows.Forms.CheckBox chkbEnableResultFiltering;
    private System.Windows.Forms.Button clearBtn;
    private System.Windows.Forms.Button btnEasyFilter;
    private System.Windows.Forms.CheckedListBox checkedListBox1;
    private System.Windows.Forms.Panel panelResultButtons;
    private System.Windows.Forms.Panel panelResultsGrid;
    private System.Windows.Forms.GroupBox gbGeneralOptions;
    private System.Windows.Forms.CheckBox chkbAutoCheckForUpdates;
    public System.Windows.Forms.Button CheckLatestButton;
    private System.Windows.Forms.GroupBox injectionOptionsGroupBox;
    private System.Windows.Forms.CheckBox chkbInjectQueryParam;
    protected System.Windows.Forms.CheckBox chkbAutoInjectPost;
    private System.Windows.Forms.CheckBox chkbCheckRequestCanary;
    private System.Windows.Forms.GroupBox encodingOptionsGroupBox;
    private System.Windows.Forms.CheckBox chkbEncodeQueryStringParam;
    private System.Windows.Forms.CheckBox chkbUrlEncodeBodyMatches;
    private System.Windows.Forms.CheckBox chkbUrlEncodeHeaderMatches;
    private System.Windows.Forms.CheckBox chkbThrottleRequests;
    private System.Windows.Forms.Label lbDelayPeriod;
    private System.Windows.Forms.Label lbBatchSize;
    private System.Windows.Forms.TextBox tbDelayPeriod;
    private System.Windows.Forms.TextBox tbBatchSize;
    private System.Windows.Forms.RichTextBox richTextBox2;
}

