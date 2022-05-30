using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Members_Message_ConflictManagment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObjdsConflictManagment.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
            GridViewConflictManagment.DataBind();
        }
    }
}