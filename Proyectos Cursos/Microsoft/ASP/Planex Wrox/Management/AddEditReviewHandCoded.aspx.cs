using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlanetWroxModel;

public partial class Management_AddEditReviewHandCoded : System.Web.UI.Page
{
  int _id = -1;

  protected void Page_Load(object sender, EventArgs e)
  {
    if (!string.IsNullOrEmpty(Request.QueryString.Get("Id")))
    {
      _id = Convert.ToInt32(Request.QueryString.Get("Id"));
    }
    if (!Page.IsPostBack && _id > -1)
    {
      using (PlanetWroxEntities myEntities = new PlanetWroxEntities())
      {
        var review = (from r in myEntities.Reviews
                      where r.Id == _id
                      select r).SingleOrDefault();
        if (review != null)
        {
          TitleText.Text = review.Title;
          SummaryText.Text = review.Summary;
          BodyText.Text = review.Body;
          GenreList.DataBind();
          ListItem myItem = GenreList.Items.FindByValue(review.GenreId.ToString());
          if (myItem != null)
          {
            myItem.Selected = true;
          }
          Authorized.Checked = review.Authorized;
        }
      }
    }
  }

  protected void SaveButton_Click(object sender, EventArgs e)
  {
    using (PlanetWroxEntities myEntities = new PlanetWroxEntities())
    {
      Review myReview;
      if (_id == -1) // Insert new item
      {
        myReview = new Review();
        myReview.CreateDateTime = DateTime.Now;
        myReview.UpdateDateTime = myReview.CreateDateTime;
        myEntities.AddToReviews(myReview);
      }
      else // update existing item
      {
        myReview = (from r in myEntities.Reviews
                    where r.Id == _id
                    select r).Single();
        myReview.UpdateDateTime = DateTime.Now;
      }
      myReview.Title = TitleText.Text;
      myReview.Summary = SummaryText.Text;
      myReview.Body = BodyText.Text;
      myReview.GenreId = Convert.ToInt32(GenreList.SelectedValue);
      myReview.Authorized = Authorized.Checked;
      myEntities.SaveChanges();
      Response.Redirect("Reviews.aspx");
    }
  }
}