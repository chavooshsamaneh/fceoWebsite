using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class UserControl_MeEngOfficeInfoUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private Boolean _IsExpired;
    public Boolean IsExpired
    {
        get { return _IsExpired; }
    }

    private Boolean _HasEngOffice;
    public Boolean HasEngOffice
    {
        get { return _HasEngOffice; }
    }
    //private Boolean _HasGasCert;
    //public Boolean HasGasCert
    //{
    //    get { return _HasGasCert; }
    //}

    private Int32 _EngOfId;
    public Int32 EngOfId
    {
        get { return _EngOfId; }
    }

    private Int16 _MemberGradeInEngOffice;
    public Int16 MemberGradeInEngOffice
    {
        get { return _MemberGradeInEngOffice; }
    }

    public void FillInfo(int MeId)
    {
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocMemberFileDetailManager DocMeFiDetail = new TSP.DataManager.DocMemberFileDetailManager();
        DataTable dtEngOffice = OfMeManager.SelectEngOfficeMemberForUserControlMeEngOfInfo(MeId,
                        ((int)TSP.DataManager.EngOfficeConfirmationType.Pending).ToString() + ","
                        + ((int)TSP.DataManager.EngOfficeConfirmationType.Confirmed).ToString() + ","
                        + ((int)TSP.DataManager.EngOfficeConfirmationType.ConditionalApprove).ToString() + ",", 0);    
        _EngOfId = -2;
        if (dtEngOffice.Rows.Count > 0)
        {
            /////بدست آورد پایه شخص در شرکت
            DataTable dtEngOfficeRep = OfMeManager.FindEngOfficeMemberForWebServiceEsys(MeId);
            if (dtEngOfficeRep.Rows.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(dtEngOfficeRep.Rows[0]["MemberGradeInEngOffice"]))
                {
                    lblEngOfficeMemberGrade.Text = dtEngOfficeRep.Rows[0]["MemberGradeInEngOffice"].ToString();
                    _MemberGradeInEngOffice = Convert.ToInt16(dtEngOfficeRep.Rows[0]["DesCode"]);
                }
            }
            /////////
            _HasEngOffice = true;
            int OfId = _EngOfId = Convert.ToInt32(dtEngOffice.Rows[0]["OfId"]);
            if (!Utility.IsDBNullOrNullValue(dtEngOffice.Rows[0]["EngOffName"]))
                lblEngOfficeMembership.Text = dtEngOffice.Rows[0]["EngOffName"].ToString();
            lblEngOfficeId.Text = OfId.ToString();

            System.Collections.ArrayList ArrayEngOfInfo = FindEngOfficeExpireDateAndFileNo(OfId);
            lblengOffExp.Text = ArrayEngOfInfo[0].ToString();
            if (string.Compare(ArrayEngOfInfo[0].ToString(), Utility.GetDateOfToday()) < 0)
            {
                lblengOffExp.Text += " (فاقد اعتبار)";
                lblengOffExp.ForeColor = System.Drawing.Color.Red;
                if (!(string.Compare(ArrayEngOfInfo[0].ToString(), "1398/11/01") > 0 && string.Compare(ArrayEngOfInfo[0].ToString(), "1400/04/31") <= 0))
                {
                    _IsExpired = true;
                }
            }
            else
                _IsExpired = false;
            if (Convert.ToInt32(dtEngOffice.Rows[0]["EngIsConfirm"]) == (int)TSP.DataManager.EngOfficeConfirmationType.Cancel)
            {
                _IsExpired = true;
            }
            lblEngOfficeFileNo.Text = ArrayEngOfInfo[1].ToString();
            OfMeManager.selectActiveEngOfficemanager(OfId);
            if (OfMeManager.Count > 0)
            {
                lblEngOfficeManager.Text = OfMeManager[0]["MeName"].ToString();
            }
            TSP.DataManager.EngOffFileManager EngOffFileManager = new TSP.DataManager.EngOffFileManager();
            EngOffFileManager.FindByEngOfficeId(OfId);
            if (EngOffFileManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(EngOffFileManager[0]["OfficeConfirmStatus"]))
                    lblDocStatus.Text = EngOffFileManager[0]["OfficeConfirmStatus"].ToString();
                if (!Utility.IsDBNullOrNullValue(EngOffFileManager[0]["TaskName"]))
                    lblLastTaskName.Text = EngOffFileManager[0]["TaskName"].ToString();
                if (!Utility.IsDBNullOrNullValue(EngOffFileManager[0]["CreateDate"]))
                    lblLastReqCreatDate.Text = EngOffFileManager[0]["CreateDate"].ToString();
            }
        }
        else
            _HasEngOffice = false;

    }

    public void Clear()
    {
        lblEngOfficeMembership.Text =
        lblEngOfficeMemberGrade.Text =
        lblengOffExp.Text =
        lblEngOfficeFileNo.Text =
        lblEngOfficeId.Text =
        lblEngOfficeManager.Text =
        lblEngOfficeGrade.Text = "- - -";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="EngOfId"></param>
    /// <returns>ArrayInfo[0]:ExpireDate ; ArrayInfo[1]: FileNo</returns>
    private System.Collections.ArrayList FindEngOfficeExpireDateAndFileNo(int EngOfId)
    {
        System.Collections.ArrayList ArrayInfo = new System.Collections.ArrayList();
        ArrayInfo.Add("- - -");
        ArrayInfo.Add("- - -");
        TSP.DataManager.EngOfficeManager EngOfficeManager = new TSP.DataManager.EngOfficeManager();
        EngOfficeManager.FindByCode(EngOfId);
        if (EngOfficeManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(EngOfficeManager[0]["ExpireDateLastConfirmReq"]))
                ArrayInfo[0] = EngOfficeManager[0]["ExpireDateLastConfirmReq"].ToString();
            if (!Utility.IsDBNullOrNullValue(EngOfficeManager[0]["FileNo"]))
                ArrayInfo[1] = EngOfficeManager[0]["FileNo"].ToString();
            //return EngOfficeManager[0]["ExpireDate"].ToString();
        }
        return ArrayInfo;
    }

}