clc;
clear
a = input('Įveskite a: ');
b = input('Įveskite b: ');
c = input('Įveskite c: ');

x = [2 4 5 6 6 8 8 8];

for i = 1:length(x)
      
    if sin(x(i)) < cos(x(i))
        y = a * x(i)^2 + b * x(i) + c;
        
    else
        y = a * x(i) + b + c;
        
    end   
    disp(y) 
end
    


