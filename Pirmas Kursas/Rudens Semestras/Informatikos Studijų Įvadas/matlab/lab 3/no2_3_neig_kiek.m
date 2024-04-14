function [kiekis]=no2_3_neig_kiek(X)

kiekis = 0;
for i = 1:length(X)
    if(X(i) < 0)
        kiekis = kiekis + 1;
    end
end