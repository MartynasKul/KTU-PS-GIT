x=[9 -5 2];
y=x.^2;
n=length(x);
% rezultatai rašomi į failą, suformavus
% rezeilute kiekvienam i
failas=fopen('rezultatai.txt','w');
for i=1:n
    rezeilute=[i; x(i); y(i)];
    fprintf(failas,'\n'); % nauja eilutė
    fprintf(failas,'%2d %6.2f %8.4f\n ',rezeilute);
end;
% privalomas failo „failas” uždarymo veiksmas
fclose(failas);