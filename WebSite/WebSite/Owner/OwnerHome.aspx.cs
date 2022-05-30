using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Owner_OwnerHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int PrjReId = -2;

            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            ProjectRequestManager.SelectRequestLastVersion(Utility.GetCurrentUser_MeId(), -1, -1);
            if (ProjectRequestManager.Count > 0)
                PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);
            prjInfo.Fill(PrjReId);

        }
    }
}