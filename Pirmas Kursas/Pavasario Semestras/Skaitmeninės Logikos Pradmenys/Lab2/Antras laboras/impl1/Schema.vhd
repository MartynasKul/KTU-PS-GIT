-- VHDL model created from schematic Schema.sch -- Mar 30 22:45:24 2022

library IEEE;
use IEEE.std_logic_1164.all;
library xp2;
use xp2.components.all;

entity SCHEMA is
      Port (      x1 : In    std_logic;
                  x2 : In    std_logic;
                  x3 : In    std_logic;
                  x4 : In    std_logic;
                 rst : In    std_logic;
             Statinis : Out   std_logic;
                   D : Out   std_logic;
                   C : Out   std_logic;
             DinaminisD : Out   std_logic;
                 DPD : Out   std_logic );

end SCHEMA;

architecture SCHEMATIC of SCHEMA is

   SIGNAL gnd : std_logic := '0';
   SIGNAL vcc : std_logic := '1';

   signal DPD_DUMMY : std_logic;
   signal     N_18 : std_logic;
   signal     N_19 : std_logic;
   signal     N_20 : std_logic;
   signal     N_21 : std_logic;
   signal     N_22 : std_logic;
   signal     N_23 : std_logic;
   signal     N_24 : std_logic;
   signal     N_25 : std_logic;
   signal     N_26 : std_logic;
   signal     N_27 : std_logic;
   signal     N_28 : std_logic;
   signal     N_29 : std_logic;
   signal DinaminisD_DUMMY : std_logic;
   signal      N_9 : std_logic;
   signal     N_10 : std_logic;
   signal     N_11 : std_logic;
   signal     N_12 : std_logic;
   signal     N_13 : std_logic;
   signal     N_14 : std_logic;
   signal     N_15 : std_logic;
   signal     N_16 : std_logic;
   signal     N_17 : std_logic;
   signal  C_DUMMY : std_logic;
   signal  D_DUMMY : std_logic;
   signal Statinis_DUMMY : std_logic;
   signal      N_6 : std_logic;
   signal      N_7 : std_logic;
   signal      N_8 : std_logic;
   signal      N_4 : std_logic;
   signal      N_5 : std_logic;

   component nd3
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   C : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component nd2
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component xor2
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component or2
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component inv
      Port (       A : In    std_logic;
                   Z : Out   std_logic );
   end component;

begin

   Statinis <= Statinis_DUMMY;
   D <= D_DUMMY;
   C <= C_DUMMY;
   DinaminisD <= DinaminisD_DUMMY;
   DPD <= DPD_DUMMY;

   I19 : nd3
      Port Map ( A=>DPD_DUMMY, B=>N_18, C=>rst, Z=>N_21 );
   I20 : nd3
      Port Map ( A=>N_24, B=>N_25, C=>rst, Z=>N_22 );
   I9 : nd3
      Port Map ( A=>N_16, B=>N_14, C=>rst, Z=>N_15 );
   I10 : nd3
      Port Map ( A=>N_15, B=>N_14, C=>N_17, Z=>N_9 );
   I11 : nd3
      Port Map ( A=>N_9, B=>N_13, C=>rst, Z=>N_17 );
   I12 : nd3
      Port Map ( A=>DinaminisD_DUMMY, B=>N_9, C=>rst, Z=>N_10 );
   I1 : nd3
      Port Map ( A=>Statinis_DUMMY, B=>N_6, C=>rst, Z=>N_7 );
   I21 : nd2
      Port Map ( A=>N_24, B=>N_19, Z=>N_20 );
   I22 : nd2
      Port Map ( A=>N_20, B=>N_19, Z=>N_18 );
   I23 : nd2
      Port Map ( A=>N_20, B=>N_21, Z=>DPD_DUMMY );
   I24 : nd2
      Port Map ( A=>N_26, B=>N_22, Z=>N_24 );
   I25 : nd2
      Port Map ( A=>N_26, B=>N_23, Z=>N_25 );
   I26 : nd2
      Port Map ( A=>N_29, B=>N_23, Z=>N_26 );
   I13 : nd2
      Port Map ( A=>N_17, B=>N_15, Z=>N_16 );
   I14 : nd2
      Port Map ( A=>N_15, B=>N_10, Z=>DinaminisD_DUMMY );
   I2 : nd2
      Port Map ( A=>D_DUMMY, B=>C_DUMMY, Z=>N_8 );
   I3 : nd2
      Port Map ( A=>N_8, B=>C_DUMMY, Z=>N_6 );
   I4 : nd2
      Port Map ( A=>N_8, B=>N_7, Z=>Statinis_DUMMY );
   I27 : xor2
      Port Map ( A=>x4, B=>N_27, Z=>N_29 );
   I15 : xor2
      Port Map ( A=>x4, B=>N_11, Z=>N_13 );
   I5 : xor2
      Port Map ( A=>x4, B=>N_4, Z=>D_DUMMY );
   I28 : or2
      Port Map ( A=>N_28, B=>x2, Z=>N_27 );
   I16 : or2
      Port Map ( A=>N_12, B=>x2, Z=>N_11 );
   I6 : or2
      Port Map ( A=>N_5, B=>x2, Z=>N_4 );
   I29 : inv
      Port Map ( A=>N_23, Z=>N_19 );
   I30 : inv
      Port Map ( A=>x1, Z=>N_23 );
   I31 : inv
      Port Map ( A=>x3, Z=>N_28 );
   I18 : inv
      Port Map ( A=>x1, Z=>N_14 );
   I17 : inv
      Port Map ( A=>x3, Z=>N_12 );
   I7 : inv
      Port Map ( A=>x3, Z=>N_5 );
   I8 : inv
      Port Map ( A=>x1, Z=>C_DUMMY );

end SCHEMATIC;
