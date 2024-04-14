package util;
import java.util.ArrayList;
import java.util.Scanner;

public class WordSearch {
    private static final ArrayList<String> board = new ArrayList<String>();
    private static Scanner scanner;
    /**
     * Sukuriamas scanner objektas skaityti informacijai iš failo
     */
    public static void createScanner() {
        try { // Kuriamas duomenų skaitymo objektas.
            scanner = new Scanner(new java.io.File("WordSearchInput.txt"));
        }
        catch (Exception e) { // Jei nėra tinkamas duomenų failo pavadinimas.
            System.out.println("Netinkamas arba nerastas duomenų failas!");
            System.exit(1);
        }
    }
    /**
     * Nuskaito duomenis iš duomenų failo (WordSearchInput.txt)
     * Panaudojamas Scanner objektas
     */
    public static void readBoard() {
        while (scanner.hasNextLine()) {
            String line = scanner.nextLine();
            if (line.equals("")){
                // Jei gauta eilutė tuščia-baigia darbą
                break;
            }
            // Įvedamoje lentelėje visas raides padaro didžiosiom
            board.add(line.replaceAll(" ","").toUpperCase());
        }
    }
    /**
     * Gražina eilučių kiekį board masyve
     * @return gražinamas eilučių kiekis board masyve
     */
    public static int getRows() {
        return board.size();
    }
    /**
     * Gražina stulpelių kiekį board masyve
     * Panaudojama get() funkcija, gauti tam tikram stulpeliui
     * @return gražinamas stulpelių kiekis board masyve
     */
    public static int getCols() {
        return board.get(0).length();
    }
    /**
     * Iš nuskaitytų duomenų išveda į konsolę lentelę
     * O(N), for ciklas sukas n kartų.
     */
    public static void printBoard() {
        int rows = getRows(); // paduoda eilučių kiekį į kintamajį rows
        for (int row=0; row<rows; row++){
            System.out.println(board.get(row));
        }
    }
    /**
     * Pradeda žodžių paiešką pagal scanner duodamą
     * O(N), while ciklas sukas n kartų.
     */
    public static void processWords() {
       // createScanner(); // iš naujo sukuriamas scanner objektas.
        while (scanner.hasNextLine()) { // ciklas vyksta kol scanner objektas turi sekančią eilutę
            String word = scanner.next(); // išgaunamas ieškomas žodis
            findWord(word); // kviečiama pirmoji findWord funkcija pagal duotą žodį.
        }
    }
    /**
     * Pradeda sukti ciklus, einančius pro lentelės elementus
     * O(N*M), nes yra ciklas cikle
     */
    public static void findWord(String word) {
        int rows = getRows(); // gaunamas eilučių kiekis
        int cols = getCols(); // gaunamas stulpelių kiekis
        for (int row=0; row<rows; row++){
            for (int col=0; col<cols; col++){
                findWord(word,row,col);
            }
        }
    }

    /**
     * Suka ciklus žodžio krypčiai perduoti
     * O(N*M), nes yra ciklas cikle
     */
    public static void findWord(String word, int row, int col) {
        for (int drow=-1; drow<=1; drow++){
            for (int dcol=-1; dcol<=1; dcol++){
                findWord(word,row,col,drow,dcol);
            }
        }
    }

    /**
     * Galutinis žodžio paieškos metodas. Apskaičiuoja parenkamas eilutes bei stulpelius pagal offset ir d col/row krypties kintamuosius.
     * O(N), for ciklas sukas n kartų.
     */
    public static void findWord(String word, int row, int col, int drow, int dcol) {
        int rows = getRows();  // gaunamas eilučių kiekis
        int cols = getCols();  // gaunamas stulpelių kiekis
        for (int offset=0; offset<word.length(); offset++) {
            int targetRow = row + offset*drow; // Parenkamas atstumtas žodžio eilutės indeksas
            int targetCol = col + offset*dcol; // Parenkamas atstumtas žodžio stulpelio indeksas
            // tikrinama ar nustatomi indeksai eina už lentelės ribų
            if ((targetRow < 0) || (targetRow >= rows) || (targetCol < 0) || (targetCol >= cols)){
                // Išeina iš duomenų lentelės ribų
                return;
            }
            char boardChar = board.get(targetRow).charAt(targetCol); // Tikrinamoji lentelės raidė
            char wordChar  = word.charAt(offset); // Ieškomo žodžio pirmoji raidė.
            if (boardChar != wordChar){
                // Nesutampa žodžio raidė ir lentelės raidė, stabdomas šis ciklas ir einama prie kito.
                return;
            }
        }
        // Jei praėjo pro du tikrinimus kad neišeitų už ribų ir ar sutampa raidės pradedamas
        // rasto žodžio pozicijos ir krypties išvedimas:
        // word - žodis
        // row - eilutė, kurioj rastas žodis
        // col - stulpelis kuriame rastas žodis
        // drow - žodžio kryptis eilutėje (-1 jei žodis eina ->, 0 jei žodis eina viršun/žemyn, 1 jei žodis eina <-)
        // dcol - žodžio kryptis stulpelyje (-1 jei žodis eina /\, 0 jei žodis eina <->, 1 jei žodis eina \/)
        System.out.printf("%s at %d,%d direction %d,%d\n", word, row, col, drow, dcol);
    }
    /**
     * Pagrindinis metodas programai paleisti.
     */
    public static void main(String[] args) {
        createScanner(); // Sukuriamas duomenų skaitytuvas
        readBoard(); // Nuskaitoma informacija iš duomenų failo
        printBoard(); // Išvedama duomenų failo informacija
        System.out.println(); // Atskiria išvedimą
        processWords(); // išvedama rastų žodžių informacija
    }
}