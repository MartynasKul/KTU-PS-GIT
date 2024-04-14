clear;clc;

n = input('Nurodyti masyvo ilgį n: ');
disp(' ---------- Masyvas A ---------')
for i = 1:n
    A(i) = input('A= ');
end;

suma=0;
sand=1;
n = length(A);

for i = 1:n
     if A(i)>0
        suma = suma + A(i);
    elseif A(i)<0
        sand = sand * A(i);
     end;
end;

disp(A);
disp(['suma= ' num2str(suma) ' sand= ' num2str(sand)]);