clc;
clear;

r = 6370;

V = 4/3 * pi * r^3;
S = pi * r^2;
S2 = S * 0.29;

format short;

disp('tūris (km^3)');
disp(V);
disp('paviršiaus plotas (km^2)');
disp(S);
disp('sausumos paviršiaus plotas (km^2)');
disp(S2);