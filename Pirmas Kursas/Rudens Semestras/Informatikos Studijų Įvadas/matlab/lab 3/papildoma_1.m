clc;
clear;

X = [1 4 2 3 4 5 6 3 1 5 6];

b = input('Įveskite pradžią: ');
d = input('Įveskite pabaiga: ');
A = [];
count = 1;

for i = b:d
    A(count) = X(i);
    count = count + 1;
end

lowest = A(1);
lowest_no = 1;
for i = 1:length(A)
    if(lowest > A(i))
        lowest = A(i);
        lowest_no = i;
    end
end

disp(A);
disp(['Mažiausias elementas: ' num2str(lowest)]);
disp(['Jo numeris: ' num2str(lowest_no)]);