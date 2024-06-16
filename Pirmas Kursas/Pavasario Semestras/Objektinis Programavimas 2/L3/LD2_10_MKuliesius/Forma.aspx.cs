using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;

namespace LD3_10_MKuliesius
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

                    LinkedList<Worker> workers = InOut.ReadWorkerFile(path1);
                    LinkedList<Detail> details = InOut.ReadDetailFile(path2);
                    Session["initial1"] = workers;
                    Session["initial2"] = details;

                    //path to result file.
                    string pathResults = Server.MapPath("AppData/Rezultatai.txt");
                    Session["Results"] = pathResults;
                    //Output initial data to file.
                    InOut.WriteInitialWorkerData(pathResults, "Pradiniai darbuotoju duomenys", workers);
                    InOut.WriteInitialDetailData(pathResults, "Pradiniai detaliu duomenys", details);

                    //Output initial data to textboxes.

                    Label5.Text = "Įvesti pradiniai duomenys. 1 lentelė - Darbuotojai, 2 lentelė - detalių kainoraštis.";
                    Table1.Visible = true;
                    Table2.Visible = true;
                    Table3.Visible = true;
                    Label5.Font.Size = 26;
                    Label5.Font.Bold = true;

                    Table1.Rows.Add(MakeRow("<b>Data</b>", "<b>Vardas</b>", "<b>Det. Kodas</b>", "<b>Pagamino</b>"));
                    MakeWorkerTableStart(workers);
                    Table2.Rows.Add(MakeRow("<b>Pavadinimas</b>", "<b>Det. Kodas</b>", "<b>Kaina</b>"));
                    MakeDetailTable(details);

                    // Daugiausiai uzdirbusio darbuotojo paieska, darbo dienu skaicius, detaliu kiekis bei viso uzdirbta suma
                    //Pirma sugeneruojam atskira sarasa su unikaliais darbuotojais, kuriame suskaiciuosime kiekvieno darbuotojo uzdirbtus pinigus, visas detales bei darbo dienu skaiciu
                    LinkedList<Worker> UniqueWorkers = TaskUtils.MakeSepparateWorkerList(workers, details);

                    TaskUtils.UpdateWorkers(UniqueWorkers, workers, details);

                    Label6.Font.Size = 26;
                    Label6.Font.Bold = true;
                    Label6.Visible = true;
                    Label6.Text = "Unikalūs darbuotojai";
                    Table3.Rows.Add(MakeRow("<b>Data</b>", "<b>Vardas</b>", "<b>Det. Kodas</b>", "<b>Pagamino</b>", "<b>Viso dirbo</b>", "<b>Viso pagamino</b>", "<b>Viso uždirbo</b>"));
                    MakeWorkerTable(UniqueWorkers);
                    Worker BestWorker = TaskUtils.BestWorker(UniqueWorkers);
                    Table3.Rows.Add(MakeRow("Geriausias darbuotojas: "));
                    Table3.Rows.Add(MakeRow("", BestWorker.Name, BestWorker.DetailCode, "", BestWorker.TotalDaysWorked.ToString(), BestWorker.TotalDetailsProduced.ToString(), BestWorker.TotalEarnings.ToString()));


                    Label9.Visible = true;
                    Label9.Font.Size = 26;
                    Label9.Font.Bold = true;
                    Label9.Visible = true;
                    Label9.Text = "Įrašykite detalę, pagal kurią norite atrinkti darbuotojus (pavadinimą)";
                }
                else
                {
                    // At least one of the extensions isn't .txt
                    Label4.ForeColor = Color.Red;
                    Label4.Text = "Netinkamo tipo failai (tinka tik .txt).";
                }
            }
            else
            {
                // FileUpload.HasFile = false
                Label4.ForeColor = Color.Red;
                Label4.Text = "Nepasirinkti visi reikiami failai.";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //Uzkraunami is sesijos pradiniai duomenys
            LinkedList<Worker> workers = (LinkedList<Worker>)Session["Initial1"];
            LinkedList<Detail> details = (LinkedList<Detail>)Session["Initial2"];
            //Sukuriamas pagal norima atributa naudotoju sarasas
            LinkedList<Worker> partWorkers = TaskUtils.MakeWorkerListByPartName(workers, TextBox5.Text, details);
            string pathResults = (string)Session["Results"];

            Label5.Text = "Įvesti pradiniai duomenys. 1 lentelė - Darbuotojai, 2 lentelė - detalių kainoraštis.";
            Table1.Visible = true;
            Table2.Visible = true;
            Table3.Visible = true;
            Label5.Font.Size = 26;
            Label5.Font.Bold = true;


            Label6.Font.Size = 26;
            Label6.Font.Bold = true;
            Label6.Visible = true;
            Table1.Rows.Add(MakeRow("<b>Data</b>", "<b>Vardas</b>", "<b>Det. Kodas</b>", "<b>Pagamino</b>"));
            MakeWorkerTableStart(workers);
            Table2.Rows.Add(MakeRow("<b>Pavadinimas</b>", "<b>Det. Kodas</b>", "<b>Kaina</b>"));
            MakeDetailTable(details);

            if (TaskUtils.Validation(TextBox5.Text))
            {
                // Viskas įvesta teisingai.
                Label4.ForeColor = Color.Green;
                Label4.Text = "Paieškos laukai užpildyti teisingai.";

                Label6.Text = "Darbuotojai atrinkti bei surikiuoti pagal pasirinktą detalę: " + TextBox5.Text;
                partWorkers.Sort();
                Table3.Rows.Add(MakeRow("<b>Data</b>", "<b>Vardas</b>", "<b>Det. Kodas</b>", "<b>Pagamino</b>", "<b>Viso dirbo</b>", "<b>Viso pagamino</b>", "<b>Viso uždirbo</b>"));
                MakeWorkerTable(partWorkers);
                Table3.Rows.Add(MakeRow("Viso pagaminta detaliu:" + TaskUtils.CalculateTotalParts(partWorkers)));
                Table3.Rows.Add(MakeRow("Is viso uzdirbta:" + TaskUtils.CalculateEarningsForPart(partWorkers, details, TextBox5.Text)));
            }
            else 
            {
                // FileUpload.HasFile = false
                Label4.ForeColor = Color.Red;
                Label4.Text = "Atrinkimui pagal detalę galima vesti tik raides ir tik vieną detalę!";
            }
        }
        protected void Button3_Click(object sender, EventArgs e) 
        {
            string path1 = Server.MapPath("U10a.txt");

            //Uzkraunami is sesijos pradiniai duomenys
            LinkedList<Worker> workers = (LinkedList<Worker>)Session["Initial1"];
            LinkedList<Detail> details = (LinkedList<Detail>)Session["Initial2"];

            //Sukuriamas atrinktu pagal kriteriju darbuotoju sarasas ir tolau surikiuojamas
            LinkedList<Worker> PreferenceWorkers = TaskUtils.MakeListByPreferences(workers, details, TextBox4.Text, TextBox6.Text);
            PreferenceWorkers.Sort();

            Label5.Visible = false;
            Label6.Visible = false;
            Button2.Visible = false;
            Label8.Visible = false;

            Label9.Text = "Atrinktu pagal kriterijus darbuotoju sarasas Isvestas i faila" + path1;




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