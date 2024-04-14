clc;
clear;

X = [1 -3 7 4 5];

didziausias = X(1);
maziausias = X(1);

for i = 1:length(X)
    if didziausias < X(i)
       didziausias = X(i);
    end
    if maziausias > X(i)
       maziausias = X(i);
    end
end

skirtumas = didziausias - maziausias;
X(length(X)) = skirtumas;
disp(X);
     

    