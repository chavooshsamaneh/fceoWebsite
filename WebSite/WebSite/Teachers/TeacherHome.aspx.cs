using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Teachers_TeacherHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
          
        }
    }
    //protected void FillForm(int TeId)
    //{
    //    TSP.DataManager.TeacherManager TeManager = new TSP.DataManager.TeacherManager();
    //    try
    //    {
    //        TeManager.FindByCode(TeId);
    //        if (TeManager.Count == 1)
    //        {
    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["Address"]))
    //                lblAddress.Text = TeManager[0]["Address"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["BirthDate"]))
    //                lblBirthDate.Text = TeManager[0]["BirthDate"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["Description"]))
    //                lblDesc.Text = TeManager[0]["Description"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["Email"]))
    //                lblEmail.Text = TeManager[0]["Email"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["Family"]))
    //                lblFamily.Text = TeManager[0]["Family"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["Father"]))
    //                lblFatherName.Text = TeManager[0]["Father"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["IdNo"]))
    //                lblIdNo.Text = TeManager[0]["IdNo"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["InActiveName"]))
    //                lblInActiveName.Text = TeManager[0]["InActiveName"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["LiName"]))
    //                lblLiName.Text = TeManager[0]["LiName"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["MjName"]))
    //                lblMjName.Text = TeManager[0]["MjName"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["MobileNo"]))
    //                lblMobileNo.Text = TeManager[0]["MobileNo"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["Name"]))
    //                lblName.Text = TeManager[0]["Name"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["SSN"]))
    //                lblSSN.Text = TeManager[0]["SSN"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["Tel"]))
    //                lblTel.Text = TeManager[0]["Tel"].ToString();

    //            if (!Utility.IsDBNullOrNullValue(TeManager[0]["TiName"]))
    //                lblTiName.Text = TeManager[0]["TiName"].ToString();
    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = " امکان مشاهده اطلاعات وجود ندارد";
    //        }

    //    }
    //    catch (Exception err)
    //    {
    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
    //    }
    //}

    
}