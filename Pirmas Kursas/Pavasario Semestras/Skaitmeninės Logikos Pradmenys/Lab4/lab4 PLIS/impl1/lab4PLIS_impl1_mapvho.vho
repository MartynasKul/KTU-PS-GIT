
-- VHDL netlist produced by program ldbanno, Version Diamond (64-bit) 3.12.0.240.2

-- ldbanno -n VHDL -o lab4PLIS_impl1_mapvho.vho -w -neg -gui -msgset C:/Users/markul4/Desktop/lab4 PLIS/promote.xml lab4PLIS_impl1_map.ncd 
-- Netlist created on Mon May 23 18:20:58 2022
-- Netlist written on Mon May 23 18:21:02 2022
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

-- entity CNT_CB
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity CNT_CB is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "CNT_CB";

      tipd_IOLDO  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_IOLDO_CNTC	 : VitalDelayType01Z := (0 ns, 0 ns, 0 ns , 0 ns, 0 ns, 0 ns)
        );

    port (IOLDO: in Std_logic; CNTC: out Std_logic);

    ATTRIBUTE Vital_Level0 OF CNT_CB : ENTITY IS TRUE;

  end CNT_CB;

  architecture Structure of CNT_CB is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal IOLDO_ipd 	: std_logic := 'X';
    signal CNTC_out 	: std_logic := 'X';

    signal GNDI: Std_logic;
    component ec2iobuf
      port (I: in Std_logic; T: in Std_logic; PAD: out Std_logic);
    end component;
    component gnd
      port (PWR0: out Std_logic);
    end component;
  begin
    CNT_C_pad: ec2iobuf
      port map (I=>IOLDO_ipd, T=>GNDI, PAD=>CNTC_out);
    DRIVEGND: gnd
      port map (PWR0=>GNDI);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(IOLDO_ipd, IOLDO, tipd_IOLDO);
    END BLOCK;

    VitalBehavior : PROCESS (IOLDO_ipd, CNTC_out)
    VARIABLE CNTC_zd         	: std_logic := 'X';
    VARIABLE CNTC_GlitchData 	: VitalGlitchDataType;


    BEGIN

    IF (TimingChecksOn) THEN

    END IF;

    CNTC_zd 	:= CNTC_out;

    VitalPathDelay01Z (
      OutSignal => CNTC, OutSignalName => "CNTC", OutTemp => CNTC_zd,
      Paths      => (0 => (InputChangeTime => IOLDO_ipd'last_event,
                           PathDelay => tpd_IOLDO_CNTC,
                           PathCondition => TRUE)),
      GlitchData => CNTC_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity mfflsre
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity mfflsre is
    port (D0: in Std_logic; SP: in Std_logic; CK: in Std_logic; 
          LSR: in Std_logic; Q: out Std_logic);

    ATTRIBUTE Vital_Level0 OF mfflsre : ENTITY IS TRUE;

  end mfflsre;

  architecture Structure of mfflsre is
    component FD1P3BX
      generic (GSR: String);
      port (D: in Std_logic; SP: in Std_logic; CK: in Std_logic; 
            PD: in Std_logic; Q: out Std_logic);
    end component;
  begin
    INST01: FD1P3BX
      generic map (GSR => "ENABLED")
      port map (D=>D0, SP=>SP, CK=>CK, PD=>LSR, Q=>Q);
  end Structure;

-- entity CNT_C_MGIOL
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity CNT_C_MGIOL is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "CNT_C_MGIOL";

      tipd_ONEG0  	: VitalDelayType01 := (0 ns, 0 ns);
      tipd_CE  	: VitalDelayType01 := (0 ns, 0 ns);
      tipd_CLK  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_CLK_IOLDO	 : VitalDelayType01 := (0 ns, 0 ns);
      ticd_CLK	: VitalDelayType := 0 ns;
      tisd_ONEG0_CLK	: VitalDelayType := 0 ns;
      tsetup_ONEG0_CLK_noedge_posedge	: VitalDelayType := 0 ns;
      thold_ONEG0_CLK_noedge_posedge	: VitalDelayType := 0 ns;
      tisd_CE_CLK	: VitalDelayType := 0 ns;
      tsetup_CE_CLK_noedge_posedge	: VitalDelayType := 0 ns;
      thold_CE_CLK_noedge_posedge	: VitalDelayType := 0 ns);

    port (IOLDO: out Std_logic; ONEG0: in Std_logic; CE: in Std_logic; 
          CLK: in Std_logic);

    ATTRIBUTE Vital_Level0 OF CNT_C_MGIOL : ENTITY IS TRUE;

  end CNT_C_MGIOL;

  architecture Structure of CNT_C_MGIOL is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal IOLDO_out 	: std_logic := 'X';
    signal ONEG0_ipd 	: std_logic := 'X';
    signal ONEG0_dly 	: std_logic := 'X';
    signal CE_ipd 	: std_logic := 'X';
    signal CE_dly 	: std_logic := 'X';
    signal CLK_ipd 	: std_logic := 'X';
    signal CLK_dly 	: std_logic := 'X';

    signal GNDI: Std_logic;
    component gnd
      port (PWR0: out Std_logic);
    end component;
    component mfflsre
      port (D0: in Std_logic; SP: in Std_logic; CK: in Std_logic; 
            LSR: in Std_logic; Q: out Std_logic);
    end component;
  begin
    CNT_C_0io: mfflsre
      port map (D0=>ONEG0_dly, SP=>CE_dly, CK=>CLK_dly, LSR=>GNDI, 
                Q=>IOLDO_out);
    DRIVEGND: gnd
      port map (PWR0=>GNDI);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(ONEG0_ipd, ONEG0, tipd_ONEG0);
      VitalWireDelay(CE_ipd, CE, tipd_CE);
      VitalWireDelay(CLK_ipd, CLK, tipd_CLK);
    END BLOCK;

    --  Setup and Hold DELAYs
    SignalDelay : BLOCK
    BEGIN
      VitalSignalDelay(ONEG0_dly, ONEG0_ipd, tisd_ONEG0_CLK);
      VitalSignalDelay(CE_dly, CE_ipd, tisd_CE_CLK);
      VitalSignalDelay(CLK_dly, CLK_ipd, ticd_CLK);
    END BLOCK;

    VitalBehavior : PROCESS (IOLDO_out, ONEG0_dly, CE_dly, CLK_dly)
    VARIABLE IOLDO_zd         	: std_logic := 'X';
    VARIABLE IOLDO_GlitchData 	: VitalGlitchDataType;

    VARIABLE tviol_ONEG0_CLK       	: x01 := '0';
    VARIABLE ONEG0_CLK_TimingDatash	: VitalTimingDataType;
    VARIABLE tviol_CE_CLK       	: x01 := '0';
    VARIABLE CE_CLK_TimingDatash	: VitalTimingDataType;

    BEGIN

    IF (TimingChecksOn) THEN
      VitalSetupHoldCheck (
        TestSignal => ONEG0_dly,
        TestSignalName => "ONEG0",
        TestDelay => tisd_ONEG0_CLK,
        RefSignal => CLK_dly,
        RefSignalName => "CLK",
        RefDelay => ticd_CLK,
        SetupHigh => tsetup_ONEG0_CLK_noedge_posedge,
        SetupLow => tsetup_ONEG0_CLK_noedge_posedge,
        HoldHigh => thold_ONEG0_CLK_noedge_posedge,
        HoldLow => thold_ONEG0_CLK_noedge_posedge,
        CheckEnabled => TRUE,
        RefTransition => '/',
        MsgOn => MsgOn, XOn => XOn,
        HeaderMsg => InstancePath,
        TimingData => ONEG0_CLK_TimingDatash,
        Violation => tviol_ONEG0_CLK,
        MsgSeverity => warning);
      VitalSetupHoldCheck (
        TestSignal => CE_dly,
        TestSignalName => "CE",
        TestDelay => tisd_CE_CLK,
        RefSignal => CLK_dly,
        RefSignalName => "CLK",
        RefDelay => ticd_CLK,
        SetupHigh => tsetup_CE_CLK_noedge_posedge,
        SetupLow => tsetup_CE_CLK_noedge_posedge,
        HoldHigh => thold_CE_CLK_noedge_posedge,
        HoldLow => thold_CE_CLK_noedge_posedge,
        CheckEnabled => TRUE,
        RefTransition => '/',
        MsgOn => MsgOn, XOn => XOn,
        HeaderMsg => InstancePath,
        TimingData => CE_CLK_TimingDatash,
        Violation => tviol_CE_CLK,
        MsgSeverity => warning);

    END IF;

    IOLDO_zd 	:= IOLDO_out;

    VitalPathDelay01 (
      OutSignal => IOLDO, OutSignalName => "IOLDO", OutTemp => IOLDO_zd,
      Paths      => (0 => (InputChangeTime => CLK_dly'last_event,
                           PathDelay => tpd_CLK_IOLDO,
                           PathCondition => TRUE)),
      GlitchData => IOLDO_GlitchData,
      Mode       => ondetect, XOn => XOn, MsgOn => MsgOn);

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

