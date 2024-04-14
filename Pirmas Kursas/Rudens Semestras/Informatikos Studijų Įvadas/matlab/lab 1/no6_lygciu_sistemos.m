clc;
clear;

% reikia lygčių sistema pasiversti į matricos pavidalą

% nežinomieji
A=[1 5.5; 4 1]; 

% laisvieji nariai
B=[7.1; 9.2]; 

% atvirkštinės matricos metodas:

X = A^-1*B;
disp('ats.:')
disp(X);
