package edu.ktu.ds.lab3.utils;

/**
 * Interfeisu aprašomas maišos lentelės charakteristikas skaičiuojantis
 * Atvaizdžio ADT
 *
 * @param <K> raktas
 * @param <V> reikšmė
 */
public interface EvaluableMap<K, V> extends Map<K, V> {

    /**
     * Grąžina maksimalų grandinėlės ilgį.
     *
     * @return Maksimalus grandinėlės ilgis.
     */
    default int getMaxChainSize(){
        return -1;
    }

    /**
     * Grąžina maišos lentelę formuojant įvykusių permaišymų kiekį.
     *
     * @return Permaišymų kiekis.
     */
    int getRehashesCounter();

    /**
     * Grąžina maišos lentelės talpą.
     *
     * @return Maišos lentelės talpa.
     */
    int getTableCapacity();

    /**
     * Grąžina paskutinio papildyto maišos lentelės masyvo elemento indeksą.
     *
     * @return Paskutinio papildyto maišos lentelės masyvo elemento indeksą.
     */
    int getLastUpdated();

    /**
     * Grąžina maišos lentelės masyvo užimtų elementų kiekį.
     *
     * @return maišos lentelės masyvo užimtų elementų kiekį.
     */
    int getNumberOfOccupied();
}
