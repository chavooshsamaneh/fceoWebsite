using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_MembersRegister_MemberLicenceReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.MemberLicenceManager.GetUserPermissionForLicenceReport(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnExportExcel.Enabled =btnExportExcel2.Enabled=GridViewMember.Visible= per.CanView;
        if (IsPostBack) 
        {
        
        }
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        Search();
    }


    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Memberlicence";
        GridViewExporter.WriteXlsToResponse(true);
    }

    private void Search() 
    {
        if (!string.IsNullOrWhiteSpace(txtMeIdFrom.Text))
            ObjdsMembers.SelectParameters["MeIdFrom"].DefaultValue = txtMeIdFrom.Text;
        else
            ObjdsMembers.SelectParameters["MeIdFrom"].DefaultValue = "-1";

        if (!string.IsNullOrWhiteSpace(txtMeIdFrom.Text))
            ObjdsMembers.SelectParameters["MeIdTo"].DefaultValue = txtMeIdTo.Text;
        else
            ObjdsMembers.SelectParameters["MeIdTo"].DefaultValue = "-1";
    }
}