namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.ContractsReport;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// View model for single contract in a report.
/// </summary>
public class Auto
{
	[DisplayName("Id")]
	[Required]
	public int Id { get; set; }


	[DisplayName("Valstybinis Nr.")]

	[Required]
	public string ValstybinisNr { get; set; }

	[DisplayName("VIN")]
	[Required]
	public char VIN { get; set; }

	[DisplayName("Modelis")]
	[Required]
	public string Modelis { get; set; }

	[DisplayName("Marke")]
	[Required]
	public string Marke { get; set; }
}

/// <summary>
/// View model of the whole report.
/// </summary>
public class Report
{

	public int idFrom { get; set; }

	public int idTo { get; set; }

	public List<Auto> Automobiliai { get; set; }

	public int VisoAuto = 0;


}