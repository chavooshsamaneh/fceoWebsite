using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class UserControl_MeMembershipLicenceInfoUserControl : System.Web.UI.UserControl
{
    private int _MeId = -2;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        FillMemberLicence(MeId);
    }

    public void FillMemberLicence(int MemberId)
    {
        MeId = MemberId;
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        System.Data.DataTable dtMeLicence = MemberLicenceManager.SelectByMemberId(MeId, 0);
        GridViewMeLicence.DataSource = dtMeLicence;
        GridViewMeLicence.DataBind();
    }

    public void Clear()
    {
        GridViewMeLicence.DataSource = null;
        GridViewMeLicence.DataBind();
    }

}