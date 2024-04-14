clc;
clear;

A = [7 9 1; 6 -8 2; 5 7 3]
B = [1 2 3; 2 3 4; 4 5 6]
C = [9 0 1]
D = [5; 6; -4]

% .* vienos  matricos  kiekvieno  elemento  sandauga  iš  kitos  matricos  atitinkamo elemento; 
% ./ vienos matricos kiekvieno elemento  dalyba  iš  kitos  matricos  atitinkamo elemento; 
% .^ vienos matricos kiekvieno elemento kėlimas laipsniu, kuris yra kitos matricos atitinkamas elementas.

disp('išskirtas matricos A antrasis stulpelis:');
A2=A(:,2);
disp(A2);

disp('išskirta matricos B trečioji eilutė:');
B3=B(3,:);
disp(B3);

disp('suformuota nauja matrica E (prie matricos A prijungtas vektorius C):');
E=[A; C];
disp(E);

disp('A matrica be trečios eilutės:');
A(3,:)=[];
disp(A);

disp('B matrica be pirmo stulpelio:');
B(:,1)=[];
disp(B);

disp('suformuota nauja matrica F (matricos B ir matricos A pirmi du stulpeliai sujungti vertikaliai):');
F=[B; A(:,1:2)];
disp(F);

disp('Matricos B pirmoji eilutė pakeista matricos A trečiuoju stulpeliu:');
B(1,:)=A(:,3)';
disp(B);