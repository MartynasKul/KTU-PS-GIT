clear;clc; 

a=[9 5 0 -2 6 ];
n = length(a);
amin=a(1);
imin=1;

for i = 2:n
    if a(i)<amin
         amin=a(i);
         imin=i;
     end;
end;

disp(a);
disp(['amin= ' num2str(amin) ' imin= ' num2str(imin)])
