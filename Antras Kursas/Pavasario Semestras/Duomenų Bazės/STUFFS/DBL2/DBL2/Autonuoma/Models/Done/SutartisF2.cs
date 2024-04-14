namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.SutartisF2;

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


	[DisplayName("Sutarties data")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime Data { get; set; }

	[DisplayName("Pradine rida")]
	public int PradRida { get; set; }

	[DisplayName("Kaina")]
	public decimal Kaina { get; set; }

	[DisplayName("Pirkimo tipas")]
	public string PirkTipas { get; set; }

	[DisplayName("Defektai")]
	public string Defektai { get; set; }

	[DisplayName("Salono id")]
	public int fk_Automobiliu_SalonasASid { get; set; }
	[DisplayName("Automobilio id")]
	public int fk_AUTOMOBILISid { get; set; }
	[DisplayName("Darbuotojo tabelio nr.")]
	public int fk_DARBUOTOJAStabelio_nr { get; set; }
	[DisplayName("Kliento asmens kodas")]
	public int fk_KLIENTASasmens_kodas  { get; set; }

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
		[DisplayName("Nr.")]
	public int Nr { get; set; }


	[DisplayName("Sutarties data")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime Data { get; set; }

	[DisplayName("Pradine rida")]
	public int PradRida { get; set; }

	[DisplayName("Kaina")]
	public decimal Kaina { get; set; }

	[DisplayName("Pirkimo tipas")]
	public string PirkTipas { get; set; }

	[DisplayName("Defektai")]
	public string Defektai { get; set; }

	[DisplayName("Salono id")]
	public int fk_Automobiliu_SalonasASid { get; set; }
	[DisplayName("Automobilio id")]
	public int fk_AUTOMOBILISid { get; set; }
	[DisplayName("Darbuotojo tabelio nr.")]
	public int fk_DARBUOTOJAStabelio_nr { get; set; }
	[DisplayName("Kliento asmens kodas")]
	public int fk_KLIENTASasmens_kodas  { get; set; }
	}

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Klientai { get; set; }
		public IList<SelectListItem> Darbuotojai { get; set; }
		public IList<SelectListItem> Automobiliai { get; set; }
	}


	/// <summary>
	/// Sutartis.
	/// </summary>
	public SutartisM Sutartis { get; set; } = new SutartisM();

	/// <summary>
	/// Related 'UzsakytaPaslauga' records.
	/// </summary>
	//public IList<UzsakytaPaslaugaM> UzsakytosPaslaugos { get; set;  } = new List<UzsakytaPaslaugaM>();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}


/// <summary>
/// 'SutartiesBusena' enumerator in lists.
/// /// </summary>
/*
public class SutartiesBusena
{
	public int Id { get; set; }

	public string Name { get; set; }
}*/