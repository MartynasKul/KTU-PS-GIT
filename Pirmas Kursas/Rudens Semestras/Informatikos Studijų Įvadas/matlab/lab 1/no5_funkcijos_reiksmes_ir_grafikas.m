clc;
clear;

% Apskaičiuokite funkcijų reikšmes ir nubraižykite funkcijų grafikus:

% f(x) = e^0,1*t, kai t kinta nuo 0 iki 20

% Argumento t ir funkcijos f(x) reikšmių formavimas 
t=0:0.1:20; 

y=exp(0.1*t).*sin(t); 
% Grafiko braižymas 

plot (t,y); 
xlabel('Laikas t '); 
ylabel ('Amplitude mm'); 
title ('2D grafikas'); 
grid on;

