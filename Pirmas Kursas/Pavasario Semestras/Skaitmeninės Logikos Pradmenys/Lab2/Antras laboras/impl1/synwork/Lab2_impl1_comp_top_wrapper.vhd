--
-- Synopsys
-- Vhdl wrapper for top level design, written on Wed Mar 30 22:45:42 2022
--
library ieee;
use ieee.std_logic_1164.all;
library xp2;
use xp2.components.all;
library work;
use work.components.all;

entity wrapper_for_SCHEMA is
   port (
      x1 : in std_logic;
      x2 : in std_logic;
      x3 : in std_logic;
      x4 : in std_logic;
      rst : in std_logic;
      Statinis : out std_logic;
      D : out std_logic;
      C : out std_logic;
      DinaminisD : out std_logic;
      DPD : out std_logic
   );
end wrapper_for_SCHEMA;

architecture schematic of wrapper_for_SCHEMA is

component SCHEMA
 port (
   x1 : in std_logic;
   x2 : in std_logic;
   x3 : in std_logic;
   x4 : in std_logic;
   rst : in std_logic;
   Statinis : out std_logic;
   D : out std_logic;
   C : out std_logic;
   DinaminisD : out std_logic;
   DPD : out std_logic
 );
end component;

signal tmp_x1 : std_logic;
signal tmp_x2 : std_logic;
signal tmp_x3 : std_logic;
signal tmp_x4 : std_logic;
signal tmp_rst : std_logic;
signal tmp_Statinis : std_logic;
signal tmp_D : std_logic;
signal tmp_C : std_logic;
signal tmp_DinaminisD : std_logic;
signal tmp_DPD : std_logic;

begin

tmp_x1 <= x1;

tmp_x2 <= x2;

tmp_x3 <= x3;

tmp_x4 <= x4;

tmp_rst <= rst;

Statinis <= tmp_Statinis;

D <= tmp_D;

C <= tmp_C;

DinaminisD <= tmp_DinaminisD;

DPD <= tmp_DPD;



u1:   SCHEMA port map (
		x1 => tmp_x1,
		x2 => tmp_x2,
		x3 => tmp_x3,
		x4 => tmp_x4,
		rst => tmp_rst,
		Statinis => tmp_Statinis,
		D => tmp_D,
		C => tmp_C,
		DinaminisD => tmp_DinaminisD,
		DPD => tmp_DPD
       );
end schematic;
