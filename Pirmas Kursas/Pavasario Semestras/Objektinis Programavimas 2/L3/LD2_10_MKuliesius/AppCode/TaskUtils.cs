using System;

namespace LD3_10_MKuliesius
{
    public class TaskUtils
    {
        
        public static decimal CalculateEarningsForPart(LinkedList<Worker> workerList, LinkedList<Detail> detailList, string desiredWorker, string desiredPart) 
        {
            decimal earnings = 0;

            foreach(Detail detail in detailList) 
            {
                if(detail.Code == desiredPart) 
                {
                    foreach(Worker worker in workerList) 
                    {
                        if(worker.Name == desiredWorker) 
                        {
                            earnings += worker.DetailProduced * detail.Price;
                        }
                    }
                }
            }
            return earnings;
        }
        public static decimal CalculateEarningsForPart(LinkedList<Worker> workerList, LinkedList<Detail> detailList, string desiredPart)
        {
            decimal total = 0;
            foreach (Worker worker in workerList)
            {
                foreach (Detail detail in detailList)
                {
                    if (detail.Name.Equals(desiredPart))
                    {
                        if (detail.Code.Equals(worker.DetailCode))
                        {
                            total += detail.Price * worker.DetailProduced;
                        }
                    }
                }
            }
            return total;
        }
        /// <summary>
        /// Makes sepparate list with unique workers
        /// </summary>
        /// <param name="workerList"></param>
        /// <returns></returns>
        public static LinkedList<Worker> MakeSepparateWorkerList(LinkedList<Worker> workerList, LinkedList<Detail> detailList) 
        {
            LinkedList<Worker> result = new LinkedList<Worker>();

            foreach (Worker worker in workerList) 
            {
                if (!IsInList(result, worker)) 
                {
                    result.Add(worker);
                }
            }
            return result;
        }
        /// <summary>
        /// Updates workers information in list
        /// </summary>
        /// <param name="Uniqueworkers"></param>
        /// <param name="workers"></param>
        /// <param name="detailList"></param>
        public static void UpdateWorkers(LinkedList<Worker> Uniqueworkers,LinkedList<Worker>workers, LinkedList<Detail> detailList) 
        {
            foreach (Worker worker in Uniqueworkers) 
            {
                worker.TotalEarnings = TotalEarnings(workers, worker, detailList);
                //decimal total = TotalEarnings(workers, worker, detailList);
                worker.TotalDaysWorked = TotalWorkDays(workers, worker);
                worker.TotalDetailsProduced = TotalDetails(workers, worker);
            }
        }
        /// <summary>
        /// Checks if worker is in list
        /// </summary>
        /// <param name="workerList"></param>
        /// <param name="worker"></param>
        /// <returns></returns>
        public static bool IsInList(LinkedList<Worker> workerList, Worker worker)
        {
            if (workerList.Empty)
            {
                return false;
            }
            else
            {
                foreach (Worker worker1 in workerList)
                {
                    if (worker.Name.Equals(worker1.Name))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Calculates the total ammount of earnings for the worker
        /// </summary>
        /// <param name="workerList"></param>
        /// <param name="worker"></param>
        /// <param name="detailList"></param>
        /// <returns></returns>
        public static decimal TotalEarnings(LinkedList<Worker> workerList, Worker worker, LinkedList<Detail> detailList) 
        {
            decimal total = 0;

            foreach (Worker worker1 in workerList) 
            {
                if (worker1.Name.Equals(worker.Name)) 
                {
                    foreach (Detail detail in detailList) 
                    {
                        if (detail.Code.Equals(worker.DetailCode)) 
                        {
                            worker1.TotalEarnings += worker1.DetailProduced * detail.Price;
                            total += worker1.DetailProduced * detail.Price;
                        }
                    }
                }
            }
            return total;
        }

        /// <summary>
        /// Calculates how many details worker has made in total
        /// </summary>
        /// <param name="workerList"></param>
        /// <param name="worker"></param>
        /// <returns></returns>
        public static int TotalDetails(LinkedList<Worker> workerList, Worker worker)
        {
            int total = 0;

            foreach (Worker worker1 in workerList)
            {
                if (worker1.Name.Equals(worker.Name))
                {
                    total+=worker1.DetailProduced;
                    worker1.TotalDetailsProduced += worker1.DetailProduced;
                }
            }


            return total;
        }
        /// <summary>
        /// Calculates how many work days worker worked in total
        /// </summary>
        /// <param name="workerList"></param>
        /// <param name="worker"></param>
        /// <returns></returns>
        public static int TotalWorkDays(LinkedList<Worker> workerList, Worker worker)
        {
            int total = 0;
            string date = "";

            foreach (Worker worker1 in workerList)
            {
                if (worker1.Name.Equals(worker.Name) && !worker1.Date.Equals(date))
                {
                    total++;
                    date = worker1.Date;
                }
            }
            return total;
        }

        /// <summary>
        /// finds best worker from unique workers list
        /// </summary>
        /// <param name="workerList"></param>
        /// <returns></returns>
        public static Worker BestWorker(LinkedList<Worker> workerList) 
        {
            Worker worker = new Worker();
            decimal max = 0;

            foreach (Worker worker1 in workerList) 
            {
                
                if (worker1.TotalEarnings.CompareTo(max) >0) 
                {
                    max = worker1.TotalEarnings;
                    worker = worker1;
                }
            }
            return worker;
        }

        /// <summary>
        /// Makes a list of workers that worked on a certain part
        /// </summary>
        /// <param name="workerList"></param>
        /// <param name="partName"></param>
        /// <param name="detailList"></param>
        /// <returns></returns>
        public static LinkedList<Worker> MakeWorkerListByPartName(LinkedList<Worker> workerList, string partName, LinkedList<Detail> detailList) 
        {
            LinkedList<Worker> result = new LinkedList<Worker>();
            foreach(Detail detail in detailList) 
            {
                if (detail.Name.Equals(partName)) 
                {
                    string partCode = detail.Code;

                    foreach (Worker worker1 in workerList) 
                    {
                        if(worker1.DetailCode.Equals(partCode)) 
                        {
                            result.Add(worker1);
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// calculates total parts made for list.
        /// </summary>
        /// <param name="workerList"></param>
        /// <returns></returns>
        public static int CalculateTotalParts(LinkedList<Worker> workerList) 
        {
            int total = 0;
            foreach (Worker worker in workerList)
            {
                total += worker.DetailProduced;
            }
            return total;
        }

        /// <summary>
        /// returns total ammount of money earned
        /// </summary>
        /// <param name="workerList"></param>
        /// <param name="detailList"></param>
        /// <returns></returns>
        public static decimal CalculateTotalEarned(LinkedList<Worker> workerList, LinkedList<Detail> detailList) 
        {
            decimal total = 0;
            foreach (Worker worker in workerList) 
            {
                foreach (Detail detail in detailList)
                {
                    if (detail.Code.Equals(worker.DetailCode))
                    {
                        total += detail.Price * worker.DetailProduced;
                    } 
                }
            }
            return total;
        }
        /// <summary>
        /// Makes list of workers that fit the user defined preferences
        /// </summary>
        /// <param name="workers"></param>
        /// <param name="details"></param>
        /// <param name="pref1"></param>
        /// <param name="pref2"></param>
        /// <returns></returns>
        public static LinkedList<Worker> MakeListByPreferences(LinkedList<Worker> workers, LinkedList<Detail> details, string pref1, string pref2) 
        {
            LinkedList<Worker> result = new LinkedList<Worker>();
            
            foreach (Worker worker in workers) 
            {
                foreach(Detail detail in details) 
                {
                    if(detail.Code == worker.DetailCode) 
                    {
                        if (worker.DetailProduced > Convert.ToInt64(pref1))
                        {
                            if (detail.Price * worker.DetailProduced < Convert.ToInt64(pref2)) 
                            {
                                result.Add(worker);
                            }
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Checks if inputed city contains only letters and whitespaces
        /// </summary>
        /// <param name="text">The input</param>
        /// <returns>True if input only contains letters and whitespaces</returns>
        public static bool Validation(string text)
        {
            foreach (char simb in text)
            {
                if (!char.IsLetter(simb) && !char.IsWhiteSpace(simb))
                {
                    return false;
                }
            }
            if (text.Length > 0)
            {
                return true;
            }
            return false;
        }
    }
}