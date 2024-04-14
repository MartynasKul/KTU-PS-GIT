lappend auto_path "C:/lscc/diamond/3.12/data/script"
package require simulation_generation
set ::bali::simulation::Para(DEVICEFAMILYNAME) {LatticeXP2}
set ::bali::simulation::Para(PROJECT) {asd}
set ::bali::simulation::Para(PROJECTPATH) {E:/KTU PIRMI METAI/Pavasario semestras/Skaitmenine Logika/Lab4/4 laboras}
set ::bali::simulation::Para(FILELIST) {"E:/KTU PIRMI METAI/Pavasario semestras/Skaitmenine Logika/Lab4/4 laboras/cntr136.vhd" "E:/KTU PIRMI METAI/Pavasario semestras/Skaitmenine Logika/Lab4/4 laboras/cntr49.vhd" "E:/KTU PIRMI METAI/Pavasario semestras/Skaitmenine Logika/Lab4/4 laboras/cntr19.vhd" "E:/KTU PIRMI METAI/Pavasario semestras/Skaitmenine Logika/Lab4/4 laboras/JM2.vhd" }
set ::bali::simulation::Para(GLBINCLIST) {}
set ::bali::simulation::Para(INCLIST) {"none" "none" "none" "none"}
set ::bali::simulation::Para(WORKLIBLIST) {"work" "work" "work" "work" }
set ::bali::simulation::Para(COMPLIST) {"VHDL" "VHDL" "VHDL" "VHDL" }
set ::bali::simulation::Para(SIMLIBLIST) {pmi_work ovi_xp2}
set ::bali::simulation::Para(MACROLIST) {}
set ::bali::simulation::Para(SIMULATIONTOPMODULE) {TOP_CNT}
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
