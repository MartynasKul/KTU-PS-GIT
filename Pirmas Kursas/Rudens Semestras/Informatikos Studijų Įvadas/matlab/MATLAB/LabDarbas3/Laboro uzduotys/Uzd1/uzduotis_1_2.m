%antra laboro uzduotis
clear;clc;

a = input('Įveskite a reikšmę: '); 
b = input('Įveskite b reikšmę: '); 
c = input('Įveskite v reikšmę: '); 
X=load('duomenys_1_2.txt');
n=length(X);
j=0;

for x=1:n
    if sin(x)<cos(x)
        j=j+1;
        Y(j)=a*x^2+b*x+c;
    elseif sin(x)>cos(x)
        j=j+1;
        Y(j)=a*x+b+c;
    end
end

disp('Masyvas Y');
for i=1:length(Y)
   disp(['Y = ' num2str(Y(i))]); 
end