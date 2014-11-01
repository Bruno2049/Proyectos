using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Helpers;

namespace PAEEEM.CentralModule
{
    public partial class Solicitud : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void wizardPages_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                Page.Validate("Siguiente"); //force to validate the page controls

                if (!Page.IsValid)
                {
                    e.Cancel = true;
                    return;
                }

                if (e.CurrentStepIndex == 0) //First step
                {
                    //
                }
                if (e.CurrentStepIndex == 1) //Second step
                {
                    wizardPages.ActiveStepIndex = 2;
                }
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError", "alert('" + ex.Message.Replace('\'', '"') + "');", true);
                new LsApplicationException(this, "Credit Request", ex, true);
            }

        }

        protected void wizardPages_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnCancel3_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void BtnCacel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}