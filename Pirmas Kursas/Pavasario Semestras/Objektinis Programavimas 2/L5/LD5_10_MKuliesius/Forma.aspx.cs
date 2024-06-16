using LD5_10_MKuliesius.AppCode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace LD5_10_MKuliesius
{
    public partial class Forma : System.Web.UI.Page
    {
        string DataFolder;
        string ResultFolder;
        List<OrderContainer> OrdersContainer;
        List<Publication> Publications;

        /// <summary>
        /// Method active only on page start-up
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            DataFolder = Server.MapPath("/AppData/DataFolder/");
            ResultFolder = Server.MapPath("/AppData/ResultFolder/");

            if (!IsPostBack) 
            {
                BindMonthsDropDown();
            }
        }


        /// <summary>
        /// Method that activates when user presses first button
        /// </summary>
        protected void Button1_Click(object sender, EventArgs e)
        {
            File.Delete(Server.MapPath("AppData/Rezultatai.txt"));

            if (ProperFiles(FileUpload1))
            {
                CopyFilesToServer(FileUpload1, DataFolder);
                //CopyFilesToServer(FileUpload2 , DataFolder); when using this, program breaks, so i just read the publication file in the normal way

                Directory.Delete(ResultFolder, true);
                Directory.CreateDirectory(ResultFolder);

                string path2 = Server.MapPath(FileUpload2.FileName);

                string file2 = Server.HtmlEncode(FileUpload2.FileName);
                string extension2 = Path.GetExtension(file2);

                try
                {
                    Debug.WriteLine("ABOBA");
                    OrdersContainer = InOut.ReadOrders(DataFolder);
                    FileUpload2.SaveAs(path2);

                    Publications = InOut.ReadPublications(path2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ABOBA FAILED READ FILES");
                    Label1.Visible = true;
                    Label1.BackColor = System.Drawing.Color.Red;
                    Label1.Text = "Nepavyko nuskaityti bent vieno is duomenu failu:" + ex.Message;

                    throw new Exception("Nepavyko nuskaityti bent vieno is duomenu failu:" + ex.Message);
                }

                if (OrdersContainer.Count > 0 && Publications.Count > 0)
                {
                    Label1.Visible = true;
                    Label1.BackColor = System.Drawing.Color.Green;
                    Label1.Text = "Failai nuskaityti teisingai";

                    Button2.Visible = true;
                    Button3.Visible = true;
                    Button4.Visible = true;
                    DropDown1.Visible = true;

                    // Storing lists to session
                    Session["OrdersContainer"] = OrdersContainer;

                    Session["Publications"] = Publications;

                }
                else 
                {
                    Debug.WriteLine("ABOBA FAILED READ FILES");
                    Label1.Visible = true;
                    Label1.BackColor = System.Drawing.Color.Red;
                    Label1.Text = "Nepavyko nuskaityti failų";
                    throw new Exception("Programa nenuskaitė teisingai failų, prašome perkrauti");
                }
            }
            else
            {
                Debug.WriteLine("ABOBA FAILED UPLOAD FILES");
                Label1.Visible = true;
                Label1.BackColor = System.Drawing.Color.Red;
                Label1.Text = "Nepasirinkti failai arba netinkami failai";
            }
        }

        /// <summary>
        /// Method for button 2 handling and doing task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            Table1.Visible = true;
            // Retrieving OrdersContainer from Session
            List<OrderContainer> ordersContainer = Session["OrdersContainer"] as List<OrderContainer>;

            // Retrieving Publications from Session
            List<Publication> publications = Session["Publications"] as List<Publication>;

            if (DropDown1.SelectedValue != null)
            {
                int selectedMonth = int.Parse(DropDown1.SelectedValue);
                var updatedPublications = TaskUtils.CalculateRevenueByPublisher(ordersContainer, publications, selectedMonth);
                Session["UpdatedPublications"] = updatedPublications;
                Label1.BackColor = System.Drawing.Color.Green;
                Label1.Text = $"Leidėjų pasirinkto mėnesio {selectedMonth} uždirbtas pelnas atvaizduotas lentelėje";
                AddRevenueToTable(Table1, updatedPublications, selectedMonth );
            }
            else 
            {
                Label1.BackColor = System.Drawing.Color.Red;
                Label1.Text = "Pasirinkite norimą mėnesį sąraše.";

            }
        }

        /// <summary>
        /// Method for button 3 handling and doing task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button3_Click(object sender, EventArgs e)
        {
            Table1.Visible = true;
            
            // Retrieving Publications from Session
            List<Publication> publications = Session["UpdatedPublications"] as List<Publication>;

            int selectedMonth = int.Parse(DropDown1.SelectedValue);
            TaskUtils.SortPublicationsByRevenueAndName(publications);
            
            Label1.BackColor = System.Drawing.Color.Green;
            Label1.Text = $"Surikiuota leidėjų informacija atvaizduota lentelėje";
            AddRevenueToTable(Table1, publications, selectedMonth);
        }

        /// <summary>
        /// Method for button 4 handling and doing task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button4_Click(object sender, EventArgs e)
        {
            Table1.Visible = true;
            // Retrieving OrdersContainer from Session
            List<OrderContainer> ordersContainer = Session["OrdersContainer"] as List<OrderContainer>;

            if (DropDown1.SelectedValue != null)
            {
                int selectedMonth = int.Parse(DropDown1.SelectedValue);
                var selectedSubscriptions = TaskUtils.GetSubscriptionsForSelectedMonth(ordersContainer, selectedMonth);

                Label1.BackColor = System.Drawing.Color.Green;
                Label1.Text = $"Leidėjų pasirinkto mėnesio {selectedMonth} uždirbtas pelnas atvaizduotas lentelėje";
                AddSubscribersToTable(Table1 , selectedSubscriptions, selectedMonth);
            }
            else
            {
                Label1.BackColor = System.Drawing.Color.Red;
                Label1.Text = "Pasirinkite norimą mėnesį sąraše.";

            }
        }
    }
}