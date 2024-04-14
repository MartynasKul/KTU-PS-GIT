clc;
clear;

%pirma dalis pirmos uzduoties
x=0.25;
x2=-2.15;

y=sqrt(5+((5*x+7)/(x+2)))+((log10(3-x))/x);
y2=sqrt(5+((5*x2+7)/(x2+2)))+((log10(3-x2))/x2);

disp(['Kai x = 0.25, y = ' num2str(y)]);
disp(['Kai x = -2.15, y = ' num2str(y2)]);

% antra dalis pirmos uzduoties
A=[1 2 3; 4 5 6];
C=[7 8 9];

B=[A; C];

disp('B matrica:');
disp(B);