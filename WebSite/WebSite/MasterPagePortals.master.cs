using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class MasterPagePortals : System.Web.UI.MasterPage
{
    Boolean _IsMasder
    {
        set { HiddenFieldPage["IsMasder"] = Convert.ToBoolean(value); }
        get { return Convert.ToBoolean(HiddenFieldPage["IsMasder"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlControl MenueLeft = this.Master.FindControl("MenueLeft") as System.Web.UI.HtmlControls.HtmlControl;
        if (MenueLeft != null)
            MenueLeft.Visible = false;

        System.Web.UI.HtmlControls.HtmlControl divControl = this.Master.FindControl("FooterWebsitMasterPage") as System.Web.UI.HtmlControls.HtmlControl;
        if (divControl != null)
            divControl.Visible = false;
        if (!IsPostBack)
        {
            VerticalMenu.Visible = true;
            switch (Utility.GetCurrentUser_LoginType())
            {
                case (int)TSP.DataManager.UserType.Employee:
                    XmlDataSourceMenu.DataFile = "~/App_Data/NavBarItems/Employee.xml";

                    break;
                case (int)TSP.DataManager.UserType.Institute:
                    XmlDataSourceMenu.DataFile = "~/App_Data/NavBarItems/Institue.xml";
                    break;
                case (int)TSP.DataManager.UserType.Member:
                    XmlDataSourceMenu.DataFile = "~/App_Data/NavBarItems/Members.xml";

                    break;
                case (int)TSP.DataManager.UserType.Office:
                    XmlDataSourceMenu.DataFile = "~/App_Data/NavBarItems/Office.xml";
                    break;
                case (int)TSP.DataManager.UserType.Settlement:
                    XmlDataSourceMenu.DataFile = "~/App_Data/NavBarItems/Settlement.xml";
                    break;
                case (int)TSP.DataManager.UserType.Teacher:
                    XmlDataSourceMenu.DataFile = "~/App_Data/NavBarItems/Teachers.xml";
                    break;
                case (int)TSP.DataManager.UserType.TemporaryMembers:
                    XmlDataSourceMenu.DataFile = "~/App_Data/NavBarItems/TemporaryMembers.xml";
                    break;
                case (int)TSP.DataManager.UserType.TSProjectOwner:
                    XmlDataSourceMenu.DataFile = "~/App_Data/NavBarItems/Owner.xml";
                    break;
                default:
                    VerticalMenu.Visible = false;
                    break;
            }
            SetRightMenuPermission(Utility.GetCurrentUser_LoginType());
        }
    }

    private void SetRightMenuPermission(int UserType)
    {
        System.Xml.XmlDocument myXml = new System.Xml.XmlDocument();
        myXml = (System.Xml.XmlDocument)XmlDataSourceMenu.GetXmlDocument();

        System.Xml.XmlReader xtr = new System.Xml.XmlNodeReader(myXml);


        System.Data.DataSet ds = new System.Data.DataSet();
        System.Data.DataTable dtMenue = new System.Data.DataTable();
        System.Data.DataTable dtfilter = new System.Data.DataTable();

        ds.ReadXml(xtr);
        if (UserType == (int)TSP.DataManager.UserType.Employee)
            dtMenue = SetNavigationBarUserAccess(ds);
        else
            dtMenue = ds.Tables["NavBarGroup"];

        RepeaterVerticalMenu.DataSource = dtMenue;
        RepeaterVerticalMenu.DataBind();
    }

    private DataTable SetNavigationBarUserAccess(System.Data.DataSet DataSetXMLFile)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        LoginManager.FindByCode(UserId);
        if (LoginManager.Count == 1)
        {
            Boolean IsAdmin = _IsMasder = false;
            if (!Utility.IsDBNullOrNullValue(LoginManager[0]["IsAdmin"]) && Convert.ToBoolean(LoginManager[0]["IsAdmin"]))
            {
                _IsMasder = IsAdmin = true;
            }
            if (UserId == 1 || IsAdmin)
                return DataSetXMLFile.Tables["NavBarGroup"];
        }
        TSP.DataManager.UserRightManager UserRightManager = new TSP.DataManager.UserRightManager();
        DataTable dt = UserRightManager.SelectUserRightForNavigationBarAccess(UserId);
        if (dt.Rows.Count == 0)
            return new DataTable();
        string s = string.Join(",", dt.Rows.OfType<DataRow>().Select(r => r["TtCode"].ToString()));
        DataTable dtXML = DataSetXMLFile.Tables["NavBarGroup"].Select("Target in" + "(" + s + ")").CopyToDataTable();
        return dtXML;
    }

    protected void RepeaterVerticalMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRow[] dr; Repeater RepeaterSubMenu = (Repeater)e.Item.FindControl("RepeaterSubMenu");
        TSP.DataManager.UserRightManager UserRightManager = new TSP.DataManager.UserRightManager();
        System.Xml.XmlDocument myXml = new System.Xml.XmlDocument();
        myXml = (System.Xml.XmlDocument)XmlDataSourceMenu.GetXmlDocument();
        System.Xml.XmlReader xtr = new System.Xml.XmlNodeReader(myXml);
        System.Data.DataSet ds = new System.Data.DataSet();
        System.Data.DataTable dtSubMenue = new System.Data.DataTable();
        ds.ReadXml(xtr);

        dtSubMenue = ds.Tables["NavBarItem"].Select("NavBarGroup_Id=" + ((System.Data.DataRowView)(e.Item.DataItem)).Row["NavBarGroup_Id"].ToString()).CopyToDataTable();
        switch (Utility.GetCurrentUser_LoginType())
        {

            case (int)TSP.DataManager.UserType.Employee:

                if (!_IsMasder)
                {
                    DataTable dtSubTitle = UserRightManager.SelectUserRightForNavigationBarSubTitleAccess(Utility.GetCurrentUser_UserId(), Convert.ToInt32(((System.Data.DataRowView)(e.Item.DataItem)).Row["Target"]));
                    string s = string.Join(",", dtSubTitle.Rows.OfType<DataRow>().Select(r => r["TtCode"].ToString()));
                    DataTable dtXML = new DataTable();
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        if (dtSubMenue.Rows.Count != 0)
                        {

                            DataView dataviewSubMenue = new DataView(dtSubMenue);
                            dataviewSubMenue.RowFilter = "Name in" + "(" + s + ")";
                            RepeaterSubMenu.DataSource = dataviewSubMenue;
                            RepeaterSubMenu.DataBind();

                            //dr = dtSubMenue.Select("Name in" + "(" + s + ")");
                            //if (dr.Length > 0)
                            //    dtXML = dr.CopyToDataTable();
                        }
                    }
                    //RepeaterSubMenu.DataSource = dtXML;
                    //RepeaterSubMenu.DataBind();
                }
                else
                {
                    RepeaterSubMenu.DataSource = dtSubMenue;
                    RepeaterSubMenu.DataBind();
                }
                break;
            default:
                RepeaterSubMenu.DataSource = dtSubMenue;
                RepeaterSubMenu.DataBind();
                break;
        }
    }
}

