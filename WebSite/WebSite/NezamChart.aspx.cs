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
using DevExpress.Web.ASPxTreeList;

public partial class NezamChart : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

    protected void TreeListNmChart_NodeExpanding(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeCancelEventArgs e)
    {
        TreeListNmChart.DataBind();
    }

    protected void TreeListNmChart_NodeExpanded(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeEventArgs e)
    {
        TreeListNmChart.DataBind();

    }

    protected void TreeListNmChart_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
    {
        if (e.GetValue("FullName") != null)
        {
            if (string.IsNullOrEmpty(e.GetValue("FullName").ToString()))
            {
                e.Row.Font.Bold = true;
            }
            else
            {
                if (e.GetValue("IsMaster") != null)
                {
                    if (Convert.ToBoolean(e.GetValue("IsMaster")))
                    {
                        e.Row.BackColor = System.Drawing.Color.Wheat;
                    }
                }
                if (e.GetValue("IsMasterPosition") != null)
                {
                    if (Convert.ToBoolean(e.GetValue("IsMasterPosition")))
                    {
                        e.Row.ForeColor = System.Drawing.Color.Blue;
                    }
                }
                if (e.GetValue("InActive") != null)
                {
                    if (Convert.ToBoolean(e.GetValue("InActive")))
                    {

                        e.Row.ForeColor = System.Drawing.Color.LightSlateGray;
                    }
                }
            }
        }
    }
    #endregion

    #region Methods

    #endregion
}
