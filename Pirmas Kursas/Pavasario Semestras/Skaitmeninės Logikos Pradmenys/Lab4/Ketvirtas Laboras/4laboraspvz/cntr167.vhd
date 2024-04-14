--KTU 2019
--Informatikos fakultetas
--Kompiuteriu katedra
--Skaitmenine Logika [P175B100]
--Kazimieras Bagdonas

library ieee;
use ieee.std_logic_1164.all;
use ieee.numeric_std.all;

entity CNT167 is port (  
		
		CLK 		: in std_logic; --Sinchro signalas
		RST 		: in std_logic; -- Reset signalas
		CNT_CMD  	: in std_logic; -- Komanda	
		CNT_C	 	: out std_logic; --Pernasa  
		CNT_O	 	: out std_logic_vector(7 downto  0)
		);
end CNT167;


architecture rtl of CNT167 is
	signal CNT_A: unsigned (7 downto  0);
begin	
	process(CLK, RST, CNT_CMD)
	begin
		if RST = '1' then
			CNT_A <= "00000000";
			CNT_C <= '0';	
		elsif CLK'event and CLK = '1' and CNT_CMD = '1' then 
			CNT_A <= CNT_A + 1;
			if CNT_A = 165 then
				CNT_C <= '1';  
			elsif  CNT_A = 166 then	  
				CNT_C <= '1';
				CNT_A <= "00000000";
			end if;
		end if;			
	end process;
CNT_O <= std_logic_vector(CNT_A);	
end rtl;		
