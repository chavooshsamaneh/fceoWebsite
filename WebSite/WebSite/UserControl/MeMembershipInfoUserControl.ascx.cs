using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class UserControl_MeMembershipInfoUserControl : System.Web.UI.UserControl
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

    private Boolean _IsMeIdValid = false;
    [Browsable(true), Category("TSP")]
    public Boolean IsMeIdValid
    {
        get
        {
            return this._IsMeIdValid;
        }
        set
        {
            this._IsMeIdValid = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        FillMember(MeId);
    }

    public void FillMember(int MemberId)
    {
        MeId = MemberId;
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count == 1)
        {

            txtbMeId.Text = MemberManager[0]["MeId"].ToString();
            txtbMeName.Text = MemberManager[0]["MeName"].ToString();
            txtbMeFatherName.Text = MemberManager[0]["FatherName"].ToString();
            txtbMeIdNo.Text = MemberManager[0]["IdNo"].ToString();
            txtbMeSSN.Text = MemberManager[0]["SSN"].ToString();
            txtbMeAgentName.Text = MemberManager[0]["AgentName"].ToString();
            IsMeIdValid = true;
        }
        else
            Clear();

    }

    public void Clear()
    {
        txtbMeId.Text =
                    txtbMeName.Text =
                    txtbMeFatherName.Text =
                    txtbMeIdNo.Text =
                    txtbMeSSN.Text =
                    txtbMeAgentName.Text = "- - -";
        IsMeIdValid = false;
        MeId = -2;
    }
}