clc;
clear;

A = [7 9 1; 6 -8 2; 5 7 3]
B = [1 2 3; 2 3 4; 4 5 6]
C = [9 0 1]
D = [5; 6; -4]

disp('A + B =')
disp(A + B)

disp('A * B =')
disp(A * B)

disp('A ir B atitinkamų elementų sandaugą:')
disp(A.*B)

disp('B^T =')
disp(B')

disp('A^-1 =')
disp(inv(A))

disp('|A| =')
disp(det(A))

