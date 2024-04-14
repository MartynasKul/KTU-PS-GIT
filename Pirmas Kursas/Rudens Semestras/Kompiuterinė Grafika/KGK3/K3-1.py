import bpy
import os
import math

os.system("cls") 

objektai = bpy.context.selected_objects 

#----------------------------------------------------------------
def PlotVidurkis(objektai):
    kiekis = 0;
    plotas = 0;

    for i in range(len(objektai)):
        kiekis += 1;
        if(objektai[i].type=='MESH'):
            for p in objektai[i].data.polygons:
                plotas += p.area;
    
    if(kiekis>0):
        plotvidurkis = plotas/kiekis
        return plotvidurkis, kiekis;
    else:
        return 0,kiekis;
    
#----------------------------------------------------------------
def PlokstVidurkis(objektai):
    kiekis = 0;
    plokstumuKiekis = 0;
    
    for i in range(len(objektai)):
        kiekis += 1;
        if(objektai[i].type=='MESH'):
            for p in objektai[i].data.polygons:
                plokstumuKiekis += 1;
    plokstumuVidurkis=plokstumuKiekis/kiekis;            

    return plokstumuVidurkis, kiekis;
        
        
plotuVidurkis, kiekis1 = PlotVidurkis(objektai);           
plokstumuVidurkis, kiekis2 = PlokstVidurkis(objektai);

if(kiekis1 > 0 and kiekis2 > 0):
    print("Pasirinktų objektų bendro ploto vidurkis: {:.2f} kvad. metr.".format(plotuVidurkis));
    print("Pasirinktų objektų plokstumų skaičiaus vidurkis: {:.2f} ".format(plokstumuVidurkis));
else:
    print("Nebuvo pasirinkta pakankamai objektų, pasirinktų objektų skaičius: {}".format(kiekis));