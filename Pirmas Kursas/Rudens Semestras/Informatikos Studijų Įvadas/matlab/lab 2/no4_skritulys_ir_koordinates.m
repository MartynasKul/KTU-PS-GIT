clc;
clear;

x1 = input('Įveskite x1: ');
x2 = input('Įveskite x2: ');

y1 = input('Įveskite y1: '); 
y2 = input('Įveskite y2: '); 

a = x1 + x2;
b = y1 + y2;
AB = sqrt(a^2 + b^2);


S = pi * (AB / 2)^2;
disp (['Skritulio plotas S = ' num2str(S)])

xc = a / 2;
yc = b / 2;

disp (['xc = ' num2str(xc)])
disp (['yc = ' num2str(yc)])