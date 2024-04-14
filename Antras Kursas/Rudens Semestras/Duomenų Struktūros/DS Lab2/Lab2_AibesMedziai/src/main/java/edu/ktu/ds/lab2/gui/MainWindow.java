package edu.ktu.ds.lab2.gui;

import edu.ktu.ds.lab2.demo.Car;
import edu.ktu.ds.lab2.demo.CarsGenerator;
import edu.ktu.ds.lab2.utils.Ks;
import edu.ktu.ds.lab2.utils.ParsableAvlSet;
import edu.ktu.ds.lab2.utils.ParsableBstSet;
import edu.ktu.ds.lab2.utils.ParsableSortedSet;

import javax.swing.*;
import javax.swing.border.TitledBorder;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.AdjustmentEvent;
import java.io.File;
import java.util.Arrays;
import java.util.Collections;
import java.util.Random;
import java.util.ResourceBundle;
import java.util.stream.IntStream;

/**
 * Lab2 langas su Swing'u
 * <pre>
 *                  MainWindow (BorderLayout)
 *  |-----------------------Center-------------------------|
 *  |  |-----------------scrollOutput-------------------|  |
 *  |  |  |------------------------------------------|  |  |
 *  |  |  |                                          |  |  |
 *  |  |  |                                          |  |  |
 *  |  |  |                 taOutput                 |  |  |
 *  |  |  |                                          |  |  |
 *  |  |  |                                          |  |  |
 *  |  |  |                                          |  |  |
 *  |  |  |                                          |  |  |
 *  |  |  |                                          |  |  |
 *  |  |  |------------------------------------------|  |  |                                                              |  |
 *  |  |------------------------------------------------|  |                                          |
 *  |------------------------South-------------------------|
 *  |  |~~~~~~~~~~~~~~~~~~~scrollSouth~~~~~~~~~~~~~~~~~~|  |
 *  |  |                                                |  |
 *  |  |    |------------||-----------||-----------|    |  |
 *  |  |    | panButtons || panParam1 || panParam2 |    |  |
 *  |  |    |            ||           ||           |    |  |
 *  |  |    |------------||-----------||-----------|    |  |
 *  |  |                                                |  |
 *  |  |~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|  |
 *  |------------------------------------------------------|
 * </pre>
 *
 * @author darius.matulis@ktu.lt
 */
public class MainWindow extends JFrame implements ActionListener {

    private static final ResourceBundle MESSAGES = ResourceBundle.getBundle("edu.ktu.ds.lab2.gui.messages");
    private static final int TF_WIDTH = 8;

    private final JTextArea taOutput = new JTextArea();
    private final JScrollPane scrollOutput = new JScrollPane(taOutput);
    private final JTextField tfDelimiter = new JTextField();
    private final JTextField tfInput = new JTextField();
    private final JComboBox<String> cmbTreeType = new JComboBox<>();
    private final JPanel panSouth = new JPanel();
    private final JScrollPane scrollSouth = new JScrollPane(panSouth);
    private final JPanel panParam2 = new JPanel();
    private MainWindowMenu mainWindowMenu;
    private Panels panParam1, panButtons;

    private static ParsableSortedSet<Car> carsSet;
    private final CarsGenerator carsGenerator = new CarsGenerator();

    private int sizeOfInitialSubSet, sizeOfGenSet, sizeOfLeftSubSet;
    private double shuffleCoef;
    private final String[] errors;

    public MainWindow() {
        errors = new String[]{
                MESSAGES.getString("badSetSize"),
                MESSAGES.getString("badInitialData"),
                MESSAGES.getString("badSetSizes"),
                MESSAGES.getString("badShuffleCoef")
        };
        initComponents();
    }

