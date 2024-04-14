clc;
clear;

% duotos figuros bendro ploto skaiciavimo scenarijus  
r = input('įveskite kraštinę: r=  '); 

[Str] = no2_trik(r,r,sqrt(2*r^2)); 
[Sst] = no2_stac(r,r); 
[Ssk] = no2_skritulio(r); 

% bendras plotas S 
S = Ssk - Str - 0.75 * Sst; 
disp('figūros plotas='); 
disp(S);