clc;
clear;

S = load('duomenys.txt');
count = 1;
count2 = 1;

for i = 1:length(S)
    if S(i) > 0
        T(count) = S(i);
        count = count + 1;
    elseif S(i) < 0
        M(count2) = S(i);
        count2 = count2 + 1;
    end      
end

disp('pradinis masyvas');
for i = 1:length(S)
    disp(S(i))
end

disp('teigiamas masyvas');
for i = 1:length(T)
    disp(T(i))
end

disp('neigiamas masyvas');
for i = 1:length(M)
    disp(M(i))
end