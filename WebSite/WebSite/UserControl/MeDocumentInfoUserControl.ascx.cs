using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class UserControl_MeDocumentInfoUserControl : System.Web.UI.UserControl
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
            txtbFileNo.Text = MemberManager[0]["FileNo"].ToString();
            txtbFileDate.Text = MemberManager[0]["FileDate"].ToString();
            txtbTrafficeId.Text = MemberManager[0]["TrafficGrdName"].ToString();
            txtbUrbenismId.Text = MemberManager[0]["UrbanismGrdName"].ToString();
            txtbMappingId.Text = MemberManager[0]["MappingGrdName"].ToString();

            txtbDesGrade.Text = MemberManager[0]["DesGrdName"].ToString();
            txtbObsGrade.Text = MemberManager[0]["ObsGrdName"].ToString();
            txtbImpGrade.Text = MemberManager[0]["ImpGrdName"].ToString();
            txtGasId.Text = MemberManager[0]["GasGrdName"].ToString();

            
            IsMeIdValid = true;           
        }
        else
            Clear();
    }
    public void Clear()
    {
        MeId = -2;
        txtbFileNo.Text =
            txtbFileDate.Text =
            txtbTrafficeId.Text =
            txtbUrbenismId.Text =
            txtbMappingId.Text =
            txtbDesGrade.Text =
            txtbObsGrade.Text =
            txtbImpGrade.Text = txtGasId.Text = "- - -";
        IsMeIdValid = false;

    }

}