//Martynas Kuliešius IFF-1/9

class DiskretDarbs {

    public static void Main(string[] args)
    {
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();

        //char[] aibe = { 'A' };
        //char[] aibe = { 'A', 'B', 'C', 'D' };
        //char[] aibe = { 'A', 'B', 'C'};
        //char[] aibe = { '1', '2', '3', '4'};
        //char[] aibe = { '!', '@', '#', '$' };
        //char[] aibe = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' };
        //char[] aibe = { '1', 'B', '#', '4', '%', 'F' };
        //char[] aibe = { 'A', 'A', 'B', 'B', 'C', 'C' };
        //char[] aibe = { '4', '5', '3', '2', '1', '9' };
        char[] aibe = { '1', '1', '3', '3'};

        List<string> keliniai = new List<string>();

        int unique = kiekis(aibe);

        int kelKiekis = fac(aibe.Length) / fac(1+(aibe.Length-unique));
        //int kelKiekis = fac(aibe.Length);


        if (unique == aibe.Length)
        {
            keliniai = GautiKelinius(aibe, fac(aibe.Length));
        }
        else 
        {
            keliniai = GautiKelinius(aibe, kelKiekis);

        }



        //GautiKelinius(aibe);

        watch.Stop();

        Console.WriteLine("\nKėlinių skaičius: " + kelKiekis);
        Console.WriteLine($"Programos veikimo laikas: {watch.ElapsedMilliseconds} ms");
        foreach (string kel in keliniai) 
            {
                Console.WriteLine(kel);
            }
    }

    /// <summary>
    /// Metodas, kuris atlieka pagrindinius keitimus, rikiavimus ir išvedimą.
    /// </summary>
    /// <param name="aibe">elementų masyvas/aibė</param>
    static List<string> GautiKelinius(char[] aibe , int kelKiekis)
    {
        List <string> Keliniai = new List<string>();
        // Išsaugomas aibės dydis
        int dydis = aibe.Length;
        int skaiciuoklis = 0;
        int iter=0;
 
        // su bazine programinės kalbos funkcija surikiuojama pradinė aibė/masyvas
        // Array.Sort(aibe);
        char[] pradineAibe = aibe;
 
        // Kuria ir spausdina kėlinius
        bool isFinished = false; // pagrindiniam ciklui valdyti naudojamas boolean kintamasis
        while (!isFinished && skaiciuoklis<kelKiekis) {
            // Išspausdina kėlinį
            spausdinti(aibe);
            skaiciuoklis++;
            //iter++;
            // Suranda dešiniausią simbolį, kuris yra mažesnis už šale esančius simbolius. jį laikau pirmesniu
            int i;
            if (iter == 5) 
                {
                    break;
                }
            for (i = dydis - 2; i >= 0; --i) 
            {
                if (aibe[i] < aibe[i + 1]) // kai randamas mažesnis mažiausias simbolis, jo indeksas išsaugomas ir pabaigiamas ciklas
                {
                    break;
                }
            }

            // Jeigu nėra dešiniausio simbolio, kuris yra mažesnis už paskiau einantį simbolį, vadinasi kad visi galimi kėliniai buvo sukurti.
            if (i == -1) 
            {
                char[] check = aibe;
                Array.Reverse(check);
                if (skaiciuoklis == fac(aibe.Length) || pradineAibe.SequenceEqual(check) ) // papildomas tikrinimas tam atvejui, jeigu buvo praleistas koks nors kelinys
                {
                    isFinished = true;
                }
            }
            else
            {
                // Suranda pirmąjį simbolį eilėje sekantį simbolį.
                int sekantisID = RastiSekanti(aibe, aibe[i], i + 1, dydis - 1);
 
                //Sukeičia šiuos du simbolius vieną su kitu.
                sukeisti(aibe, i, sekantisID);

                //Array.Sort(aibe, i + 1, dydis - i - 1); // Surikiuoja masyvą/aibę nuo i-tojo elemento iki galo.
                Burbuliukas(aibe, i + 1);
            }

            
        }
        return Keliniai;
    }

    /// <summary>
    /// Metodas, kuris randa mažiausio, pirmąjį simbolį sekančio simbolio indeksą masyve.
    /// </summary>
    /// <param name="aibe"> Masyvas kuriame saugomi visi simboliai</param>
    /// <param name="pirmesnis"> simbolis, pagal kurį ieškomi jį sekantys simboliai</param>
    /// <param name="l"> pirmesniojo elemento indeksas</param>
    /// <param name="h"> aibes dydis. Iškviečiant metodą darašomas -1 todėl, kad c# kalboje masyvų indeksacija prasideda nuo 0</param>
    /// <returns></returns>
    static int RastiSekanti(char[] aibe, char pirmesnis, int l, int h)
    {
        int ceilIndex = l;

        for (int i = l + 1; i <= h; i++)
            if (aibe[i] > pirmesnis && aibe[i] < aibe[ceilIndex])
                ceilIndex = i;

        return ceilIndex;
    }

    /// <summary>
    /// Papildomas metodas sukeisti simbolius vietomis
    /// </summary>
    /// <param name="aibe"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    ///  A<->B

    static void sukeisti(char[] aibe, int i, int j)
    {
        char t = aibe[i];
        aibe[i] = aibe[j];
        aibe[j] = t;
    }

    /// <summary>
    /// Paskaičiuojamas kėlinių kiekis, kuris apsirašo n! (n faktorialu)
    /// </summary>
    /// <param name="size"> Aibės elementų kiekis </param>
    /// <returns></returns>
    static int fac(int size)
    {
        int fact = 1;

        for (int i = 1; i <= size; i++)
        {
            fact *= i;
        }
        return fact;
    }

     static bool ArYra(char[] aibe, List<string> keliniai) 
        {
            string kelinys = "";
            for (int i = 0; i < aibe.Length; i++)
            {
                kelinys+= aibe[i].ToString();
                
            }

            if (!keliniai.Contains(kelinys))
            {
                keliniai.Add(kelinys);
                return false;
            }
            else 
            {
                return true;
            }

        }

        static int kiekis(char[] aibe) 
        {
            List<string> num = new List<string>();
            for (int i = 0; i < aibe.Length; i++)
            {
                if (!num.Contains(aibe[i].ToString())) 
                {
                    num.Add(aibe[i].ToString());
                }
            }
            return num.Count;
        
        }


    public static void Burbuliukas(char[] aibe, int startIndex)
    {
        var flag = false;
        do
        {
            flag = false;
            for (int i = startIndex; i < aibe.Count() - 1; i++)
            {
                if (aibe[i] > aibe[i + 1])
                {
                    sukeisti(aibe, i, i + 1);   
                    flag = true;
                }
            }
        } while (flag);
    }
    static void spausdinti(char[] aibe) 
    {
        for (int i = 0; i < aibe.Length; i++)
        {
            string raide = aibe[i].ToString();
            keistiSpalva(raide);
            Console.Write(raide + " ");
        }
        keistiSpalva("reset");
        Console.WriteLine();
    
    }
    static void keistiSpalva(string raide) 
    {
        switch(raide)
            {
            case "A":
                Console.ForegroundColor=ConsoleColor.Green;
                break;
            case "1":
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case "B":
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case "2":
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case "C":
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case "3":
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case "D":
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case "4":
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case "E":
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case "5":
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case "F":
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
            case "6":
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
            case "G":
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                break;
            case "7":
                Console.ForegroundColor = ConsoleColor.DarkCyan;

                break;
            
                
            case "reset":
                Console.ForegroundColor = ConsoleColor.White;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.White;
                break;
        }
    
    }
}