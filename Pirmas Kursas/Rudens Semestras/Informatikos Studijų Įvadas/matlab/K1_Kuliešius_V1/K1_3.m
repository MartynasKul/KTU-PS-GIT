clc;
clear;

x0=0.0; %pradine reiksme
xn=5.1; % galine reiksme
hx=0.1; %zingsnis

for x = 0.0:0.1;5.1;

    if x<0.3
        y=sin(x)+cos(x); 
    else
        y=exp(x+2);
    end
    
    if y>=0 && y<=1
        z=log10(x+cos(x));

    elseif y<0
        z=(-y)^2;    
    else
        z=0;
    end
        
  disp(['x = ' num2str(x) '; y = ' num2str(y) '; z = ' num2str(z)]);
   
end
