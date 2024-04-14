
-- VHDL netlist produced by program ldbanno, Version Diamond (64-bit) 3.12.1.454

-- ldbanno -n VHDL -o Laboratorinis_impl1_mapvho.vho -w -neg -gui Laboratorinis_impl1_map.ncd 
-- Netlist created on Thu Apr 14 09:54:48 2022
-- Netlist written on Thu Apr 14 09:57:27 2022
-- Design is for device LFXP2-5E
-- Design is for package TQFP144
-- Design is for performance grade 6

-- entity ec2iobuf
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity ec2iobuf is
    port (I: in Std_logic; T: in Std_logic; PAD: out Std_logic);

    ATTRIBUTE Vital_Level0 OF ec2iobuf : ENTITY IS TRUE;

  end ec2iobuf;

  architecture Structure of ec2iobuf is
    component OBZPU
      port (I: in Std_logic; T: in Std_logic; O: out Std_logic);
    end component;
  begin
    INST5: OBZPU
      port map (I=>I, T=>T, O=>PAD);
  end Structure;

-- entity gnd
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity gnd is
    port (PWR0: out Std_logic);

    ATTRIBUTE Vital_Level0 OF gnd : ENTITY IS TRUE;

  end gnd;

  architecture Structure of gnd is
    component VLO
      port (Z: out Std_logic);
    end component;
  begin
    INST1: VLO
      port map (Z=>PWR0);
  end Structure;

