package edu.ktu.ds.lab3.utils;

/**
 * @param <K>
 * @param <V>
 */
public interface ParsableMap<K, V> extends EvaluableMap<K, V> {

    V put(String key, String value);

    void load(String filePath);

    void save(String filePath);

    void println(String delimiter);

    /**
     * Grąžina suformatuotą maišos lentelės turinį atvaizdavimui ekrane. Pvz.:
     * [0] -> Pora1 -> Pora3
     * [1] -> Pora4 -> Pora2
     * ...
     *
     * @return Grąžina maišos lentelės turinį dvimačiu masyvu
     */
    String[][] getMapModel();
}
