--
-- Synopsys
-- Vhdl wrapper for top level design, written on Thu Apr 28 09:23:33 2022
--
library ieee;
use ieee.std_logic_1164.all;
library xp2;
use xp2.components.all;
library work;
use work.components.all;

entity wrapper_for_SCH is
   port (
      DR : in std_logic;
      A0 : in std_logic;
      A1 : in std_logic;
      rst : in std_logic;
      clk : in std_logic;
      D4 : in std_logic;
      Q4 : out std_logic;
      D3 : in std_logic;
      Q3 : out std_logic;
      D2 : in std_logic;
      Q2 : out std_logic;
      D1 : in std_logic;
      Q1 : out std_logic;
      D0 : in std_logic;
      DL : in std_logic;
      Q0 : out std_logic
   );
end wrapper_for_SCH;

architecture schematic of wrapper_for_SCH is

component SCH
 port (
   DR : in std_logic;
   A0 : in std_logic;
   A1 : in std_logic;
   rst : in std_logic;
   clk : in std_logic;
   D4 : in std_logic;
   Q4 : out std_logic;
   D3 : in std_logic;
   Q3 : out std_logic;
   D2 : in std_logic;
   Q2 : out std_logic;
   D1 : in std_logic;
   Q1 : out std_logic;
   D0 : in std_logic;
   DL : in std_logic;
   Q0 : out std_logic
 );
end component;

signal tmp_DR : std_logic;
signal tmp_A0 : std_logic;
signal tmp_A1 : std_logic;
signal tmp_rst : std_logic;
signal tmp_clk : std_logic;
signal tmp_D4 : std_logic;
signal tmp_Q4 : std_logic;
signal tmp_D3 : std_logic;
signal tmp_Q3 : std_logic;
signal tmp_D2 : std_logic;
signal tmp_Q2 : std_logic;
signal tmp_D1 : std_logic;
signal tmp_Q1 : std_logic;
signal tmp_D0 : std_logic;
signal tmp_DL : std_logic;
signal tmp_Q0 : std_logic;

begin

tmp_DR <= DR;

tmp_A0 <= A0;

tmp_A1 <= A1;

tmp_rst <= rst;

tmp_clk <= clk;

tmp_D4 <= D4;

Q4 <= tmp_Q4;

tmp_D3 <= D3;

Q3 <= tmp_Q3;

tmp_D2 <= D2;

Q2 <= tmp_Q2;

tmp_D1 <= D1;

Q1 <= tmp_Q1;

tmp_D0 <= D0;

tmp_DL <= DL;

Q0 <= tmp_Q0;



u1:   SCH port map (
		DR => tmp_DR,
		A0 => tmp_A0,
		A1 => tmp_A1,
		rst => tmp_rst,
		clk => tmp_clk,
		D4 => tmp_D4,
		Q4 => tmp_Q4,
		D3 => tmp_D3,
		Q3 => tmp_Q3,
		D2 => tmp_D2,
		Q2 => tmp_Q2,
		D1 => tmp_D1,
		Q1 => tmp_Q1,
		D0 => tmp_D0,
		DL => tmp_DL,
		Q0 => tmp_Q0
       );
end schematic;
