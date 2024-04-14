clc;
clear;

pradzia = -10;
pabaiga = 10;
zingsnis = 2;

format compact;

for x = -10:2:10
    y = cos(x^2 -1);
    disp([x, y])
end
