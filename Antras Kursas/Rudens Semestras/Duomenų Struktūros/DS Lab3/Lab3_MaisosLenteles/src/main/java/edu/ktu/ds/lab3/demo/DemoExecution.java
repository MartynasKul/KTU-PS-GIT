package edu.ktu.ds.lab3.demo;

import edu.ktu.ds.lab3.gui.MainWindow;

import java.util.Locale;

/*
 * Darbo atlikimo tvarka - čia yra Swing gui pradinė klasė.
 */
public class DemoExecution {

    public static void main(String[] args) {
        Locale.setDefault(Locale.US); // Suvienodiname skaičių formatus
        MainWindow.createAndShowGUI();
    }
}
