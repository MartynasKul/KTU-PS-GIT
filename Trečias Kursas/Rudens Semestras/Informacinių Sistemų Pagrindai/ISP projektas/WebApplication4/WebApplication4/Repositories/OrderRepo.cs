using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class OrderRepo
    {
        public static Order InsertOrder(Order order)
        {
            if (order.Status.Equals("Success"))
            {

                var query = "UPDATE orders SET status = ?status WHERE id = ?ID and status = 'Processing'";
                var drc = Sql.Query(query, args =>
                {
                    args.Add("?status", order.Status);
                    args.Add("?ID", order.ID);
                });
            }
            else if (order.Status.Equals("Processing"))
            {
                var query = "INSERT INTO orders (fk_userID, address, price,status) VALUES (?userID, ?adress,?price,?status)";


                int ID = (int)Sql.Insert(query, args =>
                {
                    args.Add("?userID", order.User.ID);
                    args.Add("adress", order.Adress);
                    args.Add("?price", order.Price);
                    args.Add("?status", order.Status);
                });
                order.ID = ID;
                InsertOrderedItems(order);
            }
            else if (order.Status.Equals("Failed") || order.Status.Equals("Cancelled"))
            {
                var query = "UPDATE orders SET status = ?status WHERE id = ?ID and status = 'Processing'";
                var drc = Sql.Query(query, args =>
                {
                    args.Add("?status", order.Status);
                    args.Add("?ID", order.ID);
                });
                DeleteOrderedItems(order);

            }
            return order;


        }

        public static void InsertOrderedItems(Order order)
        {
            foreach (Item item in order.items.Keys)
            {
                int amount = order.items[item];
                //int uniqueID = ItemRepo.GetItemId(item.Id);
                int uniqueID = item.Id;
                InsertOrderItem(order.ID, uniqueID, amount);
            }
        }

        public static void InsertOrderItem(int orderID, int itemID, int amount)
        {
            var query = $@"INSERT INTO item_order (fk_itemID, fk_orderID, amount) VALUES (?itemID, ?orderID, ?amount)";

            Sql.Insert(query, args =>
            {
                args.Add("?itemID", itemID);
                args.Add("?orderID", orderID);
                args.Add("?amount", amount);
            });
        }

        public static void DeleteOrderedItems(Order order)
        {
            var query = "DELETE FROM item_order WHERE fk_orderID = ?orderID";
            var drc = Sql.Query(query, args =>
            {
                args.Add("?orderID", order.ID);
            });
        }

        public static Order GetLastOrder()
        {
            var query = "SELECT * FROM orders WHERE ID = (SELECT MAX(ID) FROM orders)";
            var drc = Sql.Query(query);

            var order = Sql.MapOne<Order>(drc, (dre, t) => {
                t.ID = dre.From<int>("id");
                t.Adress = dre.From<string>("address");
                t.Price = dre.From<decimal>("price");
                t.Status = dre.From<string>("status");
            });
            return order;
        }
    }
}
