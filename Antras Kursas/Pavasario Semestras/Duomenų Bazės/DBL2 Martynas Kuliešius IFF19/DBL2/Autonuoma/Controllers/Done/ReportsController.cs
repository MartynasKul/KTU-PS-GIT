namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using LateContractsReport = Org.Ktu.Isk.P175B602.Autonuoma.Models.LateContractsReport;
using ContractsReport = Org.Ktu.Isk.P175B602.Autonuoma.Models.ContractsReport;
using ServicesReport = Org.Ktu.Isk.P175B602.Autonuoma.Models.ServicesReport;


/// <summary>
/// Controller for producing reports.
/// </summary>
public class ReportsController : Controller
{
	

	/// <summary>
	/// Produces contracts report.
	/// </summary>
	/// <param name="dateFrom">Starting date. Can be null.</param>
	/// <param name="dateTo">Ending date. Can be null.</param>
	/// <returns>Report view.</returns>
	[HttpGet]
	public ActionResult Contracts(int idFrom, int idTo)
	{
		var report = new ContractsReport.Report();
		report.idFrom = idFrom;
		report.idTo = idTo; 

		report.Automobiliai = AtaskaitaRepo.GetContracts(report.idFrom, report.idTo);

		foreach (var item in report.Automobiliai)
		{
			report.VisoAuto++;
		}

		return View(report);
	}

}