-- entity CLKB
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity CLKB is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "CLKB";

      tipd_CLKS  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_CLKS_PADDI	 : VitalDelayType01 := (0 ns, 0 ns);
      tperiod_CLKS 	: VitalDelayType := 0 ns;
      tpw_CLKS_posedge	: VitalDelayType := 0 ns;
      tpw_CLKS_negedge	: VitalDelayType := 0 ns);

    port (PADDI: out Std_logic; CLKS: in Std_logic);

    ATTRIBUTE Vital_Level0 OF CLKB : ENTITY IS TRUE;

  end CLKB;

  architecture Structure of CLKB is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDI_out 	: std_logic := 'X';
    signal CLKS_ipd 	: std_logic := 'X';

    component ec2iobuf0001
      port (Z: out Std_logic; PAD: in Std_logic);
    end component;
  begin
    CLK_pad: ec2iobuf0001
      port map (Z=>PADDI_out, PAD=>CLKS_ipd);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(CLKS_ipd, CLKS, tipd_CLKS);
    END BLOCK;

    VitalBehavior : PROCESS (PADDI_out, CLKS_ipd)
    VARIABLE PADDI_zd         	: std_logic := 'X';
    VARIABLE PADDI_GlitchData 	: VitalGlitchDataType;

    VARIABLE tviol_CLKS_CLKS          	: x01 := '0';
    VARIABLE periodcheckinfo_CLKS	: VitalPeriodDataType;

    BEGIN

    IF (TimingChecksOn) THEN
      VitalPeriodPulseCheck (
        TestSignal => CLKS_ipd,
        TestSignalName => "CLKS",
        Period => tperiod_CLKS,
        PulseWidthHigh => tpw_CLKS_posedge,
        PulseWidthLow => tpw_CLKS_negedge,
        PeriodData => periodcheckinfo_CLKS,
        Violation => tviol_CLKS_CLKS,
        MsgOn => MsgOn, XOn => XOn,
        HeaderMsg => InstancePath,
        CheckEnabled => TRUE,
        MsgSeverity => warning);

    END IF;

    PADDI_zd 	:= PADDI_out;

    VitalPathDelay01 (
      OutSignal => PADDI, OutSignalName => "PADDI", OutTemp => PADDI_zd,
      Paths      => (0 => (InputChangeTime => CLKS_ipd'last_event,
                           PathDelay => tpd_CLKS_PADDI,
                           PathCondition => TRUE)),
      GlitchData => PADDI_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity CNT_O_4_B
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity CNT_O_4_B is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "CNT_O_4_B";

      tipd_PADDO  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_PADDO_CNTO4	 : VitalDelayType01Z := (0 ns, 0 ns, 0 ns , 0 ns, 0 ns, 0 ns)
        );

    port (PADDO: in Std_logic; CNTO4: out Std_logic);

    ATTRIBUTE Vital_Level0 OF CNT_O_4_B : ENTITY IS TRUE;

  end CNT_O_4_B;

  architecture Structure of CNT_O_4_B is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDO_ipd 	: std_logic := 'X';
    signal CNTO4_out 	: std_logic := 'X';

    signal GNDI: Std_logic;
    component ec2iobuf
      port (I: in Std_logic; T: in Std_logic; PAD: out Std_logic);
    end component;
    component gnd
      port (PWR0: out Std_logic);
    end component;
  begin
    CNT_O_pad_4: ec2iobuf
      port map (I=>PADDO_ipd, T=>GNDI, PAD=>CNTO4_out);
    DRIVEGND: gnd
      port map (PWR0=>GNDI);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(PADDO_ipd, PADDO, tipd_PADDO);
    END BLOCK;

    VitalBehavior : PROCESS (PADDO_ipd, CNTO4_out)
    VARIABLE CNTO4_zd         	: std_logic := 'X';
    VARIABLE CNTO4_GlitchData 	: VitalGlitchDataType;


    BEGIN

    IF (TimingChecksOn) THEN

    END IF;

    CNTO4_zd 	:= CNTO4_out;

    VitalPathDelay01Z (
      OutSignal => CNTO4, OutSignalName => "CNTO4", OutTemp => CNTO4_zd,
      Paths      => (0 => (InputChangeTime => PADDO_ipd'last_event,
                           PathDelay => tpd_PADDO_CNTO4,
                           PathCondition => TRUE)),
      GlitchData => CNTO4_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity CNT_O_3_B
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity CNT_O_3_B is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "CNT_O_3_B";

      tipd_PADDO  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_PADDO_CNTO3	 : VitalDelayType01Z := (0 ns, 0 ns, 0 ns , 0 ns, 0 ns, 0 ns)
        );

    port (PADDO: in Std_logic; CNTO3: out Std_logic);

    ATTRIBUTE Vital_Level0 OF CNT_O_3_B : ENTITY IS TRUE;

  end CNT_O_3_B;

  architecture Structure of CNT_O_3_B is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDO_ipd 	: std_logic := 'X';
    signal CNTO3_out 	: std_logic := 'X';

    signal GNDI: Std_logic;
    component ec2iobuf
      port (I: in Std_logic; T: in Std_logic; PAD: out Std_logic);
    end component;
    component gnd
      port (PWR0: out Std_logic);
    end component;
  begin
    CNT_O_pad_3: ec2iobuf
      port map (I=>PADDO_ipd, T=>GNDI, PAD=>CNTO3_out);
    DRIVEGND: gnd
      port map (PWR0=>GNDI);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(PADDO_ipd, PADDO, tipd_PADDO);
    END BLOCK;

    VitalBehavior : PROCESS (PADDO_ipd, CNTO3_out)
    VARIABLE CNTO3_zd         	: std_logic := 'X';
    VARIABLE CNTO3_GlitchData 	: VitalGlitchDataType;


    BEGIN

    IF (TimingChecksOn) THEN

    END IF;

    CNTO3_zd 	:= CNTO3_out;

    VitalPathDelay01Z (
      OutSignal => CNTO3, OutSignalName => "CNTO3", OutTemp => CNTO3_zd,
      Paths      => (0 => (InputChangeTime => PADDO_ipd'last_event,
                           PathDelay => tpd_PADDO_CNTO3,
                           PathCondition => TRUE)),
      GlitchData => CNTO3_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity CNT_O_2_B
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity CNT_O_2_B is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "CNT_O_2_B";

      tipd_PADDO  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_PADDO_CNTO2	 : VitalDelayType01Z := (0 ns, 0 ns, 0 ns , 0 ns, 0 ns, 0 ns)
        );

    port (PADDO: in Std_logic; CNTO2: out Std_logic);

    ATTRIBUTE Vital_Level0 OF CNT_O_2_B : ENTITY IS TRUE;

  end CNT_O_2_B;

  architecture Structure of CNT_O_2_B is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDO_ipd 	: std_logic := 'X';
    signal CNTO2_out 	: std_logic := 'X';

    signal GNDI: Std_logic;
    component ec2iobuf
      port (I: in Std_logic; T: in Std_logic; PAD: out Std_logic);
    end component;
    component gnd
      port (PWR0: out Std_logic);
    end component;
  begin
    CNT_O_pad_2: ec2iobuf
      port map (I=>PADDO_ipd, T=>GNDI, PAD=>CNTO2_out);
    DRIVEGND: gnd
      port map (PWR0=>GNDI);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(PADDO_ipd, PADDO, tipd_PADDO);
    END BLOCK;

    VitalBehavior : PROCESS (PADDO_ipd, CNTO2_out)
    VARIABLE CNTO2_zd         	: std_logic := 'X';
    VARIABLE CNTO2_GlitchData 	: VitalGlitchDataType;


    BEGIN

    IF (TimingChecksOn) THEN

    END IF;

    CNTO2_zd 	:= CNTO2_out;

    VitalPathDelay01Z (
      OutSignal => CNTO2, OutSignalName => "CNTO2", OutTemp => CNTO2_zd,
      Paths      => (0 => (InputChangeTime => PADDO_ipd'last_event,
                           PathDelay => tpd_PADDO_CNTO2,
                           PathCondition => TRUE)),
      GlitchData => CNTO2_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity CNT_O_1_B
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity CNT_O_1_B is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "CNT_O_1_B";

      tipd_PADDO  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_PADDO_CNTO1	 : VitalDelayType01Z := (0 ns, 0 ns, 0 ns , 0 ns, 0 ns, 0 ns)
        );

    port (PADDO: in Std_logic; CNTO1: out Std_logic);

    ATTRIBUTE Vital_Level0 OF CNT_O_1_B : ENTITY IS TRUE;

  end CNT_O_1_B;

  architecture Structure of CNT_O_1_B is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDO_ipd 	: std_logic := 'X';
    signal CNTO1_out 	: std_logic := 'X';

    signal GNDI: Std_logic;
    component ec2iobuf
      port (I: in Std_logic; T: in Std_logic; PAD: out Std_logic);
    end component;
    component gnd
      port (PWR0: out Std_logic);
    end component;
  begin
    CNT_O_pad_1: ec2iobuf
      port map (I=>PADDO_ipd, T=>GNDI, PAD=>CNTO1_out);
    DRIVEGND: gnd
      port map (PWR0=>GNDI);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(PADDO_ipd, PADDO, tipd_PADDO);
    END BLOCK;

    VitalBehavior : PROCESS (PADDO_ipd, CNTO1_out)
    VARIABLE CNTO1_zd         	: std_logic := 'X';
    VARIABLE CNTO1_GlitchData 	: VitalGlitchDataType;


    BEGIN

    IF (TimingChecksOn) THEN

    END IF;

    CNTO1_zd 	:= CNTO1_out;

    VitalPathDelay01Z (
      OutSignal => CNTO1, OutSignalName => "CNTO1", OutTemp => CNTO1_zd,
      Paths      => (0 => (InputChangeTime => PADDO_ipd'last_event,
                           PathDelay => tpd_PADDO_CNTO1,
                           PathCondition => TRUE)),
      GlitchData => CNTO1_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity CNT_O_0_B
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity CNT_O_0_B is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "CNT_O_0_B";

      tipd_PADDO  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_PADDO_CNTO0	 : VitalDelayType01Z := (0 ns, 0 ns, 0 ns , 0 ns, 0 ns, 0 ns)
        );

    port (PADDO: in Std_logic; CNTO0: out Std_logic);

    ATTRIBUTE Vital_Level0 OF CNT_O_0_B : ENTITY IS TRUE;

  end CNT_O_0_B;

  architecture Structure of CNT_O_0_B is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDO_ipd 	: std_logic := 'X';
    signal CNTO0_out 	: std_logic := 'X';

    signal GNDI: Std_logic;
    component ec2iobuf
      port (I: in Std_logic; T: in Std_logic; PAD: out Std_logic);
    end component;
    component gnd
      port (PWR0: out Std_logic);
    end component;
  begin
    CNT_O_pad_0: ec2iobuf
      port map (I=>PADDO_ipd, T=>GNDI, PAD=>CNTO0_out);
    DRIVEGND: gnd
      port map (PWR0=>GNDI);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(PADDO_ipd, PADDO, tipd_PADDO);
    END BLOCK;

    VitalBehavior : PROCESS (PADDO_ipd, CNTO0_out)
    VARIABLE CNTO0_zd         	: std_logic := 'X';
    VARIABLE CNTO0_GlitchData 	: VitalGlitchDataType;


    BEGIN

    IF (TimingChecksOn) THEN

    END IF;

    CNTO0_zd 	:= CNTO0_out;

    VitalPathDelay01Z (
      OutSignal => CNTO0, OutSignalName => "CNTO0", OutTemp => CNTO0_zd,
      Paths      => (0 => (InputChangeTime => PADDO_ipd'last_event,
                           PathDelay => tpd_PADDO_CNTO0,
                           PathCondition => TRUE)),
      GlitchData => CNTO0_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity CNT_CMDB
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity CNT_CMDB is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "CNT_CMDB";

      tipd_CNTCMD  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_CNTCMD_PADDI	 : VitalDelayType01 := (0 ns, 0 ns);
      tperiod_CNTCMD 	: VitalDelayType := 0 ns;
      tpw_CNTCMD_posedge	: VitalDelayType := 0 ns;
      tpw_CNTCMD_negedge	: VitalDelayType := 0 ns);

    port (PADDI: out Std_logic; CNTCMD: in Std_logic);

    ATTRIBUTE Vital_Level0 OF CNT_CMDB : ENTITY IS TRUE;

  end CNT_CMDB;

  architecture Structure of CNT_CMDB is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDI_out 	: std_logic := 'X';
    signal CNTCMD_ipd 	: std_logic := 'X';

    component ec2iobuf0001
      port (Z: out Std_logic; PAD: in Std_logic);
    end component;
  begin
    CNT_CMD_pad: ec2iobuf0001
      port map (Z=>PADDI_out, PAD=>CNTCMD_ipd);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(CNTCMD_ipd, CNTCMD, tipd_CNTCMD);
    END BLOCK;

    VitalBehavior : PROCESS (PADDI_out, CNTCMD_ipd)
    VARIABLE PADDI_zd         	: std_logic := 'X';
    VARIABLE PADDI_GlitchData 	: VitalGlitchDataType;

    VARIABLE tviol_CNTCMD_CNTCMD          	: x01 := '0';
    VARIABLE periodcheckinfo_CNTCMD	: VitalPeriodDataType;

    BEGIN

    IF (TimingChecksOn) THEN
      VitalPeriodPulseCheck (
        TestSignal => CNTCMD_ipd,
        TestSignalName => "CNTCMD",
        Period => tperiod_CNTCMD,
        PulseWidthHigh => tpw_CNTCMD_posedge,
        PulseWidthLow => tpw_CNTCMD_negedge,
        PeriodData => periodcheckinfo_CNTCMD,
        Violation => tviol_CNTCMD_CNTCMD,
        MsgOn => MsgOn, XOn => XOn,
        HeaderMsg => InstancePath,
        CheckEnabled => TRUE,
        MsgSeverity => warning);

    END IF;

    PADDI_zd 	:= PADDI_out;

    VitalPathDelay01 (
      OutSignal => PADDI, OutSignalName => "PADDI", OutTemp => PADDI_zd,
      Paths      => (0 => (InputChangeTime => CNTCMD_ipd'last_event,
                           PathDelay => tpd_CNTCMD_PADDI,
                           PathCondition => TRUE)),
      GlitchData => PADDI_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity RSTB
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity RSTB is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "RSTB";

      tipd_RSTS  	: VitalDelayType01 := (0 ns, 0 ns);
      tpd_RSTS_PADDI	 : VitalDelayType01 := (0 ns, 0 ns);
      tperiod_RSTS 	: VitalDelayType := 0 ns;
      tpw_RSTS_posedge	: VitalDelayType := 0 ns;
      tpw_RSTS_negedge	: VitalDelayType := 0 ns);

    port (PADDI: out Std_logic; RSTS: in Std_logic);

    ATTRIBUTE Vital_Level0 OF RSTB : ENTITY IS TRUE;

  end RSTB;

  architecture Structure of RSTB is
    ATTRIBUTE Vital_Level0 OF Structure : ARCHITECTURE IS TRUE;

    signal PADDI_out 	: std_logic := 'X';
    signal RSTS_ipd 	: std_logic := 'X';

    component ec2iobuf0001
      port (Z: out Std_logic; PAD: in Std_logic);
    end component;
  begin
    RST_pad: ec2iobuf0001
      port map (Z=>PADDI_out, PAD=>RSTS_ipd);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(RSTS_ipd, RSTS, tipd_RSTS);
    END BLOCK;

    VitalBehavior : PROCESS (PADDI_out, RSTS_ipd)
    VARIABLE PADDI_zd         	: std_logic := 'X';
    VARIABLE PADDI_GlitchData 	: VitalGlitchDataType;

    VARIABLE tviol_RSTS_RSTS          	: x01 := '0';
    VARIABLE periodcheckinfo_RSTS	: VitalPeriodDataType;

    BEGIN

    IF (TimingChecksOn) THEN
      VitalPeriodPulseCheck (
        TestSignal => RSTS_ipd,
        TestSignalName => "RSTS",
        Period => tperiod_RSTS,
        PulseWidthHigh => tpw_RSTS_posedge,
        PulseWidthLow => tpw_RSTS_negedge,
        PeriodData => periodcheckinfo_RSTS,
        Violation => tviol_RSTS_RSTS,
        MsgOn => MsgOn, XOn => XOn,
        HeaderMsg => InstancePath,
        CheckEnabled => TRUE,
        MsgSeverity => warning);

    END IF;

    PADDI_zd 	:= PADDI_out;

    VitalPathDelay01 (
      OutSignal => PADDI, OutSignalName => "PADDI", OutTemp => PADDI_zd,
      Paths      => (0 => (InputChangeTime => RSTS_ipd'last_event,
                           PathDelay => tpd_RSTS_PADDI,
                           PathCondition => TRUE)),
      GlitchData => PADDI_GlitchData,
      Mode       => vitaltransport, XOn => XOn, MsgOn => MsgOn);

    END PROCESS;

  end Structure;

