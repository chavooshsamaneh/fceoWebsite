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

public partial class Introduction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TSP.DataManager.IntroductionManager IntroductionManager = new TSP.DataManager.IntroductionManager();
            IntroductionManager.FindByType((int)TSP.DataManager.IntroductionManager.Type.Introduction);

            if (IntroductionManager.Count>0)
            {
                if (!Utility.IsDBNullOrNullValue( IntroductionManager[0]["IntText"]))
                    labelIntroduction.InnerHtml = IntroductionManager[0]["IntText"].ToString();
            }
        }
    }
}