    private void initComponents() {
        // Kad prijungiant tekstą prie JTextArea vaizdas visada nušoktų į apačią
        scrollOutput.getVerticalScrollBar()
                .addAdjustmentListener((AdjustmentEvent e) -> taOutput.select(taOutput.getCaretPosition() * taOutput.getFont().getSize(), 0));
        //======================================================================
        // Formuojamas mygtukų tinklelis (mėlynas). Naudojame klasę Panels.
        //======================================================================
        // 4 eilutes, 2 stulpeliai
        panButtons = new Panels(
                new String[]{
                        MESSAGES.getString("button1"),
                        MESSAGES.getString("button2"),
                        MESSAGES.getString("button3"),
                        MESSAGES.getString("button4"),
                        MESSAGES.getString("button5"),
                        MESSAGES.getString("button6")},
                2, 3);
        panButtons.getButtons().forEach((btn) -> btn.addActionListener(this));
        enableButtons(false);
        //======================================================================
        // Formuojama pirmoji parametrų lentelė (žalia). Naudojame klasę Panels.
        //======================================================================
        panParam1 = new Panels(
                new String[]{
                        MESSAGES.getString("lblParam11"),
                        MESSAGES.getString("lblParam12"),
                        MESSAGES.getString("lblParam13"),
                        MESSAGES.getString("lblParam14"),
                        MESSAGES.getString("lblParam15")},
                new String[]{
                        MESSAGES.getString("tfParam11"),
                        MESSAGES.getString("tfParam12"),
                        MESSAGES.getString("tfParam13"),
                        MESSAGES.getString("tfParam14"),
                        MESSAGES.getString("tfParam15")},
                TF_WIDTH);
        //======================================================================
        // Formuojama antroji parametrų lentelė (gelsva).
        //======================================================================
        panParam2.setLayout(new GridBagLayout());
        GridBagConstraints c = new GridBagConstraints();
        c.insets = new Insets(3, 6, 3, 4);
        // Išlygiavimas į kairę
        c.anchor = GridBagConstraints.WEST;
        // Komponentų išplėtimas iki maksimalaus celės dydžio
        c.fill = GridBagConstraints.BOTH;
        // Pirmas stulpelis
        c.gridx = 0;
        panParam2.add(new JLabel(MESSAGES.getString("lblParam21")), c);
        panParam2.add(new JLabel(MESSAGES.getString("lblParam22")), c);
        panParam2.add(new JLabel(MESSAGES.getString("lblParam23")), c);
        // Antras stulpelis
        c.gridx = 1;
        for (String s : new String[]{
                MESSAGES.getString("cmbTreeType1"),
                MESSAGES.getString("cmbTreeType2"),
                MESSAGES.getString("cmbTreeType3")
        }) {
            cmbTreeType.addItem(s);
        }
        cmbTreeType.addActionListener(this);
        panParam2.add(cmbTreeType, c);
        tfDelimiter.setHorizontalAlignment(JTextField.CENTER);
        panParam2.add(tfDelimiter, c);
        // Vėl pirmas stulpelis, tačiau plotis - 2 celės
        c.gridx = 0;
        c.gridwidth = 2;
        tfInput.setEditable(false);
        tfInput.setBackground(Color.lightGray);
        panParam2.add(tfInput, c);
        //======================================================================
        // Formuojamas bendras parametrų skydelis (tamsiai pilkas).
        //======================================================================
        panSouth.setLayout(new FlowLayout(FlowLayout.CENTER, 5, 5));
        panSouth.add(panButtons);
        panSouth.add(panParam1);
        panSouth.add(panParam2);

        mainWindowMenu = new MainWindowMenu() {
            @Override
            public void actionPerformed(ActionEvent ae) {
                try {
                    Object command = ae.getSource();
                    if (command.equals(mainWindowMenu.getMenu(0).getItem(0))) {
                        fileChooseMenu();
                    } else if (command.equals(mainWindowMenu.getMenu(0).getItem(1))) {
                        KsGui.ounerr(taOutput, MESSAGES.getString("notImplemented"));
                    } else if (command.equals(mainWindowMenu.getMenu(0).getItem(3))) {
                        System.exit(0);
                    } else if (command.equals(mainWindowMenu.getMenu(1).getItem(0))) {
                        JOptionPane.showOptionDialog(this,
                                MESSAGES.getString("author"),
                                MESSAGES.getString("menuItem21"),
                                JOptionPane.YES_NO_OPTION,
                                JOptionPane.INFORMATION_MESSAGE,
                                null,
                                new String[]{"OK"},
                                null);
                    }
                } catch (ValidationException e) {
                    KsGui.ounerr(taOutput, e.getMessage());
                } catch (Exception e) {
                    KsGui.ounerr(taOutput, MESSAGES.getString("systemError"));
                    e.printStackTrace(System.out);
                }
            }
        };
        // Meniu juosta patalpinama lange
        setJMenuBar(mainWindowMenu);
        //======================================================================
        // Formuojamas Lab2 langas
        //======================================================================
        JPanel mainWindow = new JPanel();
        mainWindow.setLayout(new BorderLayout());
        // ..centre ir pietuose talpiname objektus..
        mainWindow.add(scrollOutput, BorderLayout.CENTER);
        mainWindow.add(scrollSouth, BorderLayout.SOUTH);

        // Šio freimo "turinyje" talpinamas Lab2 skydelis
        getContentPane().add(mainWindow);
        appearance();
    }

