namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.SutartisF3;


/// <summary>
/// Controller for working with 'Sutartis' entity. Implementation of F3 version.
/// </summary>
public class SutartisF3Controller : Controller
{
	/// <summary>
	/// Invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Contract list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View("ContractList", SutartisF3Repo.ListSutartis());
	}

	/// <summary>
	/// Invoked when contract create form is first opened from the list form.
	/// </summary>
	/// <returns>Contract creation form view.</returns>
	[HttpGet]
	public ActionResult ContractCreateStart()
	{
		//create new contract model
		var sutCE = new SutartisCE();

		//prefill date attributes with current date, for user convenience
		sutCE.Sutartis.SutartiesData = DateTime.Now;
		sutCE.Sutartis.NuomosDataLaikas = DateTime.Now;
		sutCE.Sutartis.PlanuojamaGrDataLaikas = DateTime.Now;
		
		//populate option lists
		PopulateOptionLists(sutCE);

		//
		return View("ContractCreate", sutCE);
	}

	/// <summary>
	/// Invoked save button is pressed in contract create form.
	/// </summary>
	/// <param name="sutCE">Contract data.</param>
	/// <returns>Redirects back to contract list form.</returns>
	[HttpPost]
	public ActionResult ContractCreateSave(SutartisCE sutCE)
	{
		//create new 'Sutartis'
		sutCE.Sutartis.Nr = SutartisF3Repo.InsertSutartis(sutCE);

		//create new 'UzsakytosPaslaugos' records
		foreach( var up in sutCE.UzsakytosPaslaugos )
			SutartisF3Repo.InsertUzsakytaPaslauga(sutCE.Sutartis.Nr, up);

		//redirect back to the contract list form
		return RedirectToAction("Index");
	}

	/// <summary>
	/// Invoked when contract editing form is first opened from the list form.
	/// </summary>
	/// <param name="id">ID of the contract to edit.</param>
	/// <returns>Contract edit form view.</returns>
	[HttpGet]
	public ActionResult ContractEditStart(int id)
	{
		//load contract model and related added services
		var sutCE = SutartisF3Repo.FindSutartisCE(id);		
		sutCE.UzsakytosPaslaugos = SutartisF3Repo.ListUzsakytaPaslauga(id);			

		//
		PopulateOptionLists(sutCE);

		//
		return View("ContractEdit", sutCE);
	}

	/// <summary>
	/// This is invoked when save button is pressed in contract edit form.
	/// </summary>
	/// <param name="sutCE">Contract data.</param>
	/// <returns>Redirects back to contract list form.</returns>
	[HttpPost]
	public ActionResult ContractEditSave(SutartisCE sutCE)
	{
		//update 'Sutartis'
		SutartisF3Repo.UpdateSutartis(sutCE);

		//delete all old 'UzsakytosPaslaugos' records
		SutartisF3Repo.DeleteUzsakytaPaslaugaForSutartis(sutCE.Sutartis.Nr);

		//create new 'UzsakytosPaslaugos' records
		foreach( var up in sutCE.UzsakytosPaslaugos )
			SutartisF3Repo.InsertUzsakytaPaslauga(sutCE.Sutartis.Nr, up);

		//redirect back to the contract list form
		return RedirectToAction("Index");
	}

	/// <summary>
	/// This is invoked when contract deletion form is first opened from the list form.
	/// </summary>
	/// <param name="id">ID of the contract to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult ContractDelete(int id)
	{
		var sutCE = SutartisF3Repo.FindSutartisCE(id);
		return View("ContractDelete", sutCE);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the contract to delete.</param>
	/// <returns>Deletion form view on error, redirects to contract list form on success.</returns>
	[HttpPost]
	public ActionResult ContractDeleteConfirm(int id)
	{
		//load 'Sutartis'
		var sutCE = SutartisF3Repo.FindSutartisCE(id);

		//'Sutartis' is in the state where deletion is permitted?
		if( sutCE.Sutartis.FkBusena == 1 || sutCE.Sutartis.FkBusena == 3 )
		{
			//delete the entity
			SutartisF3Repo.DeleteUzsakytaPaslaugaForSutartis(id);
			SutartisF3Repo.DeleteSutartis(id);

			//redired to list form
			return RedirectToAction("Index");
		}
		//'Sutartis' is in state where deletion is not permitted
		else
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;
			return View("ContractDelete", sutCE);
		}
	}

	/// <summary>
	/// Is invoked when added service addition button is clicked in contract creation or editing form.
	/// </summary>
	/// <param name="returnTo">Where to return. One of (create, edit).</param>
	/// <param name="sutCE">Contract data.</param>
	/// <returns>Added service addition form view.</returns>
	[HttpPost]
	public ActionResult AddedServiceAdd(string returnTo, SutartisCE sutCE)
	{
		//serialize current contract state into JSON; NOTE this is a quick, naive apporach and is not secure
		var contractState = JsonConvert.SerializeObject(sutCE);

		//pass contract state into ViewData, so that it is accessible in view
		ViewData["contract-state"] = contractState;

		//pass the return point setting
		ViewData["return-to"] = returnTo;

		//build a model for added service creation
		var upCE = new UzsakytaPaslaugaCE();
		upCE.Lists.Paslaugos = BuildAddedServiceOptionsList();
		
		//
		return View(upCE);
	}

	/// <summary>
	/// Is invoked when added service edit button is clicked in contract creation or editing form.
	/// </summary>
	/// <param name="upId">ID of the added service to edit.</param>
	/// <param name="returnTo">Where to return. One of (create, edit).</param>
	/// <param name="sutCE">Contract data.</param>
	/// <returns>Added service editing form view.</returns>
	[HttpPost]
	public ActionResult AddedServiceEdit(string upId, string returnTo, SutartisCE sutCE)
	{
		//serialize current contract state into JSON; NOTE this is a quick, naive apporach and is not secure
		var contractState = JsonConvert.SerializeObject(sutCE);

		//pass contract state into ViewData, so that it is accessible in view tample
		ViewData["contract-state"] = contractState;

		//pass the return point setting
		ViewData["return-to"] = returnTo;

		//build a model for added service editing
		var upCE = new UzsakytaPaslaugaCE();

		upCE.UzsakytaPaslauga = 
			sutCE.UzsakytosPaslaugos
			.First(it => it.FkPaslaugosKaina == upId);

		upCE.Lists.Paslaugos = BuildAddedServiceOptionsList();
		
		//
		return View(upCE);
	}

	/// <summary>
	/// Is invoked when added service deletion button is clicked in contract creation or editing form.
	/// </summary>
	/// <param name="upId">ID of the added service to remove.</param>
	/// <param name="returnTo">Where to return. One of (create, edit).</param>
	/// <param name="sutCE">Contract data.</param>
	/// <returns>Contract creation or editing form view, depending on the returnTo setting.</returns>
	[HttpPost]
	public ActionResult AddedServiceRemove(string upId, string returnTo, SutartisCE sutCE)
	{
		//remove the added service from the contract
		sutCE.UzsakytosPaslaugos =
			sutCE.UzsakytosPaslaugos
			.Where(it => it.FkPaslaugosKaina != upId)
			.ToList();

		//repopulate option lists
		PopulateOptionLists(sutCE);

		//remove old model state, otherwise @Html helpers will render old data
		ModelState.Clear();

		//get back to contract form
		switch( returnTo )
		{
			case "create":
				return View("ContractCreate", sutCE);

			case "edit":
				return View("ContractEdit", sutCE);

			default:
				throw new ArgumentException($"Value '{returnTo}' of argument 'returnTo' is not supported.");
		}
	}

	/// <summary>
	/// Is invoked to return from added service create or edit form back to the contract form.
	/// </summary>
	/// <param name="contractState">Saved contract state.</param>
	/// <param name="cause">Cause of return. One of (cancel, save-add, save-edit).</param>
	/// <param name="returnTo">Where to return. One of (create, edit).</param>
	/// <param name="upCE">Added service data.</param>
	/// <returns>Added service addding or editing view on validation failure. Otherwise contract creation or editing view, 
	/// depending on returnTo setting.</returns>
	[HttpPost]
	public ActionResult ContractContinue(string contractState, string cause, string returnTo, UzsakytaPaslaugaCE upCE)
	{
		switch( cause )
		{
			//adding or editing of added service was cancelled, restore saved state
			case "cancel": {
				//restore saved contract state and repopulate option lists
				var sutCE = JsonConvert.DeserializeObject<SutartisCE>(contractState);
				PopulateOptionLists(sutCE);

				//get back to contract form
				switch( returnTo )
				{
					case "create":
						return View("ContractCreate", sutCE);

					case "edit":
						return View("ContractEdit", sutCE);

					default:
						throw new ArgumentException($"Value '{returnTo}' of argument 'returnTo' is not supported.");
				}
			}

			//adding of added service was saved, try merging new data with saved contract state
			case "save-add": {
				//restore saved contract state and repopulate option lists
				var sutCE = JsonConvert.DeserializeObject<SutartisCE>(contractState);
				PopulateOptionLists(sutCE);

				//check if user is trying to add duplicate added service
				var isDuplicate =
					sutCE.UzsakytosPaslaugos
					.Any(it => it.FkPaslaugosKaina == upCE.UzsakytaPaslauga.FkPaslaugosKaina);

				//duplicate? show error
				if( isDuplicate )
				{
					//add error to model state
					ModelState.AddModelError($"UzsakytaPaslauga.FkPaslaugosKaina", "Already present in the contract.");

					//repopulate missing view parameters
					upCE.Lists.Paslaugos = BuildAddedServiceOptionsList();
					ViewData["contract-state"] = contractState;
					ViewData["return-to"] = returnTo;

					//show add form again
					return View("AddedServiceAdd", upCE);
				}

				//not trying to add a duplicate, append to the contract
				{
					//find the name for the added service, since it is not comming from the browser
					upCE.UzsakytaPaslauga.Pavadinimas = 
						BuildAddedServiceOptionsList()
						.First(it => it.Value == upCE.UzsakytaPaslauga.FkPaslaugosKaina)
						.Text;

					//append to the contract
					sutCE.UzsakytosPaslaugos.Add(upCE.UzsakytaPaslauga);
				}

				//get back to contract form
				switch( returnTo )
				{
					case "create":
						return View("ContractCreate", sutCE);

					case "edit":
						return View("ContractEdit", sutCE);

					default:
						throw new ArgumentException($"Value '{returnTo}' of argument 'returnTo' is not supported.");
				}
			}

			//edit of added contract was saved, merge new data with saved contract state
			case "save-edit": {
				//restore saved contract state and repopulate option lists
				var sutCE = JsonConvert.DeserializeObject<SutartisCE>(contractState);
				PopulateOptionLists(sutCE);

				//replace old added service data
				var upIndex =
					sutCE.UzsakytosPaslaugos
					.Select((up, index) => new { up = up, index = index })
					.First(it => it.up.FkPaslaugosKaina == upCE.UzsakytaPaslauga.FkPaslaugosKaina)
					.index;

				sutCE.UzsakytosPaslaugos[upIndex] = upCE.UzsakytaPaslauga;

				//get back to contract form
				switch( returnTo )
				{
					case "create":
						return View("ContractCreate", sutCE);

					case "edit":
						return View("ContractEdit", sutCE);

					default:
						throw new ArgumentException($"Value '{returnTo}' of argument 'returnTo' is not supported.");
				}
			}

			//wrong cause
			default:
				throw new ArgumentException($"Value '{cause}' of argument 'cause' is not supported.");
		}
	}

	/// <summary>
	/// Builds options list for added services to be used in list controls.
	/// </summary>
	/// <value>Options list for added services.</value>
	private IList<SelectListItem> BuildAddedServiceOptionsList() 
	{
		//initialize the destination list
		var result = new List<SelectListItem>();

		//load 'Paslaugos' to use for item groups
		var paslaugos = PaslaugaRepo.List();

		//create select list items from 'PaslauguKainos' related to each 'Paslaugos'
		foreach( var paslauga in paslaugos )
		{
			//create list item group for current 'Paslaugos' entity
			var itemGrp = new SelectListGroup() { Name = paslauga.Pavadinimas };

			//load related 'PaslauguKaina' entities
			var kainos = PaslaugosKainaRepo.LoadForPaslauga(paslauga.Id);

			//build list items for the group
			foreach( var kaina in kainos )
			{
				var sle =
					new SelectListItem {
						Value =
							//we use JSON here to make serialization/deserializaton of composite key more convenient
							JsonConvert.SerializeObject(new {
								FkPaslauga = paslauga.Id,
								GaliojaNuo = kaina.GaliojaNuo
							}),
						Text = $"{paslauga.Pavadinimas} {kaina.Kaina} EUR ({kaina.GaliojaNuo.ToString("yyyy-MM-dd")})",
						Group = itemGrp
					};
				result.Add(sle);
			}
		}

		//
		return result;		
	}

	/// <summary>
	/// Populates option lists used in list controls.
	/// </summary>
	/// <param name="sutCE">Contract model to populate into.</param>
	private void PopulateOptionLists(SutartisCE sutCE)
	{
		//load entities for the option lists
		var automobiliai = AutomobilisRepo.ListAutomobilis();
		var busenos = SutartisF3Repo.ListSutartiesBusena();
		var darbuotojai = DarbuotojasRepo.List();
		var klientai = KlientasRepo.List();
		var aiksteles = AiksteleRepo.List();

		//build option lists
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

		sutCE.Lists.Busenos =
			busenos
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.Id),
							Text = it.Name
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

		sutCE.Lists.Vietos =
			aiksteles
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.Id),
							Text = it.Pavadinimas
						};
				})
				.ToList();


		sutCE.Lists.Paslaugos = BuildAddedServiceOptionsList();
	}
}
