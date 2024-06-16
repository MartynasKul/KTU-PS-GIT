using LD2_10_MKuliesius.AppCode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;

namespace LD2_10_MKuliesius
{
    public partial class Forma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile && FileUpload2.HasFile)
            {
                File.Delete(Server.MapPath("AppData/Rezultatai.txt"));

                string path1 = Server.MapPath(FileUpload1.FileName);
                string path2 = Server.MapPath(FileUpload2.FileName);
                

                string file1 = Server.HtmlEncode(FileUpload1.FileName);
                string extension1 = Path.GetExtension(file1);

                string file2 = Server.HtmlEncode(FileUpload2.FileName);
                string extension2 = Path.GetExtension(file2);


                // Checks if files are .txt
                if (extension1.Equals(".txt") && extension2.Equals(".txt"))
                {
                    // Saving files
                    FileUpload1.SaveAs(path1);
                    FileUpload2.SaveAs(path2);

                    // Status update
                    Label4.ForeColor = Color.Green;
                    Label4.Text = "Failai sukelti teisingai.";

                    //Read Files

                    AppCode.LinkedList<Worker> workers = InOut.ReadWorkerFile(path1);
                    AppCode.LinkedList<Detail> details = InOut.ReadDetailFile(path2);
                    Session["initial1"] = workers;
                    Session["initial2"] = details;

                    //path to result file.
                    string pathResults = Server.MapPath("AppData/Rezultatai.txt");
                    Session["Results"] = pathResults;
                    //Output initial data to file.
                    InOut.WriteInitialWorkerData(pathResults, "Pradiniai darbuotoju duomenys", workers);
                    InOut.WriteInitialDetailData(pathResults, "Pradiniai detaliu duomenys", details);

                    //Output initial data to textboxes.

                    Label5.Text = "Pradiniai darbuotoju duomenys";
                    DisplayInTextBox(InOut.WriteInitialWorkerDataList("Pradiniai darbuotoju duomenys", workers), TextBox1);
                    Label6.Text = "Pradiniai Detaliu duomenys";
                    DisplayInTextBox(InOut.WriteInitialDetailDataList("Pradiniai detaliu duomenys", details), TextBox2);

                    // Daugiausiai uzdirbusio darbuotojo paieska, darbo dienu skaicius, detaliu kiekis bei viso uzdirbta suma
                    //Pirma sugeneruojam atskira sarasa su unikaliais darbuotojais, kuriame suskaiciuosime kiekvieno darbuotojo uzdirbtus pinigus, visas detales bei darbo dienu skaiciu
                    AppCode.LinkedList<Worker> UniqueWorkers = TaskUtils.MakeSepparateWorkerList(workers, details);

                    TaskUtils.UpdateWorkers(UniqueWorkers, workers, details);

                    Worker BestWorker = TaskUtils.BestWorker(UniqueWorkers);
                    Label9.Text= "Geriausias darbuotojas: "+ BestWorker.ToString();


                    DisplayInTextBox(InOut.WriteInitialWorkerDataList("Unikalus darbuotojai", UniqueWorkers), TextBox3);

                }

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //Uzkraunami is sesijos pradiniai duomenys
            AppCode.LinkedList<Worker> workers = (AppCode.LinkedList<Worker>)Session["Initial1"];
            AppCode.LinkedList<Detail> details = (AppCode.LinkedList<Detail>)Session["Initial2"];
            //Sukuriamas pagal norima atributa naudotoju sarasas
            AppCode.LinkedList<Worker> partWorkers = TaskUtils.MakeWorkerListByPartName(workers, TextBox5.Text, details);
            string pathResults = (string)Session["Results"];

            partWorkers.Sort();
            //Isvedama i teksto dezute
            DisplayInTextBox(InOut.WriteInitialWorkerDataList("Darbuotojai nustatytai detalei " + TextBox5.Text, partWorkers), TextBox3);
            TextBox3.Text = TextBox3.Text + "\n" + "Viso pagaminta detaliu:" + TaskUtils.CalculateTotalParts(partWorkers) +
                "\n" + "Is viso uzdirbta:" + TaskUtils.CalculateTotalEarned(partWorkers, details);
        }
        protected void Button3_Click(object sender, EventArgs e) 
        {
            string path1 = Server.MapPath("U10a.txt");

  

            //Uzkraunami is sesijos pradiniai duomenys
            AppCode.LinkedList<Worker> workers = (AppCode.LinkedList<Worker>)Session["Initial1"];
            AppCode.LinkedList<Detail> details = (AppCode.LinkedList<Detail>)Session["Initial2"];

            //Sukuriamas atrinktu pagal kriteriju darbuotoju sarasas ir tolau surikiuojamas
            AppCode.LinkedList<Worker> PreferenceWorkers = TaskUtils.MakeListByPreferences(workers, details, TextBox4.Text, TextBox6.Text);
            PreferenceWorkers.Sort();

            InOut.WriteInitialWorkerData(path1, "Atrinktu pagal kriterijus darbuotoju sarasas", PreferenceWorkers);
            //textbox4 ir textbox6




        }
        /// <summary>
        /// Papildomas metodas isvedimo i tekstini langa supaprastinimui
        /// </summary>
        /// <param name="list"></param>
        /// <param name="textBox"></param>
        protected void DisplayInTextBox(List<string> list, TextBox textBox) 
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in list) 
            {
                sb.Append(item);
            }
            textBox.Text = sb.ToString();
        }
    }
 }