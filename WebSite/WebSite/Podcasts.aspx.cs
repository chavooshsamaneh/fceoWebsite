using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Podcasts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSourcePodcast.SelectParameters["ImageType"].DefaultValue = ((int)TSP.DataManager.SiteImageManager.SiteImageType.Voice).ToString();
    }
}