    private void appearance() {
        // Grafinių objektų rėmeliai
        TitledBorder outputBorder = new TitledBorder(MESSAGES.getString("border1"));
        outputBorder.setTitleFont(new Font(Font.SANS_SERIF, Font.BOLD, 11));
        scrollOutput.setBorder(outputBorder);
        TitledBorder southBorder = new TitledBorder(MESSAGES.getString("border2"));
        southBorder.setTitleFont(new Font(Font.SANS_SERIF, Font.BOLD, 11));
        scrollSouth.setBorder(southBorder);

        panParam2.setBackground(new Color(255, 255, 153));// Gelsva
        panParam1.setBackground(new Color(204, 255, 204));// Šviesiai žalia
        panParam1.getTfOfTable().get(2).setEditable(false);
        panParam1.getTfOfTable().get(2).setForeground(Color.red);
        panParam1.getTfOfTable().get(4).setEditable(false);
        panParam1.getTfOfTable().get(4).setBackground(Color.lightGray);
        panButtons.setBackground(new Color(112, 162, 255)); // Blyškiai mėlyna
        panSouth.setBackground(Color.GRAY);
        taOutput.setFont(Font.decode("courier new-12"));
        taOutput.setEditable(false);
    }

    @Override
    public void actionPerformed(ActionEvent ae) {
        try {
            System.gc();
            System.gc();
            System.gc();
            taOutput.setBackground(Color.white);

            Object source = ae.getSource();
            if (source instanceof JButton) {
                handleButtons(source);
            } else if (source instanceof JComboBox && source.equals(cmbTreeType)) {
                enableButtons(false);
            }
        } catch (ValidationException e) {
            if (e.getCode() >= 0 && e.getCode() <= 3) {
                KsGui.ounerr(taOutput, errors[e.getCode()] + ": " + e.getMessage());
                if (e.getCode() == 2) {
                    panParam1.getTfOfTable().get(0).setBackground(Color.red);
                    panParam1.getTfOfTable().get(1).setBackground(Color.red);
                }
            } else if (e.getCode() == 4) {
                KsGui.ounerr(taOutput, MESSAGES.getString("allSetIsPrinted"));
            } else {
                KsGui.ounerr(taOutput, e.getMessage());
            }
        } catch (UnsupportedOperationException e) {
            KsGui.ounerr(taOutput, e.getLocalizedMessage());
        } catch (Exception e) {
            KsGui.ounerr(taOutput, MESSAGES.getString("systemError"));
            e.printStackTrace(System.out);
        }
    }

