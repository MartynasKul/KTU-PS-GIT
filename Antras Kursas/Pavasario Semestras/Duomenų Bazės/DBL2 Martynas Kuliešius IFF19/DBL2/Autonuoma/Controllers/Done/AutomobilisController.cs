namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.Automobilis;


/// <summary>
/// Controller for working with 'Automobilis' entity.
/// </summary>
public class AutomobilisController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View(AutomobilisRepo.ListAutomobilis());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var autoCE = new AutomobilisCE();
		PopulateSelections(autoCE);

		return View(autoCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="autoCE">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(AutomobilisCE autoCE)
	{
		//form field validation passed?
		//if( ModelState.IsValid )
		//{
			AutomobilisRepo.InsertAutomobilis(autoCE);

			//save success, go back to the entity list
			PopulateSelections(autoCE);
			return RedirectToAction("Index");
		//}
		
		//form field validation failed, go back to the form
		//return View(autoCE);
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var autoCE = AutomobilisRepo.FindAutomobolisCE(id);
		PopulateSelections(autoCE);

		return View(autoCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>		
	/// <param name="autoCE">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, AutomobilisCE autoCE)
	{
		//form field validation passed?
		//if (ModelState.IsValid)
		//{
		AutomobilisRepo.UpdateAutomobilis(autoCE);

			//save success, go back to the entity list
		PopulateSelections(autoCE);
		return RedirectToAction("Index");
		//}

		//form field validation failed, go back to the form
		//return View(autoCE);
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var autoEvm = AutomobilisRepo.FindAutomobolisCE(id);
		return View(autoEvm);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(int id)
	{
		//try deleting, this will fail if foreign key constraint fails
		try 
		{
			AutomobilisRepo.DeleteAutomobilis(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch( MySql.Data.MySqlClient.MySqlException )
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var autoCE = AutomobilisRepo.FindAutomobolisCE(id);
			PopulateSelections(autoCE);

			return View("Delete", autoCE);
		}
	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="autoCE">'Automobilis' view model to append to.</param>
	public void PopulateSelections(AutomobilisCE autoCE)
	{
		//load entities for the select lists

		
		//build select list for 'Modeliai'
		{
			//initialize the destination list
			autoCE.Lists.Modeliai = new List<SelectListItem>();

			//load 'Marke' entities to use for item groups
			var markes = MarkeRepo.List();

			//create select list items from 'Modelis' related to each 'Marke'
			foreach( var marke in markes )
			{
				//create list item group for current 'Marke' entity
				var itemGrp = new SelectListGroup() { Name = marke.Pavadinimas};

				//load related 'Modelis' entities
				var modeliai = ModelisRepo.ListForMarke(marke.Id);

				//build list items for the group
				foreach( var modelis in modeliai )
				{
					var sle =
						new SelectListItem {
							Value = Convert.ToString(modelis.Id),
							Text = modelis.Pavadinimas,
							Group = itemGrp
						};
					autoCE.Lists.Modeliai.Add(sle);
				}
			}
		}
	}
}
