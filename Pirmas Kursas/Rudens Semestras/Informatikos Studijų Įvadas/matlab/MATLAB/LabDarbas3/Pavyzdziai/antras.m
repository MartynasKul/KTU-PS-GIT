clear;clc;

X=[-1 5 7 0 9 -6];
n=length(X);
j = 0;
for i = 1:n
    if X(i)>0
        j = j + 1;
        Y(j) = X(i);
    end;
end;
disp ('Duotas masyvas X '); disp(X)
if j~=0
 disp ('Naujas masyvas Y '); disp(Y)
else
disp ('Duotame masyve X nėra teigiamų elementų ')
end