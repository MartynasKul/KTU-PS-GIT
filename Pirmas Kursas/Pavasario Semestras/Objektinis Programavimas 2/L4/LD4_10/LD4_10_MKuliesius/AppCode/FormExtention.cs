using LD4_10_MKuliesius.AppCode;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace LD4_10_MKuliesius
{
    public partial class Forma : System.Web.UI.Page
    {

        /// <summary>
        /// Checks if correct files were uploaded
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        public bool ProperFiles(FileUpload fileUpload) 
        {
            foreach(HttpPostedFile file in fileUpload.PostedFiles) 
            {
                if(!(file.FileName.EndsWith(".txt") || file.FileName.EndsWith(".csv")))
                {
                    return false;
                }
            }
            return fileUpload.HasFiles;
        }

        /// <summary>
        /// Copies files to the folder
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <param name="path"></param>
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
        /// Isveda siemens gaminiu kieki i lentele ekrane
        /// </summary>
        /// <param name="table"></param>
        /// <param name="siemensCount"></param>
        public void AddSiemensCountToTable(Table table, int siemensCount) 
        {
            TableCell text= new TableCell() 
            {
                Text="Is viso Siemens gaminiu yra: "
            };
            TableCell text2 = new TableCell()
            {
                Text = siemensCount.ToString()
            };

            TableRow tableRow = new TableRow()
            {
                Cells =
                {
                    text,
                    text2
                }
            };
            
            table.Rows.Add(tableRow);
        }


        /// <summary>
        /// Isveda pigiausius saldytuvus i lentele
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fridges"></param>
        public void CreateCheapFridgeTable(Table table, List<Fridge> fridges)
        {
            ///gamintojas modelis talpa kaina
            ///
            TableCell Title1 = new TableCell()
            {
                Text = "Gamintojas"
            };
            TableCell Title2 = new TableCell()
            {
                Text = "Modelis"
            };
            TableCell Title3 = new TableCell()
            {
                Text = "Talpa"
            };
            TableCell Title4 = new TableCell()
            {
                Text = "Kaina"
            };

            TableRow Row = new TableRow()
            {
                Cells =
                {
                    Title1,
                    Title2,
                    Title3,
                    Title4
                }
            };

            table.Rows.Add(Row);

            foreach (Fridge fridge in fridges) 
            {
                TableCell Maker = new TableCell()
                {
                    Text = fridge.Maker
                };
                TableCell Model = new TableCell()
                {
                    Text = fridge.Model
                };
                TableCell Capacity = new TableCell()
                {
                    Text = fridge.Capacity.ToString()
                };
                TableCell Price = new TableCell()
                {
                    Text = fridge.Price.ToString()
                };
                TableRow Row2 = new TableRow()
                {
                    Cells =
                    {
                        Maker,
                        Model,
                        Capacity,
                        Price
                    }
                };
                table.Rows.Add(Row2);
            }
        }
    }
}