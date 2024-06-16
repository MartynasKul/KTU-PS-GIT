using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace LD5_10_MKuliesius.AppCode
{
    public class InOut
    {
        /// <summary>
        /// Methods reads all files in filepath given by the user. Files contain information about orders. And also handles exceptions
        /// </summary>
        /// <param name="filePath"> File path string</param>
        /// <returns></returns>
        /// <exception cref="Exception">possible exceptions for missreading files</exception>
        public static List<OrderContainer> ReadOrders(string filePath)
        {
            List<OrderContainer> allOrders = new List<OrderContainer>();
            try
            {
                foreach (string file in Directory.GetFiles(filePath))
                {
                    List<Order> orders = new List<Order>();

                    string[] Lines = File.ReadAllLines(file);
                    if (Lines.Length >= 2)
                    {
                        DateTime date = Convert.ToDateTime(Lines[0]);

                        for (int i = 1; i < Lines.Length; i++)
                        {
                            string[] Bits = Lines[i].Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                            if (Bits.Length < 6 || Bits.Length > 6)
                            {
                                Debug.WriteLine("ABOBA DUOMENYS NE TOKIO TIPO");
                                throw new Exception($"Data incorrectly formatted at line {i + 1}");
                            }
                            string Name = Bits[0];
                            string Address = Bits[1];
                            int startMonth = Convert.ToInt32(Bits[2]);
                            int duration = Convert.ToInt32(Bits[3]);
                            string PubCode = Bits[4];

                            Order newOrder = new Order(Name, Address, startMonth, duration, PubCode);
                            orders.Add(newOrder);
                        }

                        OrderContainer newOrderContainer = new OrderContainer(date, orders);
                        allOrders.Add(newOrderContainer);
                    }
                    else
                    {
                        Debug.WriteLine("ABOBA PER MAZAI DUOMENU FAILE");
                        throw new Exception($"Per mažai duomenų");
                    }
                }
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("ABOBA NEPAVYKO NUSKAITYTI");
                throw new Exception(ex.Message + " Klaida");
            }
                    
            return allOrders;
        }

        /// <summary>
        /// Method reads all lines in a given file. And also handles exceptions
        /// </summary>
        /// <param name="filePath">string variable with the path to file</param>
        /// <returns></returns>
        /// <exception cref="Exception">possible exceptions for missreading files</exception>
        public static List<Publication> ReadPublications(string filePath)
        {

            List<Publication> allPublications = new List<Publication>();
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines) 
                {
                    string[] parts = line.Split(';');
                    string code = parts[0];
                    string name = parts[1];
                    string publisherName = parts[2];
                    decimal price = Convert.ToDecimal(parts[3]);

                    Publication newPub = new Publication(code, name, publisherName, price);

                    allPublications.Add(newPub);
                }
                return allPublications;
                
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }        
    }
}
