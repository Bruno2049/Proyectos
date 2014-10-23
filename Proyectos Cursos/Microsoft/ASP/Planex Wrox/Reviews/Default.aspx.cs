using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlanetWroxModel;

public partial class Reviews_Default : BasePage
{
  protected void Page_Load(object sender, EventArgs e)
  {
    using (PlanetWroxEntities myEntities = new PlanetWroxEntities())
    {
      if (Profile.FavoriteGenres.Count > 0)
      {
        var favGenres = from genre in myEntities.Genres.Include("Reviews")
                        orderby genre.Name
                        where Profile.FavoriteGenres.Contains(genre.Id)
                        select new { genre.Name, genre.Reviews };
        GenreRepeater.DataSource = favGenres;
        GenreRepeater.DataBind();
      }
      GenreRepeater.Visible = GenreRepeater.Items.Count > 0;
      NoRecords.Visible = !GenreRepeater.Visible;
    }
  }
}