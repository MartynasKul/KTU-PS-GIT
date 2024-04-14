%trecia laboro uzduotis
clear;clc;

a = input('Įveskite a reikšmę: '); 
Y=load('duomenys_1_3.txt');
n=length(Y);

j=0;

for i=1:n
   if Y(i)>a
       j=j+1;
       C(j)=Y(i);
   end    
end   

disp('Masyvas C');
for i=1:length(C)
   disp(['C = ' num2str(C(i))]); 
end