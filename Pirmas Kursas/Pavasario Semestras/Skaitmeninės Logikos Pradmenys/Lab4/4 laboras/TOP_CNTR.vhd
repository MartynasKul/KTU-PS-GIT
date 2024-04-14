library ieee;
use ieee.std_logic_1164.all;
use ieee.numeric_std.all;

entity TOP_CNT is port (
	Clock : in std_logic; -- Sinchro signalas
	Reset : in std_logic; -- Reset signalas
	Enable : in std_logic; -- Aktyvavimo signalas
	CO : out std_logic -- Pernasa
	);
end TOP_CNT;
architecture struct of TOP_CNT is

signal C,RST_internal,C1,C2 : std_logic;
signal Q3_Q0 : std_logic_vector(3 downto 0);
signal Q8_Q4 : std_logic_vector(4 downto 0);

component CNT19
	Port ( Clk : In std_logic;
		Cmd : In std_logic;
		Cout : Out std_logic;
		Rst : In std_logic;
		Q : out std_logic_vector (4 downto 0) );
end component;
component CNT49
	Port ( Clk : In std_logic;
		Cmd : In std_logic;
		Cout : Out std_logic;
		Rst : In std_logic;
		Q : out std_logic_vector (4 downto 0) );
end component;

begin

CNT_1: CNT19 port map (Clk=>Clock,
	Rst=>RST_internal, Cmd=>Enable,
	Cout=>C1, Q=>Q3_Q0);
CNT_2: CNT49 port map (Clk=> C1,
	Rst=>RST_internal, Cmd=>Enable,
	Cout=>C2, Q=>Q8_Q4);

	process(Clock,Reset)
	begin
		if (Reset = '1') then
			RST_internal <= '1'; CO <= '0';
		elsif Clock'event and Clock = '1' then
			if ((Q8_Q4(4) = '1')
			and (Q8_Q4(0) = '1')
			and (Q3_Q0(0) = '1')) then
				RST_internal <= '1';
				CO <= '1';
		else
			RST_internal <= '0';
			CO <= '0';
			end if;
		end if;
	end process;
end struct;