clc;
clear

xp = input('Įveskite xp: ');
xg = input('Įveskite xg: ');
hx = input('Įveskite hx: ');


for x = xp:hx:xg
    a = sin(x)^2;
    b = cos(x);

    if a < b
        y = log(a - b);

    elseif a > b
        y = log(a + b);

    else
        y = a;
    
    end
    disp([x, y])
end
