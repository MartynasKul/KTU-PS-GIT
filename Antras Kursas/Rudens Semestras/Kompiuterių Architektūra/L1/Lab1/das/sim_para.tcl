lappend auto_path "C:/lscc/diamond/3.12/data/script"
package require simulation_generation
set ::bali::simulation::Para(DEVICEFAMILYNAME) {LatticeXP2}
set ::bali::simulation::Para(PROJECT) {das}
set ::bali::simulation::Para(PROJECTPATH) {E:/Kompiuteriu architektura/L1/Lab1}
set ::bali::simulation::Para(FILELIST) {"E:/Kompiuteriu architektura/L1/Lab1/Priverstine_adr/F_flg.vhd" "E:/Kompiuteriu architektura/L1/Lab1/Priverstine_adr/F_reg.vhd" "E:/Kompiuteriu architektura/L1/Lab1/Priverstine_adr/F_cnt.vhd" "E:/Kompiuteriu architektura/L1/Lab1/Priverstine_adr/F_rom.vhd" "E:/Kompiuteriu architektura/L1/Lab1/Priverstine_adr/F_alu_s.vhd" "E:/Kompiuteriu architektura/L1/Lab1/Priverstine_adr/F_mux.vhd" "E:/Kompiuteriu architektura/L1/Lab1/Priverstine_adr/F_ctrl.vhd" "E:/Kompiuteriu architektura/L1/Lab1/Priverstine_adr/F_top.vhd" }
set ::bali::simulation::Para(GLBINCLIST) {}
set ::bali::simulation::Para(INCLIST) {"none" "none" "none" "none" "none" "none" "none" "none"}
set ::bali::simulation::Para(WORKLIBLIST) {"work" "work" "work" "work" "work" "work" "work" "work" }
set ::bali::simulation::Para(COMPLIST) {"VHDL" "VHDL" "VHDL" "VHDL" "VHDL" "VHDL" "VHDL" "VHDL" }
set ::bali::simulation::Para(SIMLIBLIST) {pmi_work ovi_xp2}
set ::bali::simulation::Para(MACROLIST) {}
set ::bali::simulation::Para(SIMULATIONTOPMODULE) {TOP}
set ::bali::simulation::Para(SIMULATIONINSTANCE) {}
set ::bali::simulation::Para(LANGUAGE) {VHDL}
set ::bali::simulation::Para(SDFPATH)  {}
set ::bali::simulation::Para(INSTALLATIONPATH) {C:/lscc/diamond/3.12}
set ::bali::simulation::Para(ADDTOPLEVELSIGNALSTOWAVEFORM)  {1}
set ::bali::simulation::Para(RUNSIMULATION)  {1}
set ::bali::simulation::Para(HDLPARAMETERS) {}
set ::bali::simulation::Para(POJO2LIBREFRESH)    {}
set ::bali::simulation::Para(POJO2MODELSIMLIB)   {}
::bali::simulation::ModelSim_Run
