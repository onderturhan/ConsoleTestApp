using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConfigurationWebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int i = 0;
                foreach (var item in ConfigurationManager.AppSettings)
                {

                    Label lbl = new Label();
                    lbl.ID = "lbl" + i;
                    lbl.Text = item.ToString();
                    pnl.Controls.Add(lbl);
                    i++;
                }
            }
        }
        protected void ButtonSave_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonGet_Click(object sender, EventArgs e)
        {

        }

        private static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private static void SetSetting(string key, string value)
        {
            Configuration configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}