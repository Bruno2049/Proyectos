using PlanetWroxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reviews_All : BasePage
{
  protected void Page_Load(object sender, EventArgs e)
  {
    using (PlanetWroxEntities myEntities = new PlanetWroxEntities())
    {
      var authorizedReviews = from review in myEntities.Reviews
                              where review.Authorized == true
                              orderby review.CreateDateTime descending
                              select review;
      Repeater1.DataSource = authorizedReviews;
      Repeater1.DataBind();
    }
  }
}