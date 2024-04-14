using WebApplication4.Models;
namespace WebApplication4.Repositories
{
    public class ItemRepo
    {
        public static int GetItemId(int fk_id)
        {
            var query = $@"SELECT id FROM `items` WHERE fk_id=?fk_id";
            var drc = Sql.Query(query, args =>
            {           
                args.Add("?fk_id", fk_id);
            });

            var a = Sql.MapOne<ItemID>(drc, (dre, t) => {
                t.ID = dre.From<int>("id");

            });
            return a.ID;

        }
        public static void InsertItem(string Pavadinimas, string Picture, double Kaina, string Svoris, string Pagaminimo_data,int FK_CategoryID, int FK_ManufacturerID, string Serija = null,
            string Spalva = null, string Plotis = null, string Aukstis = null, string Ilgis = null, string Garantija = null,
            string Ekrano_dydis = null, string Operacine_sistema = null, string Procesorius = null, string Kietasis_diskas = null,
            string Vaizdo_plokste = null, string Jungtys = null, string Baterija = null, string Papildoma_info = null,
            string Procesoriaus_karta = null){
                var query = $@"INSERT INTO items (
                    Pavadinimas, Picture, Kaina, Svoris, Pagaminimo_data, Serija, Spalva, Plotis, Aukstis, Ilgis,
                    Garantija, Ekrano_dydis, Operacine_sistema, Procesorius, Kietasis_diskas, Vaizdo_plokste,
                    Jungtys, Baterija, Papildoma_info, Procesoriaus_karta, FK_CategoryID, FK_ManufacturerID
                ) VALUES (
                    @Pavadinimas, @Picture, @Kaina, @Svoris, @Pagaminimo_data, @Serija, @Spalva, @Plotis, @Aukstis, @Ilgis,
                    @Garantija, @Ekrano_dydis, @Operacine_sistema, @Procesorius, @Kietasis_diskas, @Vaizdo_plokste,
                    @Jungtys, @Baterija, @Papildoma_info, @Procesoriaus_karta, @FK_CategoryID, @FK_ManufacturerID
                )";

                Sql.Insert(query, args =>
                {
                    args.Add("@Pavadinimas", Pavadinimas);
                    args.Add("@Picture", Picture);
                    args.Add("@Kaina", Kaina);
                    args.Add("@Svoris", Svoris);
                    args.Add("@Pagaminimo_data", Pagaminimo_data);
                    args.Add("@Serija", Serija);
                    args.Add("@Spalva", Spalva);
                    args.Add("@Plotis", Plotis);
                    args.Add("@Aukstis", Aukstis);
                    args.Add("@Ilgis", Ilgis);
                    args.Add("@Garantija", Garantija);
                    args.Add("@Ekrano_dydis", Ekrano_dydis);
                    args.Add("@Operacine_sistema", Operacine_sistema);
                    args.Add("@Procesorius", Procesorius);
                    args.Add("@Kietasis_diskas", Kietasis_diskas);
                    args.Add("@Vaizdo_plokste", Vaizdo_plokste);
                    args.Add("@Jungtys", Jungtys);
                    args.Add("@Baterija", Baterija);
                    args.Add("@Papildoma_info", Papildoma_info);
                    args.Add("@Procesoriaus_karta", Procesoriaus_karta);
                    args.Add("@FK_CategoryID", FK_CategoryID);
                    args.Add("@FK_ManufacturerID", FK_ManufacturerID);
                });
            }
        public static List<Item> GetAllItems()
        {
            var query = "SELECT * FROM items";

            var items = new List<Item>();

            Sql.MapAll<Item>(Sql.Query(query), (extractor, item) =>
            {
                item.Id =extractor.From<int>("item_ID");
                item.Name = extractor.From<string>("Pavadinimas");
                item.Picture = extractor.From<string>("Picture");
                item.Price = extractor.From<decimal>("Kaina");
                item.Svoris = extractor.From<string>("Svoris");
                item.Pagaminimo_data = extractor.From<string>("Pagaminimo_data");
                item.Serija = extractor.From<string>("Serija");
                item.Spalva = extractor.From<string>("Spalva");
                item.Plotis = extractor.From<string>("Plotis");
                item.Aukstis = extractor.From<string>("Aukstis");
                item.Ilgis = extractor.From<string>("Ilgis");
                item.Garantija = extractor.From<string>("Garantija");
                item.Ekrano_dydis = extractor.From<string>("Ekrano_dydis");
                item.Operacine_sistema = extractor.From<string>("Operacine_sistema");
                item.Procesorius = extractor.From<string>("Procesorius");
                item.Kietasis_diskas = extractor.From<string>("Kietasis_diskas");
                item.Vaizdo_plokste = extractor.From<string>("Vaizdo_plokste");
                item.Jungtys = extractor.From<string>("Jungtys");
                item.Baterija = extractor.From<string>("Baterija");
                item.Papildoma_info = extractor.From<string>("Papildoma_info");
                item.Procesoriaus_karta = extractor.From<string>("Procesoriaus_karta");
                item.FK_CategoryID = extractor.From<int>("FK_CategoryID");
                item.FK_ManufacturerID = extractor.From<int>("FK_ManufacturerID");
                item.AddedTimestamp = extractor.From<DateTime>("AddedTimestamp");

                items.Add(item);
            });

            return items;
        }

