package edu.ktu.ds.lab2.gui;

import javax.swing.*;
import javax.swing.text.JTextComponent;
import java.awt.*;
import java.util.List;
import java.util.Queue;
import java.util.*;
import java.util.stream.Collectors;

/**
 * Klasės objektu galima sukurti du panelius: parametrų lentelę ir mygtukų
 * tinklelį. Panelyje talpinamų objektų kiekis nustatomas parametrais.
 *
 * @author darius.matulis@ktu.lt
 */
public class Panels extends JPanel {

    private final static int SPACING = 4;
    private final List<JTextField> tfs = new ArrayList<>();
    private final List<JButton> btns = new ArrayList<>();

    /**
     * Sukuriama parametrų lentelė (GridBag išdėstymo dėsnis)
     * <pre>
     * |-------------------------------|
     * |                |------------| |
     * |   lblTexts[0]  | tfTexts[0] | |
     * |                |------------| |
     * |                               |
     * |                |------------| |
     * |   lblTexts[1]  | tfTexts[1] | |
     * |                |------------| |
     * |      ...             ...      |
     * |-------------------------------|
     * </pre>
     *
     * @param lblTexts
     * @param tfTexts
     * @param columnWidth
     */
    public Panels(String[] lblTexts, String[] tfTexts, int columnWidth) {
        super();
        if (lblTexts == null || tfTexts == null) {
            throw new IllegalArgumentException("Arguments for table of parameters are incorrect");
        }

        if (lblTexts.length > tfTexts.length) {
            tfTexts = Arrays.copyOf(tfTexts, lblTexts.length);
            Arrays.fill(tfTexts, "");
        }

        initTableOfParameters(columnWidth, lblTexts, tfTexts);
    }

    /**
     * Sukuriamas mygtukų tinklelis (GridLayout išdėstymo dėsnis)
     * <pre>
     * |-------------------------------------|
     * | |-------------| |-------------|     |
     * | | btnNames[0] | | btnNames[1] | ... |
     * | |-------------| |-------------|     |
     * |                                     |
     * | |-------------| |-------------|     |
     * | | btnNames[2] | | btnNames[3] | ... |
     * | |-------------| |-------------|     |
     * |       ...              ...          |
     * |-------------------------------------|
     * </pre>
     *
     * @param btnNames
     * @param gridX
     * @param gridY
     */
    public Panels(String[] btnNames, int gridX, int gridY) {
        super();
        if (btnNames == null || gridX < 1 || gridY < 1) {
            throw new IllegalArgumentException("Arguments for buttons grid are incorrect");
        }
        initGridOfButtons(gridX, gridY, btnNames);
    }

    private void initTableOfParameters(int columnWidth, String[] lblTexts, String[] tfTexts) {
        setLayout(new GridBagLayout());
        GridBagConstraints c = new GridBagConstraints();
        // Spacing'as tarp komponentų
        c.insets = new Insets(SPACING, SPACING, SPACING, SPACING);
        // Lygiavimas į kairę
        c.anchor = GridBagConstraints.WEST;
        // Pasirenkamas pirmas stulpelis..
        c.gridx = 0;
        // ..ir į jį sudedami labeliai
        Arrays.stream(lblTexts).forEach((lblText) -> add(new JLabel(lblText), c));
        // Pasirenkamas antras stulpelis..
        c.gridx = 1;
        // ..ir į jį sudedami textfieldai

        for (String tfText : tfTexts) {
            JTextField tf = new JTextField(tfText, columnWidth);
            tf.setHorizontalAlignment(JTextField.CENTER);
            tf.setBackground(Color.WHITE);
            tfs.add(tf);
            add(tf, c);
        }
    }

    private void initGridOfButtons(int gridX, int gridY, String[] btnNames) {
        setLayout(new GridLayout(gridY, gridX, SPACING, SPACING));
        Queue<String> btnNamesQueue = new LinkedList<>(Arrays.asList(btnNames));
        for (int i = 0; i < gridX; i++) {
            for (int j = 0; j < gridY; j++) {
                if (btnNamesQueue.isEmpty()) {
                    break;
                }
                JButton button = new JButton(btnNamesQueue.poll());
                btns.add(button);
                add(button);
            }
        }
    }

    /**
     * Gražinamas parametrų lentelės parametrų sąrašas
     *
     * @return Gražinamas parametrų lentelės parametrų sąrašas
     */
    public List<String> getParametersOfTable() {
        return tfs.stream().map(JTextComponent::getText).collect(Collectors.toList());
    }

    /**
     * Gražinamas parametrų lentelės JTextField objektų sąrašas
     *
     * @return Gražinamas parametrų lentelės JTextField objektų sąrašas
     */
    public List<JTextField> getTfOfTable() {
        return tfs;
    }

    /**
     * Gražinamas mygtukų tinklelio JButton objektų sąrašas
     *
     * @return Gražinamas mygtukų tinklelio JButton objektų sąrašas
     */
    public List<JButton> getButtons() {
        return btns;
    }
}
