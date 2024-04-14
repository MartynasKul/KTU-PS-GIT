namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.ServicesReport;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// View model for a single service in services report.
/// </summary>
public class Paslauga
{
	[DisplayName("Id")]
	public int Id { get; set; }

	[DisplayName("Pavadinimas")]
	public string Pavadinimas { get; set; }

	[DisplayName("Kiekis")]
	public int Kiekis { get; set; }

	[DisplayName("Suma")]
	public decimal Suma { get; set; }
}

/// <summary>
/// View model of the whole report.
/// </summary>
public class Report
{
	[DataType(DataType.DateTime)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime? DateFrom { get; set; }

	[DataType(DataType.DateTime)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime? DateTo { get; set; }

	public List<Paslauga> Paslaugos { get; set; }

	public int VisoUzsakyta { get; set; }

	public decimal BendraSuma { get; set; }
}
