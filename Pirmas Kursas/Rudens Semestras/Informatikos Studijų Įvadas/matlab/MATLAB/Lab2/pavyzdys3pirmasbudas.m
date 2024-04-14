% Pirmasis sprendimo būdas
a = 15; % a reikšmės priskyrimas
b = 25; % b reikšmės priskyrimas
c = 9; % c reikšmės priskyrimas
if a > b
     if a > c
         max = a;
     else
         max = c;
     end
elseif b > c
    max = b;
else
    max = c;
end
disp(max) %rezultato išvedimas komandiniame lange