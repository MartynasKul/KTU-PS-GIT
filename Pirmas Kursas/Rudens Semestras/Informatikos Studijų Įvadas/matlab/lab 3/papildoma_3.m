clc;
clear;

Y = [1 2 4 6 1 3 5 0 -2 -2 -1];
b = input('Įveskite pradžią: ');
d = input('Įveskite pabaiga: ');
X = [];
count = 1;

for i = 1:b-1
    X(count) = Y(i);
    count = count + 1;
end

for i = d+1:length(Y)
    X(count) = Y(i);
    count = count + 1;
end

disp(X);

A = [];
B = [];
count1 = 1;
count2 = 1;
for i = 1:length(X)
    if X(i) ~= 0
        A(count1) = X(i);
        count1 = count1 + 1;
    end
    if X(i) < 0
        B(count2) = X(i);
        count2 = count2 + 1;
    end
end
sand = 1;
for i = 1:length(A)
    sand = sand * A(i);
end
[vidurkis] = no2_4_vidurkis(B);

disp(sand);
disp(vidurkis);
