using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class UserControl_SeminarInfoUserControl : System.Web.UI.UserControl
{
    private int _SeId;
    [Browsable(true), Category("TSP")]
    public int SeId
    {
        get
        {
            return this._SeId;
        }
        set
        {
            this._SeId = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
        SeminarManager.FindByCode(_SeId);
        if (SeminarManager.Count <= 0)
        {
            TblMemberInfo.Visible = false;
            lblErrorText.Visible = true;
            lblErrorText.Text = "خطایی در ارتباط با سرور به وجود آمده است";
            return;
        }
        if (!Utility.IsDBNullOrNullValue(SeminarManager[0]["Subject"]))
            lblSeminarSubject.Text = SeminarManager[0]["Subject"].ToString();
        else
            lblSeminarSubject.Text = "----";

        if (!Utility.IsDBNullOrNullValue(SeminarManager[0]["InsName"]))
            lblIns.Text = SeminarManager[0]["InsName"].ToString();
        else
            lblIns.Text = "----";

        if (!Utility.IsDBNullOrNullValue(SeminarManager[0]["StartDate"]))
            lblStartDate.Text = SeminarManager[0]["StartDate"].ToString();
        else
            lblStartDate.Text = "----";

        if (!Utility.IsDBNullOrNullValue(SeminarManager[0]["EndDate"]))
            lblEndDate.Text = SeminarManager[0]["EndDate"].ToString();
        else
            lblEndDate.Text = "----";
    }
}