-- entity GSR5MODE
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity GSR5MODE is
    port (GSRP: in Std_logic);

    ATTRIBUTE Vital_Level0 OF GSR5MODE : ENTITY IS TRUE;

  end GSR5MODE;

  architecture Structure of GSR5MODE is
    signal GSRMODE: Std_logic;
    component BUFBA
      port (A: in Std_logic; Z: out Std_logic);
    end component;
    component GSR
      port (GSR: in Std_logic);
    end component;
  begin
    INST10: BUFBA
      port map (A=>GSRP, Z=>GSRMODE);
    INST20: GSR
      port map (GSR=>GSRMODE);
  end Structure;

-- entity GSR_INSTB
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity GSR_INSTB is
    -- miscellaneous vital GENERICs
    GENERIC (
      TimingChecksOn	: boolean := TRUE;
      XOn           	: boolean := FALSE;
      MsgOn         	: boolean := TRUE;
      InstancePath  	: string := "GSR_INSTB";

      tipd_GSRNET  	: VitalDelayType01 := (0 ns, 0 ns));

    port (GSRNET: in Std_logic);

    ATTRIBUTE Vital_Level0 OF GSR_INSTB : ENTITY IS TRUE;

  end GSR_INSTB;

  architecture Structure of GSR_INSTB is
    signal GSRNET_ipd 	: std_logic := 'X';

    component GSR5MODE
      port (GSRP: in Std_logic);
    end component;
  begin
    GSR_INST_GSRMODE: GSR5MODE
      port map (GSRP=>GSRNET_ipd);

    --  INPUT PATH DELAYs
    WireDelay : BLOCK
    BEGIN
      VitalWireDelay(GSRNET_ipd, GSRNET, tipd_GSRNET);
    END BLOCK;

    VitalBehavior : PROCESS (GSRNET_ipd)


    BEGIN

    IF (TimingChecksOn) THEN

    END IF;



    END PROCESS;

  end Structure;

