clc;
clear;

A = [1 2 3 4 5 6; 7 8 9 10 11 12; 13 14 15 16 17 18; 19 20 21 22 23 24; 25 26 27 28 29 30; 31 32 33 34 35 36];
C = [1; 2; 3; 4];
disp('A = ');
disp(A);
disp('C = ');
disp(C);

B = A(2:5,1:4);
disp('B = ');
disp(B);

S = (B * C)';
disp('S = ');
disp(S');

disp('B paskutinis stulpelis pakeistas su vektoriumi C:')
B(:,4)=C';
disp(B);

disp('B determinantas:')
disp(det(B));

disp('B be pirmos eilutės:')
B(1,:)=[];
disp(B);

disp('Kai kiekvienas B elementas pakeltas kvadratu:')
disp(B.^2);
