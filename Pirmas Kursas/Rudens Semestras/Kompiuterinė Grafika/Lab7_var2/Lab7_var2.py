import bpy
import os
from os import system
import math
from mathutils import Matrix as Matrica
from mathutils import Vector as V

cls = lambda: system('cls')

cls() #this function call will clear the console

objektas = bpy.context.object

M1 = objektas.matrix_world

def Daryti(axis1, axis2, axis3, coeficient1, coeficient2, coeficient3, scale, DM):
    M = Matrica.Identity(4)
    
    new_object = objektas.copy()
    new_object_data = objektas.data.copy()
    
    
    M[axis1][3] = ((1-scale)/2+scale)*DM[axis1]*coeficient1
    M[axis2][3] = ((1-scale)/2+scale)*DM[axis2]*coeficient2
    M[axis3][3] = ((1-scale)/2+scale)*DM[axis3]*coeficient3
    
    S = Matrica.Scale(scale, 4)
    
    new_object.matrix_world = M1 @ M @ S
    bpy.data.scenes[0].collection.objects.link(new_object)
    
    return

DM = objektas.dimensions


Daryti(0, 1, 2, 1, 1, 1, 0.5, DM)
Daryti(0, 1, 2, -1, 1, 1, 0.5, DM)
Daryti(0, 1, 2, 1, -1, 1, 0.5, DM)
Daryti(0, 1, 2, 1, 1, -1, 0.5, DM)
Daryti(0, 1, 2, -1, -1, 1, 0.5, DM)
Daryti(0, 1, 2, 1, -1, -1, 0.5, DM)
Daryti(0, 1, 2, -1, 1, -1, 0.5, DM)
Daryti(0, 1, 2, -1, -1, -1, 0.5, DM)