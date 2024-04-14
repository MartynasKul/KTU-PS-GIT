namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;


/// <summary>
/// Database operations related to 'Darbuotojas' entity.
/// </summary>
public class DarbuotojasRepo
{
	public static List<Darbuotojas> List()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}darbuotojas` ORDER BY tabelio_nr ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Darbuotojas>(drc, (dre, t) => {
				t.Tabelis = dre.From<int>("tabelio_nr");
				t.Vardas = dre.From<string>("vardas");
				t.Pavarde = dre.From<string>("pavarde");
				t.fk_Automobiliu_Atstovybeid=dre.From<int>("fk_Automobiliu_Atstovybeid");
			});

		return result;
	}

	public static Darbuotojas Find(int tabnr)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}darbuotojas` WHERE tabelio_nr=?tab";

		var drc = 
			Sql.Query(query, args => {
				args.Add("?tab", tabnr);
			});

		if( drc.Count > 0 )
		{
			var result = 
				Sql.MapOne<Darbuotojas>(drc, (dre, t) => {
					t.Tabelis = dre.From<int>("tabelio_nr");
					t.Vardas = dre.From<string>("vardas");
					t.Pavarde = dre.From<string>("pavarde");
				});
			
			return result;
		}

		return null;
	}

	public static void Update(Darbuotojas darb)
	{						
		var query = 
			$@"UPDATE `{Config.TblPrefix}darbuotojas`
			SET 
				vardas=?vardas, 
				pavarde=?pavarde,
				fk_Automobiliu_Atstovybeid=?atstovybe

			WHERE 
				tabelio_nr=?tab";

		Sql.Update(query, args => {
			args.Add("?vardas", darb.Vardas);
			args.Add("?pavarde", darb.Pavarde);
			args.Add("?tab", darb.Tabelis.ToString());
			args.Add("?atstovybe", darb.fk_Automobiliu_Atstovybeid.ToString());
		});				
	}

	public static void Insert(Darbuotojas darb)
	{							
		var query = 
			$@"INSERT INTO `{Config.TblPrefix}darbuotojas`
			(
				tabelio_nr,
				vardas,
				pavarde
				
			)
			VALUES(
				?tabelio_nr,
				?vardas,
				?pavarde

			)";

		Sql.Insert(query, args => {
			args.Add("?vardas", darb.Vardas);
			args.Add("?pavarde", darb.Pavarde);
			args.Add("?tabelio_nr", darb.Tabelis);
			args.Add("?tabelio_nr", darb.fk_Automobiliu_Atstovybeid);
		});				
	}

	public static void Delete(int id)
	{			
		var query = $@"DELETE FROM `{Config.TblPrefix}darbuotojas` WHERE tabelio_nr=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});			
	}
}