-- entity Q0B
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity Q0B is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "Q0B";

      tipd_PADDO  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_PADDO_Q0S	 : VitalDelayType01Z := (0 ns, 0 ns, 0 ns , 0 ns, 0 ns, 0 ns));

    port (PADDO: in Std_logic; Q0S: out Std_logic);

    ATTRIBUTE Vital_Level0 OF Q0B : ENTITY IS TRUE;

  end Q0B;

  architecture Structure of Q0B is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDO_ipd 	: std_logic := 'X';
    signal Q0S_out 	: std_logic := 'X';

    signal GNDI: Std_logic;
    component ec2iobuf
      port (I: in Std_logic; T: in Std_logic; PAD: out Std_logic);
    end component;
    component gnd
      port (PWR0: out Std_logic);
    end component;
  begin
    Q0_pad: ec2iobuf
      port map (I=>PADDO_ipd, T=>GNDI, PAD=>Q0S_out);
    DRIVEGND: gnd
      port map (PWR0=>GNDI);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(PADDO_ipd, PADDO, tipd_PADDO);
    END BLOCK;

    VitalBehavior : PROCESS (PADDO_ipd, Q0S_out)
    VARIABLE Q0S_zd         	: std_logic := 'X';
    VARIABLE Q0S_GlitchData 	: VitalGlitchDataType;


    BEGIN

    IF (TimingChecksOn) THEN

    END IF;

    Q0S_zd 	:= Q0S_out;

    VitalPathDelay01Z (
      OutSignal => Q0S, OutSignalName => "Q0S", OutTemp => Q0S_zd,
      Paths      => (0 => (InputChangeTime => PADDO_ipd'last_event,
                           PathDelay => tpd_PADDO_Q0S,
                           PathCondition => TRUE)),
      GlitchData => Q0S_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity ec2iobuf0001
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity ec2iobuf0001 is
    port (Z: out Std_logic; PAD: in Std_logic);

    ATTRIBUTE Vital_Level0 OF ec2iobuf0001 : ENTITY IS TRUE;

  end ec2iobuf0001;

  architecture Structure of ec2iobuf0001 is
    component IBPU
      port (I: in Std_logic; O: out Std_logic);
    end component;
  begin
    INST1: IBPU
      port map (I=>PAD, O=>Z);
  end Structure;

-- entity A0B
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity A0B is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "A0B";

      tipd_A0S  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_A0S_PADDI	 : VitalDelayType01 := (0 ns, 0 ns);
      tperiod_A0S 	: VitalDelayType := 0 ns;
      tpw_A0S_posedge	: VitalDelayType := 0 ns;
      tpw_A0S_negedge	: VitalDelayType := 0 ns);

    port (PADDI: out Std_logic; A0S: in Std_logic);

    ATTRIBUTE Vital_Level0 OF A0B : ENTITY IS TRUE;

  end A0B;

  architecture Structure of A0B is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDI_out 	: std_logic := 'X';
    signal A0S_ipd 	: std_logic := 'X';

    component ec2iobuf0001
      port (Z: out Std_logic; PAD: in Std_logic);
    end component;
  begin
    A0_pad: ec2iobuf0001
      port map (Z=>PADDI_out, PAD=>A0S_ipd);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(A0S_ipd, A0S, tipd_A0S);
    END BLOCK;

    VitalBehavior : PROCESS (PADDI_out, A0S_ipd)
    VARIABLE PADDI_zd         	: std_logic := 'X';
    VARIABLE PADDI_GlitchData 	: VitalGlitchDataType;

    VARIABLE tviol_A0S_A0S          	: x01 := '0';
    VARIABLE periodcheckinfo_A0S	: VitalPeriodDataType;

    BEGIN

    IF (TimingChecksOn) THEN
      VitalPeriodPulseCheck (
        TestSignal => A0S_ipd,
        TestSignalName => "A0S",
        Period => tperiod_A0S,
        PulseWidthHigh => tpw_A0S_posedge,
        PulseWidthLow => tpw_A0S_negedge,
        PeriodData => periodcheckinfo_A0S,
        Violation => tviol_A0S_A0S,
        MsgOn => MsgOn, XOn => XOn,
        HeaderMsg => InstancePath,
        CheckEnabled => TRUE,
        MsgSeverity => warning);

    END IF;

    PADDI_zd 	:= PADDI_out;

    VitalPathDelay01 (
      OutSignal => PADDI, OutSignalName => "PADDI", OutTemp => PADDI_zd,
      Paths      => (0 => (InputChangeTime => A0S_ipd'last_event,
                           PathDelay => tpd_A0S_PADDI,
                           PathCondition => TRUE)),
      GlitchData => PADDI_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity DRB
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity DRB is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "DRB";

      tipd_DRS  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_DRS_PADDI	 : VitalDelayType01 := (0 ns, 0 ns);
      tperiod_DRS 	: VitalDelayType := 0 ns;
      tpw_DRS_posedge	: VitalDelayType := 0 ns;
      tpw_DRS_negedge	: VitalDelayType := 0 ns);

    port (PADDI: out Std_logic; DRS: in Std_logic);

    ATTRIBUTE Vital_Level0 OF DRB : ENTITY IS TRUE;

  end DRB;

  architecture Structure of DRB is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDI_out 	: std_logic := 'X';
    signal DRS_ipd 	: std_logic := 'X';

    component ec2iobuf0001
      port (Z: out Std_logic; PAD: in Std_logic);
    end component;
  begin
    DR_pad: ec2iobuf0001
      port map (Z=>PADDI_out, PAD=>DRS_ipd);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(DRS_ipd, DRS, tipd_DRS);
    END BLOCK;

    VitalBehavior : PROCESS (PADDI_out, DRS_ipd)
    VARIABLE PADDI_zd         	: std_logic := 'X';
    VARIABLE PADDI_GlitchData 	: VitalGlitchDataType;

    VARIABLE tviol_DRS_DRS          	: x01 := '0';
    VARIABLE periodcheckinfo_DRS	: VitalPeriodDataType;

    BEGIN

    IF (TimingChecksOn) THEN
      VitalPeriodPulseCheck (
        TestSignal => DRS_ipd,
        TestSignalName => "DRS",
        Period => tperiod_DRS,
        PulseWidthHigh => tpw_DRS_posedge,
        PulseWidthLow => tpw_DRS_negedge,
        PeriodData => periodcheckinfo_DRS,
        Violation => tviol_DRS_DRS,
        MsgOn => MsgOn, XOn => XOn,
        HeaderMsg => InstancePath,
        CheckEnabled => TRUE,
        MsgSeverity => warning);

    END IF;

    PADDI_zd 	:= PADDI_out;

    VitalPathDelay01 (
      OutSignal => PADDI, OutSignalName => "PADDI", OutTemp => PADDI_zd,
      Paths      => (0 => (InputChangeTime => DRS_ipd'last_event,
                           PathDelay => tpd_DRS_PADDI,
                           PathCondition => TRUE)),
      GlitchData => PADDI_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity DLB
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity DLB is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "DLB";

      tipd_DLS  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_DLS_PADDI	 : VitalDelayType01 := (0 ns, 0 ns);
      tperiod_DLS 	: VitalDelayType := 0 ns;
      tpw_DLS_posedge	: VitalDelayType := 0 ns;
      tpw_DLS_negedge	: VitalDelayType := 0 ns);

    port (PADDI: out Std_logic; DLS: in Std_logic);

    ATTRIBUTE Vital_Level0 OF DLB : ENTITY IS TRUE;

  end DLB;

  architecture Structure of DLB is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDI_out 	: std_logic := 'X';
    signal DLS_ipd 	: std_logic := 'X';

    component ec2iobuf0001
      port (Z: out Std_logic; PAD: in Std_logic);
    end component;
  begin
    DL_pad: ec2iobuf0001
      port map (Z=>PADDI_out, PAD=>DLS_ipd);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(DLS_ipd, DLS, tipd_DLS);
    END BLOCK;

    VitalBehavior : PROCESS (PADDI_out, DLS_ipd)
    VARIABLE PADDI_zd         	: std_logic := 'X';
    VARIABLE PADDI_GlitchData 	: VitalGlitchDataType;

    VARIABLE tviol_DLS_DLS          	: x01 := '0';
    VARIABLE periodcheckinfo_DLS	: VitalPeriodDataType;

    BEGIN

    IF (TimingChecksOn) THEN
      VitalPeriodPulseCheck (
        TestSignal => DLS_ipd,
        TestSignalName => "DLS",
        Period => tperiod_DLS,
        PulseWidthHigh => tpw_DLS_posedge,
        PulseWidthLow => tpw_DLS_negedge,
        PeriodData => periodcheckinfo_DLS,
        Violation => tviol_DLS_DLS,
        MsgOn => MsgOn, XOn => XOn,
        HeaderMsg => InstancePath,
        CheckEnabled => TRUE,
        MsgSeverity => warning);

    END IF;

    PADDI_zd 	:= PADDI_out;

    VitalPathDelay01 (
      OutSignal => PADDI, OutSignalName => "PADDI", OutTemp => PADDI_zd,
      Paths      => (0 => (InputChangeTime => DLS_ipd'last_event,
                           PathDelay => tpd_DLS_PADDI,
                           PathCondition => TRUE)),
      GlitchData => PADDI_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity Q1B
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity Q1B is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "Q1B";

      tipd_PADDO  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_PADDO_Q1S	 : VitalDelayType01Z := (0 ns, 0 ns, 0 ns , 0 ns, 0 ns, 0 ns));

    port (PADDO: in Std_logic; Q1S: out Std_logic);

    ATTRIBUTE Vital_Level0 OF Q1B : ENTITY IS TRUE;

  end Q1B;

  architecture Structure of Q1B is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDO_ipd 	: std_logic := 'X';
    signal Q1S_out 	: std_logic := 'X';

    signal GNDI: Std_logic;
    component ec2iobuf
      port (I: in Std_logic; T: in Std_logic; PAD: out Std_logic);
    end component;
    component gnd
      port (PWR0: out Std_logic);
    end component;
  begin
    Q1_pad: ec2iobuf
      port map (I=>PADDO_ipd, T=>GNDI, PAD=>Q1S_out);
    DRIVEGND: gnd
      port map (PWR0=>GNDI);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(PADDO_ipd, PADDO, tipd_PADDO);
    END BLOCK;

    VitalBehavior : PROCESS (PADDO_ipd, Q1S_out)
    VARIABLE Q1S_zd         	: std_logic := 'X';
    VARIABLE Q1S_GlitchData 	: VitalGlitchDataType;


    BEGIN

    IF (TimingChecksOn) THEN

    END IF;

    Q1S_zd 	:= Q1S_out;

    VitalPathDelay01Z (
      OutSignal => Q1S, OutSignalName => "Q1S", OutTemp => Q1S_zd,
      Paths      => (0 => (InputChangeTime => PADDO_ipd'last_event,
                           PathDelay => tpd_PADDO_Q1S,
                           PathCondition => TRUE)),
      GlitchData => Q1S_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity Q2B
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity Q2B is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "Q2B";

      tipd_PADDO  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_PADDO_Q2S	 : VitalDelayType01Z := (0 ns, 0 ns, 0 ns , 0 ns, 0 ns, 0 ns));

    port (PADDO: in Std_logic; Q2S: out Std_logic);

    ATTRIBUTE Vital_Level0 OF Q2B : ENTITY IS TRUE;

  end Q2B;

  architecture Structure of Q2B is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDO_ipd 	: std_logic := 'X';
    signal Q2S_out 	: std_logic := 'X';

    signal GNDI: Std_logic;
    component ec2iobuf
      port (I: in Std_logic; T: in Std_logic; PAD: out Std_logic);
    end component;
    component gnd
      port (PWR0: out Std_logic);
    end component;
  begin
    Q2_pad: ec2iobuf
      port map (I=>PADDO_ipd, T=>GNDI, PAD=>Q2S_out);
    DRIVEGND: gnd
      port map (PWR0=>GNDI);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(PADDO_ipd, PADDO, tipd_PADDO);
    END BLOCK;

    VitalBehavior : PROCESS (PADDO_ipd, Q2S_out)
    VARIABLE Q2S_zd         	: std_logic := 'X';
    VARIABLE Q2S_GlitchData 	: VitalGlitchDataType;


    BEGIN

    IF (TimingChecksOn) THEN

    END IF;

    Q2S_zd 	:= Q2S_out;

    VitalPathDelay01Z (
      OutSignal => Q2S, OutSignalName => "Q2S", OutTemp => Q2S_zd,
      Paths      => (0 => (InputChangeTime => PADDO_ipd'last_event,
                           PathDelay => tpd_PADDO_Q2S,
                           PathCondition => TRUE)),
      GlitchData => Q2S_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity Q3B
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity Q3B is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "Q3B";

      tipd_PADDO  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_PADDO_Q3S	 : VitalDelayType01Z := (0 ns, 0 ns, 0 ns , 0 ns, 0 ns, 0 ns));

    port (PADDO: in Std_logic; Q3S: out Std_logic);

    ATTRIBUTE Vital_Level0 OF Q3B : ENTITY IS TRUE;

  end Q3B;

  architecture Structure of Q3B is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDO_ipd 	: std_logic := 'X';
    signal Q3S_out 	: std_logic := 'X';

    signal GNDI: Std_logic;
    component ec2iobuf
      port (I: in Std_logic; T: in Std_logic; PAD: out Std_logic);
    end component;
    component gnd
      port (PWR0: out Std_logic);
    end component;
  begin
    Q3_pad: ec2iobuf
      port map (I=>PADDO_ipd, T=>GNDI, PAD=>Q3S_out);
    DRIVEGND: gnd
      port map (PWR0=>GNDI);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(PADDO_ipd, PADDO, tipd_PADDO);
    END BLOCK;

    VitalBehavior : PROCESS (PADDO_ipd, Q3S_out)
    VARIABLE Q3S_zd         	: std_logic := 'X';
    VARIABLE Q3S_GlitchData 	: VitalGlitchDataType;


    BEGIN

    IF (TimingChecksOn) THEN

    END IF;

    Q3S_zd 	:= Q3S_out;

    VitalPathDelay01Z (
      OutSignal => Q3S, OutSignalName => "Q3S", OutTemp => Q3S_zd,
      Paths      => (0 => (InputChangeTime => PADDO_ipd'last_event,
                           PathDelay => tpd_PADDO_Q3S,
                           PathCondition => TRUE)),
      GlitchData => Q3S_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity Q4B
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity Q4B is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "Q4B";

      tipd_PADDO  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_PADDO_Q4S	 : VitalDelayType01Z := (0 ns, 0 ns, 0 ns , 0 ns, 0 ns, 0 ns));

    port (PADDO: in Std_logic; Q4S: out Std_logic);

    ATTRIBUTE Vital_Level0 OF Q4B : ENTITY IS TRUE;

  end Q4B;

  architecture Structure of Q4B is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDO_ipd 	: std_logic := 'X';
    signal Q4S_out 	: std_logic := 'X';

    signal GNDI: Std_logic;
    component ec2iobuf
      port (I: in Std_logic; T: in Std_logic; PAD: out Std_logic);
    end component;
    component gnd
      port (PWR0: out Std_logic);
    end component;
  begin
    Q4_pad: ec2iobuf
      port map (I=>PADDO_ipd, T=>GNDI, PAD=>Q4S_out);
    DRIVEGND: gnd
      port map (PWR0=>GNDI);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(PADDO_ipd, PADDO, tipd_PADDO);
    END BLOCK;

    VitalBehavior : PROCESS (PADDO_ipd, Q4S_out)
    VARIABLE Q4S_zd         	: std_logic := 'X';
    VARIABLE Q4S_GlitchData 	: VitalGlitchDataType;


    BEGIN

    IF (TimingChecksOn) THEN

    END IF;

    Q4S_zd 	:= Q4S_out;

    VitalPathDelay01Z (
      OutSignal => Q4S, OutSignalName => "Q4S", OutTemp => Q4S_zd,
      Paths      => (0 => (InputChangeTime => PADDO_ipd'last_event,
                           PathDelay => tpd_PADDO_Q4S,
                           PathCondition => TRUE)),
      GlitchData => Q4S_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity clkB
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity clkB is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "clkB";

      tipd_clkS  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_clkS_PADDI	 : VitalDelayType01 := (0 ns, 0 ns);
      tperiod_clkS 	: VitalDelayType := 0 ns;
      tpw_clkS_posedge	: VitalDelayType := 0 ns;
      tpw_clkS_negedge	: VitalDelayType := 0 ns);

    port (PADDI: out Std_logic; clkS: in Std_logic);

    ATTRIBUTE Vital_Level0 OF clkB : ENTITY IS TRUE;

  end clkB;

  architecture Structure of clkB is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDI_out 	: std_logic := 'X';
    signal clkS_ipd 	: std_logic := 'X';

    component ec2iobuf0001
      port (Z: out Std_logic; PAD: in Std_logic);
    end component;
  begin
    clk_pad: ec2iobuf0001
      port map (Z=>PADDI_out, PAD=>clkS_ipd);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(clkS_ipd, clkS, tipd_clkS);
    END BLOCK;

    VitalBehavior : PROCESS (PADDI_out, clkS_ipd)
    VARIABLE PADDI_zd         	: std_logic := 'X';
    VARIABLE PADDI_GlitchData 	: VitalGlitchDataType;

    VARIABLE tviol_clkS_clkS          	: x01 := '0';
    VARIABLE periodcheckinfo_clkS	: VitalPeriodDataType;

    BEGIN

    IF (TimingChecksOn) THEN
      VitalPeriodPulseCheck (
        TestSignal => clkS_ipd,
        TestSignalName => "clkS",
        Period => tperiod_clkS,
        PulseWidthHigh => tpw_clkS_posedge,
        PulseWidthLow => tpw_clkS_negedge,
        PeriodData => periodcheckinfo_clkS,
        Violation => tviol_clkS_clkS,
        MsgOn => MsgOn, XOn => XOn,
        HeaderMsg => InstancePath,
        CheckEnabled => TRUE,
        MsgSeverity => warning);

    END IF;

    PADDI_zd 	:= PADDI_out;

    VitalPathDelay01 (
      OutSignal => PADDI, OutSignalName => "PADDI", OutTemp => PADDI_zd,
      Paths      => (0 => (InputChangeTime => clkS_ipd'last_event,
                           PathDelay => tpd_clkS_PADDI,
                           PathCondition => TRUE)),
      GlitchData => PADDI_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity rstB
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity rstB is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "rstB";

      tipd_rstS  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_rstS_PADDI	 : VitalDelayType01 := (0 ns, 0 ns);
      tperiod_rstS 	: VitalDelayType := 0 ns;
      tpw_rstS_posedge	: VitalDelayType := 0 ns;
      tpw_rstS_negedge	: VitalDelayType := 0 ns);

    port (PADDI: out Std_logic; rstS: in Std_logic);

    ATTRIBUTE Vital_Level0 OF rstB : ENTITY IS TRUE;

  end rstB;

  architecture Structure of rstB is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDI_out 	: std_logic := 'X';
    signal rstS_ipd 	: std_logic := 'X';

    component ec2iobuf0001
      port (Z: out Std_logic; PAD: in Std_logic);
    end component;
  begin
    rst_pad: ec2iobuf0001
      port map (Z=>PADDI_out, PAD=>rstS_ipd);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(rstS_ipd, rstS, tipd_rstS);
    END BLOCK;

    VitalBehavior : PROCESS (PADDI_out, rstS_ipd)
    VARIABLE PADDI_zd         	: std_logic := 'X';
    VARIABLE PADDI_GlitchData 	: VitalGlitchDataType;

    VARIABLE tviol_rstS_rstS          	: x01 := '0';
    VARIABLE periodcheckinfo_rstS	: VitalPeriodDataType;

    BEGIN

    IF (TimingChecksOn) THEN
      VitalPeriodPulseCheck (
        TestSignal => rstS_ipd,
        TestSignalName => "rstS",
        Period => tperiod_rstS,
        PulseWidthHigh => tpw_rstS_posedge,
        PulseWidthLow => tpw_rstS_negedge,
        PeriodData => periodcheckinfo_rstS,
        Violation => tviol_rstS_rstS,
        MsgOn => MsgOn, XOn => XOn,
        HeaderMsg => InstancePath,
        CheckEnabled => TRUE,
        MsgSeverity => warning);

    END IF;

    PADDI_zd 	:= PADDI_out;

    VitalPathDelay01 (
      OutSignal => PADDI, OutSignalName => "PADDI", OutTemp => PADDI_zd,
      Paths      => (0 => (InputChangeTime => rstS_ipd'last_event,
                           PathDelay => tpd_rstS_PADDI,
                           PathCondition => TRUE)),
      GlitchData => PADDI_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity A1B
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity A1B is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "A1B";

      tipd_A1S  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_A1S_PADDI	 : VitalDelayType01 := (0 ns, 0 ns);
      tperiod_A1S 	: VitalDelayType := 0 ns;
      tpw_A1S_posedge	: VitalDelayType := 0 ns;
      tpw_A1S_negedge	: VitalDelayType := 0 ns);

    port (PADDI: out Std_logic; A1S: in Std_logic);

    ATTRIBUTE Vital_Level0 OF A1B : ENTITY IS TRUE;

  end A1B;

  architecture Structure of A1B is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDI_out 	: std_logic := 'X';
    signal A1S_ipd 	: std_logic := 'X';

    component ec2iobuf0001
      port (Z: out Std_logic; PAD: in Std_logic);
    end component;
  begin
    A1_pad: ec2iobuf0001
      port map (Z=>PADDI_out, PAD=>A1S_ipd);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(A1S_ipd, A1S, tipd_A1S);
    END BLOCK;

    VitalBehavior : PROCESS (PADDI_out, A1S_ipd)
    VARIABLE PADDI_zd         	: std_logic := 'X';
    VARIABLE PADDI_GlitchData 	: VitalGlitchDataType;

    VARIABLE tviol_A1S_A1S          	: x01 := '0';
    VARIABLE periodcheckinfo_A1S	: VitalPeriodDataType;

    BEGIN

    IF (TimingChecksOn) THEN
      VitalPeriodPulseCheck (
        TestSignal => A1S_ipd,
        TestSignalName => "A1S",
        Period => tperiod_A1S,
        PulseWidthHigh => tpw_A1S_posedge,
        PulseWidthLow => tpw_A1S_negedge,
        PeriodData => periodcheckinfo_A1S,
        Violation => tviol_A1S_A1S,
        MsgOn => MsgOn, XOn => XOn,
        HeaderMsg => InstancePath,
        CheckEnabled => TRUE,
        MsgSeverity => warning);

    END IF;

    PADDI_zd 	:= PADDI_out;

    VitalPathDelay01 (
      OutSignal => PADDI, OutSignalName => "PADDI", OutTemp => PADDI_zd,
      Paths      => (0 => (InputChangeTime => A1S_ipd'last_event,
                           PathDelay => tpd_A1S_PADDI,
                           PathCondition => TRUE)),
      GlitchData => PADDI_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity SCH
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity SCH is
    port (A0: in Std_logic; A1: in Std_logic; rst: in Std_logic; 
          clk: in Std_logic; Q0: out Std_logic; Q4: out Std_logic; 
          Q3: out Std_logic; Q2: out Std_logic; Q1: out Std_logic; 
          DL: in Std_logic; DR: in Std_logic);



  end SCH;

  architecture Structure of SCH is
    signal rst_c: Std_logic;
    signal N_6: Std_logic;
    signal N_2: Std_logic;
    signal N_5: Std_logic;
    signal N_1: Std_logic;
    signal clk_c: Std_logic;
    signal N_15: Std_logic;
    signal N_16: Std_logic;
    signal N_10: Std_logic;
    signal N_4: Std_logic;
    signal N_9: Std_logic;
    signal N_3: Std_logic;
    signal N_18: Std_logic;
    signal N_21: Std_logic;
    signal N_8: Std_logic;
    signal N_7: Std_logic;
    signal N_23: Std_logic;
    signal A0_c: Std_logic;
    signal DR_c: Std_logic;
    signal A1_c: Std_logic;
    signal DL_c: Std_logic;
    signal Q0_c: Std_logic;
    signal Q1_c: Std_logic;
    signal Q2_c: Std_logic;
    signal Q3_c: Std_logic;
    signal Q4_c: Std_logic;
    signal VCCI: Std_logic;
    component VHI
      port (Z: out Std_logic);
    end component;
    component PUR
      port (PUR: in Std_logic);
    end component;
    component GSR
      port (GSR: in Std_logic);
    end component;
    component Q0B
      port (PADDO: in Std_logic; Q0S: out Std_logic);
    end component;
    component A0B
      port (PADDI: out Std_logic; A0S: in Std_logic);
    end component;
    component DRB
      port (PADDI: out Std_logic; DRS: in Std_logic);
    end component;
    component DLB
      port (PADDI: out Std_logic; DLS: in Std_logic);
    end component;
    component Q1B
      port (PADDO: in Std_logic; Q1S: out Std_logic);
    end component;
    component Q2B
      port (PADDO: in Std_logic; Q2S: out Std_logic);
    end component;
    component Q3B
      port (PADDO: in Std_logic; Q3S: out Std_logic);
    end component;
    component Q4B
      port (PADDO: in Std_logic; Q4S: out Std_logic);
    end component;
    component clkB
      port (PADDI: out Std_logic; clkS: in Std_logic);
    end component;
    component rstB
      port (PADDI: out Std_logic; rstS: in Std_logic);
    end component;
    component A1B
      port (PADDI: out Std_logic; A1S: in Std_logic);
    end component;
  begin
    SLICE_0I: SLOGICB
      generic map (CLKMUX=>"SIG", CEMUX=>"VHI", GSR=>"DISABLED", 
                   SRMODE=>"ASYNC", LSRONMUX=>"OFF", LUT0_INITVAL=>X"8888", 
                   LUT1_INITVAL=>X"8888", REG1_SD=>"VHI", REG0_SD=>"VHI", 
                   CHECK_DI1=>TRUE, CHECK_DI0=>TRUE)
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>N_6, B1=>rst_c, C1=>'X', 
                D1=>'X', DI1=>N_5, DI0=>N_1, A0=>N_2, B0=>rst_c, C0=>'X', 
                D0=>'X', M0=>'X', CE=>'X', CLK=>clk_c, LSR=>'X', OFX1=>open, 
                F1=>N_5, Q1=>N_16, OFX0=>open, F0=>N_1, Q0=>N_15);
    SLICE_1I: SLOGICB
      generic map (CLKMUX=>"SIG", CEMUX=>"VHI", GSR=>"DISABLED", 
                   SRMODE=>"ASYNC", LSRONMUX=>"OFF", LUT0_INITVAL=>X"8888", 
                   LUT1_INITVAL=>X"8888", REG1_SD=>"VHI", REG0_SD=>"VHI", 
                   CHECK_DI1=>TRUE, CHECK_DI0=>TRUE)
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>N_10, B1=>rst_c, C1=>'X', 
                D1=>'X', DI1=>N_9, DI0=>N_3, A0=>N_4, B0=>rst_c, C0=>'X', 
                D0=>'X', M0=>'X', CE=>'X', CLK=>clk_c, LSR=>'X', OFX1=>open, 
                F1=>N_9, Q1=>N_21, OFX0=>open, F0=>N_3, Q0=>N_18);
    SLICE_2I: SLOGICB
      generic map (CLKMUX=>"SIG", CEMUX=>"VHI", GSR=>"DISABLED", 
                   SRMODE=>"ASYNC", LSRONMUX=>"OFF", LUT0_INITVAL=>X"8888", 
                   REG0_SD=>"VHI", CHECK_DI0=>TRUE)
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>'X', B1=>'X', C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>N_7, A0=>N_8, B0=>rst_c, C0=>'X', 
                D0=>'X', M0=>'X', CE=>'X', CLK=>clk_c, LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>open, F0=>N_7, Q0=>N_23);
    I35_MUX21_SLICE_3I: SLOGICB
      generic map (M0MUX=>"SIG", LUT0_INITVAL=>X"CACA", LUT1_INITVAL=>X"EEEE")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>A0_c, B1=>N_23, C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>N_21, B0=>DR_c, C0=>A0_c, 
                D0=>'X', M0=>A1_c, CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>N_10, F0=>open, Q0=>open);
    I31_MUX21_SLICE_4I: SLOGICB
      generic map (M0MUX=>"SIG", LUT0_INITVAL=>X"CACA", LUT1_INITVAL=>X"EEEE")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>A0_c, B1=>DL_c, C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>N_15, B0=>N_18, C0=>A0_c, 
                D0=>'X', M0=>A1_c, CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>N_2, F0=>open, Q0=>open);
    I32_MUX21_SLICE_5I: SLOGICB
      generic map (M0MUX=>"SIG", LUT0_INITVAL=>X"CACA", LUT1_INITVAL=>X"EEEE")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>A0_c, B1=>N_15, C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>N_18, B0=>N_16, C0=>A0_c, 
                D0=>'X', M0=>A1_c, CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>N_4, F0=>open, Q0=>open);
    I33_MUX21_SLICE_6I: SLOGICB
      generic map (M0MUX=>"SIG", LUT0_INITVAL=>X"CACA", LUT1_INITVAL=>X"4444")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>A0_c, B1=>N_18, C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>N_16, B0=>N_23, C0=>A0_c, 
                D0=>'X', M0=>A1_c, CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>N_6, F0=>open, Q0=>open);
    I34_MUX21_SLICE_7I: SLOGICB
      generic map (M0MUX=>"SIG", LUT0_INITVAL=>X"CACA", LUT1_INITVAL=>X"EEEE")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>A0_c, B1=>N_16, C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>N_23, B0=>N_21, C0=>A0_c, 
                D0=>'X', M0=>A1_c, CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>N_8, F0=>open, Q0=>open);
    SLICE_8I: SLOGICB
      generic map (LUT0_INITVAL=>X"5555")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>'X', B1=>'X', C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>N_15, B0=>'X', C0=>'X', 
                D0=>'X', M0=>'X', CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>open, F0=>Q0_c, Q0=>open);
    SLICE_9I: SLOGICB
      generic map (LUT0_INITVAL=>X"5555")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>'X', B1=>'X', C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>N_18, B0=>'X', C0=>'X', 
                D0=>'X', M0=>'X', CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>open, F0=>Q1_c, Q0=>open);
    SLICE_10I: SLOGICB
      generic map (LUT0_INITVAL=>X"5555")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>'X', B1=>'X', C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>N_16, B0=>'X', C0=>'X', 
                D0=>'X', M0=>'X', CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>open, F0=>Q2_c, Q0=>open);
    SLICE_11I: SLOGICB
      generic map (LUT0_INITVAL=>X"5555")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>'X', B1=>'X', C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>N_23, B0=>'X', C0=>'X', 
                D0=>'X', M0=>'X', CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>open, F0=>Q3_c, Q0=>open);
    SLICE_12I: SLOGICB
      generic map (LUT0_INITVAL=>X"5555")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>'X', B1=>'X', C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>N_21, B0=>'X', C0=>'X', 
                D0=>'X', M0=>'X', CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>open, F0=>Q4_c, Q0=>open);
    Q0I: Q0B
      port map (PADDO=>Q0_c, Q0S=>Q0);
    A0I: A0B
      port map (PADDI=>A0_c, A0S=>A0);
    DRI: DRB
      port map (PADDI=>DR_c, DRS=>DR);
    DLI: DLB
      port map (PADDI=>DL_c, DLS=>DL);
    Q1I: Q1B
      port map (PADDO=>Q1_c, Q1S=>Q1);
    Q2I: Q2B
      port map (PADDO=>Q2_c, Q2S=>Q2);
    Q3I: Q3B
      port map (PADDO=>Q3_c, Q3S=>Q3);
    Q4I: Q4B
      port map (PADDO=>Q4_c, Q4S=>Q4);
    clkI: clkB
      port map (PADDI=>clk_c, clkS=>clk);
    rstI: rstB
      port map (PADDI=>rst_c, rstS=>rst);
    A1I: A1B
      port map (PADDI=>A1_c, A1S=>A1);
    VHI_INST: VHI
      port map (Z=>VCCI);
    PUR_INST: PUR
      port map (PUR=>VCCI);
    GSR_INST: GSR
      port map (GSR=>VCCI);
  end Structure;



  library IEEE, vital2000, XP2;
  configuration Structure_CON of SCH is
    for Structure
    end for;
  end Structure_CON;


