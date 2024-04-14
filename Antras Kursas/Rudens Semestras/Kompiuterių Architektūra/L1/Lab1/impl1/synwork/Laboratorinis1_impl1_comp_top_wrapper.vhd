--
-- Synopsys
-- Vhdl wrapper for top level design, written on Fri Sep 30 09:55:56 2022
--
library ieee;
use ieee.std_logic_1164.all;
use ieee.numeric_std.all;

entity wrapper_for_TOP is
   port (
      CLK : in std_logic;
      RST : in std_logic;
      Din : in std_logic_vector(15 downto 0);
      MAIN_Dout : out std_logic_vector(15 downto 0);
      S_Done : out std_logic
   );
end wrapper_for_TOP;

architecture struct of wrapper_for_TOP is

component TOP
 port (
   CLK : in std_logic;
   RST : in std_logic;
   Din : in std_logic_vector (15 downto 0);
   MAIN_Dout : out std_logic_vector (15 downto 0);
   S_Done : out std_logic
 );
end component;

signal tmp_CLK : std_logic;
signal tmp_RST : std_logic;
signal tmp_Din : std_logic_vector (15 downto 0);
signal tmp_MAIN_Dout : std_logic_vector (15 downto 0);
signal tmp_S_Done : std_logic;

begin

tmp_CLK <= CLK;

tmp_RST <= RST;

tmp_Din <= Din;

MAIN_Dout <= tmp_MAIN_Dout;

S_Done <= tmp_S_Done;



u1:   TOP port map (
		CLK => tmp_CLK,
		RST => tmp_RST,
		Din => tmp_Din,
		MAIN_Dout => tmp_MAIN_Dout,
		S_Done => tmp_S_Done
       );
end struct;
