using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PirmasWeb
{
    public partial class Forma1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

  

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "Labas " + Textbox1.Text + "!";
            Textbox1.Visible = false;
        }
    }
}