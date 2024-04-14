package edu.ktu.ds.lab3.utils;

import edu.ktu.ds.lab3.demo.CarsGenerator;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.UncheckedIOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.function.Function;

import static edu.ktu.ds.lab3.gui.Table.ARROW;

public class ParsableHashMapOa<K, V extends Parsable<V>> extends HashMapOa<K, V> implements ParsableMap<K, V> {

    private final Function<String, K> keyCreateFunction;   // funkcija bazinio rakto objekto kūrimui
    private final Function<String, V> valueCreateFunction; // funkcija bazinio reikšmės objekto kūrimui

    /**
     * Konstruktorius su funkcija bazinių rakto ir reikšmės objektų kūrimui
     *
     * @param keyCreateFunction
     * @param valueCreateFunction
     * @param ht
     */
    public ParsableHashMapOa(Function<String, K> keyCreateFunction,
                             Function<String, V> valueCreateFunction,
                             HashManager.HashType ht,
                             OpenAddressingType oaType) {

        this(keyCreateFunction, valueCreateFunction, DEFAULT_INITIAL_CAPACITY, DEFAULT_LOAD_FACTOR, ht, oaType);
    }


    /**
     * Konstruktorius su funkcija bazinių rakto ir reikšmės objektų kūrimui
     *
     * @param keyCreateFunction
     * @param valueCreateFunction
     * @param initialCapacity
     * @param loadFactor
     * @param ht
     */
    public ParsableHashMapOa(Function<String, K> keyCreateFunction,
                             Function<String, V> valueCreateFunction,
                             int initialCapacity,
                             float loadFactor,
                             HashManager.HashType ht,
                             OpenAddressingType oaType) {

        super(initialCapacity, loadFactor, ht, oaType);
        this.keyCreateFunction = keyCreateFunction;
        this.valueCreateFunction = valueCreateFunction;
    }

    @Override
    public V put(String key, String value) {
        return super.put(
                create(keyCreateFunction, key, "Nenustatyta raktų kūrimo funkcija"),
                create(valueCreateFunction, value, "Nenustatyta reikšmių kūrimo funkcija")
        );
    }

    /**
     * Suformuoja atvaizdį iš filePath failo
     *
     * @param filePath
     */
    @Override
    public void load(String filePath) {
        if (filePath == null || filePath.length() == 0) {
            return;
        }
        clear();
        try (BufferedReader fReader = Files.newBufferedReader(Paths.get(filePath), StandardCharsets.UTF_8)) {
            fReader.lines()
                    .map(String::trim)
                    .filter(line -> !line.isEmpty())
                    .forEach(line -> put(CarsGenerator.generateId(), line));
        } catch (FileNotFoundException e) {
            Ks.ern("Tinkamas duomenų failas nerastas: " + e.getLocalizedMessage());
        } catch (IOException | UncheckedIOException e) {
            Ks.ern("Failo skaitymo klaida: " + e.getLocalizedMessage());
        }
    }

    /**
     * Išsaugoja sąrašą faile fName tekstiniu formatu tinkamu vėlesniam
     * skaitymui
     *
     * @param filePath
     */
    @Override
    public void save(String filePath) {
        throw new UnsupportedOperationException("Atvaizdžio išsaugojimas.. šiuo metu nepalaikomas");
    }

    /**
     * Atvaizdis spausdinamas į Ks.ouf("")
     *
     * @param delimiter Atvaizdžio poros toString() eilutės kirtiklis
     */
    @Override
    public void println(String delimiter) {
        if (super.isEmpty()) {
            Ks.oun("Atvaizdis yra tuščias");
            return;
        }

        Ks.oufln("****** Atvaizdis ******");
        Ks.printMapModel(delimiter, getMapModel());
        Ks.oufln("****** Bendras porų kiekis yra " + super.size());
    }

    @Override
    public String[][] getMapModel() {
        String[][] result = new String[table.length][];
        int count = 0;
        for (Entry<K, V> n : table) {
            List<String> list = new ArrayList<>();
            list.add("[ " + count + " ]");
            if (n != null && n.key != null) {
                list.add(ARROW);
                list.add(n.toString());
            }
            result[count++] = list.toArray(new String[0]);
        }
        return result;
    }

    private static <T, R> R create(Function<T, R> function, T data, String errorMessage) {
        return Optional.ofNullable(function)
                .map(f -> f.apply(data))
                .orElseThrow(() -> new IllegalStateException(errorMessage));
    }
}
