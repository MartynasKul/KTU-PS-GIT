package edu.ktu.ds.lab3.utils;

import java.io.*;
import java.util.Arrays;
import java.util.IntSummaryStatistics;
import java.util.Map;
import java.util.function.Function;
import java.util.stream.Collectors;

import static edu.ktu.ds.lab3.gui.Table.ARROW;
import static java.util.stream.Collectors.summarizingInt;

/**
 * Klasė yra skirta patogiam duomenų paėmimui iš klaviatūros bei efektyviam
 * rezultatų pateikimui į sout ir serr srautus. Visi metodai yra statiniai ir
 * skirti vienam duomenų tipui. Studentai savarankiškai paruošia metodus dėl
 * short ir byte skaičių tipų.
 *
 * @author eimutis
 */
public class Ks { // KTU system - imituojama Javos System klasė

    private static final BufferedReader keyboard = new BufferedReader(new InputStreamReader(System.in));
    private static String dataFolder = "data";

    static public String giveString(String prompt) {
        Ks.ou(prompt);
        try {
            return keyboard.readLine();
        } catch (IOException e) {
            Ks.ern("Neveikia klaviatūros srautas, darbas baigtas");
            System.exit(0);
        }
        return "";
    }

    static public long giveLong(String prompt) {
        while (true) {
            String s = giveString(prompt);
            try {
                return Long.parseLong(s);
            } catch (NumberFormatException e) {
                Ks.ern("Neteisingas skaičiaus formatas, pakartokite");
            }
        }
    }

    static public long giveLong(String prompt, long bound1, long bound2) {
        while (true) {
            long a = giveLong(prompt + " tarp ribų [" + bound1 + ":" + bound2 + "]=");

            if (a < bound1) {
                Ks.ern("Skaičius mažesnis nei leistina, pakartokite");
            } else if (a > bound2) {
                Ks.ern("Skaičius didesnis nei leistina, pakartokite");
            } else {
                return a;
            }
        }
    }

    static public int giveInt(String prompt) {
        while (true) {
            long a = giveLong(prompt);

            if (a < Integer.MIN_VALUE) {
                Ks.ern("Skaičius mažesnis nei Integer.MIN_VALUE"
                        + ", pakartokite");
            } else if (a > Integer.MAX_VALUE) {
                Ks.ern("Skaičius didesnis nei Integer.MAX_VALUE"
                        + ", pakartokite");
            } else {
                return (int) a;
            }
        }
    }

    static public int giveInt(String prompt, int bound1, int bound2) {
        return (int) giveLong(prompt, bound1, bound2);
    }

    static public double giveDouble(String prompt) {
        while (true) {
            String s = giveString(prompt);
            try {
                return Double.parseDouble(s);
            } catch (NumberFormatException e) {
                if (s.contains(",")) {
                    Ks.ern("Vietoje kablelio naudokite tašką"
                            + ", pakartokite");
                } else {
                    Ks.ern("Neteisingas skaičiaus formatas"
                            + ", pakartokite");
                }
            }
        }
    }

    static public double giveDouble(String prompt, double bound1, double bound2) {
        while (true) {
            double a = giveDouble(prompt + " tarp ribų [" + bound1 + ":" + bound2 + "]=");

            if (a < bound1) {
                Ks.ern("Skaičius mažesnis nei leistina, pakartokite");
            } else if (a > bound2) {
                Ks.ern("Skaičius didesnis nei leistina, pakartokite");
            } else {
                return a;
            }
        }
    }

    static public String giveFileName() {
        File dir = new File(dataFolder);
        dir.mkdir();
        oun("Jums prieinami failai " + Arrays.toString(dir.list()));
        return giveString("Nurodykite pasirinktą duomenų failo vardą: ");
    }

    static public String getDataFolder() {
        return dataFolder;
    }

    static public void setDataFolder(String folderName) {
        dataFolder = folderName;
    }

    private static final PrintStream sout = System.out;
    private static final PrintStream serr = System.out;
    private static int lineNr;
    private static int errorNr;
    private static final boolean formatStartOfLine = true;

    static public void ou(Object obj) {
        if (formatStartOfLine) {
            sout.printf("%2d| %s", ++lineNr, obj);
        } else {
            sout.println(obj);
        }
    }

    static public void oun(Object obj) {
        if (formatStartOfLine) {
            sout.printf("%2d| %s\n", ++lineNr, obj);
        } else {
            sout.println(obj);
        }
    }

    static public void ounn(Object obj) {
        if (formatStartOfLine) {
            sout.printf("%s\n", obj);
        } else {
            sout.println(obj);
        }
    }

    static public void oufln(String format, Object... args) {
        sout.printf(format, args);
        sout.println();
    }

    static public void out(Object obj) {
        sout.print(obj);
    }

    static public void ouf(String format, Object... args) {
        sout.printf(format, args);
    }

    static public void er(Object obj) {
        serr.printf("***Klaida %d: %s", ++errorNr, obj);
    }

    static public void ern(Object obj) {
        serr.printf("***Klaida %d: %s\n", ++errorNr, obj);
    }

    static public void erf(String format, Object... args) {
        serr.printf(format, args);
    }

    public static void printMapModel(String delimiter, String[][] mapModel) {
        java.util.Map<Boolean, IntSummaryStatistics> mappingsStatistics = summaryStatistics(mapModel, delimiter);

        for (String[] row : mapModel) {
            for (int j = 0; j < row.length; j++) {
                String format;
                if (j == 0) { // pirmas stulpelis, pvz: [ 0 ]
                    format = "%" + (mappingsStatistics.get(Boolean.FALSE).getMax() + 1) + "s";
                } else { // likę stulpeliai
                    format = j % 2 == 1 ? "%-2s" : "%-" + (mappingsStatistics.get(Boolean.TRUE).getMax() + 1) + "s";
                }

                String value = row[j];
                Ks.ouf(format, (value == null ? "" : split(value, delimiter) + " "));
            }
            Ks.oufln("");
        }
    }

    private static Map<Boolean, IntSummaryStatistics> summaryStatistics(String[][] mapModel, String delimiter) {
        Function<String, Boolean> groupingFunction = s -> !s.equals(ARROW) && !s.startsWith("[") && !s.endsWith("]");
        return Arrays
                .stream(mapModel)
                .flatMap(Arrays::stream)
                .map(s -> split(s, delimiter))
                .collect(Collectors.groupingBy(groupingFunction, summarizingInt(String::length)));
    }

    private static String split(String s, String delimiter) {
        int k = s.indexOf(delimiter);
        if (k <= 0) {
            return s;
        }
        return s.substring(0, k);
    }
}