-- entity CNT19
  library IEEE, vital2000, XP2;
  use IEEE.STD_LOGIC_1164.all;
  use vital2000.vital_timing.all;
  use XP2.COMPONENTS.ALL;

  entity CNT19 is
    port (CLK: in Std_logic; RST: in Std_logic; CNT_CMD: in Std_logic; 
          CNT_C: out Std_logic; CNT_O: out Std_logic_vector (4 downto 0));



  end CNT19;

  architecture Structure of CNT19 is
    signal VCC: Std_logic;
    signal CNT_A_0: Std_logic;
    signal CNT_A_s_0: Std_logic;
    signal CNT_A_cry_0: Std_logic;
    signal CNT_A_4: Std_logic;
    signal CNT_A_3: Std_logic;
    signal CNT_A_cry_2: Std_logic;
    signal CNT_A_s_3: Std_logic;
    signal CNT_A_s_4: Std_logic;
    signal CNT_A_2: Std_logic;
    signal CNT_A_1: Std_logic;
    signal CNT_A_s_1: Std_logic;
    signal CNT_A_s_2: Std_logic;
    signal op_lt_cnt_a8lto4_0_a2_0: Std_logic;
    signal CNT_A_lm_1: Std_logic;
    signal CNT_A_lm_0: Std_logic;
    signal CNT_CMD_c: Std_logic;
    signal CLK_c: Std_logic;
    signal CNT_A_lm_3: Std_logic;
    signal CNT_A_lm_2: Std_logic;
    signal CNT_A_lm_4: Std_logic;
    signal op_eq_cnt_c3_i: Std_logic;
    signal CNT_A_i_3: Std_logic;
    signal CNT_A_i_1: Std_logic;
    signal CNT_A_i_2: Std_logic;
    signal CNT_A_i_0: Std_logic;
    signal CNT_A_i_4: Std_logic;
    signal CNT_C_c: Std_logic;
    signal RST_c: Std_logic;
    signal VCCI: Std_logic;
    component VHI
      port (Z: out Std_logic);
    end component;
    component PUR
      port (PUR: in Std_logic);
    end component;
    component CNT_CB
      port (IOLDO: in Std_logic; CNTC: out Std_logic);
    end component;
    component CNT_C_MGIOL
      port (IOLDO: out Std_logic; ONEG0: in Std_logic; CE: in Std_logic; 
            CLK: in Std_logic);
    end component;
    component CLKB
      port (PADDI: out Std_logic; CLKS: in Std_logic);
    end component;
    component CNT_O_4_B
      port (PADDO: in Std_logic; CNTO4: out Std_logic);
    end component;
    component CNT_O_3_B
      port (PADDO: in Std_logic; CNTO3: out Std_logic);
    end component;
    component CNT_O_2_B
      port (PADDO: in Std_logic; CNTO2: out Std_logic);
    end component;
    component CNT_O_1_B
      port (PADDO: in Std_logic; CNTO1: out Std_logic);
    end component;
    component CNT_O_0_B
      port (PADDO: in Std_logic; CNTO0: out Std_logic);
    end component;
    component CNT_CMDB
      port (PADDI: out Std_logic; CNTCMD: in Std_logic);
    end component;
    component RSTB
      port (PADDI: out Std_logic; RSTS: in Std_logic);
    end component;
    component GSR_INSTB
      port (GSRNET: in Std_logic);
    end component;
  begin
    SLICE_0I: SCCU2B
      generic map (CCU2_INJECT1_0=>"NO", CCU2_INJECT1_1=>"NO", 
                   INIT0_INITVAL=>"0x00FF", INIT1_INITVAL=>"0xAA00")
      port map (M1=>'X', A1=>CNT_A_0, B1=>'X', C1=>'X', D1=>VCC, DI1=>'X', 
                DI0=>'X', A0=>'X', B0=>'X', C0=>'X', D0=>VCC, FCI=>'X', 
                M0=>'X', CE=>'X', CLK=>'X', LSR=>'X', FCO=>CNT_A_cry_0, 
                F1=>CNT_A_s_0, Q1=>open, F0=>open, Q0=>open);
    SLICE_1I: SCCU2B
      generic map (CCU2_INJECT1_0=>"NO", CCU2_INJECT1_1=>"NO", 
                   INIT0_INITVAL=>"0xAA00", INIT1_INITVAL=>"0xAAAA")
      port map (M1=>'X', A1=>CNT_A_4, B1=>'X', C1=>'X', D1=>'X', DI1=>'X', 
                DI0=>'X', A0=>CNT_A_3, B0=>'X', C0=>'X', D0=>VCC, 
                FCI=>CNT_A_cry_2, M0=>'X', CE=>'X', CLK=>'X', LSR=>'X', 
                FCO=>open, F1=>CNT_A_s_4, Q1=>open, F0=>CNT_A_s_3, Q0=>open);
    SLICE_2I: SCCU2B
      generic map (CCU2_INJECT1_0=>"NO", CCU2_INJECT1_1=>"NO", 
                   INIT0_INITVAL=>"0xAA00", INIT1_INITVAL=>"0xAA00")
      port map (M1=>'X', A1=>CNT_A_2, B1=>'X', C1=>'X', D1=>VCC, DI1=>'X', 
                DI0=>'X', A0=>CNT_A_1, B0=>'X', C0=>'X', D0=>VCC, 
                FCI=>CNT_A_cry_0, M0=>'X', CE=>'X', CLK=>'X', LSR=>'X', 
                FCO=>CNT_A_cry_2, F1=>CNT_A_s_2, Q1=>open, F0=>CNT_A_s_1, 
                Q0=>open);
    SLICE_3I: SLOGICB
      generic map (CLKMUX=>"SIG", CEMUX=>"SIG", SRMODE=>"ASYNC", 
                   LSRONMUX=>"OFF", LUT0_INITVAL=>X"7030", 
                   LUT1_INITVAL=>X"7030", REG1_SD=>"VHI", REG0_SD=>"VHI", 
                   CHECK_DI1=>TRUE, CHECK_DI0=>TRUE, CHECK_CE=>TRUE)
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>CNT_A_2, B1=>CNT_A_4, 
                C1=>CNT_A_s_1, D1=>op_lt_cnt_a8lto4_0_a2_0, DI1=>CNT_A_lm_1, 
                DI0=>CNT_A_lm_0, A0=>CNT_A_2, B0=>CNT_A_4, C0=>CNT_A_s_0, 
                D0=>op_lt_cnt_a8lto4_0_a2_0, M0=>'X', CE=>CNT_CMD_c, 
                CLK=>CLK_c, LSR=>'X', OFX1=>open, F1=>CNT_A_lm_1, Q1=>CNT_A_1, 
                OFX0=>open, F0=>CNT_A_lm_0, Q0=>CNT_A_0);
    SLICE_4I: SLOGICB
      generic map (CLKMUX=>"SIG", CEMUX=>"SIG", SRMODE=>"ASYNC", 
                   LSRONMUX=>"OFF", LUT0_INITVAL=>X"7030", 
                   LUT1_INITVAL=>X"7030", REG1_SD=>"VHI", REG0_SD=>"VHI", 
                   CHECK_DI1=>TRUE, CHECK_DI0=>TRUE, CHECK_CE=>TRUE)
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>CNT_A_2, B1=>CNT_A_4, 
                C1=>CNT_A_s_3, D1=>op_lt_cnt_a8lto4_0_a2_0, DI1=>CNT_A_lm_3, 
                DI0=>CNT_A_lm_2, A0=>CNT_A_2, B0=>CNT_A_4, C0=>CNT_A_s_2, 
                D0=>op_lt_cnt_a8lto4_0_a2_0, M0=>'X', CE=>CNT_CMD_c, 
                CLK=>CLK_c, LSR=>'X', OFX1=>open, F1=>CNT_A_lm_3, Q1=>CNT_A_3, 
                OFX0=>open, F0=>CNT_A_lm_2, Q0=>CNT_A_2);
    SLICE_5I: SLOGICB
      generic map (CLKMUX=>"SIG", CEMUX=>"SIG", SRMODE=>"ASYNC", 
                   LSRONMUX=>"OFF", LUT0_INITVAL=>X"7030", 
                   LUT1_INITVAL=>X"1111", REG0_SD=>"VHI", CHECK_DI0=>TRUE, 
                   CHECK_CE=>TRUE)
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>CNT_A_1, B1=>CNT_A_3, C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>CNT_A_lm_4, A0=>CNT_A_2, B0=>CNT_A_4, 
                C0=>CNT_A_s_4, D0=>op_lt_cnt_a8lto4_0_a2_0, M0=>'X', 
                CE=>CNT_CMD_c, CLK=>CLK_c, LSR=>'X', OFX1=>open, 
                F1=>op_lt_cnt_a8lto4_0_a2_0, Q1=>open, OFX0=>open, 
                F0=>CNT_A_lm_4, Q0=>CNT_A_4);
    SLICE_6I: SLOGICB
      generic map (LUT0_INITVAL=>X"DFFF", LUT1_INITVAL=>X"5555")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>CNT_A_3, B1=>'X', C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>CNT_A_0, B0=>CNT_A_2, 
                C0=>CNT_A_4, D0=>op_lt_cnt_a8lto4_0_a2_0, M0=>'X', CE=>'X', 
                CLK=>'X', LSR=>'X', OFX1=>open, F1=>CNT_A_i_3, Q1=>open, 
                OFX0=>open, F0=>op_eq_cnt_c3_i, Q0=>open);
    SLICE_7I: SLOGICB
      generic map (LUT0_INITVAL=>X"FFFF")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>'X', B1=>'X', C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>'X', B0=>'X', C0=>'X', 
                D0=>'X', M0=>'X', CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>open, F0=>VCC, Q0=>open);
    SLICE_8I: SLOGICB
      generic map (LUT0_INITVAL=>X"5555")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>'X', B1=>'X', C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>CNT_A_1, B0=>'X', C0=>'X', 
                D0=>'X', M0=>'X', CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>open, F0=>CNT_A_i_1, Q0=>open);
    SLICE_9I: SLOGICB
      generic map (LUT0_INITVAL=>X"5555")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>'X', B1=>'X', C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>CNT_A_2, B0=>'X', C0=>'X', 
                D0=>'X', M0=>'X', CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>open, F0=>CNT_A_i_2, Q0=>open);
    SLICE_10I: SLOGICB
      generic map (LUT0_INITVAL=>X"5555")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>'X', B1=>'X', C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>CNT_A_0, B0=>'X', C0=>'X', 
                D0=>'X', M0=>'X', CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>open, F0=>CNT_A_i_0, Q0=>open);
    SLICE_11I: SLOGICB
      generic map (LUT0_INITVAL=>X"5555")
      port map (M1=>'X', FXA=>'X', FXB=>'X', A1=>'X', B1=>'X', C1=>'X', 
                D1=>'X', DI1=>'X', DI0=>'X', A0=>CNT_A_4, B0=>'X', C0=>'X', 
                D0=>'X', M0=>'X', CE=>'X', CLK=>'X', LSR=>'X', OFX1=>open, 
                F1=>open, Q1=>open, OFX0=>open, F0=>CNT_A_i_4, Q0=>open);
    CNT_CI: CNT_CB
      port map (IOLDO=>CNT_C_c, CNTC=>CNT_C);
    CNT_C_MGIOLI: CNT_C_MGIOL
      port map (IOLDO=>CNT_C_c, ONEG0=>op_eq_cnt_c3_i, CE=>CNT_CMD_c, 
                CLK=>CLK_c);
    CLKI: CLKB
      port map (PADDI=>CLK_c, CLKS=>CLK);
    CNT_O_4_I: CNT_O_4_B
      port map (PADDO=>CNT_A_i_4, CNTO4=>CNT_O(4));
    CNT_O_3_I: CNT_O_3_B
      port map (PADDO=>CNT_A_i_3, CNTO3=>CNT_O(3));
    CNT_O_2_I: CNT_O_2_B
      port map (PADDO=>CNT_A_i_2, CNTO2=>CNT_O(2));
    CNT_O_1_I: CNT_O_1_B
      port map (PADDO=>CNT_A_i_1, CNTO1=>CNT_O(1));
    CNT_O_0_I: CNT_O_0_B
      port map (PADDO=>CNT_A_i_0, CNTO0=>CNT_O(0));
    CNT_CMDI: CNT_CMDB
      port map (PADDI=>CNT_CMD_c, CNTCMD=>CNT_CMD);
    RSTI: RSTB
      port map (PADDI=>RST_c, RSTS=>RST);
    GSR_INST: GSR_INSTB
      port map (GSRNET=>RST_c);
    VHI_INST: VHI
      port map (Z=>VCCI);
    PUR_INST: PUR
      port map (PUR=>VCCI);
  end Structure;



  library IEEE, vital2000, XP2;
  configuration Structure_CON of CNT19 is
    for Structure
    end for;
  end Structure_CON;