    private void handleButtons(Object source) throws ValidationException {
        if (source.equals(panButtons.getButtons().get(0))) {
            treeGeneration(null);
        } else if (source.equals(panButtons.getButtons().get(1))) {
            treeIteration();
        } else if (source.equals(panButtons.getButtons().get(2))) {
            treeAdd();
        } else if (source.equals(panButtons.getButtons().get(3))) {
            treeRemove();
        } else if (source.equals(panButtons.getButtons().get(4))
                || source.equals(panButtons.getButtons().get(5))) {
            KsGui.setFormatStartOfLine(true);
            KsGui.ounerr(taOutput, MESSAGES.getString("notImplemented"));
            KsGui.setFormatStartOfLine(false);
        }
    }

    public void treeGeneration(String filePath) throws ValidationException {
        // Nuskaitomi uždavinio parametrai
        readTreeParameters();
        // Sukuriamas aibės objektas, priklausomai nuo medžio pasirinkimo
        // cmbTreeType objekte
        createTree();

        Car[] carsArray;
        // Jei failas nenurodytas - generuojama
        if (filePath == null) {
            carsArray = carsGenerator.generateShuffle(sizeOfGenSet, sizeOfInitialSubSet, shuffleCoef);
            panParam1.getTfOfTable().get(2).setText(String.valueOf(sizeOfLeftSubSet));
        } else { // Skaitoma is failo
            carsSet.load(filePath);
            carsArray = new Car[carsSet.size()];
            int i = 0;
            for (Object o : carsSet.toArray()) {
                carsArray[i++] = (Car) o;
            }
            // Skaitant iš failo išmaišoma standartiniu Collections.shuffle metodu.
            Collections.shuffle(Arrays.asList(carsArray), new Random());
        }

        // Išmaišyto masyvo elementai surašomi i aibę
        carsSet.clear();
        Arrays.stream(carsArray).forEach(a -> carsSet.add(a));

        // Išvedami rezultatai
        // Nustatoma, kad eilutės pradžioje neskaičiuotų išvedamų eilučių skaičiaus
        KsGui.setFormatStartOfLine(true);
        KsGui.oun(taOutput, carsSet.toVisualizedString(tfDelimiter.getText()),
                MESSAGES.getString("setInTree"));
        // Nustatoma, kad eilutės pradžioje skaičiuotų išvedamų eilučių skaičių
        KsGui.setFormatStartOfLine(false);
        enableButtons(true);
    }

    private void treeAdd() throws ValidationException {
        KsGui.setFormatStartOfLine(true);
        Car car = carsGenerator.takeCar();
        carsSet.add(car);
        panParam1.getTfOfTable().get(2).setText(String.valueOf(--sizeOfLeftSubSet));
        KsGui.setFormatStartOfLine(true);
        KsGui.oun(taOutput, car, MESSAGES.getString("setAdd"));
        KsGui.oun(taOutput, carsSet.toVisualizedString(tfDelimiter.getText()));
        KsGui.setFormatStartOfLine(false);
    }

    private void treeRemove() {
        KsGui.setFormatStartOfLine(true);
        if (carsSet.isEmpty()) {
            KsGui.ounerr(taOutput, MESSAGES.getString("setIsEmpty"));
        } else {
            int nr = new Random().nextInt(carsSet.size());

            Car auto = (Car) carsSet.toArray()[nr];

            carsSet.remove(auto);
            KsGui.oun(taOutput, auto, MESSAGES.getString("setRemoval"));
        }
        KsGui.oun(taOutput, carsSet.toVisualizedString(tfDelimiter.getText()));
        KsGui.setFormatStartOfLine(false);
    }

    private void treeIteration() {
        KsGui.setFormatStartOfLine(true);
        if (carsSet.isEmpty()) {
            KsGui.ounerr(taOutput, MESSAGES.getString("setIsEmpty"));
        } else {
            KsGui.oun(taOutput, carsSet, MESSAGES.getString("setIterator"));
        }

        KsGui.setFormatStartOfLine(false);
    }

