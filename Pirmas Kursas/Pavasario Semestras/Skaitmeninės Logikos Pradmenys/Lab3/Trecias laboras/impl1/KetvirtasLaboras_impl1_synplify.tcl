#-- Lattice Semiconductor Corporation Ltd.
#-- Synplify OEM project file

#device options
set_option -technology LATTICE-XP2
set_option -part LFXP2_5E
set_option -package TN144C
set_option -speed_grade -5

#compilation/mapping options
set_option -symbolic_fsm_compiler true
set_option -resource_sharing true

#use verilog 2001 standard option
set_option -vlog_std v2001

#map options
set_option -frequency 200
set_option -maxfan 1000
set_option -auto_constrain_io 0
set_option -disable_io_insertion false
set_option -retiming false; set_option -pipe true
set_option -force_gsr false
set_option -compiler_compatible 0
set_option -dup false

set_option -default_enum_encoding default

#simulation options


#timing analysis options



#automatic place and route (vendor) options
set_option -write_apr_constraint 1

#synplifyPro options
set_option -fix_gated_and_generated_clocks 1
set_option -update_models_cp 0
set_option -resolve_multiple_driver 0


set_option -seqshift_no_replicate 0

#-- add_file options
add_file -vhdl {C:/lscc/diamond/3.12/cae_library/synthesis/vhdl/xp2.vhd}
add_file -vhdl -lib "work" {C:/Users/markul4/Desktop/4laboras/cntr23.vhd}
add_file -vhdl -lib "work" {C:/Users/markul4/Desktop/4laboras/cntr167.vhd}
add_file -vhdl -lib "work" {C:/Users/markul4/Desktop/4laboras/top_cntr.vhd}


#-- set result format/file last
project -result_file {E:/KTU PIRMI METAI/Pavasario semestras/Skaitmenine Logika/Trecias laboras/impl1/KetvirtasLaboras_impl1.edi}

#-- error message log file
project -log_file {KetvirtasLaboras_impl1.srf}

#-- set any command lines input by customer


#-- run Synplify with 'arrange HDL file'
project -run hdl_info_gen -fileorder
project -run -clean
