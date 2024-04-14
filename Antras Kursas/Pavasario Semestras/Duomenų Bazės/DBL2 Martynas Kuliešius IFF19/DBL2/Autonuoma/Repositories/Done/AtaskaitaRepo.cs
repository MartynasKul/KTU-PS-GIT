namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using LateContractsReport = Org.Ktu.Isk.P175B602.Autonuoma.Models.LateContractsReport;
using ContractsReport = Org.Ktu.Isk.P175B602.Autonuoma.Models.ContractsReport;
using ServicesReport = Org.Ktu.Isk.P175B602.Autonuoma.Models.ServicesReport;


/// <summary>
/// Database operations related to reports.
/// </summary>
public class AtaskaitaRepo
{

	public static List<ContractsReport.Auto> GetContracts(int idFrom, int idTo)
	{
		var query =
			$@"SELECT
				a.id,
				a.valstybinis_nr,
				a.VIN,
				ko.pavadinimas AS Modelis
				
			FROM
				{Config.TblPrefix}automobilis a
				LEFT JOIN `{Config.TblPrefix}modelis` ko ON ko.id = a.fk_MODELISid
			WHERE
				a.id >= IFNULL(?nuo, a.id)
				AND a.id <= IFNULL(?iki, a.id)
			
			ORDER BY a.id ASC";

		var drc = Sql.Query(query, args => {
				args.Add("?nuo", idFrom);
				args.Add("?iki", idTo);
			});

		var result = 
			Sql.MapAll<ContractsReport.Auto>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.ValstybinisNr = dre.From<string>("valstybinis_nr");

				t.Modelis = dre.From<string>("Modelis");
				
			});

		return result;
	}

}
