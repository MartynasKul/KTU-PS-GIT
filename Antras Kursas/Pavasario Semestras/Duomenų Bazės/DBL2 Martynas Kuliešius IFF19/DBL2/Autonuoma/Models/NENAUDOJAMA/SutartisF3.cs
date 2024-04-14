namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.SutartisF3;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// 'Sutartis' in list form.
/// </summary>
public class SutartisL
{
	[DisplayName("Nr.")]
	public int Nr { get; set; }


	[DisplayName("Sudarymo data")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime Data { get; set; }


	[DisplayName("Darbuotojas")]
	public string Darbuotojas { get; set; }


	[DisplayName("Nuomininkas")]
	public string Nuomininkas { get; set; }


	[DisplayName("Būsena")]
	public string Busena { get; set; }
}


/// <summary>
/// Representation of 'UzsakytaPaslauga' entity
/// </summary>
public class UzsakytaPaslaugaM
{
	//this field is used in added service adding form to display the added service prices drop down list
	[DisplayName("Pavadinimas")]
	[Required]
	public string FkPaslaugosKaina { get; set; }

	//this field is only used in added service editing form and is not changed
	[DisplayName("Pavadinimas")]
	public string Pavadinimas { get; set; }

	[DisplayName("Kiekis")]
	[Required]
	[Range(1, int.MaxValue)]
	public int Kiekis { get; set; }

	[DisplayName("Kaina")]
	[Required]
	public decimal Kaina { get; set; }
}


/// <summary>
/// Representation of 'UzsakytaPaslauga' entity for creation and editing
/// </summary>
public class UzsakytaPaslaugaCE
{
	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Paslaugos {get;set;}
	}

	/// <summary>
	/// Related 'UzsakytaPaslauga' records.
	/// </summary>
	public UzsakytaPaslaugaM UzsakytaPaslauga { get; set;  } = new UzsakytaPaslaugaM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}


/// <summary>
/// 'Sutartis' in create and edit forms.
/// </summary>
public class SutartisCE
{
	/// <summary>
	/// Entity data.
	/// </summary>
	public class SutartisM
	{
		[DisplayName("Nr")]
		public int Nr { get; set; }

		[DisplayName("Data")]
		[DataType(DataType.Date)]
		[Required]
		public DateTime? SutartiesData { get; set; }

		[DisplayName("Nuomos data ir laikas")]
		[DataType(DataType.DateTime)]
		[Required]
		public DateTime? NuomosDataLaikas { get; set; }

		[DisplayName("Planuojama grąžinti")]
		[DataType(DataType.DateTime)]
		[Required]
		public DateTime? PlanuojamaGrDataLaikas { get; set; }

		[DisplayName("Grąžinta")]
		[DataType(DataType.DateTime)]
		public DateTime? FaktineGrDataLaikas { get; set; }

		[DisplayName("Rida paimant")]
		[Required]
		public int PradineRida { get; set; }

		[DisplayName("Rida grąžinus")]
		public int? GalineRida { get; set; }

		[DisplayName("Nuomos kaina")]
		[Required]
		public decimal Kaina { get; set; }

		[DisplayName("Degalų kiekis paimant")]
		[Required]
		public int DegaluKiekisPaimant { get; set; }

		[DisplayName("Degalų kiekis gražinus")]
		public int? DegaluKiekisGrazinant { get; set; }
		
		[DisplayName("Būsena")]
		[Required]
		public int FkBusena { get; set; }

		[DisplayName("Klientas")]
		[Required]
		public string FkKlientas { get; set; }

		[DisplayName("Darbuotojas")]
		[Required]
		public string FkDarbuotojas { get; set; }

		[DisplayName("Automobilis")]
		[Required]
		public int FkAutomobilis { get; set; }

		[DisplayName("Gražinimo vieta")]
		[Required]
		public int FkGrazinimoVieta { get; set; }

		[DisplayName("Paėmimo vieta")]
		[Required]
		public int FkPaemimoVieta { get; set; }
	}

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Busenos { get; set; }
		public IList<SelectListItem> Klientai { get; set; }
		public IList<SelectListItem> Darbuotojai { get; set; }
		public IList<SelectListItem> Automobiliai { get; set; }
		public IList<SelectListItem> Vietos { get; set; }
		public IList<SelectListItem> Paslaugos {get;set;}
	}

	/// <summary>
	/// Sutartis.
	/// </summary>
	public SutartisM Sutartis { get; set; } = new SutartisM();

	/// <summary>
	/// Related 'UzsakytaPaslauga' records.
	/// </summary>
	public IList<UzsakytaPaslaugaM> UzsakytosPaslaugos { get; set;  } = new List<UzsakytaPaslaugaM>();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}


/// <summary>
/// 'SutartiesBusena' enumerator in lists.
/// /// </summary>
public class SutartiesBusena
{
	public int Id { get; set; }

	public string Name { get; set; }
}