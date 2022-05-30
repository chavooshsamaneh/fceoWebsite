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
using System.ComponentModel;
public partial class UserControl_PrintEnvelopeDetailsUserControl : System.Web.UI.UserControl
{

    public String ClientInstanceName
    {
        get { return popupChooseDetails.ClientInstanceName; }
        //set { popupChooseDetails.ClientInstanceName = value; }
    }

    public int AddressType
    {
        get { return Convert.ToInt32(comboAddress.SelectedItem.Value); }
    }

    public bool PageBreak
    {
        get
        {
            int PrintType = Convert.ToInt32(comboPrintType.SelectedItem.Value.ToString());
            if (PrintType == 0)
                return false;
            else
                return true;
        }
    }

    public int SId
    {
        get { return Convert.ToInt32(comboSecretariat.SelectedItem.Value.ToString()); }
    }

    private String _CallbackName;
    public String CallbackName
    {
        get { return _CallbackName; }
        set { _CallbackName = value; }
    }

    private String _CallbackParam;
    public String CallbackParam
    {
        get { return _CallbackParam; }
        set { _CallbackParam = value; }
    }

    string printURL = "";
    [Browsable(true), Category("Behavior"), Description("printURL that's supposed to Redirect to")]
    public string PrintURL
    {
        get
        {
            return this.printURL;
        }
        set
        {
            this.printURL = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String script = "<script type='text/javascript'> function btnSaveClick(s,e){btnClosePopup.cpClose=1;" + popupChooseDetails.ClientInstanceName + ".Hide();}" +
                "function PerformCallback(s,e){if(btnClosePopup.cpClose==1){btnClosePopup.cpClose=0; " + _CallbackName + ".PerformCallback('" + _CallbackParam + "');}} </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetClick", script);

            ObjectDataSourceSecretariat.SelectParameters["EmpId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
            comboSecretariat.DataBind();
        }
    }


}
