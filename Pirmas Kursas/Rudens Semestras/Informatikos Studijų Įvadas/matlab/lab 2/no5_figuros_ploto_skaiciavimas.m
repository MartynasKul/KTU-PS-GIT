clc;
clear;

a = input('Įveskite a: ');
b = input('Įveskite b: '); 
c = input('Įveskite c: '); 
d = input('Įveskite d: '); 

% pernaudoju funkcijas
S1 = no2_trik(a, b, d) * 2;
S2 = no2_stac(d, 2 * c);
S3 = no2_skritulio(c) / 2;

bendras = S1 + S2 + S3;

disp (['S = ' num2str(bendras)]);