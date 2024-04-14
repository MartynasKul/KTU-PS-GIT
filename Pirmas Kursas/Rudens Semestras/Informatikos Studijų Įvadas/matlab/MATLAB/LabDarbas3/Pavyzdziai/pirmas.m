clear;clc;

x=[1 -5 0 2.5 6 ];
[m, n ] = size(x);
for i = 1:n
    y(i) = 2*x(i)-1;
end;
disp('Masyvas y');
disp(y)