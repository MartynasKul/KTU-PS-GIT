import bpy
import os
import math

os.system("cls") 

from mathutils import Vector as Vektorius 
from mathutils import Matrix as Matrica

pi = math.pi
objektas = bpy.context.object
M = objektas.matrix_world 
DM = objektas.dimensions

#-------------------------------------------------------------------------------------
def Dirbti(asis1, asis2, slinkti1, slinkti2, dydis, kopij_sk):
    
    M2 = Matrica.Identity(4);
    M1 = Matrica.Identity(4);
    
    asis1 = abs(asis1) - 1;
    asis2 = abs(asis2) - 1;
        
    if (move_axis1 > 0):
         M1[asis1][3] = slinkti1 * DM[asis1] / (objektas.scale[asis1]);
         
    if (move_axis1 < 0):
        M1[asis1][3] = (-1) * slinkti1 * DM[asis1] / (objektas.scale[asis1]);
        
    S = Matrica.Scale(dydis, 4);
    
    for i in range(kopij_sk): 
        if (move_axis2 > 0):
            M2[asis2][3] = i * DM[asis2] * slinkti2 / (objektas.scale[asis2]);
            
        if (move_axis2 < 0):
            M2[asis2][3] = (-1) * i * DM[asis2] * slinkti2 / (objektas.scale[asis2]);
            
        naujas_objektas = objektas.copy();
        naujas_objektas.data = objektas.data.copy(); 
        R = Matrica.Rotation((i) * pi / 8, 4, 'Z');
     
        naujas_objektas.matrix_world = M @ M2 @ R @ M1 @ S;
        bpy.data.scenes[0].collection.objects.link(naujas_objektas);
        
        
        
    return;
#-------------------------------------------------------------------------------------

Dirbti(2, 3, 1.5, 0.5, 0.5, 8)
Dirbti(-2, -3, 1.5, 0.5, 0.5, 8)