%pirma laboro uzduotis
clear;clc;

xp = input('Įveskite pradinę xp reikšmę: '); %pradinės reikšmės įvedimas
xg = input('Įveskite galinę xg reikšmę: '); %galinės reikšmės įvedimas
hx = input('Įveskite žingsnį hx: '); %žingsnio įvedimas
j=1;

for x = xp:hx:xg %ciklo užrašymas
    a=(sin(x))^2;
    b=cos(x);
    
    if a<b
        Y(j)=log(a-b)
        j=j+1;
    elseif a>b
        Y(j)=log(a+b);
        j=j+1;
    else
        Y(j)=a;
        j=j+1;
    end;
end;

disp('Masyvas Y');
for i=1:j-1
   disp(['Y = ' num2str(Y(i))]); 
end;
    
