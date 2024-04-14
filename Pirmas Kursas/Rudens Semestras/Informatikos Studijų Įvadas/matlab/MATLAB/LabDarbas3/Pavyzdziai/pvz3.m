clear;clc;
n = input('Nurodyti masyvo ilgį n: ');
disp(' ---------- Masyvas X ---------')
for i = 1:n
    X(i) = input('X= ');
end;

% pasizaidimo dalis

disp(' ---------- Masyvas X ---------')
for i = 1:n
  disp( ['X = ' num2str(X(i))]);
end;

% pasizaidimas 2

M=load('duomenys.txt');

disp(' ---------- Masyvas Y ---------')
for i = 1:length(M)
  disp( ['Y = ' num2str(M(i))]);
end;