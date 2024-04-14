package edu.ktu.ds.lab2.gui;

import javax.swing.*;
import java.awt.*;

/**
 * Klasė, skirta duomenų išvedimui į GUI
 *
 * @author darius
 */
public class KsGui {

    private static int lineNr;
    private static boolean formatStartOfLine = true;

    public static void setFormatStartOfLine(boolean formatStartOfLine) {
        KsGui.formatStartOfLine = formatStartOfLine;
    }

    private static String getStartOfLine() {
        return (formatStartOfLine) ? ++lineNr + "| " : "";
    }

    public static void oun(JTextArea ta, Object o) {
        ou(ta, o);
        ta.append(System.lineSeparator());
    }

    public static void ou(JTextArea ta, Object o) {
        if (o instanceof Iterable) {
            for (Object iterable : (Iterable) o) {
                ta.append(iterable.toString() + System.lineSeparator());
            }
        } else {
            ta.append(o.toString());
        }
    }

    public static void ou(JTextArea ta, Object o, String msg) {
        ta.append(getStartOfLine() + msg + ": ");
        oun(ta, o);
    }

    public static void oun(JTextArea ta, Object o, String msg) {
        ta.append(getStartOfLine() + msg + ": " + System.lineSeparator());
        oun(ta, o);
    }

    public static void ounerr(JTextArea ta, Exception e) {
        ta.setBackground(Color.pink);
        ta.append(getStartOfLine() + e.toString() + System.lineSeparator());
        for (StackTraceElement ste : e.getStackTrace()) {
            ta.append(ste.toString() + System.lineSeparator());
        }
        ta.append(System.lineSeparator());
    }

    public static void ounerr(JTextArea ta, String msg) {
        ta.setBackground(Color.pink);
        ta.append(getStartOfLine() + msg + ". " + System.lineSeparator());
    }

    public static void ounerr(JTextArea ta, String msg, String parameter) {
        ta.setBackground(Color.pink);
        ta.append(getStartOfLine() + msg + ": " + parameter + System.lineSeparator());
    }
}