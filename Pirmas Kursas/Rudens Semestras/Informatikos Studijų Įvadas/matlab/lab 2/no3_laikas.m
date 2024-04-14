clc;
clear;

x = input('Įveskite x: ');
y = input('Įveskite y: '); 

minutes = x * 60 + y;
sekundes = minutes * 60;

disp (['m = ' num2str(minutes)])
disp (['s = ' num2str(sekundes)])