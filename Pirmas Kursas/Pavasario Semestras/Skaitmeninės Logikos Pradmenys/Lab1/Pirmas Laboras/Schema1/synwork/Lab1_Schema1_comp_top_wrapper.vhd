--
-- Synopsys
-- Vhdl wrapper for top level design, written on Wed Mar  9 20:30:58 2022
--
library ieee;
use ieee.std_logic_1164.all;
library xp2;
use xp2.components.all;
library work;
use work.components.all;

entity wrapper_for_KLASIKA is
   port (
      X6 : in std_logic;
      X5 : in std_logic;
      X4 : in std_logic;
      X3 : in std_logic;
      X2 : in std_logic;
      X1 : in std_logic;
      isv : out std_logic;
      isv2 : out std_logic;
      isv3 : out std_logic
   );
end wrapper_for_KLASIKA;

architecture schematic of wrapper_for_KLASIKA is

component KLASIKA
 port (
   X6 : in std_logic;
   X5 : in std_logic;
   X4 : in std_logic;
   X3 : in std_logic;
   X2 : in std_logic;
   X1 : in std_logic;
   isv : out std_logic;
   isv2 : out std_logic;
   isv3 : out std_logic
 );
end component;

signal tmp_X6 : std_logic;
signal tmp_X5 : std_logic;
signal tmp_X4 : std_logic;
signal tmp_X3 : std_logic;
signal tmp_X2 : std_logic;
signal tmp_X1 : std_logic;
signal tmp_isv : std_logic;
signal tmp_isv2 : std_logic;
signal tmp_isv3 : std_logic;

begin

tmp_X6 <= X6;

tmp_X5 <= X5;

tmp_X4 <= X4;

tmp_X3 <= X3;

tmp_X2 <= X2;

tmp_X1 <= X1;

isv <= tmp_isv;

isv2 <= tmp_isv2;

isv3 <= tmp_isv3;



u1:   KLASIKA port map (
		X6 => tmp_X6,
		X5 => tmp_X5,
		X4 => tmp_X4,
		X3 => tmp_X3,
		X2 => tmp_X2,
		X1 => tmp_X1,
		isv => tmp_isv,
		isv2 => tmp_isv2,
		isv3 => tmp_isv3
       );
end schematic;
