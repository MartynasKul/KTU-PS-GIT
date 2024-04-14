clear;
clc;

D = [2 0 -2 1 0 -1];
E = [3 4 -1 -1 -1 1];

[kiekis1] = no2_3_neig_kiek(D); 
[kiekis2] = no2_3_neig_kiek(E); 

if(kiekis1 > kiekis2)
    disp('masyvas D turi daugiau neigiamų elementų')
    disp('jų turi:')
    disp(kiekis1)
else
    disp('masyvas E turi daugiau neigiamų elementų')
    disp('jų turi:')
    disp(kiekis2)
end