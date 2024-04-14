clc;
clear;

X = [1 3 -1 -2 0 8 -10];
disp(X);
Teigiami = [];
count = 1;
highest = X(1);
highest_no = X(1);
for i = 1:length(X)
    if X(i) > 0
        Teigiami(count) = X(i);
        count = count + 1;
    end

    if highest < X(i)
        highest = X(i);
        highest_no = i;
    end
end
[vidurkis] = no2_4_vidurkis(Teigiami);
X(highest_no) = vidurkis;


X(highest_no) = vidurkis;
disp(X);