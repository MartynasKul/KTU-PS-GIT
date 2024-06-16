using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SesijaWeb
{
    public partial class Forma1 : System.Web.UI.Page
    {
        private string storedText;

        protected void Page_Load(object sender, EventArgs e)
        {
            InsertRecord(storedText);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            storedText = Textbox1.Text;
            InsertRecord(storedText);
        }

        private void InsertRecord(string text)
        {
            TableCell cell = new TableCell();
            cell.Text = text;
            TableRow row = new TableRow(); 
            row.Cells.Add(cell);
            Table1.Rows.Add(row);

        }
    
    }
}