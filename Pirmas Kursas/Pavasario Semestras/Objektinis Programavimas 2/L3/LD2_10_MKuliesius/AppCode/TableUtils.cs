using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace LD3_10_MKuliesius
{
    public partial class Forma : System.Web.UI.Page 
    {
        /// <summary>
        /// Makes a table with 7 cells for workers.
        /// </summary>
        /// <param name="cell1">A cell</param>
        /// <param name="cell2">A cell</param>
        /// <param name="cell3">A cell</param>
        /// <param name="cell4">A cell</param>
        /// <param name="cell5">A cell</param>
        /// <param name="cell6">A cell</param>
        /// <param name="cell7">A cell</param>
        /// <returns> A Row of cells</returns>

        public TableRow MakeRow(string cell1, string cell2, string cell3, string cell4, string cell5, string cell6, string cell7) 
        {
            TableRow row = new TableRow();

            TableCell Cell1 = new TableCell();
            Cell1.Text = cell1;
            row.Cells.Add(Cell1);

            TableCell Cell2 = new TableCell();
            Cell2.Text = cell2;
            row.Cells.Add(Cell2);

            TableCell Cell3 = new TableCell();
            Cell3.Text = cell3;
            row.Cells.Add(Cell3);

            TableCell Cell4 = new TableCell();
            Cell4.Text = cell4;
            row.Cells.Add(Cell4);

            TableCell Cell5 = new TableCell();
            Cell5.Text = cell5;
            row.Cells.Add(Cell5);

            TableCell Cell6 = new TableCell();
            Cell6.Text = cell6;
            row.Cells.Add(Cell6);

            TableCell Cell7 = new TableCell();
            Cell7.Text = cell7 + " e";
            row.Cells.Add(Cell7);

            return row;
        }

        /// <summary>
        /// Makes a table with 3 cells for parts.
        /// </summary>
        /// <param name="cell1">A cell</param>
        /// <param name="cell2">A cell</param>
        /// <param name="cell3">A cell</param>
        /// <returns> A Row of cells</returns>
        public TableRow MakeRow(string cell1, string cell2, string cell3) 
        {
            TableRow row = new TableRow();

            TableCell Cell1 = new TableCell();
            Cell1.Text = cell1;
            row.Cells.Add(Cell1);

            TableCell Cell2 = new TableCell();
            Cell2.Text = cell2;
            row.Cells.Add(Cell2);

            TableCell Cell3 = new TableCell();
            Cell3.Text = cell3+" e";
            row.Cells.Add(Cell3);

            return row;
        }

        /// <summary>
        /// Makes a table with 4 cells for workers.
        /// </summary>
        /// <param name="cell1">A cell</param>
        /// <param name="cell2">A cell</param>
        /// <param name="cell3">A cell</param>
        /// <param name="cell4">A cell</param>
        /// <returns> A Row of cells</returns>

        public TableRow MakeRow(string cell1, string cell2, string cell3, string cell4)
        {
            TableRow row = new TableRow();

            TableCell Cell1 = new TableCell();
            Cell1.Text = cell1;
            row.Cells.Add(Cell1);

            TableCell Cell2 = new TableCell();
            Cell2.Text = cell2;
            row.Cells.Add(Cell2);

            TableCell Cell3 = new TableCell();
            Cell3.Text = cell3;
            row.Cells.Add(Cell3);

            TableCell Cell4 = new TableCell();
            Cell4.Text = cell4;
            row.Cells.Add(Cell4);

            return row;
        }

        /// <summary>
        /// Makes a table with 1 cells for workers.
        /// </summary>
        /// <param name="cell1">A cell</param>
        /// <returns> A Row of cells</returns>
        public TableRow MakeRow(string cell1)
        {
            TableRow row = new TableRow();

            TableCell Cell1 = new TableCell();
            Cell1.Text = cell1;
            row.Cells.Add(Cell1);

            return row;
        }

        /// <summary>
        /// Creates a table of workers starting data
        /// </summary>
        /// <param name="workers">List of workers</param>
        public void MakeWorkerTableStart(LinkedList<Worker> workers) 
        {
            foreach (Worker worker in workers) 
            {
                if (worker != null) 
                {
                   Table1.Rows.Add(MakeRow(worker.Date, worker.Name, worker.DetailCode, worker.DetailProduced.ToString()));
                }
            }
        }

        /// <summary>
        /// Creates a table of Details data
        /// </summary>
        /// <param name="workers">List of details</param>
        public void MakeDetailTable(LinkedList<Detail> details)
        {
            foreach (Detail detail in details)
            {
                if (detail != null)
                {
                    Table2.Rows.Add(MakeRow(detail.Name, detail.Code, detail.Price.ToString()));
                }
            }
        }

        /// <summary>
        /// Creates a table of workers data after calculations
        /// </summary>
        /// <param name="workers">List of workers</param>
        public void MakeWorkerTable(LinkedList<Worker> workers)
        {
            foreach (Worker worker in workers)
            {
                if (worker != null)
                {
                    Table3.Rows.Add(MakeRow(worker.Date, worker.Name, worker.DetailCode, worker.DetailProduced.ToString(), worker.TotalDaysWorked.ToString(), worker.TotalDetailsProduced.ToString(), worker.TotalEarnings.ToString()));
                }
            }
        }
    }
}