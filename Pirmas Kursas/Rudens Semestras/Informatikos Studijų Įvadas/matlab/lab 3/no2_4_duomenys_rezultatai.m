clc;
clear;

A = load("duomenys_1.txt");
B = load("duomenys_2.txt");
C = [];
count = 1;

for i = 1:length(A)
    if A(i) == B(i)
        C(count) = A(i);
        count = count + 1;
    end
end


[vidurkis] = no2_4_vidurkis(C);

failas=fopen('rezultatai.txt', 'w'); 
fprintf(failas, '%f', vidurkis);
fclose(failas);

