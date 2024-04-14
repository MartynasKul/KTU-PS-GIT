% duotos figuros bendro ploto skaiciavimo scenarijus
r = input('įveskite kraštinę: r= ');
[Str] = trik(r,r,sqrt(2*r^2));
[Sst] = stac(r,r);
[Ssk] = skritulio(r);
% bendras plotas S
S = Ssk - Str - 0.75 * Sst;
disp('figūros plotas=');
disp(S);
