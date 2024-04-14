clc;
clear;

% v(x) = 5sin(x) - 8cos(2x), kai x kinta nuo -6 iki 6 žingsniu 0,1
% g(t) = 3t^3 + 2t^2 - 3t + 1, kai t kinta nuo -2,5 iki 2 žingsnsiu 0,2

% Pirmojo grafiko argumento ir funkcijos reikšmių apskaičiavimas 
x=-6:0.1:6; 
v=5*sin(x)-8*cos(2*x);

% Antrojo grafiko argumento ir funkcijos reikšmių apskaičiavimas 
t=-2.5:0.2:2; 
g=3*t.^3+2*t.^2-3*t+1; 

% Grafiku braizymas;   
plot (x,v,t,g);  
xlabel('x, t'); 
ylabel('v(x), g(t)'); 
grid on 
legend ('v(x)=5*sin(x)-8*cos(2*x)', 'g(t)=3*t^3+2*t^2-3*t+1');  
grid on 
% Grafikų parametrai (spalva, simbolius, linijos tipas ir kt.) 
% nustatomi grafiniame lange Figure 1 