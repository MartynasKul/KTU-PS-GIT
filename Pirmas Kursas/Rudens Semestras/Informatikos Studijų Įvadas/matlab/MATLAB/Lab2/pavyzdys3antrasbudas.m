% Antrasis sprendimo būdas
a = input('Įveskite a: '); % a reikšmės įvedimas klaviatūra
b = input('Įveskite b: '); % b reikšmės įvedimas klaviatūra
c = input('Įveskite c: '); % c reikšmės įvedimas klaviatūra
max = a;
if b > max
    max = b;
end
if c > max
    max = c;
end
disp('Maksimali reikšmė'); %teksto spausdinimas komandiniame lange
disp(max); %rezultato spausdinimas komandiniame lange