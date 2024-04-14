clc;
clear;  

x = -4.5;
y = sqrt(abs((x^2 - 5*x + 6) / (x^2 - 7*x + 10)) + 1);

disp(['x = ' num2str(x)]);
disp(['y = ' num2str(y)]);
disp(' ');

x = 5.2;
y = sqrt(abs((x^2 - 5*x + 6) / (x^2 - 7*x + 10)) + 1);

disp(['x = ' num2str(x)]);
disp(['y = ' num2str(y)]);