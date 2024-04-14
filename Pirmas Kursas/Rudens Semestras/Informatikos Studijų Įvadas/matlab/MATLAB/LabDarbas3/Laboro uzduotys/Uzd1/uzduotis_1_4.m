%ketvirta laboro uzduotis
clear;clc;

S=load('duomenys_1_4.txt');
n=length(S);

j=0; % T masyvo indeksas
k=0; % M masyvo indeksas

for i=1:n
   if S(i)>0
       j=j+1;
       T(j)=S(i)
   else
       k=k+1;
       M(k)=S(i)
   end    
end

disp('Masyvai S');
for i=1:n
    disp(['s = ' num2str(S(i))]);
end

disp('Masyvai T');
for i=1:j
    disp(['t = ' num2str(T(i))]);
end

disp('Masyvai M');
for i=1:k
    disp(['m = ' num2str(M(i))]);
end