namespace LD1_10_MKuliesius.AppClasses
{
    public class Moles
    {
        //Kurmio urvo dydis simboliais
        public int CaveSize { get; set; }

        public Moles(int caveSize)
        {
            this.CaveSize = caveSize;
        }
        public Moles(){}

        /// <summary>
        /// Overridinamas ToString metodas tam, kad teisingai išvestų rezultatus su apskaičiuota kūbinių centimetrų reikšme
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            
            return (CaveSize*5).ToString();
        }

    }
}