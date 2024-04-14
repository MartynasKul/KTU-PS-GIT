% trikampio ploto skaiciavimas pagal herono formule 
function [S]=no2_trik(a,b,c) 
p=(a+b+c)/2; 
S=sqrt(p*(p-a)*(p-b)*(p-c)); 