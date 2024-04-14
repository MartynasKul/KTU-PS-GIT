namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Automobilis;


/// <summary>
/// Database operations related to 'Automobilis'.
/// </summary>
public class AutomobilisRepo
{
	public static List<AutomobilisL> ListAutomobilis()
	{
		var query =
			$@"SELECT
				a.id,
				a.valstybinis_nr,
				a.pagaminimo_data,
				a.rida,
				a.radijas,
				a.grotuvas,
				a.kondicionierius,
				a.kebulas,
				a.registravimo_data,
				a.verte,
				a.VIN,
				a.Kuro_tipas,
				a.Pavaru_deze,
				a.Kebulo_tipas,
				ko.pavadinimas AS Modelis
			FROM
				{Config.TblPrefix}automobilis a
				LEFT JOIN `{Config.TblPrefix}modelis` ko ON ko.id = a.fk_MODELISid
			ORDER BY a.id ASC";

		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<AutomobilisL>(drc, (dre, t) => {
				t.id = dre.From<int>("id");
				t.valstybinisNr = dre.From<string>("valstybinis_nr");
				t.Modelis = dre.From<string>("modelis");
			});

		return result;
	}

	public static AutomobilisCE FindAutomobolisCE(int id)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}automobilis` WHERE id=?id";

		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result =
			Sql.MapOne<AutomobilisCE>(drc, (dre, t) => {
				//make a shortcut
				var auto = t.Automobilis;

				//
				auto.Id = dre.From<int>("id");
				auto.ValstybinisNr = dre.From<string>("valstybinis_nr");
				auto.PagaminimoData = dre.From<DateTime>("pagaminimo_data");
				auto.Rida = dre.From<int>("rida");
				auto.Radijas = dre.From<bool>("radijas");
				auto.Grotuvas = dre.From<bool>("grotuvas");
				auto.Kondicionierius = dre.From<bool>("kondicionierius");
				auto.VietuSkaicius = dre.From<int>("vietu_skaicius");
				auto.RegistravimoData = dre.From<DateTime>("registravimo_data");
				auto.Verte = dre.From<decimal>("verte");
				auto.PavaruDeze = dre.From<string>("Pavaru_deze");
				auto.KuroTipas = dre.From<string>("Kuro_tipas");
				auto.KebuloTipas = dre.From<string>("kebulas");
				auto.FkModelisid = dre.From<int>("fk_MODELISid");
			});

		return result;
	}

	public static void InsertAutomobilis(AutomobilisCE autoCE)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}automobilis`
			(
				`id`,
				`valstybinis_nr`,
				`pagaminimo_data`,
				`rida`,
				`radijas`,
				`grotuvas`,
				`kondicionierius`,
				`vietu_skaicius`,
				`registravimo_data`,
				`verte`,
				`Pavaru_deze`,
				`Kuro_tipas`,
				`kebulas`,
				`fk_MODELISid`
			)
			VALUES (
				?id,
				?vlst_nr,
				?pag_data,
				?rida,
				?radijas,
				?grotuvas,
				?kond,
				?viet_sk,
				?reg_dt,
				?verte,
				?pav_deze,
				?dega_tip,
				?kebulas,
				?fk_mod
			)";

		Sql.Insert(query, args => {
			//make a shortcut
			var auto = autoCE.Automobilis;

			//
			args.Add("?id", auto.Id);
			args.Add("?vlst_nr", auto.ValstybinisNr);
			args.Add("?pag_data", auto.PagaminimoData?.ToString("yyyy-MM-dd"));
			args.Add("?rida", auto.Rida);
			args.Add("?radijas", (auto.Radijas ? 1 : 0));
			args.Add("?grotuvas", (auto.Grotuvas ? 1 : 0));
			args.Add("?kond", (auto.Kondicionierius ? 1 : 0));
			args.Add("?viet_sk", auto.VietuSkaicius);
			args.Add("?reg_dt", auto.RegistravimoData?.ToString("yyyy-MM-dd"));
			args.Add("?verte", auto.Verte);
			args.Add("?pav_deze", auto.PavaruDeze);
			args.Add("?dega_tip", auto.KuroTipas);
			args.Add("?kebulas", auto.KebuloTipas);
			args.Add("?fk_mod", auto.FkModelisid);

		});
	}

	public static void UpdateAutomobilis(AutomobilisCE autoCE)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}automobilis`
			SET
				`valstybinis_nr` = ?vlst_nr,
				`pagaminimo_data` = ?pag_data,
				`rida` = ?rida,
				`radijas` = ?radijas,
				`grotuvas` = ?grotuvas,
				`kondicionierius` = ?kond,
				`vietu_skaicius` = ?viet_sk,
				`registravimo_data` = ?reg_dt,
				`verte` = ?verte,
				`Pavaru_deze` = ?pav_deze,
				`Kuro_tipas` = ?dega_tip,
				`kebulas` = ?kebulas,
				`fk_MODELISid` = ?fk_mod
			WHERE
				id=?id";

		Sql.Update(query, args => {
			//make a shortcut
			var auto = autoCE.Automobilis;

			//
			args.Add("?vlst_nr", auto.ValstybinisNr);
			args.Add("?pag_data", auto.PagaminimoData?.ToString("yyyy-MM-dd"));
			args.Add("?rida", auto.Rida);
			args.Add("?radijas", (auto.Radijas ? 1 : 0));
			args.Add("?grotuvas", (auto.Grotuvas ? 1 : 0));
			args.Add("?kond", (auto.Kondicionierius ? 1 : 0));
			args.Add("?viet_sk", auto.VietuSkaicius);
			args.Add("?reg_dt", auto.RegistravimoData?.ToString("yyyy-MM-dd"));
			args.Add("?verte", auto.Verte);
			args.Add("?pav_deze", auto.PavaruDeze);
			args.Add("?dega_tip", auto.KuroTipas);
			args.Add("?kebulas", auto.KebuloTipas);
			args.Add("?fk_mod", auto.FkModelisid);

			args.Add("?id", auto.Id);
		});
	}

	public static void DeleteAutomobilis(int id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}automobilis` WHERE id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}

}
