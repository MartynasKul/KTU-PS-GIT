function ketvirtis = no7_kurisketvirtis(x, y)

if x > 0 && y > 0
    ketvirtis = 1;
end

if x < 0 && y > 0
    ketvirtis = 2;
end

if x < 0 && y < 0
    ketvirtis = 3;
end

if x > 0 && y < 0
    ketvirtis = 4;
end