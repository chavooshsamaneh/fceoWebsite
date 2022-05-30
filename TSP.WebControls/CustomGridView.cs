using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("TSP.WebControls", "TSPControls")]
namespace TSP.WebControls
{
  //  [DefaultProperty("Text")]
  
    [ToolboxData("<{0}:CustomGridView runat=server></{0}:CustomGridView>")]
    public class CustomGridView : GridView
    {
        public CustomGridView()
        {
            
            this.RowDataBound += new GridViewRowEventHandler(CustomGridView_RowDataBound);
        }

        void CustomGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            if (row.RowType != DataControlRowType.DataRow) return;

            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (this.Columns[i].GetType() == typeof(CommandField))
                    continue;
                System.Data.DataRowView rowView = (System.Data.DataRowView)row.DataItem;

                if (rowView[i] is byte[])
                {
                    // Special treatment for byte arrays
                    row.Cells[i].Text = System.Text.Encoding.Unicode.GetString((byte[])rowView[i]);
                    byte [] b= System.Text.Encoding.Unicode.GetBytes(row.Cells[i].Text);
                }
            }
        }

        public override bool IsBindableType(Type type)
        {
            return (type == typeof(byte[])) || base.IsBindableType(type);
        }
      /*  protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);
            GridViewRow row = e.Row;
            if (row.RowType != DataControlRowType.DataRow) return;

            for (int i = 0; i < row.Cells.Count; ++i)
            {
                System.Data.DataRowView rowView = (System.Data.DataRowView)row.DataItem;

                if (rowView[i] is byte[])
                {
                    // Special treatment for byte arrays
                    row.Cells[i].Text = System.Text.Encoding.UTF8.GetString((byte[])rowView[i]);
                }
            }

        }*/
    }
}
