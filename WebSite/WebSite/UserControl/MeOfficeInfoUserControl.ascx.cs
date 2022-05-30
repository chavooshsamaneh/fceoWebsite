using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class UserControl_MeOfficeInfoUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private Boolean _IsExpired;
    public Boolean IsExpired
    {
        get { return _IsExpired; }
    }
    private Boolean _HasOffice;
    public Boolean HasOffice
    {
        get { return _HasOffice; }
    }
    private Int32 _OfId;
    public Int32 OfId
    {
        get { return _OfId; }
    }
    private Int16 _MemberGradeInOffice;
    public Int16 MemberGradeInOffice
    {
        get { return _MemberGradeInOffice; }
    }
    private Boolean _HasEfficientGrade;
    public Boolean HasEfficientGrade
    {
        get { return _HasEfficientGrade; }
    }

    public int FillInfo(int MemberId)
    {
        DataTable dtOfficeMemberInfo = new DataTable();
        dtOfficeMemberInfo.Columns.Add("OfmId"); 
        dtOfficeMemberInfo.Columns["OfmId"].AutoIncrement = true;
        dtOfficeMemberInfo.Columns["OfmId"].AutoIncrementSeed = 1;
        dtOfficeMemberInfo.Constraints.Add("PK_ID", dtOfficeMemberInfo.Columns["OfmId"], true);
        dtOfficeMemberInfo.Columns.Add("OfId");
        dtOfficeMemberInfo.Columns.Add("OfName");
        dtOfficeMemberInfo.Columns.Add("OfficeFileNo");
        dtOfficeMemberInfo.Columns.Add("OfficeExp");
        dtOfficeMemberInfo.Columns.Add("MemberGradeInOffice");
        dtOfficeMemberInfo.Columns.Add("DesCode");
        dtOfficeMemberInfo.Columns.Add("OfficeManager");
        dtOfficeMemberInfo.Columns.Add("DocumentStatusName");
        dtOfficeMemberInfo.Columns.Add("LastReqCreatDate");
        dtOfficeMemberInfo.Columns.Add("TaskName");
        dtOfficeMemberInfo.Columns.Add("MembershipRequstType");
        dtOfficeMemberInfo.Columns.Add("MFTypeName");

         _HasEfficientGrade = false;
        _HasOffice = false;
        _OfId = -2;
        _IsExpired = false;
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        DataTable dtOfficeMeInfo = OfMeManager.SelectOfficeMemberForOfficeUserControl(MemberId);

        for (int i = 0; i < dtOfficeMeInfo.Rows.Count; i++)
        {
            DataRow drOfMe = dtOfficeMemberInfo.NewRow();
             drOfMe["OfmId"] = dtOfficeMeInfo.Rows[i]["OfmId"];
            drOfMe["DesCode"] = dtOfficeMeInfo.Rows[i]["DesCode"];
            drOfMe["OfId"] = dtOfficeMeInfo.Rows[i]["OfId"];
            drOfMe["OfName"] = dtOfficeMeInfo.Rows[i]["OfName"];
            drOfMe["OfficeFileNo"] = dtOfficeMeInfo.Rows[i]["OfficeFileNo"];
            drOfMe["OfficeExp"] = dtOfficeMeInfo.Rows[i]["OfficeExp"];
            if (Convert.ToInt16(dtOfficeMeInfo.Rows[i]["MFType"]) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)
            {
                _HasEfficientGrade = Convert.ToBoolean(dtOfficeMeInfo.Rows[i]["HasEfficientGrade"]);
                _HasOffice = true;
                _OfId = Convert.ToInt32(dtOfficeMeInfo.Rows[i]["OfId"]);
                _MemberGradeInOffice = Convert.ToInt16(dtOfficeMeInfo.Rows[i]["DesCode"]);

            }
            if (string.Compare(dtOfficeMeInfo.Rows[i]["OfficeExp"].ToString(), Utility.GetDateOfToday()) < 0)
            {
                drOfMe["OfficeExp"] += " (فاقد اعتبار)";
                //////// lblOfficeExp.ForeColor = System.Drawing.Color.Red;
                if (!(string.Compare(dtOfficeMeInfo.Rows[i]["OfficeExp"].ToString(), "1398/11/01") > 0 && string.Compare(dtOfficeMeInfo.Rows[i]["OfficeExp"].ToString(), "1400/04/31") <= 0))
                {
                    if (Convert.ToInt16(dtOfficeMeInfo.Rows[i]["MFType"]) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)
                        _IsExpired = true;
                }
            }
            switch (Convert.ToInt16(dtOfficeMeInfo.Rows[i]["DesCode"]))
            {
                case (int)TSP.DataManager.DocumentGrads.Grade1:
                    drOfMe["MemberGradeInOffice"] = "یک";
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade2:
                    drOfMe["MemberGradeInOffice"] = "دو";
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade3:
                    drOfMe["MemberGradeInOffice"] = "سه";
                    break;
                case (int)TSP.DataManager.DocumentGrads.Arshad:
                    drOfMe["MemberGradeInOffice"] = "ارشد";
                    break;
            }
            drOfMe["OfficeManager"] = dtOfficeMeInfo.Rows[i]["OfficeManager"]; ;
            drOfMe["DocumentStatusName"] = dtOfficeMeInfo.Rows[i]["DocumentStatusName"];
            drOfMe["LastReqCreatDate"] = dtOfficeMeInfo.Rows[i]["LastReqCreatDate"];
            drOfMe["MembershipRequstType"] = dtOfficeMeInfo.Rows[i]["MembershipRequstType"];
            drOfMe["TaskName"] = dtOfficeMeInfo.Rows[i]["TaskName"];
            drOfMe["MFTypeName"] = dtOfficeMeInfo.Rows[i]["MFTypeName"];
            dtOfficeMemberInfo.Rows.Add(drOfMe);

        }
        GridViewOfficeMember.DataSource = dtOfficeMemberInfo;
        GridViewOfficeMember.DataBind();

        return _OfId;
    }



    public void Clear()
    {

    }
}