clc;
clear;
% Apskaičiuokite funkcijos  y=x2+1 reikšmes, kai funkcijos y argumentas x kinta 
% nuo -5 iki 5 žingsniu 1

% Formuojamos argumento x reikšmės 
x=-5:5; 
 
% Apskaičiuojamos funkcijų reikšmės 
y=x.^2+1; 
 
% Rezultatų išvedimas 
disp([x'   y' ]);