namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;



/// <summary>
/// Database operations related to 'PaslaugosKaina' entity.
/// </summary>
public class PaslaugosKainaRepo
{
	public static List<PaslaugosKaina> LoadForPaslauga(int id)
	{
		var query =
			$@"SELECT
				pk.fk_paslauga,
				pk.galioja_nuo,
				pk.galioja_iki,
				pk.kaina,
				count(up.fk_sutartis) as kiekis
			FROM
				`{Config.TblPrefix}paslaugu_kainos` as pk
				LEFT JOIN `{Config.TblPrefix}uzsakytos_paslaugos` up
					ON up.fk_paslauga=pk.fk_paslauga AND up.fk_kaina_galioja_nuo=pk.galioja_nuo
			WHERE pk.fk_paslauga=?id
			GROUP BY
				pk.fk_paslauga,
				pk.galioja_nuo,
				pk.galioja_iki,
				pk.kaina
			ORDER BY pk.galioja_nuo DESC";

		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result = 
			Sql.MapAll<PaslaugosKaina>(drc, (dre, t) => {
				t.IsReadonly = dre.From<int>("kiekis") > 0;
				t.FkPaslauga = dre.From<int>("fk_paslauga");
				t.GaliojaNuo = dre.From<DateTime>("galioja_nuo");
				t.GaliojaIki = dre.From<DateTime?>("galioja_iki");
				t.Kaina = dre.From<decimal>("kaina");
			});

		for( int i = 0; i < result.Count; i++ )
			result[i].InListId = i;

		return result;
	}

	public static void Delete(int paslaugaId, DateTime galiojaNuo)
	{
		var query = 
			$@"DELETE FROM `{Config.TblPrefix}paslaugu_kainos`
			WHERE 
				fk_paslauga=?paslaugaId AND galioja_nuo=?galiojaNuo";

		Sql.Delete(query, args => {
			args.Add("?paslaugaId", paslaugaId);
			args.Add("?galiojaNuo", galiojaNuo);
		});
	}

	public static void Insert(PaslaugosKaina PaslaugosKainaVM)
	{
		string query = 
			$@"INSERT INTO `{Config.TblPrefix}paslaugu_kainos`
			(
				fk_paslauga,
				galioja_nuo,
				galioja_iki,
				kaina
			)
			VALUES(
				?fk_paslauga,
				?galioja_nuo,
				?galioja_iki,
				?kaina
			)";

		Sql.Insert(query, args => {
			args.Add("?fk_paslauga", PaslaugosKainaVM.FkPaslauga);
			args.Add("?galioja_nuo", PaslaugosKainaVM.GaliojaNuo);
			args.Add("?galioja_iki", PaslaugosKainaVM.GaliojaIki);
			args.Add("?kaina", PaslaugosKainaVM.Kaina);
		});
	}

	public static void Update(PaslaugosKaina PaslaugosKainaVM)
	{
		string query = 
			$@"UPDATE `{Config.TblPrefix}paslaugu_kainos`
			SET
				galioja_iki = ?galioja_iki,
				kaina = ?kaina				
			WHERE 
				fk_paslauga = ?fk_paslauga AND galioja_nuo = ?galioja_nuo";

		Sql.Insert(query, args => {
			args.Add("?fk_paslauga", PaslaugosKainaVM.FkPaslauga);
			args.Add("?galioja_nuo", PaslaugosKainaVM.GaliojaNuo);
			args.Add("?galioja_iki", PaslaugosKainaVM.GaliojaIki);
			args.Add("?kaina", PaslaugosKainaVM.Kaina);
		});
	}
}