    private void readTreeParameters() throws ValidationException {
        // Truputėlis kosmetikos..
        for (int i = 0; i < 2; i++) {
            panParam1.getTfOfTable().get(i).setBackground(Color.WHITE);
        }
        // Nuskaitomos parametrų reiksmės. Jei konvertuojant is String
        // įvyksta klaida, sugeneruojama NumberFormatException situacija. Tam, kad
        // atskirti kuriame JTextfield'e ivyko klaida, panaudojama nuosava
        // situacija MyException
        int i = 0;
        try {
            // Pakeitimas (replace) tam, kad sukelti situaciją esant
            // neigiamam skaičiui            
            sizeOfGenSet = Integer.parseInt(panParam1.getParametersOfTable().get(i).replace("-", "x"));
            sizeOfInitialSubSet = Integer.parseInt(panParam1.getParametersOfTable().get(++i).replace("-", "x"));
            sizeOfLeftSubSet = sizeOfGenSet - sizeOfInitialSubSet;
            ++i;
            shuffleCoef = Double.parseDouble(panParam1.getParametersOfTable().get(++i).replace("-", "x"));
        } catch (NumberFormatException e) {
            // Galima ir taip: pagauti exception'ą ir vėl mesti
            throw new ValidationException(panParam1.getParametersOfTable().get(i), e, i);
        }
    }

    private void createTree() throws ValidationException {
        switch (cmbTreeType.getSelectedIndex()) {
            case 0:
                carsSet = new ParsableBstSet<>(Car::new);
                break;
            case 1:
                carsSet = new ParsableAvlSet<>(Car::new);
                break;
            default:
                enableButtons(false);
                throw new ValidationException(MESSAGES.getString("notImplemented"));
        }
    }

    private void enableButtons(boolean enable) {
        IntStream.rangeClosed(1, 5)
                .filter(i -> i < panButtons.getButtons().size() && panButtons.getButtons().get(i) != null)
                .forEachOrdered(i -> panButtons.getButtons().get(i).setEnabled(enable));
    }

    private void fileChooseMenu() throws ValidationException {
        JFileChooser fc = new JFileChooser(".");

        // Nuimamas "all Files" filtras
        // fc.setAcceptAllFileFilterUsed(false);
        // Papildoma mūsų sukurtais filtrais
        fc.addChoosableFileFilter(new javax.swing.filechooser.FileFilter() {
            @Override
            public boolean accept(File file) {
                String filename = file.getName();
                // Rodomos tik direktorijos ir txt failai
                return file.isDirectory() || filename.endsWith(".txt");
            }

            @Override
            public String getDescription() {
                return "*.txt";
            }
        });
        int returnVal = fc.showOpenDialog(this);
        if (returnVal == JFileChooser.APPROVE_OPTION) {
            File file = fc.getSelectedFile();
            treeGeneration(file.getAbsolutePath());
        }
    }

    public static void createAndShowGUI() {
        try {
            UIManager.setLookAndFeel(UIManager.getCrossPlatformLookAndFeelClassName()
                    // Arba sitaip, tada swing komponentu isvaizda priklausys
                    // nuo naudojamos OS:
                    //  UIManager.getSystemLookAndFeelClassName()
                    // Arba taip:
                    //  "com.sun.java.swing.plaf.motif.MotifLookAndFeel"
                    // Linux'e dar taip:
                    //  "com.sun.java.swing.plaf.gtk.GTKLookAndFeel"
            );
            UIManager.put("Button.defaultButtonFollowsFocus", Boolean.TRUE);
        } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | UnsupportedLookAndFeelException ex) {
            Ks.ou(ex.getMessage());
        }
        MainWindow window = new MainWindow();
        window.setLocation(50, 50);
        window.setIconImage(new ImageIcon(MESSAGES.getString("icon")).getImage());
        window.setTitle(MESSAGES.getString("title"));
        window.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        window.setPreferredSize(new Dimension(1100, 650));
        window.pack();
        window.setVisible(true);
    }
}
