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

public partial class Interduction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TSP.DataManager.IntroductionManager introductionManager =
                new TSP.DataManager.IntroductionManager();
            if (introductionManager.Fill() > 0)
            {
                //literalIntroduction.Text = introductionManager[0]["IntText"].ToString();
                labelIntroduction.Text = introductionManager[0]["IntText"].ToString();
            }
        }
    }
}