        public static Item GetItemById(int itemId)
        {
            var query = "SELECT * FROM items WHERE item_ID = ?itemId";
            var dataReader = Sql.Query(query, args =>
            {
                args.Add("?itemId", itemId);
            });

            try
            {
                var result = Sql.MapOne<Item>(dataReader, (extractor, item) =>
                {

                    item.Id = extractor.From<int>("item_ID");
                    item.Name = extractor.From<string>("Pavadinimas");
                    item.Picture = extractor.From<string>("Picture");
                    item.Price = extractor.From<decimal>("Kaina");
                    item.Svoris = extractor.From<string>("Svoris");
                    item.Pagaminimo_data = extractor.From<string>("Pagaminimo_data");
                    item.Serija = extractor.From<string>("Serija");
                    item.Spalva = extractor.From<string>("Spalva");
                    item.Plotis = extractor.From<string>("Plotis");
                    item.Aukstis = extractor.From<string>("Aukstis");
                    item.Ilgis = extractor.From<string>("Ilgis");
                    item.Garantija = extractor.From<string>("Garantija");
                    item.Ekrano_dydis = extractor.From<string>("Ekrano_dydis");
                    item.Operacine_sistema = extractor.From<string>("Operacine_sistema");
                    item.Procesorius = extractor.From<string>("Procesorius");
                    item.Kietasis_diskas = extractor.From<string>("Kietasis_diskas");
                    item.Vaizdo_plokste = extractor.From<string>("Vaizdo_plokste");
                    item.Jungtys = extractor.From<string>("Jungtys");
                    item.Baterija = extractor.From<string>("Baterija");
                    item.Papildoma_info = extractor.From<string>("Papildoma_info");
                    item.Procesoriaus_karta = extractor.From<string>("Procesoriaus_karta");
                    item.FK_CategoryID = extractor.From<int>("FK_CategoryID");
                    item.FK_ManufacturerID = extractor.From<int>("FK_ManufacturerID");
                    item.AddedTimestamp = extractor.From<DateTime>("AddedTimestamp");
                });

                return result;
            }
            catch
            {
                Console.WriteLine("Item with ID '{0}' not found", itemId);
                return null; // or new Item() if you prefer
            }
        }


