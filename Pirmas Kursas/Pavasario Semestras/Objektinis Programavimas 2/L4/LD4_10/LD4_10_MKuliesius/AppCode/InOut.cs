using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LD4_10_MKuliesius.AppCode
{
    public class InOut
    {
        public static List<DevicesContainer> ReadDevices(string path) 
        {
            List<DevicesContainer> allShops =  new List<DevicesContainer>();
            foreach (string file in Directory.GetFiles(path))
            {
                List<Device> devices = new List<Device>();
                string[] Lines = File.ReadAllLines(file); // Correct file reading
                if (Lines.Length >= 3)
                {
                    string shopName = Lines[0];
                    string address = Lines[1];
                    string phone = Lines[2];

                    for (int i = 3; i < Lines.Length; i++) // Adjusted to iterate correctly
                    {
                        string[] Bits = Lines[i].Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        if (Bits.Length < 6)
                        {
                            throw new Exception($"Data incorrectly formatted at line {i + 1}");
                        }
                        string type = Bits[0];
                        string maker = Bits[1];
                        string model = Bits[2];
                        string energyClass = Bits[3];
                        string color = Bits[4];
                        decimal price = Convert.ToDecimal(Bits[5]);
                        switch (type) 
                        {
                            case "Fridge":
                                int capacity = Convert.ToInt32(Bits[6]);
                                string mountType = Bits[7];

                                string booly = Bits[8];
                                bool hasFreezer = false;
                                if (booly == "Yes")
                                {
                                    hasFreezer = true;
                                }
                                double height = Convert.ToDouble(Bits[9]);
                                double width = Convert.ToDouble(Bits[10]);
                                double depth = Convert.ToDouble(Bits[10]);

                                Fridge fridge = new Fridge(maker, model, energyClass, color, price, type, capacity, mountType, hasFreezer, height, width, depth);
                                devices.Add(fridge);
                                break;

                            case "Kettle":
                                int power = Convert.ToInt32(Bits[6]);
                                double capacity1 = Convert.ToDouble(Bits[7]);

                                Kettle kettle = new Kettle(maker, model, energyClass, color,price,type,power,capacity1);
                                devices.Add(kettle);
                                break;

                            case "Oven":
                                int power1 = Convert.ToInt32(Bits[6]);
                                int programNum = Convert.ToInt32(Bits[7]);

                                Oven oven = new Oven(maker, model, energyClass, color, price, type, power1, programNum);
                                devices.Add(oven);
                                break;
                            default: throw new Exception($"Ivestas netinkamo tipo narys {type}");
                        }
                    }
                    DevicesContainer container = new DevicesContainer(shopName, address, phone, devices);
                    allShops.Add(container);
                }
                else throw new Exception($"Per mažai duomenų");
            }
            return allShops;
        }

        /// <summary>
        /// Isveda  visus duomenis i startiniu duomenu faila
        /// </summary>
        /// <param name="shops"></param>
        /// <param name="fin"></param>
        /// <param name="header"></param>
        /// <param name="Format"></param>
        public static void PrintStarting(List<DevicesContainer> shops, string fin, string header, string Format) 
        {
            string dashes = new string('-', 115);
            using (var file = new StreamWriter(fin, false, Encoding.UTF8))
            {
                foreach (DevicesContainer shop in shops)
                {
                    file.WriteLine(header);
                    file.WriteLine(dashes);
                    if (!shop.IsEmpty())
                    {
                        file.WriteLine(shop.Name);
                        file.WriteLine(shop.Address);
                        file.WriteLine(shop.PhoneNum);
                        file.WriteLine(dashes);
                        file.WriteLine(Format);
                        file.WriteLine(dashes);
                        for (int i = 0; i < shop.Count(); i++)
                        {
                            file.WriteLine(shop.Get(i).ToString());
                        }
                    }
                    else file.WriteLine("Sąrašas tusčias");
                    file.WriteLine(dashes);
                    file.WriteLine();
                    file.WriteLine();
                }
            }
        }

        /// <summary>
        /// Spausdinimas i csv faila tik tom prekem, kurias galima rasti tik vienoje parduotuveje.
        /// </summary>
        /// <param name="shops"></param>
        /// <param name="fin"></param>
        public static void PrintToCsvOnlyOneShop(List<DevicesContainer> shops, string fin)
        {
            using (StreamWriter sw = new StreamWriter(fin))
            {
                foreach (DevicesContainer shop in shops) 
                {
                    sw.WriteLine("{0};",shop.Name);
                    sw.WriteLine("{0};",shop.Address);

                    for (int i = 0;i < shop.Count();i++) 
                    {
                        Device device = shop.Get(i);
                        if (device is Fridge)
                        {
                            Fridge fr = (Fridge)device;
                            sw.WriteLine("Saldytuvas");
                            sw.WriteLine("{0};{1}", fr.Maker, fr.Model);
                        }
                        if (device is Kettle)
                        {
                            Kettle ke = (Kettle)device;
                            sw.WriteLine("Virdulys");
                            sw.WriteLine("{0};{1}", ke.Maker, ke.Model);
                        }
                        if (device is Oven)
                        {
                            Oven ov = (Oven)device;
                            sw.WriteLine("Mikrobange");
                            sw.WriteLine("{0};{1}", ov.Maker, ov.Model);
                        }
                    }
                } 
            }
        }

        /// <summary>
        /// Spausdinimas i csv faila visiems brangiems produktams
        /// </summary>
        /// <param name="devices"></param>
        /// <param name="fin"></param>
        public static void PrintToCsvExpensive(List<Device> devices, string fin) 
        {
            using (StreamWriter sw = new StreamWriter(fin))
            {
                foreach (Device device in devices)
                {
                    if (device is Fridge fr)
                    {
                        sw.WriteLine("Saldytuvas");
                        sw.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};", fr.Maker,
                            fr.Model, fr.EnergyClass, fr.Colour, fr.Price.ToString(), fr.Capacity.ToString(), fr.MountType, fr.HasFreezer.ToString(), fr.Height.ToString(), fr.Width.ToString(), fr.Depth.ToString());
                    }
                    if (device is Kettle ke)
                    {
                        sw.WriteLine("Virdulys");
                        sw.WriteLine("{0};{1};{2};{3};{4};{5};{6}", ke.Maker, ke.Model, ke.EnergyClass, ke.Colour, ke.Price.ToString(), ke.Power.ToString(), ke.Capacity.ToString());
                    }
                    if (device is Oven ov)
                    {
                        sw.WriteLine("Mikrobange");
                        sw.WriteLine("{0};{1};{2};{3};{4};{5};{6}", ov.Maker, ov.Model, ov.EnergyClass, ov.Colour, ov.Price.ToString(), ov.Power.ToString(), ov.ProgramNum.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Prints siemens count to result file
        /// </summary>
        /// <param name="fin"></param>
        /// <param name="count"></param>
        /// <param name="header"></param>
        public static void PrintToTxt(string fin, int count, string header) 
        {
            string dashes = new string('-', 115);

            File.AppendAllText(fin, dashes);
            File.AppendAllText(fin, "\n");

            File.AppendAllText(fin, header + count.ToString());
            File.AppendAllText(fin, "\n");
            File.AppendAllText(fin, dashes);
            File.AppendAllText(fin, "\n");
        }

        /// <summary>
        /// Prints siemens count to result file
        /// </summary>
        /// <param name="fin"></param>
        /// <param name="count"></param>
        /// <param name="header"></param>
        public static void PrintToTxt(string fin, List<Fridge> fridges, string header)
        {
            string dashes = new string('-', 115);
            File.AppendAllText(fin, header);
            File.AppendAllText(fin, "\n");
            File.AppendAllText(fin, dashes);
            File.AppendAllText(fin, "\n");

            foreach (Fridge fridge in fridges) 
            {
                File.AppendAllText(fin, fridge.Maker + " " + fridge.Model + " " + fridge.Price.ToString() + " " + fridge.Capacity.ToString());
                File.AppendAllText(fin, "\n");
            }
            
            File.AppendAllText(fin, dashes);
            File.AppendAllText(fin, "\n");
        }
    }
}
