clc;
clear;

x=input('Įveskite kiek valandų pradėjo nuo vidurnakčio: ');
y=input('Įveskite kiek minučių pradėjo nuo vidurnakčio: ');

[MMin]=minutes(x,y);
[SSek]=sekundes(MMin);

disp(['m=' num2str(MMin)]);
disp(['s=' num2str(SSek)]);