-- VHDL model created from schematic Klasika.sch -- Mar 09 20:29:42 2022

library IEEE;
use IEEE.std_logic_1164.all;
library xp2;
use xp2.components.all;

entity KLASIKA is
      Port (      X6 : In    std_logic;
                  X5 : In    std_logic;
                  X4 : In    std_logic;
                  X3 : In    std_logic;
                  X2 : In    std_logic;
                  X1 : In    std_logic;
                 isv : Out   std_logic;
                isv2 : Out   std_logic;
                isv3 : Out   std_logic );

end KLASIKA;

architecture SCHEMATIC of KLASIKA is

   SIGNAL gnd : std_logic := '0';
   SIGNAL vcc : std_logic := '1';

   signal     N_58 : std_logic;
   signal     N_59 : std_logic;
   signal     N_61 : std_logic;
   signal     N_62 : std_logic;
   signal     N_63 : std_logic;
   signal     N_64 : std_logic;
   signal     N_65 : std_logic;
   signal     N_66 : std_logic;
   signal     N_67 : std_logic;
   signal     N_68 : std_logic;
   signal     N_69 : std_logic;
   signal     N_70 : std_logic;
   signal     N_71 : std_logic;
   signal     N_72 : std_logic;
   signal     N_73 : std_logic;
   signal     N_74 : std_logic;
   signal     N_75 : std_logic;
   signal     N_76 : std_logic;
   signal     N_77 : std_logic;
   signal     N_78 : std_logic;
   signal     N_79 : std_logic;
   signal     N_80 : std_logic;
   signal     N_81 : std_logic;
   signal     N_83 : std_logic;
   signal     N_84 : std_logic;
   signal     N_85 : std_logic;
   signal     N_86 : std_logic;
   signal     N_87 : std_logic;
   signal     N_88 : std_logic;
   signal     N_89 : std_logic;
   signal     N_90 : std_logic;
   signal     N_91 : std_logic;
   signal     N_92 : std_logic;
   signal     N_93 : std_logic;
   signal     N_94 : std_logic;
   signal     N_95 : std_logic;
   signal     N_96 : std_logic;
   signal     N_97 : std_logic;
   signal     N_98 : std_logic;
   signal     N_99 : std_logic;
   signal    N_100 : std_logic;
   signal    N_101 : std_logic;
   signal    N_102 : std_logic;
   signal    N_103 : std_logic;
   signal    N_104 : std_logic;
   signal    N_105 : std_logic;
   signal    N_106 : std_logic;
   signal    N_107 : std_logic;
   signal    N_108 : std_logic;
   signal    N_109 : std_logic;
   signal    N_110 : std_logic;
   signal    N_111 : std_logic;
   signal    N_112 : std_logic;
   signal    N_113 : std_logic;
   signal    N_114 : std_logic;
   signal    N_115 : std_logic;
   signal    N_116 : std_logic;
   signal    N_117 : std_logic;
   signal    N_118 : std_logic;
   signal    N_119 : std_logic;
   signal    N_120 : std_logic;
   signal     N_43 : std_logic;
   signal     N_48 : std_logic;
   signal     N_50 : std_logic;
   signal     N_54 : std_logic;
   signal     N_56 : std_logic;
   signal     N_57 : std_logic;
   signal     N_31 : std_logic;
   signal     N_32 : std_logic;
   signal     N_15 : std_logic;
   signal      N_7 : std_logic;
   signal      N_8 : std_logic;
   signal      N_9 : std_logic;

   component or5
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   C : In    std_logic;
                   D : In    std_logic;
                   E : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component and4
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   C : In    std_logic;
                   D : In    std_logic;
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

   component nd2
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component nd4
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   C : In    std_logic;
                   D : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component nd3
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   C : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component or4
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   C : In    std_logic;
                   D : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component or2
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component or3
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   C : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component and3
      Port (       A : In    std_logic;
                   B : In    std_logic;
                   C : In    std_logic;
                   Z : Out   std_logic );
   end component;

   component inv
      Port (       A : In    std_logic;
                   Z : Out   std_logic );
   end component;

