namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;


/// <summary>
/// Database operations related to 'Klientas' entity.
/// </summary>
public class KlientasRepo
{
	public static List<Klientas> List()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}klientas` ORDER BY asmens_kodas ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Klientas>(drc, (dre, t) => {
				t.AsmensKodas = dre.From<string>("asmens_kodas");
				t.Vardas = dre.From<string>("vardas");
				t.Pavarde = dre.From<string>("pavarde");
				t.GimimoData = dre.From<DateTime>("gimimo_data");
				t.Telefonas = dre.From<string>("telefonas");
				t.Epastas = dre.From<string>("e_pastas");
			});

		return result;
	}

	public static Klientas Find(string asmkodas)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}klientas` WHERE asmens_kodas=?asmkodas";

		var drc =
			Sql.Query(query, args => {
				args.Add("?asmkodas", asmkodas);
			});

		if( drc.Count > 0 )
		{
			var result = 
				Sql.MapOne<Klientas>(drc, (dre, t) => {
					t.AsmensKodas = dre.From<string>("asmens_kodas");
					t.Vardas = dre.From<string>("vardas");
					t.Pavarde = dre.From<string>("pavarde");
					t.GimimoData = dre.From<DateTime>("gimimo_data");
					t.Telefonas = dre.From<string>("telefonas");
					t.Epastas = dre.From<string>("e_pastas");
				});

			return result;
		}

		return null;
	}

	public static void Insert(Klientas klientas)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}klientas`
			(
				asmens_kodas,
				vardas,
				pavarde,
				gimimo_data,
				telefonas,
				e_pastas
			)
			VALUES(
				?asmkod,
				?vardas,
				?pavarde,
				?gimdata,
				?tel,
				?email
			)";

		Sql.Insert(query, args => {
			args.Add("?asmkod", klientas.AsmensKodas);
			args.Add("?vardas", klientas.Vardas);
			args.Add("?pavarde", klientas.Pavarde);
			args.Add("?gimdata", klientas.GimimoData);
			args.Add("?tel", klientas.Telefonas);
			args.Add("?email", klientas.Epastas);
		});
	}

	public static void Update(Klientas klientas)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}klientas`
			SET
				vardas=?vardas,
				pavarde=?pavarde,
				gimimo_data=?gimdata,
				telefonas=?tel,
				e_pastas=?email
			WHERE
				asmens_kodas=?asmkod";

		Sql.Update(query, args => {
			args.Add("?asmkod", klientas.AsmensKodas);
			args.Add("?vardas", klientas.Vardas);
			args.Add("?pavarde", klientas.Pavarde);
			args.Add("?gimdata", klientas.GimimoData);
			args.Add("?tel", klientas.Telefonas);
			args.Add("?email", klientas.Epastas);
		});
	}

	public static void Delete(string id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}klientas` WHERE asmens_kodas=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}
}
