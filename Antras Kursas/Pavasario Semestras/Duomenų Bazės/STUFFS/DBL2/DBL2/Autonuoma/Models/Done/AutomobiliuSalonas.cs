namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// Model of 'AutomobiliuSalonas' entity.
/// </summary>
public class AutomobiliuSalonas
{
	public int Id { get; set; }

	public string Pavadinimas { get; set; }

    public string ImonesKodas { get; set; }

	public string Adresas { get; set; }	
	
	public int FkMiestas { get; set; }
}