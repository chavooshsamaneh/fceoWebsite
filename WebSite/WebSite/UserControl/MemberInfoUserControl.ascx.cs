using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class UserControl_MemberInfoUserControl : System.Web.UI.UserControl
{
    private int _MeId;
    [Browsable(true), Category("TSP")]
    public int MeId
    {
        get
        {
            return this._MeId;
        }
        set
        {
            this._MeId = value;
        }
    }

    private int _MReId;
    [Browsable(true), Category("TSP")]
    public int MReId
    {
        get
        {
            return this._MReId;
        }
        set
        {
            this._MReId = value;
        }
    }
    private bool _IsMeTemp;
    [Browsable(true), Category("TSP")]
    public bool IsMeTemp
    {
        get
        {
            return this._IsMeTemp;
        }
        set
        {
            if (Utility.IsDBNullOrNullValue(value))
                this._IsMeTemp = false;
            else
                this._IsMeTemp = value;
        }
    }

    private int _MemberInfoUserControRequester;
    [Browsable(true), Category("TSP")]
    public int MemberInfoUserControRequester
    {
        get
        {
            return this._MemberInfoUserControRequester;
        }
        set
        {
            if (Utility.IsDBNullOrNullValue(value))
                this._MemberInfoUserControRequester = (int)TSP.DataManager.MemberInfoUserControRequester.Member;
            else
                this._MemberInfoUserControRequester = value;
        }
    }
    private string _MeName;
    [Browsable(true), Category("TSP")]
    public string MeName
    {
        get
        {
            return this._MeName;
        }
        set
        {
            this._MeName = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        switch (MemberInfoUserControRequester)
        {
            case (int)TSP.DataManager.MemberInfoUserControRequester.Member:
                Fill(this._MReId);
                break;
            case (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile:

                Fill(this._MeId, this._IsMeTemp);
                break;
        }
    }
    public void Fill(int MeId, Boolean IsMeTemp)
    {
        string SexName = "";
        string TiName = "";
        string MeName = "";
        if (!IsMeTemp)
        {
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count <= 0)
            {
                TblMemberInfo.Visible = false;
                lblErrorText.Visible = true;
                lblErrorText.Text = "خطایی در ارتباط با سرور به وجود آمده است";
                return;
            }

            lblMeId.Text = MemberManager[0]["MeId"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MeNo"]))
                lblMeNo.Text = MemberManager[0]["MeNo"].ToString();
            else
                lblMeNo.Text = "----";
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SSN"]))
                lblCodeMelli.Text = MemberManager[0]["SSN"].ToString();
            else
                lblCodeMelli.Text = "----";

            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SexName1"]))
                SexName = MemberManager[0]["SexName1"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["TiName"]))
                TiName = MemberManager[0]["TiName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MeName"]))
                MeName = MemberManager[0]["MeName"].ToString();
            lblName.Text = MemberManager[0]["SexName1"].ToString() + " " + MemberManager[0]["TiName"].ToString() + " " + MemberManager[0]["MeName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileDate"]))
                lblFileDate.Text = MemberManager[0]["FileDate"].ToString();
            else
                lblFileDate.Text = "----";
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileNo"]))
                lblFileNo.Text = MemberManager[0]["FileNo"].ToString();
            else
                lblFileNo.Text = "----";
        }
        else if (IsMeTemp)
        {
            TSP.DataManager.TempMemberManager TempMemberManager = new TSP.DataManager.TempMemberManager();
            TempMemberManager.FindByCode(MeId);
            if (TempMemberManager.Count <= 0)
            {
                TblMemberInfo.Visible = false;
                lblErrorText.Visible = true;
                lblErrorText.Text = "خطایی در ارتباط با سرور به وجود آمده است";
                return;
            }
            lblMeIdTitle.Text = "نام کاربری";
            lblMeId.Text = "M" + TempMemberManager[0]["TMeId"].ToString();
            lblMeNo.Text = "----";
            if (!Utility.IsDBNullOrNullValue(TempMemberManager[0]["SSN"]))
                lblCodeMelli.Text = TempMemberManager[0]["SSN"].ToString();
            else
                lblCodeMelli.Text = "----";
            if (!Utility.IsDBNullOrNullValue(TempMemberManager[0]["SexName1"]))
                SexName = TempMemberManager[0]["SexName1"].ToString();
            if (!Utility.IsDBNullOrNullValue(TempMemberManager[0]["TiName"]))
                TiName = TempMemberManager[0]["TiName"].ToString();
            if (!Utility.IsDBNullOrNullValue(TempMemberManager[0]["MeName"]))
                MeName = TempMemberManager[0]["MeName"].ToString();
            lblName.Text = TempMemberManager[0]["SexName1"].ToString() + " " + TempMemberManager[0]["TiName"].ToString() + " " + TempMemberManager[0]["MeName"].ToString();
            lblFileDate.Text = "----";
            lblFileNo.Text = "----";
        }
        switch (MemberInfoUserControRequester)
        {
            case (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile:
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                System.Data.DataTable dtDocMeFile = DocMemberFileManager.SelectMainRequest(MeId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
                if (!Utility.IsDBNullOrNullValue(dtDocMeFile.Rows[0]["TaskName"]))
                    lblWorkFlowState.Text = "وضعیت درخواست: " + dtDocMeFile.Rows[0]["TaskName"].ToString();
                else
                    lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
                break;
            default:
                lblWorkFlowState.Text = "";
                break;
        }
    }

    public void Fill(int MReId)
    {
        string SexName = "";
        string TiName = "";
        string MeName = "";

        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        System.Data.DataTable dtMeInfo = MemberRequestManager.SelectMemberRequestForMeUserControl(MReId);
        if (dtMeInfo.Rows.Count <= 0)
        {
            TblMemberInfo.Visible = false;
            lblErrorText.Visible = true;
            lblErrorText.Text = "خطایی در ارتباط با سرور به وجود آمده است";
            return;
        }

        lblMeId.Text = dtMeInfo.Rows[0]["MeId"].ToString();
        if (!Utility.IsDBNullOrNullValue(dtMeInfo.Rows[0]["MeNo"]))
            lblMeNo.Text = dtMeInfo.Rows[0]["MeNo"].ToString();
        else
            lblMeNo.Text = "----";
        if (!Utility.IsDBNullOrNullValue(dtMeInfo.Rows[0]["SSN"]))
            lblCodeMelli.Text = dtMeInfo.Rows[0]["SSN"].ToString();
        else
            lblCodeMelli.Text = "----";

        if (!Utility.IsDBNullOrNullValue(dtMeInfo.Rows[0]["SexName"]))
            SexName = dtMeInfo.Rows[0]["SexName"].ToString();
        if (!Utility.IsDBNullOrNullValue(dtMeInfo.Rows[0]["TiName"]))
            TiName = dtMeInfo.Rows[0]["TiName"].ToString();
        if (!Utility.IsDBNullOrNullValue(dtMeInfo.Rows[0]["MeName"]))
            _MeName = MeName = dtMeInfo.Rows[0]["MeName"].ToString();
        lblName.Text = dtMeInfo.Rows[0]["SexName"].ToString() + " " + dtMeInfo.Rows[0]["TiName"].ToString() + " " + dtMeInfo.Rows[0]["MeName"].ToString();
        if (!Utility.IsDBNullOrNullValue(dtMeInfo.Rows[0]["FileDate"]))
            lblFileDate.Text = dtMeInfo.Rows[0]["FileDate"].ToString();
        else
            lblFileDate.Text = "----";
        if (!Utility.IsDBNullOrNullValue(dtMeInfo.Rows[0]["FileNo"]))
            lblFileNo.Text = dtMeInfo.Rows[0]["FileNo"].ToString();
        else
            lblFileNo.Text = "----";       
    }
}