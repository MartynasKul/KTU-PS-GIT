function [c, d, z] = no6_sav_funcdz(x, y)


d = sqrt(y^2 + 1);
c = sqrt(y^2 + 1);

if c > d
    a = x;
end

if c <= d
    a = y;
end

if c <= d
    b = c;
end

if c > d
    b = d;
end

z = a + b + c + d;