namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Automobilis;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// 'Automobilis' in list form.
/// </summary>
public class AutomobilisL
{
	[DisplayName("Id")]
	public int id { get; set; }

	[DisplayName("Valstybinis Nr.")]
	public string valstybinisNr { get; set; }

	[DisplayName("Pagaminimo Data")]
	public string PagaminimoData { get; set; }

	[DisplayName("Modelis")]
	public string Modelis { get; set; }

	[DisplayName("Markė")]
	public string Marke { get; set; }

	[DisplayName("Rida")]
	public int Rida { get; set; }

	[DisplayName("Radijas")]
	public bool Radijas { get; set; }

	[DisplayName("Grotuvas")]
	public bool Grotuvas { get; set; }

	[DisplayName("Kondicionierius")]
	public bool Kondicionierius { get; set; }

	[DisplayName("Vietų skaičius")]
	public int VietuSkaicius { get; set; }

	[DisplayName("Registravimo data")]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime? RegistravimoData { get; set; }

	[DisplayName("Vertė")]
	[DataType(DataType.Currency)]
	public decimal Verte { get; set; }

	[DisplayName("VIN")]
	public char VIN { get; set; }

	[DisplayName("Kuro tipas")]
	public string KuroTipas { get; set; }

	[DisplayName("Pavarų dėžė")]
	public string PavaruDeze { get; set; }

	[DisplayName("Kėbulo tipas")]
	public string KebuloTipas { get; set; }
}

/// <summary>
/// 'Automobilis' in create and edit forms.
/// </summary>
public class AutomobilisCE
{
	/// <summary>
	/// Automobilis.
	/// </summary>
	public class AutomobilisM
	{
		[DisplayName("Id")]
		[Required]
		public int Id { get; set; }


		[DisplayName("Valstybinis Nr.")]

		[Required]
		public string ValstybinisNr { get; set; }

		[DisplayName("Pagaminimo data")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		[Required]
		public DateTime? PagaminimoData { get; set; }

		[DisplayName("Rida")]
		[Required]
		public int Rida { get; set; }

		[DisplayName("Radijas")]
		[Required]
		public bool Radijas { get; set; }

		[DisplayName("Grotuvas")]
		[Required]
		public bool Grotuvas { get; set; }

		[DisplayName("Kondicionierius")]
		[Required]
		public bool Kondicionierius { get; set; }

		[DisplayName("Vietų skaičius")]
		[Required]
		public int VietuSkaicius { get; set; }

		[DisplayName("Registravimo data")]
		[Required]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime? RegistravimoData { get; set; }

		[DisplayName("Vertė")]
		[Required]
		[DataType(DataType.Currency)]
		public decimal Verte { get; set; }

		[DisplayName("VIN")]
		[Required]
		public char VIN { get; set; }

		[DisplayName("Kuro tipas")]
		[Required]
		public string KuroTipas { get; set; }

		[DisplayName("Pavarų dėžė")]
		[Required]
		public string PavaruDeze { get; set; }

		[DisplayName("Kėbulo tipas")]
		[Required]
		public string KebuloTipas { get; set; }

		[DisplayName("Modelis")]
		[Required]
		public int FkModelisid { get; set; }

		[DisplayName("Marke")]
		[Required]
		public int FkMarkeid { get; set; }


	}

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Modeliai { get; set; }

	}


	/// <summary>
	/// Automobilis.
	/// </summary>
	public AutomobilisM Automobilis { get ; set; } = new AutomobilisM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}


