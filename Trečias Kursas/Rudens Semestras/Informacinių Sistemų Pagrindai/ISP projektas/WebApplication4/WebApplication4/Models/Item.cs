using System.ComponentModel.DataAnnotations;

public class Item
{
        public int Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    public decimal Price { get; set; }
    public string Svoris { get; set; }
    public string Pagaminimo_data { get; set; }
    public string Serija { get; set; }
    public string Spalva { get; set; }
    public string Plotis { get; set; }
    public string Aukstis { get; set; }
    public string Ilgis { get; set; }
    public string Garantija { get; set; }
    public string Ekrano_dydis { get; set; }
    public string Operacine_sistema { get; set; }
    public string Procesorius { get; set; }
    public string Kietasis_diskas { get; set; }
    public string Vaizdo_plokste { get; set; }
    public string Jungtys { get; set; }
    public string Baterija { get; set; }
    public string Papildoma_info { get; set; }
    public string Procesoriaus_karta { get; set; }
    public int FK_CategoryID { get; set; }
    public int FK_ManufacturerID { get; set; }
    public DateTime AddedTimestamp { get; set; }



        public Item(string pic, int id, string name, decimal price)
        {
            this.Picture = pic;
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public Item() { }

        public Item(string[] parts)
        {
            try
            {
                this.Picture = parts[0];
                this.Id = int.Parse(parts[1]);
                this.Name = parts[2];
                this.Price = decimal.Parse(parts[3]);
            }
            catch
            {
                throw new Exception("Item parsing went wrong");
            }
        }

        // public List<string> Print()
        // {
        //     List<string> list = new List<string>();
        //     list.Add("Id: " + Id.ToString());
        //     list.Add("Kaina: " + Price);
        //     return list;
        // }
        public List<string> Print()
    {
        List<string> list = new List<string>
        {
            $"Item_ID: {Id}",
            $"Pavadinimas: {Name}",
            $"Kaina: {Price}",
            $"Svoris: {Svoris}",
            $"Pagaminimo_data: {Pagaminimo_data}",
            $"Serija: {Serija}",
            $"Spalva: {Spalva}",
            $"Plotis: {Plotis}",
            $"Aukstis: {Aukstis}",
            $"Ilgis: {Ilgis}",
            $"Garantija: {Garantija}",
            $"Ekrano_dydis: {Ekrano_dydis}",
            $"Operacine_sistema: {Operacine_sistema}",
            $"Procesorius: {Procesorius}",
            $"Kietasis_diskas: {Kietasis_diskas}",
            $"Vaizdo_plokste: {Vaizdo_plokste}",
            $"Jungtys: {Jungtys}",
            $"Baterija: {Baterija}",
            $"Papildoma_info: {Papildoma_info}",
            $"Procesoriaus_karta: {Procesoriaus_karta}",
            $"FK_CategoryID: {FK_CategoryID}",
            $"FK_ManufacturerID: {FK_ManufacturerID}"
        };
        return list;
    }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            Item other = obj as Item;
            if (this.Id == other.Id && this.Name.Equals(other.Name) &&
               this.Price == other.Price)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            int hash = 13;

            hash = hash * 23 + this.Id.GetHashCode();
            hash = hash * 23 + this.Price.GetHashCode();

            return hash;
        }

        public bool CompareTo(object? obj)
        {
            return this.Equals(obj);
        }

        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3}", this.Picture, this.Id, this.Name, this.Price);
        }
}

