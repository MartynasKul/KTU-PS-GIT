% funkcijos y = x*x + x +2 reikšmių skaičiavimo programa.
clear;clc;

xp = 1; %pradinės reikšmės priskyrimas
xg = 3; %galinės reikšmės priskyrimas
hx = 1; %žingsnio priskyrimas
x = xp; %pradinės reikšmės priskyrimas ciklo kintamajam
while x <= xg %ciklo pabaigos sąlygos tikrinimas
 y = x^2 + x +2; %y skaičiavimas
 disp([x, y]); %x ir y reikšmių spausdinimas
 x = x + hx; %ciklo kintamojo keitimas
end