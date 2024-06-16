using LD5_10_MKuliesius.AppCode;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace LD5_10_MKuliesius
{
    public partial class Forma : System.Web.UI.Page
    {
        /// <summary>
        /// Checks if correct files were uploaded
        /// </summary>
        /// <param name="fileUpload"> fileUpload element from form</param>
        /// <returns></returns>
        public bool ProperFiles(FileUpload fileUpload)
        {
            foreach (HttpPostedFile file in fileUpload.PostedFiles)
            {
                if (!(file.FileName.EndsWith(".txt") || file.FileName.EndsWith(".csv")))
                {
                    return false;
                }
            }
            return fileUpload.HasFiles;
        }

        /// <summary>
        /// Copies files to the folder
        /// </summary>
        /// <param name="fileUpload"> fileupload element from form</param>
        /// <param name="path"> path to create/delete</param>
        public void CopyFilesToServer(FileUpload fileUpload, string path)
        {
            IList<HttpPostedFile> files = fileUpload.PostedFiles;
            Directory.Delete(path, true);
            Directory.CreateDirectory(path);
            for (int i = 0; i < fileUpload.PostedFiles.Count; i++)
            {
                files[i].SaveAs(path + files[i].FileName);
            }
        }

        /// <summary>
        /// Binds months to dropdownList
        /// </summary>
        private void BindMonthsDropDown()
        {
            // Clear existing items
            DropDown1.Items.Clear();

            // Add items to the dropdown
            for (int i = 1; i <= 12; i++)
            {
                DropDown1.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        /// <summary>
        /// Method to display selected months revenues to table
        /// </summary>
        /// <param name="table"> table variable</param>
        /// <param name="publications"> list of publishers</param>
        /// <param name="selectedMonth"> selected month variable</param>
        public static void AddRevenueToTable(Table table, List<Publication> publications, int selectedMonth) 
        {
            TableCell text = new TableCell() 
            {
                Text = $"Pasirinktam mėnesiui {selectedMonth} leidėjų uždarbiai:"
            };

            TableRow tablerow = new TableRow() 
            {
                Cells = { text }
            };
            table.Rows.Add(tablerow);

            foreach (Publication publication in publications) 
            {
                string publisherInfo = $"Leidėjas {publication.PublisherName} už {publication.Name} uždirbo {publication.ProfitMonth}";
                table.Rows.Add(new TableRow { Cells = { new TableCell { Text = publisherInfo } } });
            }
        }

        /// <summary>
        /// Adds subscribers to table
        /// </summary>
        /// <param name="table"> table variable </param>
        /// <param name="orders"> List of subscribers </param>
        /// <param name="selectedMonth"> selected month variable </param>
        public static void AddSubscribersToTable(Table table, List<Order> orders, int selectedMonth) 
        {
            TableCell text = new TableCell()
            {
                Text = $"Pasirinkto mėnesio {selectedMonth} prenumeratoriai:"
            };

            TableRow tablerow = new TableRow()
            {
                Cells = { text }
            };
            table.Rows.Add(tablerow);

            foreach(Order order in orders) 
            {
                TableCell name = new TableCell() 
                {
                    Text = order.Name
                };

                TableCell address = new TableCell()
                {
                    Text = order.Address
                };

                TableCell publication = new TableCell() 
                {
                    Text = order.PublicationCode
                };

                TableRow tableRow = new TableRow()
                {
                    Cells = 
                    {
                        name,
                        address,
                        publication
                    }
                };
                
                table.Rows.Add(tableRow); 
            }
        }
    }
}