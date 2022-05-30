using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Members_Message_RecievedSms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            objdsSMS.SelectParameters["RecieverId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
            objdsSMS.SelectParameters["IsDelivered"].DefaultValue = "1";
            
        }
    }
}