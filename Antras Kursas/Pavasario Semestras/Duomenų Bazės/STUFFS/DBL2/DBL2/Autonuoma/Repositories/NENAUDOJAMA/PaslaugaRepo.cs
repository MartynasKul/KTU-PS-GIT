namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Paslauga;


/// <summary>
/// Database operations related to 'Paslauga' entity.
/// </summary>
public class PaslaugaRepo
{
	public static List<Paslauga> List()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}paslaugos` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Paslauga>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Aprasymas = dre.From<string>("aprasymas");
			});

		return result;
	}

	public static Paslauga Find(int id)
	{
		var query = 
			$@"SELECT 
				a.id,
				a.pavadinimas,
				a.aprasymas
			FROM `{Config.TblPrefix}paslaugos` a
			WHERE a.id=?id";

		var drc = 
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result = 
			Sql.MapOne<Paslauga>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Aprasymas = dre.From<string>("aprasymas");
			});

		return result;
	}

	public static void Update(Paslauga paslauga)
	{
		var query = 
			$@"UPDATE `{Config.TblPrefix}paslaugos` p 
			SET p.pavadinimas=?pavadinimas, p.aprasymas=?aprasymas 
			WHERE p.id=?id";

		Sql.Update(query, args => {
			args.Add("?id", paslauga.Id);
			args.Add("?pavadinimas", paslauga.Pavadinimas);
			args.Add("?aprasymas", paslauga.Aprasymas);
		});
	}

	public static int Insert(Paslauga paslauga)
	{
		string query = 
			$@"INSERT INTO `{Config.TblPrefix}paslaugos`
			(pavadinimas,aprasymas)
			VALUES
			(?pavadinimas,?aprasymas)";

		var id =
			(int)Sql.Insert(query, args => {
				args.Add("?pavadinimas", paslauga.Pavadinimas);
				args.Add("?aprasymas", paslauga.Aprasymas);
			});

		return (int)id;
	}

	public static void Delete(int id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}paslaugos` WHERE id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}
}
