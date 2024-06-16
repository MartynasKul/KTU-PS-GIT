using LD4_10_MKuliesius.AppCode;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;

namespace LD4_10_MKuliesius
{
    ///
    public partial class Forma : System.Web.UI.Page
    {
        string DataFolder;
        string ResultFolder;
        List<DevicesContainer> AllDataContainers = new List<DevicesContainer>();
        List<DevicesContainer> AllContainers = new List<DevicesContainer>();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataFolder = Server.MapPath("/AppData/DataFolder/");
            ResultFolder = Server.MapPath("/AppData/ResultFolder/");
        }

        protected void Button1_Click(object sender, EventArgs e) 
        {
            File.Delete(Server.MapPath("AppData/Rezultatai.txt"));

            if (ProperFiles(FileUpload1))
            {
                CopyFilesToServer(FileUpload1, DataFolder);
                Directory.Delete(ResultFolder, true);
                Directory.CreateDirectory(ResultFolder);

                try
                {
                    Debug.WriteLine("ABOBA");
                    AllContainers = InOut.ReadDevices(DataFolder);
                    AllDataContainers = InOut.ReadDevices(DataFolder);
                }
                catch (Exception ex) 
                {
                    Label5.Visible = true;
                    Debug.WriteLine("ABOBA2");
                    Label5.BackColor = System.Drawing.Color.Red;
                    Debug.WriteLine("Nepavyko nuskaityti bent vieno is duomenu failu:" + ex.Message);
                    Label5.Text = "Nepavyko nuskaityti bent vieno is duomenu failu:" + ex.Message;
                    //Controls.Add(new Label() { Text = "Nepavyko nuskaityti bent vieno is duomenu failu:" + ex.Message }); testavimui
                    throw new Exception("Nepavyko nuskaityti bent vieno is duomenu failu:" + ex.Message);
                }
                if (AllContainers.Count > 0) 
                {
                    string DataFormat = string.Format("| {0,20} | {1,20} | {2, 4} | {3, 20} | {4,-8} |",
                        "Gamintojas", "Modelis", "Energ. Klase", "Spalva", "Kaina");
                    InOut.PrintStarting(AllContainers, ResultFolder + "Rezultatai.txt", "Pradiniai duomenys", DataFormat);

                    //Task 1
                    Table1.Visible= true;
                    Label1.Visible= true;
                    Label1.Text = "Siemens firmos gaminiu viso rasta:";

                    int SiemensCount = TaskUtils.SiemensCount(AllContainers);
                    AddSiemensCountToTable(Table1, SiemensCount);
                    InOut.PrintToTxt(ResultFolder + "Rezultatai.txt", SiemensCount, "Siemens firmos gaminiu viso rasta: ");

                    //Task2
                    Label2.Visible= true;
                    Table2.Visible= true;
                    Label2.Text = "10 Pigiausių pastatomų šaldytuvų su talpa didesne nei 80 sąrašas:"; 
                    List<Fridge> fridges = TaskUtils.StandingCheapestFridges(AllContainers);
                    CreateCheapFridgeTable(Table2, fridges);
                    InOut.PrintToTxt(ResultFolder + "Rezultatai.txt", fridges, "10 Pigiausių pastatomų šaldytuvų su talpa didesne nei 80 sąrašas:");

                    //Task3
                    Label3.Visible= true;
                    Label3.Text = "Išskirtinai parduodamų prekių sąrašas faile TikTen.csv";
                    List<DevicesContainer> exclusive = TaskUtils.OnlyOneShop(AllContainers);
                    InOut.PrintToCsvOnlyOneShop(exclusive, ResultFolder + "TikTen.csv");

                    //Task4
                    Label4.Visible= true;
                    Label4.Text = "Brangių buitinių prietaisų sąrašas sudarytas ir išvestas į Brangus.csv";
                    List<Device> expensive = TaskUtils.ExpensiveDevices(AllContainers);
                    InOut.PrintToCsvExpensive(expensive, ResultFolder + "Brangus.csv");

                }
                else
                {
                    Label5.Visible = true;
                    Label5.BackColor = System.Drawing.Color.Red;
                    Label5.Text = "Sąrašas tusčias";
                }
            }
        }
    }
}