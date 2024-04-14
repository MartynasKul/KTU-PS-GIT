function [vidurkis]=no2_4_vidurkis(C)

sum = 0;
for i = 1:length(C)
    sum = sum + C(i);
end
format short g;
vidurkis = sum / length(C);