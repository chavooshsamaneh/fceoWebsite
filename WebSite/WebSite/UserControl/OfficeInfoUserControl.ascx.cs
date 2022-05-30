using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class UserControl_OfficeInfoUserControl : System.Web.UI.UserControl
{
    //private int _OfId;
    //[Browsable(true), Category("TSP")]
    //public int OfId
    //{
    //    get
    //    {
    //        return this._OfId;
    //    }
    //    set
    //    {
    //        this._OfId = value;
    //    }
    //}

    private int _OfReId;
    [Browsable(true), Category("TSP")]
    public int OfReId
    {
        get
        {
            return this._OfReId;
        }
        set
        {
            this._OfReId = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Fill(this._OfReId);
    }
    public void Fill(int OfReId)
    {
        try
        {
            lblOfId.Text = ""; lblOfficeType.Text = ""; lblOfName.Text = "";
            lblOfNo.Text = ""; lblManagerName.Text = ""; lblDocStatus.Text = "";

            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
            ReqManager.FindByCode(OfReId);
            if (ReqManager.Count != 1)
            {
                lblErrorText.Text = "خطایی در بازخوانی اطلاعات شرکت ایجاد گردیده است.";
                return;
            }
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["OfId"]))
                lblOfId.Text = ReqManager[0]["OfId"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["OtName"]))
                lblOfficeType.Text = ReqManager[0]["OtName"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["OfName"]))
                lblOfName.Text = ReqManager[0]["OfName"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MeNo"]))
                lblOfNo.Text = ReqManager[0]["MeNo"].ToString();

            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["DocumentStatusName"]))
                lblDocStatus.Text = ReqManager[0]["DocumentStatusName"].ToString();
            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            System.Data.DataTable dt = OfManager.SelectOfficeManagerByOfId(Convert.ToInt32(ReqManager[0]["OfId"]), 0, _OfReId);
            if (dt.Rows.Count != 0)
            {
                if (!Utility.IsDBNullOrNullValue(dt.Rows[0]["FirstName"]) && !Utility.IsDBNullOrNullValue(dt.Rows[0]["LastName"]))
                    lblManagerName.Text = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
            }


        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }
}