package edu.ktu.ds.lab3.gui;

import javax.swing.*;
import javax.swing.table.DefaultTableCellRenderer;
import javax.swing.table.TableCellRenderer;
import javax.swing.table.TableColumn;
import java.awt.*;

/**
 * Klasėje specifikuota maišos lentelės atvaizdavimui skirta lentelė.
 *
 * @author darius
 */
public class Table extends JTable {

    public static final String ARROW = "\u2794";

    public void setModel(String[][] tableContent, String delimiter, int maxChainSize, int colWidth) {
        if (tableContent == null || delimiter == null) {
            throw new IllegalArgumentException("Table content or delimiter is null");
        }

        if (maxChainSize < 0 || colWidth < 0) {
            throw new IllegalArgumentException("Table column width or max chain size is <0: " + colWidth + ", " + maxChainSize);
        }

        setModel(new TableModel(tableContent, delimiter, maxChainSize));
        appearance(colWidth);
    }

    private void appearance(int colWidth) {
        setShowGrid(false);
        // Celės stilius - pacentruojame
        DefaultTableCellRenderer toCenter = new DefaultTableCellRenderer();
        toCenter.setHorizontalAlignment(JLabel.CENTER);
        for (int i = 0; i < getColumnCount(); i++) {
            if (i == 0) {
                getColumnModel().getColumn(i).setPreferredWidth(1);
                // Nustatome nulinio stulpelio celių stilių
                getColumnModel().getColumn(i).setCellRenderer(toCenter);
            } else if (i % 2 != 0) {
                getColumnModel().getColumn(i).setPreferredWidth(30);
                // Nustatome stulpelių su rodyklėmis celių stilių
                getColumnModel().getColumn(i).setCellRenderer(toCenter);
            } else {
                getColumnModel().getColumn(i).setMaxWidth(colWidth);
                getColumnModel().getColumn(i).setMinWidth(colWidth);
            }
        }

        // Lentelės antraštės
        getTableHeader().setResizingAllowed(false);
        getTableHeader().setReorderingAllowed(false);
        getTableHeader().setFont(new Font(Font.SANS_SERIF, Font.PLAIN, 13));
        setFont(new Font(Font.SANS_SERIF, Font.PLAIN, 13));
        // Išcentruojamos antraštės
        ((DefaultTableCellRenderer) getTableHeader().getDefaultRenderer()).setHorizontalAlignment(SwingConstants.CENTER);
        setAutoResizeMode(JTable.AUTO_RESIZE_OFF);
    }

    @Override
    public Component prepareRenderer(TableCellRenderer renderer, int row, int column) {
        Component c = super.prepareRenderer(renderer, row, column);
        // Nustatomas tooltips'ų rodymas
        String value = (String) getValueAt(row, column);
        if (c instanceof JComponent) {
            JComponent jc = (JComponent) c;
            jc.setToolTipText(value);
        }
        // Morkine spalva nuspalvinamos celės, kuriose kas nors įrašyta, išskyrus rodyklę
        if (value != null && !value.equals("") && !value.equals(ARROW)) {
            c.setBackground(Color.ORANGE);
        } //Baltai - likusias celes
        else {
            c.setBackground(Color.WHITE);
        }

        int rendererWidth = c.getPreferredSize().width;
        TableColumn tableColumn = getColumnModel().getColumn(column);
        tableColumn.setPreferredWidth(Math.max(rendererWidth + getIntercellSpacing().width, tableColumn.getPreferredWidth()));
        return c;
    }
}
