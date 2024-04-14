clc;
clear;

UGIAI = [];

n = input('Nurodyti masyvo ilgį  n: '); 
disp(' ---------- UGIAI ---------') 
for i = 1:n 
    UGIAI(i) = input(['ugis nr. ' num2str(i) ': ']); 
end 

didziausias = UGIAI(1);
maziausias = UGIAI(1);
for i = 1:length(UGIAI)
    if didziausias < UGIAI(i)
       didziausias = UGIAI(i);
    end
end

disp(['didžiausias ūgis sąraše: ' num2str(didziausias)])
count = 0;
for i = 1:length(UGIAI)
    if UGIAI(i) == didziausias
        count = count + 1;
    end
end
disp(['tokio ūgio sąraše yra tiek: ' num2str(count)])