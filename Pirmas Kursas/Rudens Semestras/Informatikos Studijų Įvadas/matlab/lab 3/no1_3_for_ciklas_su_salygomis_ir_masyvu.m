clc;
clear;

Y = [2 4 5 2 6 6 6];
a = 3;
count = 1;
for i = 1:length(Y)
    if Y(i) > a
        X(count) = Y(i);
        count = count + 1;
    end
end

for i = 1:length(X)
    disp(X(i))
end