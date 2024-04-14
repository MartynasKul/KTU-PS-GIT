namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.SutartisF2;


/// <summary>
/// Controller for working with 'Sutartis' entity. Implementation of F2 version.
/// </summary>
public class SutartisF2Controller : Controller
{
	/*/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View(SutartisF2Repo.ListSutartis());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in a browser.
	/// </summary>
	/// <returns>Entity creation form.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var sutCE = new SutartisCE();

		sutCE.Sutartis.Data = DateTime.Now;
		
		PopulateLists(sutCE);

		return View(sutCE);
	}


	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="sutCE">Entity view model filled with latest data.</param>
	/// <returns>Returns creation from view or redirets back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(int? save, int? add, int? remove, SutartisCE sutCE)
	{
		
			
		ModelState.Clear();

			//go back to the form
		PopulateLists(sutCE);
		return View(sutCE);
		


	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var sutCE = SutartisF2Repo.FindSutartisCE(id);
		
		//sutCE.UzsakytosPaslaugos = SutartisF2Repo.ListUzsakytaPaslauga(id);			
		
		PopulateLists(sutCE);

		return View(sutCE);
	


		

		//should not reach here
		throw new Exception("Should not reach here.");
	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="sutCE">'Sutartis' view model to append to.</param>
	void PopulateLists(SutartisCE sutCE)
	{
		//load entities for the select lists
		//var automobiliai = AutomobilisRepo.ListAutomobilis();
		//var busenos = SutartisF2Repo.ListSutartiesBusena();
		var darbuotojai = DarbuotojasRepo.List();
		var klientai = KlientasRepo.List();
		var aiksteles = AiksteleRepo.List();

		//build select lists
		sutCE.Lists.Automobiliai =
			automobiliai
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.id),
							Text = $"{it.valstybinisNr} - {it.Marke} {it.Modelis}"
						};
				})
				.ToList();


		sutCE.Lists.Darbuotojai =
			darbuotojai
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = it.Tabelis.ToString(),
							Text = $"{it.Vardas} {it.Pavarde}"
						};
				})
				.ToList();

		sutCE.Lists.Klientai =
			klientai
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = it.AsmensKodas,
							Text = $"{it.Vardas} {it.Pavarde}"
						};
				})
				.ToList();

		
	}*/
}
