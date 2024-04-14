--
-- Synopsys
-- Vhdl wrapper for top level design, written on Thu Apr 28 10:19:26 2022
--
library ieee;
use ieee.std_logic_1164.all;
library xp2;
use xp2.components.all;
library work;
use work.components.all;

entity wrapper_for_SCH is
   port (
      rst : in std_logic;
      clk : in std_logic;
      Q0 : out std_logic;
      Q4 : out std_logic;
      Q3 : out std_logic;
      Q2 : out std_logic;
      Q1 : out std_logic;
      DL : in std_logic;
      DR : in std_logic;
      A0 : in std_logic;
      A1 : in std_logic
   );
end wrapper_for_SCH;

architecture schematic of wrapper_for_SCH is

component SCH
 port (
   rst : in std_logic;
   clk : in std_logic;
   Q0 : out std_logic;
   Q4 : out std_logic;
   Q3 : out std_logic;
   Q2 : out std_logic;
   Q1 : out std_logic;
   DL : in std_logic;
   DR : in std_logic;
   A0 : in std_logic;
   A1 : in std_logic
 );
end component;

signal tmp_rst : std_logic;
signal tmp_clk : std_logic;
signal tmp_Q0 : std_logic;
signal tmp_Q4 : std_logic;
signal tmp_Q3 : std_logic;
signal tmp_Q2 : std_logic;
signal tmp_Q1 : std_logic;
signal tmp_DL : std_logic;
signal tmp_DR : std_logic;
signal tmp_A0 : std_logic;
signal tmp_A1 : std_logic;

begin

tmp_rst <= rst;

tmp_clk <= clk;

Q0 <= tmp_Q0;

Q4 <= tmp_Q4;

Q3 <= tmp_Q3;

Q2 <= tmp_Q2;

Q1 <= tmp_Q1;

tmp_DL <= DL;

tmp_DR <= DR;

tmp_A0 <= A0;

tmp_A1 <= A1;



u1:   SCH port map (
		rst => tmp_rst,
		clk => tmp_clk,
		Q0 => tmp_Q0,
		Q4 => tmp_Q4,
		Q3 => tmp_Q3,
		Q2 => tmp_Q2,
		Q1 => tmp_Q1,
		DL => tmp_DL,
		DR => tmp_DR,
		A0 => tmp_A0,
		A1 => tmp_A1
       );
end schematic;
