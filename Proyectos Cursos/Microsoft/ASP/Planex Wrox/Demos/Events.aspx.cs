using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.CompilerServices;

public partial class Demos_Events : BasePage
{
  protected void Page_Load(object sender, EventArgs e)
  {
    WriteMessage();
  }

  protected void Button1_Click(object sender, EventArgs e)
  {
    WriteMessage();
  }

  protected void GridView1_Sorted(object sender, EventArgs e)
  {
    WriteMessage();
  }

  protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
  {
    WriteMessage();
  }

  protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
  {
    WriteMessage();
  }

  protected void GridView1_DataBinding(object sender, EventArgs e)
  {
    WriteMessage();
  }

  protected void GridView1_DataBound(object sender, EventArgs e)
  {
    WriteMessage();
  }

  protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
  {
    WriteMessage();
  }

  protected void EntityDataSource1_ContextCreating(object sender, EntityDataSourceContextCreatingEventArgs e)
  {
    WriteMessage();
  }

  protected void EntityDataSource1_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
  {
    WriteMessage();
  }

  protected void Page_PreRenderComplete(object sender, EventArgs e)
  {
    WriteMessage("Page_PreRenderComplete<br /> — — — — — — — — ");
  }

  private void WriteMessage([CallerMemberName] string handlerName = "")
  {
    if (Page.IsPostBack)
    {
      PostBack.Text += handlerName + "<br />";
    }
    else
    {
      NoPostBack.Text += handlerName + "<br />";
    }
  }
}