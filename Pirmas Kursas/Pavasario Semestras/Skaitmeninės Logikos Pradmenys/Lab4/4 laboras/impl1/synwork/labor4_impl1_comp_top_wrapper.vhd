--
-- Synopsys
-- Vhdl wrapper for top level design, written on Thu May 19 10:23:54 2022
--
library ieee;
use ieee.std_logic_1164.all;
use ieee.numeric_std.all;

entity wrapper_for_TOP_CNT is
   port (
      CLK_I : in std_logic;
      RST_I : in std_logic;
      ENBL_I : in std_logic;
      CNT_CO : out std_logic
   );
end wrapper_for_TOP_CNT;

architecture struct of wrapper_for_TOP_CNT is

component TOP_CNT
 port (
   CLK_I : in std_logic;
   RST_I : in std_logic;
   ENBL_I : in std_logic;
   CNT_CO : out std_logic
 );
end component;

signal tmp_CLK_I : std_logic;
signal tmp_RST_I : std_logic;
signal tmp_ENBL_I : std_logic;
signal tmp_CNT_CO : std_logic;

begin

tmp_CLK_I <= CLK_I;

tmp_RST_I <= RST_I;

tmp_ENBL_I <= ENBL_I;

CNT_CO <= tmp_CNT_CO;



u1:   TOP_CNT port map (
		CLK_I => tmp_CLK_I,
		RST_I => tmp_RST_I,
		ENBL_I => tmp_ENBL_I,
		CNT_CO => tmp_CNT_CO
       );
end struct;
