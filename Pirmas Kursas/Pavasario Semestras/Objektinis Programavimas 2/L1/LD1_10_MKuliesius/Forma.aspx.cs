using LD1_10_MKuliesius.AppClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LD1_10_MKuliesius
{
    public partial class Forma : System.Web.UI.Page
    {
        /// <summary>
        /// Duomenų ir išvedimo failai
        /// </summary>
        const string CFd = "U3.txt";
        const string CFr = "Result.txt";
        string dataPath;


        /// <summary>
        /// Metodas vykdantis nurodaytas užduotis užkraunant puslapį.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //Nuskaito pradinių duomenų failą ir išveda į teksto dėžutę
            dataPath = Server.MapPath(CFd).ToString();

            var data = File.ReadAllText(Server.MapPath(CFd));
            TextBox1.Text = data.ToString();

            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<Moles> moles = new List<Moles>();
            string[] lines = InOutUtils.ReadData(dataPath);
            TaskUtils.Work(lines, moles);

            TextBox2.Text= moles.Count.ToString(); 
            
            foreach(Moles mole in moles) 
            {
                TextBox2.Text = TextBox2.Text + "\n" + mole.ToString();
            }

            InOutUtils.PrintToFile(moles, CFr);

        }
    }
}