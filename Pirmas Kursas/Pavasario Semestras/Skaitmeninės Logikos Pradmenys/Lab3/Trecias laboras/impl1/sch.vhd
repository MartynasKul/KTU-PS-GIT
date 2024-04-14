-- VHDL model created from schematic sch.sch -- May 12 03:19:20 2022

library IEEE;
use IEEE.std_logic_1164.all;
library xp2;
use xp2.components.all;

entity SCH is
      Port (     rst : In    std_logic;
                 clk : In    std_logic;
                  Q0 : Out   std_logic;
                  Q4 : Out   std_logic;
                  Q3 : Out   std_logic;
                  Q2 : Out   std_logic;
                  Q1 : Out   std_logic;
                  DL : In    std_logic;
                  DR : In    std_logic;
                  A0 : In    std_logic;
                  A1 : In    std_logic );

end SCH;

architecture SCHEMATIC of SCH is

   SIGNAL gnd : std_logic := '0';
   SIGNAL vcc : std_logic := '1';

   signal     N_16 : std_logic;
   signal     N_17 : std_logic;
   signal     N_18 : std_logic;
   signal     N_19 : std_logic;
   signal     N_20 : std_logic;
   signal     N_21 : std_logic;
   signal     N_22 : std_logic;
   signal     N_23 : std_logic;
   signal     N_24 : std_logic;
   signal     N_15 : std_logic;
   signal      N_1 : std_logic;
   signal      N_2 : std_logic;
   signal      N_3 : std_logic;
   signal      N_4 : std_logic;
   signal      N_5 : std_logic;
   signal      N_6 : std_logic;
   signal      N_7 : std_logic;
   signal      N_8 : std_logic;
   signal      N_9 : std_logic;
   signal     N_10 : std_logic;

   component vlo
      Port (       Z : Out   std_logic );
   end component;

   component vhi
      Port (       Z : Out   std_logic );
   end component;

   component inv
      Port (       A : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component fd1s3ax
      Port (      CK : In    std_logic;
                   D : In    std_logic;
                   Q : Out   std_logic );
   end component;

   component and2
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component mux41
      Port (      D0 : In    std_logic;
                  D1 : In    std_logic;
                  D2 : In    std_logic;
                  D3 : In    std_logic;
                 SD1 : In    std_logic;
                 SD2 : In    std_logic;
                   Z : Out   std_logic );
   end component;

begin

   I50 : vlo
      Port Map ( Z=>N_17 );
   I41 : vlo
      Port Map ( Z=>N_24 );
   I45 : vhi
      Port Map ( Z=>N_22 );
   I44 : vhi
      Port Map ( Z=>N_20 );
   I43 : vhi
      Port Map ( Z=>N_19 );
   I39 : inv
      Port Map ( A=>N_18, Z=>Q1 );
   I38 : inv
      Port Map ( A=>N_16, Z=>Q2 );
   I37 : inv
      Port Map ( A=>N_23, Z=>Q3 );
   I36 : inv
      Port Map ( A=>N_21, Z=>Q4 );
   I40 : inv
      Port Map ( A=>N_15, Z=>Q0 );
   I21 : fd1s3ax
      Port Map ( CK=>clk, D=>N_1, Q=>N_15 );
   I22 : fd1s3ax
      Port Map ( CK=>clk, D=>N_3, Q=>N_18 );
   I23 : fd1s3ax
      Port Map ( CK=>clk, D=>N_5, Q=>N_16 );
   I24 : fd1s3ax
      Port Map ( CK=>clk, D=>N_7, Q=>N_23 );
   I25 : fd1s3ax
      Port Map ( CK=>clk, D=>N_9, Q=>N_21 );
   I26 : and2
      Port Map ( A=>N_2, B=>rst, Z=>N_1 );
   I27 : and2
      Port Map ( A=>N_4, B=>rst, Z=>N_3 );
   I28 : and2
      Port Map ( A=>N_6, B=>rst, Z=>N_5 );
   I29 : and2
      Port Map ( A=>N_8, B=>rst, Z=>N_7 );
   I30 : and2
      Port Map ( A=>N_10, B=>rst, Z=>N_9 );
   I31 : mux41
      Port Map ( D0=>N_15, D1=>N_18, D2=>DL, D3=>N_17, SD1=>A0, SD2=>A1,
                 Z=>N_2 );
   I32 : mux41
      Port Map ( D0=>N_18, D1=>N_16, D2=>N_15, D3=>N_19, SD1=>A0,
                 SD2=>A1, Z=>N_4 );
   I33 : mux41
      Port Map ( D0=>N_16, D1=>N_23, D2=>N_18, D3=>N_24, SD1=>A0,
                 SD2=>A1, Z=>N_6 );
   I34 : mux41
      Port Map ( D0=>N_23, D1=>N_21, D2=>N_16, D3=>N_20, SD1=>A0,
                 SD2=>A1, Z=>N_8 );
   I35 : mux41
      Port Map ( D0=>N_21, D1=>DR, D2=>N_23, D3=>N_22, SD1=>A0, SD2=>A1,
                 Z=>N_10 );

end SCHEMATIC;
