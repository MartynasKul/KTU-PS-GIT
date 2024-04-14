clear;
clc;

b=input('Iveskite b reiksme: ');
c=input('Iveskite c reiksme: ');
d=input('Iveksite d reiksme: ');

a=istrizaine(b,d);
S1=plotTrik(b,d);
S2=plotStac(d,c);
S3=plotSkritPus(c);
VISASPLOTAS=visasPLOT(S1,S1,S2,S3);

disp('Pradiniai duomenys:');
disp(['a = ' num2str(a)]);
disp(['b = ' num2str(b)]);
disp(['c = ' num2str(c)]);
disp(['d = ' num2str(d)]);

disp('Plotai:');
disp(['S1 = ' num2str(S1)]);
disp(['S2 = ' num2str(S2)]);
disp(['S3 = ' num2str(S3)]);
disp(['Visas plotas = ' num2str(VISASPLOTAS)]);