using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Fiddler;
using Casaba;
using CasabaSecurity.Web.x5s;
using Equin.ApplicationFramework;

public partial class UAUserInterface : Form{//UserControl {
    
    //Delegate's to handle updating the DataGridView due to the "Creation thread" constraints. 
    public delegate void AddRow(List<ResponseResult> matches);
    public AddRow ar;
    public delegate void SetColumnAutoSort();
    public SetColumnAutoSort scas;

    //Variable to hold all matches. 
    public SortableBindingList<ResponseResult> matches;
    
    //Datasource
    private BindingListView<ResponseResult> view;

    public UAUserInterface(UAEngine xa) {
        InitializeComponent();
 
        //When xa is loaded settings are loaded.. 
        this.xa = xa;
        
        //SetState sets up the different check boxes based on the settings. 
        SetState();
        
        //Setting the textbox with the default value. 
        this.tbCanary.Text = xa.Settings.canary;
       
        //Setup the databinding for results view
        this.matches = new SortableBindingList<ResponseResult>();
        this.view = new BindingListView<ResponseResult>(this.matches);
        this.ResultsDataGridView.DataSource = this.view;
       
        //Some other defaults
        this.lbDomainFilters.DataSource = xa.Settings.domainFilters;
        this.dgUnicodeTestMappings.DataSource = xa.Settings.UnicodeTestMappings.GetAll();
        
        //Add the default items to the filter ComboBox. 
        this.cbFilter.Items.Add("All");
        this.cbFilter.Items.Add("Transformable");
        this.cbFilter.Items.Add("Traditional");
        this.cbFilter.Items.Add("Overlong");
        this.cbFilter.SelectedItem = "Traditional";

        // Initialize the throttle UI values
        this.tbBatchSize.Text = String.Format("{0}", this.xa.Settings.throttleBatchSize);
        this.tbDelayPeriod.Text = String.Format("{0}", this.xa.Settings.throttleDelayPeriod);
        
        //Delegate method to ensure when adding data to the datasource it origanates from the creating thread. 
        ar = new AddRow(AddRowMethod);
        
        //Set columns to sortable
        this.scas = new SetColumnAutoSort(setColumnAutoSort);
        this.Dock = DockStyle.Fill;
    }
    
    public void setColumnAutoSort() {
        for (int i = 0; i < this.ResultsDataGridView.Columns.Count; i++) {
            this.ResultsDataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
        }
    }

    private void SetState() 
    {
        this.chkbEnabled.Enabled = true;
        this.chkbEnabled.Checked = this.xa.Settings.Enabled;
        this.chkbEnableDomainFilter.Checked = this.xa.Settings.domainFilterEnabled;
        this.chkbAutoInjectPost.Checked = this.xa.Settings.injectIntoPost;
        this.chkbCheckRequestCanary.Checked = this.xa.Settings.checkRequestForCanary;         
        this.chkbInjectQueryParam.Checked = this.xa.Settings.injectIntoQueryString;
        this.chkbEncodeQueryStringParam.Checked = this.xa.Settings.urlEncodeQueryStringMatches;
        this.chkbUrlEncodeBodyMatches.Checked = this.xa.Settings.urlEncodeBodyMatches;
        this.chkbUrlEncodeHeaderMatches.Checked = this.xa.Settings.urlEncodeHeaderMatches;
        this.chkbEnableResultFiltering.Checked = this.xa.Settings.advancedFilter;
        this.chkbAutoCheckForUpdates.Checked = this.xa.Settings.autoCheckForUpdates;
        this.chkbFilterReq.Checked = this.xa.Settings.filterRequests;
        this.chkbFilterRes.Checked = this.xa.Settings.filterResponse;
        this.chkbThrottleRequests.Checked = this.xa.Settings.throttleRequests;
    }

    public void ClearMatchListMethod() {
        this.matches.Clear();
    }