begin

   I68 : or5
      Port Map ( A=>N_74, B=>N_75, C=>N_78, D=>N_77, E=>N_76, Z=>N_65 );
   I69 : and4
      Port Map ( A=>N_58, B=>X3, C=>X5, D=>X6, Z=>N_81 );
   I70 : and4
      Port Map ( A=>X2, B=>N_59, C=>X5, D=>X6, Z=>N_68 );
   I71 : and4
      Port Map ( A=>X2, B=>X3, C=>N_61, D=>X6, Z=>N_69 );
   I72 : and4
      Port Map ( A=>N_58, B=>X3, C=>N_61, D=>N_62, Z=>N_67 );
   I73 : mux41
      Port Map ( D0=>N_63, D1=>N_64, D2=>N_65, D3=>N_66, SD1=>X1,
                 SD2=>X4, Z=>isv3 );
   I42 : nd2
      Port Map ( A=>N_92, B=>N_91, Z=>N_85 );
   I43 : nd4
      Port Map ( A=>N_84, B=>N_88, C=>N_87, D=>N_83, Z=>isv2 );
   I49 : nd3
      Port Map ( A=>N_85, B=>X3, C=>N_50, Z=>N_83 );
   I46 : nd3
      Port Map ( A=>N_101, B=>N_100, C=>N_99, Z=>N_90 );
   I52 : nd3
      Port Map ( A=>N_90, B=>N_50, C=>N_48, Z=>N_84 );
   I45 : nd3
      Port Map ( A=>N_98, B=>N_97, C=>N_96, Z=>N_89 );
   I51 : nd3
      Port Map ( A=>N_89, B=>N_43, C=>X2, Z=>N_88 );
   I44 : nd3
      Port Map ( A=>N_95, B=>N_94, C=>N_93, Z=>N_86 );
   I50 : nd3
      Port Map ( A=>N_86, B=>X5, C=>X6, Z=>N_87 );
   I59 : nd3
      Port Map ( A=>X1, B=>N_57, C=>X3, Z=>N_93 );
   I60 : nd3
      Port Map ( A=>X1, B=>X3, C=>N_56, Z=>N_94 );
   I61 : nd3
      Port Map ( A=>X2, B=>N_54, C=>N_56, Z=>N_95 );
   I56 : nd3
      Port Map ( A=>N_54, B=>X4, C=>X5, Z=>N_96 );
   I57 : nd3
      Port Map ( A=>X3, B=>X4, C=>X6, Z=>N_97 );
   I58 : nd3
      Port Map ( A=>X3, B=>N_50, C=>X6, Z=>N_98 );
   I53 : nd3
      Port Map ( A=>X1, B=>X3, C=>X4, Z=>N_99 );
   I54 : nd3
      Port Map ( A=>X2, B=>N_54, C=>X4, Z=>N_100 );
   I55 : nd3
      Port Map ( A=>N_57, B=>X3, C=>N_56, Z=>N_101 );
   I47 : nd3
      Port Map ( A=>N_43, B=>X4, C=>X6, Z=>N_91 );
   I48 : nd3
      Port Map ( A=>X1, B=>N_57, C=>N_56, Z=>N_92 );
   I74 : or4
      Port Map ( A=>N_70, B=>N_73, C=>N_72, D=>N_71, Z=>N_64 );
   I23 : or4
      Port Map ( A=>N_116, B=>N_111, C=>N_110, D=>N_102, Z=>isv );
   I24 : or2
      Port Map ( A=>N_105, B=>N_104, Z=>N_103 );
   I75 : or3
      Port Map ( A=>N_81, B=>N_80, C=>N_79, Z=>N_66 );
   I76 : or3
      Port Map ( A=>N_67, B=>N_69, C=>N_68, Z=>N_63 );
   I25 : or3
      Port Map ( A=>N_109, B=>N_108, C=>N_107, Z=>N_106 );
   I5 : or3
      Port Map ( A=>N_120, B=>N_119, C=>N_118, Z=>N_117 );
   I26 : or3
      Port Map ( A=>N_115, B=>N_114, C=>N_113, Z=>N_112 );
   I77 : and3
      Port Map ( A=>X3, B=>N_61, C=>N_62, Z=>N_80 );
   I78 : and3
      Port Map ( A=>X2, B=>N_61, C=>N_62, Z=>N_79 );
   I79 : and3
      Port Map ( A=>X3, B=>N_61, C=>X6, Z=>N_76 );
   I80 : and3
      Port Map ( A=>X2, B=>N_59, C=>N_62, Z=>N_74 );
   I81 : and3
      Port Map ( A=>X2, B=>N_59, C=>X5, Z=>N_75 );
   I82 : and3
      Port Map ( A=>X2, B=>X5, C=>X6, Z=>N_78 );
   I83 : and3
      Port Map ( A=>X2, B=>X3, C=>X6, Z=>N_77 );
   I84 : and3
      Port Map ( A=>X2, B=>X5, C=>X6, Z=>N_71 );
   I85 : and3
      Port Map ( A=>X3, B=>X5, C=>X6, Z=>N_72 );
   I86 : and3
      Port Map ( A=>N_58, B=>X3, C=>X6, Z=>N_73 );
   I87 : and3
      Port Map ( A=>N_58, B=>X3, C=>N_61, Z=>N_70 );
   I41 : and3
      Port Map ( A=>X1, B=>X3, C=>X4, Z=>N_118 );
   I40 : and3
      Port Map ( A=>X2, B=>N_7, C=>X4, Z=>N_119 );
   I39 : and3
      Port Map ( A=>N_31, B=>X3, C=>N_8, Z=>N_120 );
   I37 : and3
      Port Map ( A=>N_7, B=>X4, C=>X5, Z=>N_113 );
   I36 : and3
      Port Map ( A=>X3, B=>X4, C=>X6, Z=>N_114 );
   I35 : and3
      Port Map ( A=>X3, B=>N_9, C=>X6, Z=>N_115 );
   I34 : and3
      Port Map ( A=>X1, B=>N_31, C=>N_8, Z=>N_105 );
   I33 : and3
      Port Map ( A=>N_32, B=>X4, C=>X6, Z=>N_104 );
   I29 : and3
      Port Map ( A=>X2, B=>N_7, C=>N_8, Z=>N_109 );
   I28 : and3
      Port Map ( A=>X1, B=>X3, C=>N_8, Z=>N_108 );
   I27 : and3
      Port Map ( A=>X1, B=>N_31, C=>X3, Z=>N_107 );
   I30 : and3
      Port Map ( A=>N_112, B=>N_32, C=>X2, Z=>N_111 );
   I31 : and3
      Port Map ( A=>N_103, B=>X3, C=>N_9, Z=>N_102 );
   I32 : and3
      Port Map ( A=>N_106, B=>X5, C=>X6, Z=>N_110 );
   I38 : and3
      Port Map ( A=>N_117, B=>N_9, C=>N_15, Z=>N_116 );
   I88 : inv
      Port Map ( A=>X6, Z=>N_62 );
   I89 : inv
      Port Map ( A=>X5, Z=>N_61 );
   I90 : inv
      Port Map ( A=>X3, Z=>N_59 );
   I91 : inv
      Port Map ( A=>X2, Z=>N_58 );
   I62 : inv
      Port Map ( A=>X2, Z=>N_57 );
   I63 : inv
      Port Map ( A=>X3, Z=>N_54 );
   I64 : inv
      Port Map ( A=>X4, Z=>N_56 );
   I65 : inv
      Port Map ( A=>X5, Z=>N_50 );
   I66 : inv
      Port Map ( A=>X1, Z=>N_43 );
   I67 : inv
      Port Map ( A=>X6, Z=>N_48 );
   I17 : inv
      Port Map ( A=>X2, Z=>N_31 );
   I18 : inv
      Port Map ( A=>X3, Z=>N_7 );
   I19 : inv
      Port Map ( A=>X4, Z=>N_8 );
   I20 : inv
      Port Map ( A=>X5, Z=>N_9 );
   I21 : inv
      Port Map ( A=>X1, Z=>N_32 );
   I22 : inv
      Port Map ( A=>X6, Z=>N_15 );

end SCHEMATIC;
