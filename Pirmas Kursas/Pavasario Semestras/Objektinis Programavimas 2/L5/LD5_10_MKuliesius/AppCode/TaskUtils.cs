using System;
using System.Collections.Generic;
using System.Linq;

namespace LD5_10_MKuliesius.AppCode
{
    public class TaskUtils
    {

        /// <summary>
        /// Method calculates revenue for every publisher for selected month
        /// </summary>
        /// <param name="orderContainers"> container of all orders </param>
        /// <param name="publications"> list of all publishers </param>
        /// <param name="specifiedMonth"> selected month variable</param>
        /// <returns></returns>
        public static List<Publication> CalculateRevenueByPublisher(List<OrderContainer> orderContainers, List<Publication> publications, int specifiedMonth)
        {
            // Reset ProfitMonth for all publications
            foreach (var publication in publications)
            {
                publication.ProfitMonth = 0;
            }

            // Aggregate all orders from all order containers
            var allOrders = orderContainers.SelectMany(oc => oc.GetAllOrders()).ToList();

            foreach (var order in allOrders)
            {
                if (order.StartMonth <= specifiedMonth && (order.StartMonth + order.Duration - 1) >= specifiedMonth)
                {
                    var publication = publications.FirstOrDefault(p => p.Code == order.PublicationCode);
                    if (publication != null)
                    {
                        int months = Math.Min(order.Duration, specifiedMonth - order.StartMonth + 1);
                        decimal revenue = months * publication.PricePerMonth;
                        publication.ProfitMonth += revenue;
                    }
                }
            }

            return publications;
        }

        /// <summary>
        /// Sorts given list by revenue and name
        /// </summary>
        /// <param name="publications"> list of publishers that need to be sorted</param>
        /// <returns></returns>
        public static void SortPublicationsByRevenueAndName(List<Publication> publications)
        {
            if (publications == null)
            {
                // If the publications list is null, return immediately
                return;
            }

            // Sort publications by monthly revenue (descending) and then by name (ascending)
            publications.Sort((p1, p2) =>
            {
                // First, compare by profit month (descending)
                int compareResult = p2.ProfitMonth.CompareTo(p1.ProfitMonth);

                // If profit months are equal, compare by name (ascending)
                if (compareResult == 0)
                {
                    compareResult = p1.Name.CompareTo(p2.Name);
                }

                return compareResult;
            });
        }


        /// <summary>
        /// Method selects users for selected month
        /// </summary>
        /// <param name="orderContainers"> Container of all orders </param>
        /// <param name="selectedMonth"> selected month variable </param>
        /// <returns></returns>
        public static List<Order> GetSubscriptionsForSelectedMonth(List<OrderContainer> orderContainers, int selectedMonth)
        {
            // Get all orders for the selected month
            var selectedMonthOrders = orderContainers.SelectMany(oc => oc.Orders)
                                                     .Where(order => order.StartMonth <= selectedMonth &&
                                                                      order.StartMonth + order.Duration - 1 >= selectedMonth)
                                                     .ToList();

            return selectedMonthOrders;
        }
    }
}