    //This delegate method is used to ensure that the Datagridview is updated via the thread that created it. 
    public void AddRowMethod(List<ResponseResult> matches) {
        foreach (ResponseResult m in matches) {
            if (!this.matches.Contains(m)) {
                this.matches.Add(m);
            }
        }
    }

    //Setting the check boxes to enabled and all that jazz based on the plugin being enabled. 
    private void enabledChkBox_CheckedChanged(object sender, EventArgs e) {
        this.SuspendLayout();

        // We're enabled if the Enabled button is checked; save the enabled setting
        bool enabled = this.chkbEnabled.Checked;
        this.xa.Settings.Enabled = enabled;

        // Toggle the Canary option
        this.lblCanary.Enabled = enabled;
        this.tbCanary.Enabled = enabled;

        // Toggle the Injection Options group box
        this.injectionOptionsGroupBox.Enabled = enabled;
        this.chkbThrottleRequests.Checked = this.xa.Settings.throttleRequests;
        cbThrottleRequests_CheckedChanged(this, e);

        // Toggle the Encoding options group box
        this.encodingOptionsGroupBox.Enabled = enabled;

        // Toggle the Result Filtering group box
        //this.gbResultFilterOpt.Enabled = enabled;
        this.chkbEnableResultFiltering.Enabled = enabled;
        this.chkbEnableResultFiltering.Checked = this.xa.Settings.advancedFilter;
        this.gbResultFilterOpt.Enabled = enabled && this.chkbEnableResultFiltering.Checked;
        this.checkedListBox1.Enabled = enabled && this.chkbEnableResultFiltering.Checked;
        //chkbAdvanceFilter_CheckedChanged(this, e);

        // Toggle the Domain Filter group box
        this.chkbEnableDomainFilter.Enabled = enabled;
        this.chkbEnableDomainFilter.Checked = this.xa.Settings.domainFilterEnabled;
        chkbEnableDomainFilter_CheckedChanged(this, e);

        // Toggle the General Options group box
        this.gbGeneralOptions.Enabled = enabled;
        this.chkbAutoCheckForUpdates.Checked = this.xa.Settings.autoCheckForUpdates;
        chkbAutoCheckForUpdates_CheckedChanged(this, e);
      
        this.ResumeLayout();
    }

    //This causes the results list to refreash incase items are removed etc. 
    public void refreshBindings(){
        BindingManagerBase bmb = this.ResultsDataGridView.BindingContext[this.view];
        bmb.SuspendBinding();
        bmb.ResumeBinding();
    }

    //private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e) {
    //    this.richTextBox1.Text = "Can't jump to location.";
        
    //    //May require the view
    //    //ResponseResult res = this.matches[e.RowIndex];

    //    ResponseResult res = (ResponseResult)this.view[e.RowIndex];


    //    List<Fiddler.Session> sessions = oAllSessions;
    //    Fiddler.Session targetSession = null;
    //    int tSessionIndex = -1;
    //    //Locate the session object and it's index..
    //    for (int i = 0; i < sessions.Length; i++) {  
    //        if (sessions[i].id == res.Match.SessionId) {
    //            targetSession = sessions[i];
    //            tSessionIndex = i;
    //            break;
    //        }
    //    }
    //    if (targetSession == null || tSessionIndex == -1) {
    //        //ouch, no jump for this session ;(
    //        return;
    //    }
    //    //Jump to fiddler session in the right box. 
    //    FiddlerApplication.UI.lvSessions.SelectedItems.Clear();
    //    FiddlerApplication.UI.lvSessions.Items[tSessionIndex].Focused = true;
    //    FiddlerApplication.UI.lvSessions.Items[tSessionIndex].Selected = true;

    //    //Dump text to textbox and bail. 
    //    string headers = targetSession.oResponse.headers.ToString();
    //    string body = Encoding.UTF8.GetString(targetSession.responseBodyBytes);
    //    this.richTextBox1.Text = headers + "\r\n\r\n" + body;

