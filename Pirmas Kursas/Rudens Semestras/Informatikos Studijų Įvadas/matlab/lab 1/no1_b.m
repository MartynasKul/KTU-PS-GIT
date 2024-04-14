clc;
clear;  

x = 5;
y = exp(x); 
z = (5 * x^2+3 * x+3)/sqrt(5 * y+1); 
format short;
disp(['x = ' num2str(x)]);
disp(['y = ' num2str(y)]);
disp(['z = ' num2str(z)]);