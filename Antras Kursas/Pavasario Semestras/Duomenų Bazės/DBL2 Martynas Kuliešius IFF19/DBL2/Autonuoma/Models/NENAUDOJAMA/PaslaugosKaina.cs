namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// View model of 'PaslaugosKaina' entity.
/// </summary>
public class PaslaugosKaina
{
	/// <summary>
	/// ID of the record in the form. Is used when adding and removing records.
	/// </summary>
	public int InListId { get; set; }

	/// <summary>
	/// Indicates if record should not be editable.
	/// </summary>
	public bool IsReadonly { get; set; }


	public int FkPaslauga { get; set; }

	[DisplayName("Galioja nuo")]
	[Required]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	public DateTime GaliojaNuo { get; set; }
	

	[DisplayName("Galioja iki")]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	public DateTime? GaliojaIki { get; set; }

	[DisplayName("Kaina")]
	[Required]
	public decimal Kaina { get; set; }
}
