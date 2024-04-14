clc;
clear;

X = [1 2 3 4 -1 -2 -3 -4];
Y = [];

count = 1;
for i = 1:length(X)
    if X(i) < 0
        Y(count) = X(i);
        count = count + 1;
    end
end

kiekis = 0;
[vidurkis] = no2_4_vidurkis(Y);
for i = 1:length(Y)
    if Y(i) > vidurkis
        kiekis = kiekis + 1;
    end
end

Y(1) = kiekis;
disp(Y);

        