    //    //Lets see if we can jump to the proper place in the RichTextBox to highlihgt the location for quicker inspection. 
    //    int offset = 0;
    //    if (res.Match is HeaderMatch) {
    //        HeaderMatch hm = (HeaderMatch)res.Match;
    //        offset = headers.IndexOf(hm.HeaderName) + hm.HeaderName.Length + 2; //+2  to cover the : and space..
    //        offset += res.Match.Offset;
    //    } else if (res.Match is BodyMatch) {
    //        BodyMatch bm = (BodyMatch)res.Match;
    //        offset = headers.Length + 4 + bm.Offset; 
    //    }
    //    if (offset - 20 > 0 && offset + 20 < headers.Length + 4 + body.Length) {
    //        this.richTextBox1.Select(offset - 20, 40);
    //        this.richTextBox1.SelectionColor = Color.Red;
    //        this.richTextBox1.ScrollToCaret();
    //    }
    //}

    private void clearBtn_Click(object sender, EventArgs e) {
        this.matches.Clear();
        this.xa.Matches.Clear();
        this.richTextBox1.Text = "";
    }

    private void chkbInjectQueryParam_CheckedChanged(object sender, EventArgs e) {
        if (this.chkbInjectQueryParam.Checked) {
            this.xa.Settings.injectIntoQueryString = true;
        } else {
            this.xa.Settings.injectIntoQueryString = false;
        }
    }

    private void btnAddToDomainFilterList_Click(object sender, EventArgs e) {
        if (this.tbDomain.Text != "") {
            this.xa.Settings.domainFilters.Add(this.tbDomain.Text);
            this.tbDomain.Text = "";
            BindingManagerBase bmb = this.lbDomainFilters.BindingContext[this.xa.Settings.domainFilters];
            bmb.SuspendBinding();
            bmb.ResumeBinding();
        }
    }

    private void btnRemoveDomainFilter_Click(object sender, EventArgs e) {
        if (this.lbDomainFilters.SelectedIndex >= 0) {
            string s = this.xa.Settings.domainFilters[this.lbDomainFilters.SelectedIndex];
            this.tbDomain.Text = s;
            this.xa.Settings.domainFilters.Remove(s);
            BindingManagerBase bmb = this.lbDomainFilters.BindingContext[this.xa.Settings.domainFilters];
            bmb.SuspendBinding();
            bmb.ResumeBinding();
        }
    }

    private void chkbFilterReq_CheckedChanged(object sender, EventArgs e) {
        if (this.chkbFilterReq.Checked) {
            this.xa.Settings.filterRequests = true;
        } else {
            this.xa.Settings.filterRequests = false;
        }
    }

    private void chkbFilterRes_CheckedChanged(object sender, EventArgs e) {
        if (this.chkbFilterRes.Checked) {
            this.xa.Settings.filterResponse = true;
        } else {
            this.xa.Settings.filterResponse = false;
        }
    }

    private void chkbEnableDomainFilter_CheckedChanged(object sender, EventArgs e)
    {
        this.SuspendLayout();
        bool enabled = this.chkbEnabled.Checked && this.chkbEnableDomainFilter.Checked;

        this.xa.Settings.domainFilterEnabled = this.chkbEnableDomainFilter.Checked;
        this.domainFilteringGroupBox.Enabled = enabled;
        this.chkbFilterReq.Enabled = enabled;
        this.chkbFilterRes.Enabled = enabled;
        this.tbDomain.Enabled = enabled;
        this.btnAddToDomainFilterList.Enabled = enabled;
        this.btnRemoveDomainFilter.Enabled = enabled;
        this.chkbFilterReq.Enabled = enabled;
        this.chkbFilterRes.Enabled = enabled;
        this.lbDomainFilters.Enabled = enabled;
        this.label3.Enabled = enabled;

        this.ResumeLayout();
    }

