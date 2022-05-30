using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faq : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TSP.DataManager.IntroductionManager IntroductionManager = new TSP.DataManager.IntroductionManager();
            IntroductionManager.FindByType((int)TSP.DataManager.IntroductionManager.Type.FAQ);

            if (IntroductionManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(IntroductionManager[0]["IntText"]))
                    labelIntroduction.InnerHtml = IntroductionManager[0]["IntText"].ToString();
            }
        }
    }
}