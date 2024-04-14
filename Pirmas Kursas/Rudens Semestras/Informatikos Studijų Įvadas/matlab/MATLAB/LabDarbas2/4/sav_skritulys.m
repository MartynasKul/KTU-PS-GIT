clear;
clc;

x1=input('Įveskite x1:');
y1=input('Įveskite y1:');
x2=input('Įveskite x2:');
y2=input('Įveskite y2:');

atkarposilgis=vekilg(x1,y1,x2,y2);
skritPlotas=splotas(atkarposilgis);
xc=vidurys(x1,x2);
yc=vidurys(y1,y2);

disp('Skritulio plotas S= ');
disp(skritPlotas);
disp(['skritulio centro koordinates: xc= ' num2str(xc) ', yc= ' num2str(yc)]);
