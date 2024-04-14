% funkcijos y = x*x + x +2 reikšmių skaičiavimo programa.
clc; clear;
xp = input('Įveskite pradinę xp reikšmę: '); %pradinės reikšmės įvedimas
xg = input('Įveskite galinę xg reikšmę: '); %galinės reikšmės įvedimas
hx = input('Įveskite žingsnį hx: '); %žingsnio įvedimas
for x = xp:hx:xg %ciklo užrašymas
 y = x^2 + x +2; %y skaičiavimas
 disp([x, y]); %x ir y reikšmių spausdinimas
end
