using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;


public partial class Employee_TechnicalServices_Project_TraceObserverSelected : System.Web.UI.Page
{
   int _PageIndex
    {
        get { return Convert.ToInt32(HiddenFieldPage["PageIndex"]); }
        set { HiddenFieldPage["PageIndex"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _PageIndex = 1;
            ASPxComboBoxAgent.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
        }
        lblPageNo.Text = _PageIndex.ToString();
        BindGrid(_PageIndex);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid(_PageIndex);
    }
    protected void PageSize_Changed(object sender, EventArgs e)
    {
        BindGrid(1);
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
         _PageIndex = int.Parse((sender as LinkButton).CommandArgument);
        lblPageNo.Text = _PageIndex.ToString();
        BindGrid(_PageIndex);
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "TraceObserverSelected";
        GridViewExporter.WriteXlsxToResponse(true);
    }


    private DataTable ConvertJsonToDatatable(string jsonString)
    {
        var jsonLinq = JObject.Parse(jsonString);

        // Find the first array using Linq
        var linqArray = jsonLinq.Descendants().Where(x => x is JArray).First();
        var jsonArray = new JArray();
        foreach (JObject row in linqArray.Children<JObject>())
        {
            var createRow = new JObject();
            foreach (JProperty column in row.Properties())
            {
                // Only include JValue types
                if (column.Value is JValue)
                {
                    createRow.Add(column.Name, column.Value);
                }
            }
            jsonArray.Add(createRow);
        }

        return JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
    }
    private DataTable ConvertJsonToDatatable2(string jsonString)
    {
        DataTable dt = new DataTable();
        //strip out bad characters
        string[] jsonParts = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");

        //hold column names
        List<string> dtColumns = new List<string>();

        //get columns
        foreach (string jp in jsonParts)
        {
            //only loop thru once to get column names
            string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
            foreach (string rowData in propData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string n = rowData.Substring(0, idx - 1);
                    string v = rowData.Substring(idx + 1);
                    if (!dtColumns.Contains(n))
                    {
                        dtColumns.Add(n.Replace("\"", ""));
                    }
                }
                catch (Exception ex)
                {
                    continue;
                    //throw new Exception(string.Format("Error Parsing Column Name : {0}", rowData));
                }

            }
            break; // TODO: might not be correct. Was : Exit For
        }

        //build dt
        foreach (string c in dtColumns)
        {
            dt.Columns.Add(c);
        }
        //get table data
        foreach (string jp in jsonParts)
        {
            string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in propData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string n = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string v = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[n] = v;
                }
                catch (Exception ex)
                {
                    continue;
                }

            }
            if(nr.Table.Columns.Count!=0)
            dt.Rows.Add(nr);
        }
        return dt;
    }
    protected void GridViewTraceObserverSelected_PageIndexChanged(object sender, EventArgs e)
    {
        if (!Utility.IsDBNullOrNullValue(Session["dtGridView"]))
        {
            GridViewTraceObserverSelected.DataSource = (DataTable)Session["dtGridView"];
            GridViewTraceObserverSelected.DataBind();
        }

    }

    private void BindGrid(int PageIndex)
    {
        try
        {
            if (Utility.IsDBNullOrNullValue(ASPxComboBoxAgent.SelectedItem) && Utility.IsDBNullOrNullValue(txtProjectNo.Text) && Utility.IsDBNullOrNullValue(txtCreateDateFrom.Text) && Utility.IsDBNullOrNullValue(txtCreateDateTo.Text))
                return;
            int AgentId = -1;
            int ProjectId = -1;
            string FromDate = "%";
            string ToDate = "%";
            if (!Utility.IsDBNullOrNullValue(ASPxComboBoxAgent.SelectedItem) && !Utility.IsDBNullOrNullValue(ASPxComboBoxAgent.SelectedItem.Value))
            {
                AgentId = Convert.ToInt32(ASPxComboBoxAgent.SelectedItem.Value);
            }
            if (!Utility.IsDBNullOrNullValue(txtProjectNo.Text))
            {
                ProjectId = Convert.ToInt32(txtProjectNo.Text);
            }
            if (!Utility.IsDBNullOrNullValue(txtCreateDateFrom.Text))
            {
                FromDate = txtCreateDateFrom.Text;
            }
            if (!Utility.IsDBNullOrNullValue(txtCreateDateTo.Text))
            {
                ToDate = txtCreateDateTo.Text;
            }
            //Session["dtGridView"] = null;
            TSP.DataManager.Permission Per = TSP.DataManager.TechnicalServices.TSFunctionALogsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridViewTraceObserverSelected.Visible = Per.CanView;
            DataTable dtGridView = new DataTable();

            dtGridView.Columns.Add("Id");
            dtGridView.Columns["Id"].AutoIncrement = true;
            dtGridView.Columns["Id"].AutoIncrementSeed = 1;
            dtGridView.Constraints.Add("PK_ID", dtGridView.Columns["Id"], true);
            dtGridView.Columns.Add("RowNum", typeof(string));
            dtGridView.Columns.Add("ProjectId", typeof(string));
            dtGridView.Columns.Add("IngridiantMajor", typeof(string));
            dtGridView.Columns.Add("StageId", typeof(string));
            dtGridView.Columns.Add("StageName", typeof(string));
            dtGridView.Columns.Add("Date", typeof(string));
            dtGridView.Columns.Add("Count", typeof(string));
            dtGridView.Columns.Add("MeId", typeof(string));
            dtGridView.Columns.Add("ObsDate", typeof(string));
            dtGridView.Columns.Add("MembershipDate", typeof(string));
            dtGridView.Columns.Add("WantedWorkType", typeof(string));
            dtGridView.Columns.Add("Group1", typeof(string));
            dtGridView.Columns.Add("Group2", typeof(string));
            dtGridView.Columns.Add("Group3", typeof(string));
            dtGridView.Columns.Add("Group4", typeof(string));
            dtGridView.Columns.Add("City1", typeof(string));
            dtGridView.Columns.Add("City2", typeof(string));
            dtGridView.Columns.Add("UsedCapacity", typeof(string));
            dtGridView.Columns.Add("RemainCapacity", typeof(string));
            dtGridView.Columns.Add("RemainCapacityObs", typeof(string));
            dtGridView.Columns.Add("RemainCapacityObsReal", typeof(string));
            dtGridView.Columns.Add("TotalCapacity", typeof(string));
            dtGridView.Columns.Add("CapacityObs", typeof(string));
            dtGridView.Columns.Add("CapacityDesign", typeof(string));
            dtGridView.Columns.Add("CountRemainWorkCount", typeof(string));
            dtGridView.Columns.Add("PercentOfCapacityUsage", typeof(string));

            TSP.DataManager.TechnicalServices.TSFunctionALogsManager TSFunctionALogsManager = new TSP.DataManager.TechnicalServices.TSFunctionALogsManager();
            DataTable TSFunctionA = TSFunctionALogsManager.SelectForManagementPage(AgentId, ProjectId, FromDate, ToDate, PageIndex, int.Parse(ddlPageSize.SelectedValue));
            DataTable dtRecordCount = TSFunctionALogsManager.SelectCountForManagementPage(AgentId, ProjectId, FromDate, ToDate);
            for (int i = 0; i < TSFunctionA.Rows.Count; i++)
            {
                DataTable dt = ConvertJsonToDatatable2(TSFunctionA.Rows[i]["LogJson"].ToString());
                int CountLogjson = dt.Rows.Count;
                 int j = 0;
                do
                {
                    DataRow dr = dtGridView.NewRow();
                    dr["ProjectId"] = TSFunctionA.Rows[i]["ProjectId"].ToString();
                    dr["RowNum"] = TSFunctionA.Rows[i]["RowNum"].ToString();

                    switch (TSFunctionA.Rows[i]["IngridiantMajor"].ToString())
                    {
                        case "3":
                            dr["IngridiantMajor"] = "عمران";
                            break;
                        case "5, 4":
                            dr["IngridiantMajor"] = "برق یا مکانیک";
                            break;
                        case "4, 5":
                            dr["IngridiantMajor"] = "برق یا مکانیک";
                            break;
                        case "4":
                            dr["IngridiantMajor"] = "مکانیک";
                            break;
                        case "5":
                            dr["IngridiantMajor"] = "برق";
                            break;
                        case "1":
                            dr["IngridiantMajor"] = "معماری";
                            break;
                        case "2":
                            dr["IngridiantMajor"] = "شهرسازی";
                            break;
                        case "6":
                            dr["IngridiantMajor"] = "نقشه برداری";
                            break;
                        case "7":
                            dr["IngridiantMajor"] = "ترافیک";
                            break;
                        case "1, 3":
                            dr["IngridiantMajor"] = "معماری یا عمران";
                            break;
                        case "3, 1":
                            dr["IngridiantMajor"] = "معماری یا عمران";
                            break;
                        default:
                            dr["IngridiantMajor"] = TSFunctionA.Rows[i]["IngridiantMajor"].ToString();
                            break;
                    }

                    dr["StageId"] = TSFunctionA.Rows[i]["StageId"].ToString();
                    switch (Convert.ToInt32(TSFunctionA.Rows[i]["StageId"]))
                    {
                        case 101:
                            dr["StageName"] = "فیلتر براساس ";
                            break;
                        case 102:
                            dr["StageName"] = "";
                            break;
                        case 103:
                            dr["StageName"] = "";
                            break;
                        case 1031:
                            dr["StageName"] = "";
                            break;
                        case 1041:
                            dr["StageName"] = "";
                            break;
                        case 1042:
                            dr["StageName"] = "";
                            break;
                        case 1043:
                            dr["StageName"] = "";
                            break;
                        case 1044:
                            dr["StageName"] = "";
                            break;
                        case 1045:
                            dr["StageName"] = "";
                            break;
                        case 1046:
                            dr["StageName"] = "";
                            break;
                        case 104:
                            dr["StageName"] = "";
                            break;
                        case 1051:
                            dr["StageName"] = "";
                            break;
                        case 1052:
                            dr["StageName"] = "";
                            break;
                        case 105:
                            dr["StageName"] = "";
                            break;
                        case 106:
                            dr["StageName"] = "";
                            break;
                        case 1062:
                            dr["StageName"] = "";
                            break;
                        case 107:
                            dr["StageName"] = "";
                            break;
                        case 1072:
                            dr["StageName"] = "";
                            break;
                        case 108:
                            dr["StageName"] = "";
                            break;
                        case 1082:
                            dr["StageName"] = "";
                            break;
                        case 109:
                            dr["StageName"] = "";
                            break;
                        case 1010:
                            dr["StageName"] = "";
                            break;
                        case 10102:
                            dr["StageName"] = "";
                            break;
                        case 1011:
                            dr["StageName"] = "";
                            break;

                        case (int)TSP.DataManager.TSObserverSelectLog.PercentOfCapacityUsageSort:
                            dr["StageName"] = "مرتب سازی براساس درصد مصرف ظرفیت نظارت";
                            break;
                        default:
                            dr["StageName"] = TSFunctionA.Rows[i]["StageId"].ToString();
                            break;
                    }

                    dr["Date"] = TSFunctionA.Rows[i]["Date"].ToString();

                    dr["MeId"] = CountLogjson>0 ?  dt.Rows[j]["MeId"].ToString(): "";
                    dr["Count"] = CountLogjson>0 ? dt.Rows.Count.ToString() : "0";
                    dr["ObsDate"] = CountLogjson>0 ? dt.Rows[j]["ObsDate"].ToString() : "";
                    dr["MembershipDate"] = CountLogjson>0 ? dt.Rows[j]["MembershipDate"].ToString() : "";
                    dr["CountRemainWorkCount"] = CountLogjson>0 ? dt.Rows[j]["CountRemainWorkCount"].ToString() : "";
                    dr["PercentOfCapacityUsage"] = CountLogjson>0 ? dt.Rows[j]["PercentOfCapacityUsage"].ToString() : "";
                    if (CountLogjson>0)
                    {
                        switch (dt.Rows[j]["WantedWorkType"].ToString())
                        {
                            case "0":
                                dr["WantedWorkType"] = "فقط نظارت";
                                break;
                            case "1":
                                dr["WantedWorkType"] = "فقط طراحی";
                                break;
                            case "2":
                                dr["WantedWorkType"] = "طراحی و نظارت";
                                break;
                            default:
                                dr["WantedWorkType"] = dt.Rows[j]["WantedWorkType"].ToString();
                                break;
                        }
                    }
                    else dr["WantedWorkType"] = "";
                    dr["Group1"] = CountLogjson>0 ? dt.Rows[j]["Group1"].ToString() : "";
                    dr["Group2"] = CountLogjson>0 ? dt.Rows[j]["Group2"].ToString() : "";
                    dr["Group3"] = CountLogjson>0 ? dt.Rows[j]["Group3"].ToString() : "";
                    dr["Group4"] = CountLogjson>0 ? dt.Rows[j]["Group4"].ToString() : "";

                    if (CountLogjson>0 && !Utility.IsDBNullOrNullValue(dt.Rows[j]["City1"]))
                    {
                        TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
                        CityManager.FindByCode(Convert.ToInt32(dt.Rows[j]["City1"].ToString()));
                        dr["City1"] = CityManager[0]["CitName"].ToString();
                    }
                    else
                        dr["City1"] = "";
                    if (CountLogjson>0 && !Utility.IsDBNullOrNullValue(dt.Rows[j]["City2"]))
                    {
                        TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
                        CityManager.FindByCode(Convert.ToInt32(dt.Rows[j]["City2"].ToString()));
                        dr["City2"] = CityManager[0]["CitName"].ToString();
                    }
                    else
                        dr["City2"] = "";

                    dr["UsedCapacity"] = CountLogjson>0 ? dt.Rows[j]["UsedCapacity"].ToString() : "";
                    dr["RemainCapacity"] = CountLogjson>0 ? dt.Rows[j]["RemainCapacity"].ToString() : "";
                    dr["RemainCapacityObs"] = CountLogjson>0 ? dt.Rows[j]["RemainCapacityObs"].ToString() : "";
                    dr["RemainCapacityObs"] = CountLogjson>0 ? dt.Rows[j]["RemainCapacityObs"].ToString() : "";
                    dr["RemainCapacityObsReal"] = CountLogjson>0 ? dt.Rows[j]["RemainCapacityObsReal"].ToString() : "";
                    dr["TotalCapacity"] = CountLogjson>0 ? dt.Rows[j]["TotalCapacity"].ToString() : "";
                    dr["CapacityObs"] = CountLogjson>0 ? dt.Rows[j]["CapacityObs"].ToString() : "";
                    dr["CapacityDesign"] = CountLogjson>0 ? dt.Rows[j]["CapacityDesign"].ToString() : "";
                    dtGridView.Rows.Add(dr);
                    dtGridView.AcceptChanges();
                    j++; } while (j < dt.Rows.Count);

               
            }
            GridViewTraceObserverSelected.DataSource = dtGridView;
            GridViewTraceObserverSelected.SettingsPager.PageSize = dtGridView.Rows.Count;
            GridViewTraceObserverSelected.DataBind();
            //Session["dtGridView"] = GridViewTraceObserverSelected.DataSource = dtGridView;
            PopulatePager(Convert.ToInt32(dtRecordCount.Rows[0]["CountRow"]), PageIndex);
        }

        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }

    }

    private void PopulatePager(int recordCount, int currentPage)
    {
        double dblPageCount = (double)((decimal)recordCount / decimal.Parse(ddlPageSize.SelectedValue));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            pages.Add(new ListItem("First", "1", currentPage > 1));
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
            pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }
}