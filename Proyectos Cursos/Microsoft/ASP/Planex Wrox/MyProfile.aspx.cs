using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _MyProfile : BasePage
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      FirstName.Text = Profile.FirstName;
      LastName.Text = Profile.LastName;
      DateOfBirth.Text = Profile.DateOfBirth.ToShortDateString();
      Bio.Text = Profile.Bio;
    }
  }

  protected void SaveButton_Click(object sender, EventArgs e)
  {
    if (Page.IsValid)
    {
      Profile.FirstName = FirstName.Text;
      Profile.LastName = LastName.Text;
      Profile.DateOfBirth = DateTime.Parse(DateOfBirth.Text);
      Profile.Bio = Bio.Text;

      // Clear the existing list
      Profile.FavoriteGenres.Clear();

      // Now add the selected genres
      foreach (ListItem myItem in PreferenceList.Items)
      {
        if (myItem.Selected)
        {
          Profile.FavoriteGenres.Add(Convert.ToInt32(myItem.Value));
        }
      }
    }
  }

  protected void PreferenceList_DataBound(object sender, EventArgs e)
  {
    foreach (ListItem myItem in PreferenceList.Items)
    {
      int currentValue = Convert.ToInt32(myItem.Value);
      if (Profile.FavoriteGenres.Contains(currentValue))
      {
        myItem.Selected = true;
      }
    }
  }
}