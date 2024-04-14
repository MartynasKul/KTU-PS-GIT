clc;
clear;

D = [2 3 0 4 -2 -1 2];
E = [1 2 1 0 -1 -6 7];
part1 = 0;
part2 = 0;
part3 = 0;

for i = 1:length(D)
    part1 = part1 + D(i) * E(i);
    if D(i) >= 0
        part2 = part2 + D(i);
    end

    if E(i) ~= 0
        part3 = part3 * E(i);
    end

end

ats = part1 / part2 + part3;
disp(ats);

