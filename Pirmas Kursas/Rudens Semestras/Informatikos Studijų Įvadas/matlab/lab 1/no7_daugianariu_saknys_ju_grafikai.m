clc;
clear;

% Raskite daugianarių šaknis ir nubraižykite jų grafikus:

% y(x) = 2x^3 + 3x^2 - 9x + 1; daugianario grafiką nubraižykite intervale [-4, 2].

% Vektoriaus, sudaryto iš daugianario koeficientų, formavimas 
C=[2 3 -9  1]; 
% Daugianario šaknų paieška 
saknys=roots(C);
disp("šaknys:");
disp(saknys);

% Daugianario grafiko formavimas ir braižymas 
x=-4:0.1:2; 
y=2*x.^3+3*x.^2-9*x+1; 
plot(x,y); 
grid on; 
xlabel('x'); 
ylabel('y(x)=2*x.^3+3*x.^2-9*x+1'); 