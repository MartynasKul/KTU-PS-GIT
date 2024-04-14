clc;
clear;

M = [1 -3 7 4 4];

didziausias = M(1);
maziausias = M(1);
numeris1 = 1;
numeris2 = 1;

for i = 1:length(M)
    if didziausias < M(i)
       didziausias = M(i);
       numeris1 = i;
    end
    if maziausias > M(i)
       maziausias = M(i);
       numeris2 = i;
    end
end

M(numeris1) = 1000;
M(numeris2) = -1000;

% ištrina pasikartojančius elementus
M = unique(M,'stable');
disp(M);