    private void chkbEncodeQueryStringParam_CheckedChanged(object sender, EventArgs e) {
        if (this.chkbEncodeQueryStringParam.Checked) {
            this.xa.Settings.urlEncodeQueryStringMatches = true;
        } else {
            this.xa.Settings.urlEncodeQueryStringMatches = false;
        }
    }


    private void chkbAutoInjectPost_CheckedChanged(object sender, EventArgs e) {
        if (this.chkbAutoInjectPost.Checked) {
            this.xa.Settings.injectIntoPost = true;
        } else {
            this.xa.Settings.injectIntoPost = false;
        }
    }

    private void chkbCheckRequestCanary_CheckedChanged(object sender, EventArgs e) {
        if (this.chkbCheckRequestCanary.Checked) {
            this.xa.Settings.checkRequestForCanary = true;
        } else {
            this.xa.Settings.checkRequestForCanary = false;
        }
    }

    //private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
    //    FiddlerApplication.UI.actInspectSession();
    //}

    private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (this.cbFilter.SelectedItem.ToString())
        {
            case ("All"): 
                this.dgUnicodeTestMappings.DataSource = xa.Settings.UnicodeTestMappings.GetAll();
                foreach (DataGridViewRow row in this.dgUnicodeTestMappings.Rows)
                {
                    row.Cells[0].Value = true;
                }
                break;
            case ("Traditional"): 
                this.dgUnicodeTestMappings.DataSource = xa.Settings.UnicodeTestMappings.GetMappingsByType(UnicodeTestCaseTypes.Traditional);
                foreach (DataGridViewRow row in this.dgUnicodeTestMappings.Rows)
                {
                    row.Cells[0].Value = true;
                }
                break;
            case ("Transformable"): 
                this.dgUnicodeTestMappings.DataSource = xa.Settings.UnicodeTestMappings.GetMappingsByType(UnicodeTestCaseTypes.Transformable);
                foreach (DataGridViewRow row in this.dgUnicodeTestMappings.Rows)
                {
                    row.Cells[0].Value = true;
                }
                break;
            case ("Overlong"): 
                this.dgUnicodeTestMappings.DataSource = xa.Settings.UnicodeTestMappings.GetMappingsByType(UnicodeTestCaseTypes.Overlong);
                foreach (DataGridViewRow row in this.dgUnicodeTestMappings.Rows)
                {
                    row.Cells[0].Value = true;
                }
                break;  
        }
    }

    private void dgUnicodeTestMappings_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == 0)
        {
            UnicodeTestCase  mapping = (UnicodeTestCase)this.dgUnicodeTestMappings.CurrentRow.DataBoundItem;
            if (mapping.Enabled)
            {
                mapping.Enabled = false;
            }
            else
            {
                mapping.Enabled = true;
            }
        }
    }

    private void chkbUrlEncodeHeaderMatches_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chkbUrlEncodeHeaderMatches.Checked)
        {
            this.xa.Settings.urlEncodeHeaderMatches = true;
        }
        else
        {
            this.xa.Settings.urlEncodeHeaderMatches = false;
        }
    }

    private void chkbUrlEncodeBodyMatches_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chkbUrlEncodeBodyMatches.Checked)
        {
            this.xa.Settings.urlEncodeBodyMatches = true;
        }
        else
        {
            this.xa.Settings.urlEncodeBodyMatches = false;
        }
    }

    private void dgUnicodeTestMappings_SelectionChanged(object sender, EventArgs e)
    {
        if (this.dgUnicodeTestMappings.CurrentRow != null)
        {
            UnicodeTestCase mapping = (UnicodeTestCase)this.dgUnicodeTestMappings.CurrentRow.DataBoundItem;
            this.richTextBox2.Text = mapping.Description;
            this.lblChrNameValue.Text = mapping.SourcePoint.Name;
            this.lblSourceCodePointText.Text = mapping.SourcePoint.ToHexStringCodePoint();
            if (mapping.Target != null)
            {

                this.lblTargetCodePoint.Visible = true;
                this.lblTargetCodePointText.Visible = true;
                this.lblTargetCodePointText.Text = mapping.Target.ToHexStringCodePoint();
            }
            else
            {
                this.lblTargetCodePoint.Visible = false;
                this.lblTargetCodePointText.Visible = false;
            }
        }
    }

    /// <summary>
    /// Visit the Casaba Security web site.
    /// </summary>
    private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        try
        {
            e.Link.Visited = true;
            System.Diagnostics.Process.Start("http://www.secsay.com/");
        }

        catch (Win32Exception ex)
        {
            // Thrown when a Win32 error code is returned
            Trace.TraceError("Error: Win32Exception: {0}", ex.Message);
        }

        catch (ObjectDisposedException ex)
        {
            // Thrown when an operation is performed on a disposed object
            Trace.TraceError("Error: ObjectDisposedException: {0}", ex.Message);
        }

        catch (FileNotFoundException ex)
        {
            // Thrown when the resource does not exist
            Trace.TraceError("Error: FileNotFoundException: {0}", ex.Message);
        }
    }

    private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        //Should always be true,lt in logic though to allow for a option to control the box itself in case
        Transformation t = (Transformation) 0x0;

        List<int> indexes = new List<int>();
        foreach (int index in this.checkedListBox1.CheckedIndices)
        {
            indexes.Add(index);
        }
        
        if (e.NewValue == CheckState.Checked)
        {
            indexes.Add(e.Index);
        }else if( e.NewValue == CheckState.Unchecked)
        {
            indexes.Remove(e.Index);
        }

        foreach (int index in indexes)
        {
            string s = this.checkedListBox1.Items[index].ToString();
            t |= UAUtilities.GetTransformationFromString(s);
        }

        view.ApplyFilter(delegate(ResponseResult rr) { return ((rr.Transformation) & t) > 0; }); 
        this.refreshBindings();
    }

    private void chkbAdvanceFilter_CheckedChanged(object sender, EventArgs e)
    {
        bool enable = this.chkbEnableResultFiltering.Enabled && this.chkbEnableResultFiltering.Checked;

        this.xa.Settings.advancedFilter = this.chkbEnableResultFiltering.Checked;
        this.btnEasyFilter.Enabled = !this.chkbEnableResultFiltering.Checked;
        this.gbResultFilterOpt.Enabled = enable;
        this.checkedListBox1.Enabled = enable;

        if (this.chkbEnableResultFiltering.Checked == false)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
            }
            this.SetEasyFilter(this.btnEasyFilter.Text);
        }
    }

    private void SetEasyFilter(String s)
    {
        if (s == "Show All")
        {
            this.btnEasyFilter.Text = "Show All";

            view.ApplyFilter(delegate(ResponseResult rr)
            {

                if (rr.TestCase.Type == UnicodeTestCaseTypes.Traditional && rr.Transformation == Transformation.None)
                    return true;
                else if (rr.TestCase.Type == UnicodeTestCaseTypes.Transformable && rr.Transformation == Transformation.Transformed)
                    return true;
                else if (rr.TestCase.Type == UnicodeTestCaseTypes.Overlong && rr.Transformation == Transformation.ShortestForm)
                    return true;

                return false;
            });
        }
        else if (s == "Show Hotspots")
        {
            this.btnEasyFilter.Text = "Show Hotspots";
            if (view != null) this.view.RemoveFilter();
        }
        if (view != null) this.view.Refresh();
        this.ResultsDataGridView.Refresh();
        if (view != null) this.refreshBindings();
    }

    private void FlipEasyFilter()
    {
        if (this.btnEasyFilter.Text == "Show Hotspots")
        {
            this.SetEasyFilter("Show All");
        }
        else if (this.btnEasyFilter.Text == "Show All")
        {
            this.SetEasyFilter("Show Hotspots");
        }
    }

    private void btnEasyFilter_Click(object sender, EventArgs e)
    {
        FlipEasyFilter();
    }

    /// <summary>
    /// Handle the "check for new version(s) on start-up" check box
    /// </summary>
    private void chkbAutoCheckForUpdates_CheckedChanged(object sender, EventArgs e)
    {
        this.xa.Settings.autoCheckForUpdates = this.chkbAutoCheckForUpdates.Checked;
    }

    /// <summary>
    /// Handle the "check for updates" button
    /// </summary>
    private void CheckLatestButton_Click(object sender, EventArgs e)
    {
        UpdateManager updateManager = new UpdateManager();
        updateManager.CheckForUpdate();
    }

    /// <summary>
    /// Handle changes to the preamble text box
    /// </summary>
    private void tbCanary_TextChanged(object sender, EventArgs e)
    {
        this.xa.Settings.canary = this.tbCanary.Text;
    }

    /// <summary>
    /// Handle "request generation throttling" check box 
    /// </summary>
    private void cbThrottleRequests_CheckedChanged(object sender, EventArgs e)
    {
        bool enabled = this.chkbThrottleRequests.Checked;

        this.lbBatchSize.Enabled = enabled;
        this.tbBatchSize.Enabled = enabled;
        this.lbDelayPeriod.Enabled = enabled;
        this.tbDelayPeriod.Enabled = enabled;

        // Toggle the throttling functionality; Update the configuration
        FiddlerUtils.RequestManager.Throttle = enabled;
        this.xa.Settings.throttleRequests = enabled;
    }

    /// <summary>
    /// Handle changes to the Delay Period text box
    /// </summary>
    private void tbDelayPeriod_TextChanged(object sender, EventArgs e)
    {
        int delay;

        // Attempt to convert the value entered to an int
        if (Int32.TryParse(this.tbDelayPeriod.Text, out delay) == false)
        {
            delay = this.xa.Settings.throttleDelayPeriod;
        }

        // Update the Request Manager.  It may correct the batch size in the setter, so use
        // the getter value when updating the configuration option.
        FiddlerUtils.RequestManager.Delay = delay;
        this.xa.Settings.throttleDelayPeriod = FiddlerUtils.RequestManager.Delay;

        // Prevent recursion as we will be modifying the text box
        tbDelayPeriod.TextChanged -= new EventHandler(this.tbDelayPeriod_TextChanged);

        // Modify the text box
        this.tbDelayPeriod.Text = String.Format("{0}", FiddlerUtils.RequestManager.Delay);

        // Re-enable the event handler
        tbDelayPeriod.TextChanged += new EventHandler(this.tbDelayPeriod_TextChanged);
    }

    /// <summary>
    /// Handle changes to the Batch Size text box
    /// </summary>
    private void tbBatchSize_TextChanged(object sender, EventArgs e)
    {
        int batchSize;

        // Attempt to convert the value entered to an int
        if (Int32.TryParse(this.tbBatchSize.Text, out batchSize) == false)
        {
            batchSize = this.xa.Settings.throttleBatchSize;
        }

        // Update the Request Manager.  It may correct the batch size in the setter, so use
        // the getter value when updating the configuration option.
        FiddlerUtils.RequestManager.BatchSize = batchSize;
        this.xa.Settings.throttleBatchSize = FiddlerUtils.RequestManager.BatchSize; 

        // Prevent recursion as we will be modifying the text box
        tbBatchSize.TextChanged -= new EventHandler(this.tbBatchSize_TextChanged);

        // Modify the text box
        this.tbBatchSize.Text = String.Format("{0}", FiddlerUtils.RequestManager.BatchSize);

        // Re-enable the event handler
        tbBatchSize.TextChanged += new EventHandler(this.tbBatchSize_TextChanged);
    }
}