        public static void DeleteItem(int itemId)
        {
            // Validate input
            if (itemId <= 0)
            {
                throw new ArgumentException(nameof(itemId));
            }

            // Write your SQL query for deleting the item by ID
            var query = "DELETE FROM items WHERE item_ID = ?itemId";

            // Execute the query
            Sql.Delete(query, args =>
            {
                args.Add("?itemId", itemId);
            });
        }
        public static void UpdateItem(Item item){
            // Validate input
            if (item == null || item.Id <= 0)
            {
                throw new ArgumentException(nameof(item));
            }

            // Write your SQL query for updating the item
            var query = @"UPDATE items
                        SET Pavadinimas = IFNULL(NULLIF(?Name, ''), NULL), 
                            Picture = IFNULL(NULLIF(?Picture, ''), NULL), 
                            Kaina = IFNULL(NULLIF(?Price, ''), NULL), 
                            Svoris = IFNULL(NULLIF(?Svoris, ''), NULL), 
                            Pagaminimo_data = IFNULL(NULLIF(?Pagaminimo_data, ''), NULL),
                            Serija = IFNULL(NULLIF(?Serija, ''), NULL), 
                            Spalva = IFNULL(NULLIF(?Spalva, ''), NULL), 
                            Plotis = IFNULL(NULLIF(?Plotis, ''), NULL), 
                            Aukstis = IFNULL(NULLIF(?Aukstis, ''), NULL), 
                            Ilgis = IFNULL(NULLIF(?Ilgis, ''), NULL), 
                            Garantija = IFNULL(NULLIF(?Garantija, ''), NULL), 
                            Ekrano_dydis = IFNULL(NULLIF(?Ekrano_dydis, ''), NULL), 
                            Operacine_sistema = IFNULL(NULLIF(?Operacine_sistema, ''), NULL), 
                            Procesorius = IFNULL(NULLIF(?Procesorius, ''), NULL), 
                            Kietasis_diskas = IFNULL(NULLIF(?Kietasis_diskas, ''), NULL), 
                            Vaizdo_plokste = IFNULL(NULLIF(?Vaizdo_plokste, ''), NULL), 
                            Jungtys = IFNULL(NULLIF(?Jungtys, ''), NULL), 
                            Baterija = IFNULL(NULLIF(?Baterija, ''), NULL), 
                            Papildoma_info = IFNULL(NULLIF(?Papildoma_info, ''), NULL), 
                            Procesoriaus_karta = IFNULL(NULLIF(?Procesoriaus_karta, ''), NULL), 
                            FK_CategoryID = IFNULL(NULLIF(?FK_CategoryID, ''), NULL), 
                            FK_ManufacturerID = IFNULL(NULLIF(?FK_ManufacturerID, ''), NULL)
                        WHERE item_ID = ?Id";

            // Execute the query
            Sql.Update(query, args =>
            {
                args.Add("?Name", item.Name);
                args.Add("?Picture", item.Picture);
                args.Add("?Price", item.Price);
                args.Add("?Svoris", item.Svoris);
                args.Add("?Pagaminimo_data", item.Pagaminimo_data);
                args.Add("?Serija", item.Serija);
                args.Add("?Spalva", item.Spalva);
                args.Add("?Plotis", item.Plotis);
                args.Add("?Aukstis", item.Aukstis);
                args.Add("?Ilgis", item.Ilgis);
                args.Add("?Garantija", item.Garantija);
                args.Add("?Ekrano_dydis", item.Ekrano_dydis);
                args.Add("?Operacine_sistema", item.Operacine_sistema);
                args.Add("?Procesorius", item.Procesorius);
                args.Add("?Kietasis_diskas", item.Kietasis_diskas);
                args.Add("?Vaizdo_plokste", item.Vaizdo_plokste);
                args.Add("?Jungtys", item.Jungtys);
                args.Add("?Baterija", item.Baterija);
                args.Add("?Papildoma_info", item.Papildoma_info);
                args.Add("?Procesoriaus_karta", item.Procesoriaus_karta);
                args.Add("?FK_CategoryID", item.FK_CategoryID);
                args.Add("?FK_ManufacturerID", item.FK_ManufacturerID);
                args.Add("?Id", item.Id);
            });
        }

    }
}
