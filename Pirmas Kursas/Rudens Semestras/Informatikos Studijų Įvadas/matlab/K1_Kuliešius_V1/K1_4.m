clc;
clear;

C=[1 2 3 4 5];
D=[6 7 8 9 10];

idC=F4(C);
idD=F4(D);

C(idC)=D(idD);

disp(C);
