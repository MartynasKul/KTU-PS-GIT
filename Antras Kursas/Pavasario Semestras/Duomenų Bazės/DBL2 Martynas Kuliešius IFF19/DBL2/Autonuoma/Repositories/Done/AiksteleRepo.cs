namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;



/// <summary>
/// Database operations related to 'Aikstele' entity.
/// </summary>
public class AiksteleRepo
{
	public static List<AutomobiliuSalonas> List()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}aiksteles` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<AutomobiliuSalonas>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Adresas = dre.From<string>("adresas");
				t.FkMiestas = dre.From<int>("fk_miestas");
			});

		return result;
	}
}
