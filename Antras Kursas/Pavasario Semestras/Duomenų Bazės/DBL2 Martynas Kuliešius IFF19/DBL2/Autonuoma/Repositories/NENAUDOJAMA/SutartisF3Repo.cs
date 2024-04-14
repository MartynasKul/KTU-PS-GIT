namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Newtonsoft.Json;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.SutartisF3;


/// <summary>
/// Database operations related to 'Sutartis' entity.
/// </summary>
public class SutartisF3Repo
{
	public static List<SutartisL> ListSutartis()
	{
		var query =
			$@"SELECT
				s.nr,
				s.sutarties_data as data,
				CONCAT(d.vardas,' ', d.pavarde) as darbuotojas,
				CONCAT(n.vardas,' ',n.pavarde) as nuomininkas,
				b.name as busena
			FROM
				`{Config.TblPrefix}sutartys` s
				LEFT JOIN `{Config.TblPrefix}darbuotojai` d ON s.fk_darbuotojas=d.tabelio_nr
				LEFT JOIN `{Config.TblPrefix}klientai` n ON s.fk_klientas=n.asmens_kodas
				LEFT JOIN `{Config.TblPrefix}sutarties_busenos` b ON s.busena=b.id
			ORDER BY s.nr DESC";

		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<SutartisL>(drc, (dre, t) => {
				t.Nr = dre.From<int>("nr");
				t.Darbuotojas = dre.From<string>("darbuotojas");
				t.Nuomininkas = dre.From<string>("nuomininkas");
				t.Data = dre.From<DateTime>("data");
				t.Busena = dre.From<string>("busena");
			});

		return result;
	}

	public static SutartisCE FindSutartisCE(int nr)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}sutartys` WHERE nr=?nr";
		var drc =
			Sql.Query(query, args => {
				args.Add("?nr", nr);
			});

		var result =
			Sql.MapOne<SutartisCE>(drc, (dre, t) => {
				//make a shortcut
				var sut = t.Sutartis;

				//
				sut.Nr = dre.From<int>("nr");
				sut.SutartiesData = dre.From<DateTime>("sutarties_data");
				sut.NuomosDataLaikas = dre.From<DateTime>("nuomos_data_laikas");
				sut.PlanuojamaGrDataLaikas = dre.From<DateTime>("planuojama_grazinimo_data_laikas");
				sut.FaktineGrDataLaikas = dre.From<DateTime?>("faktine_grazinimo_data_laikas");
				sut.PradineRida = dre.From<int>("pradine_rida");
				sut.GalineRida = dre.From<int?>("galine_rida");
				sut.Kaina = dre.From<decimal>("kaina");
				sut.DegaluKiekisPaimant = dre.From<int>("degalu_kiekis_paimant");
				sut.DegaluKiekisGrazinant = dre.From<int?>("dagalu_kiekis_grazinus");
				sut.FkBusena = dre.From<int>("busena");
				sut.FkKlientas = dre.From<string>("fk_klientas");
				sut.FkDarbuotojas = dre.From<string>("fk_darbuotojas");
				sut.FkAutomobilis = dre.From<int>("fk_automobilis");
				sut.FkGrazinimoVieta = dre.From<int>("fk_grazinimo_vieta");
				sut.FkPaemimoVieta = dre.From<int>("fk_paemimo_vieta");
			});

		return result;
	}

	public static int InsertSutartis(SutartisCE sutCE)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}sutartys`
			(
				`sutarties_data`,
				`nuomos_data_laikas`,
				`planuojama_grazinimo_data_laikas`,
				`faktine_grazinimo_data_laikas`,
				`pradine_rida`,
				`galine_rida`,
				`kaina`,
				`degalu_kiekis_paimant`,
				`dagalu_kiekis_grazinus`,
				`busena`,
				`fk_klientas`,
				`fk_darbuotojas`,
				`fk_automobilis`,
				`fk_grazinimo_vieta`,
				`fk_paemimo_vieta`
			)
			VALUES(
				?sutdata,
				?nuomdata,
				?plgrlaikas,
				?fkgrlaikas,
				?prrida,
				?glrida,
				?kaina,
				?dkiekispa,
				?dkiekisgr,
				?busena,
				?klientas,
				?darbuotojas,
				?automobilis,
				?grvieta,
				?pavieta
			)";

		var nr =
			Sql.Insert(query, args => {
				//make a shortcut
				var sut = sutCE.Sutartis;

				//
				args.Add("?sutdata", sut.SutartiesData);
				args.Add("?nuomdata", sut.NuomosDataLaikas);
				args.Add("?plgrlaikas", sut.PlanuojamaGrDataLaikas);
				args.Add("?fkgrlaikas", sut.FaktineGrDataLaikas);
				args.Add("?prrida", sut.PradineRida);
				args.Add("?glrida", sut.GalineRida);
				args.Add("?kaina", sut.Kaina);
				args.Add("?dkiekispa", sut.DegaluKiekisPaimant);
				args.Add("?dkiekisgr", sut.DegaluKiekisGrazinant);
				args.Add("?busena", sut.FkBusena);
				args.Add("?darbuotojas", sut.FkDarbuotojas);
				args.Add("?klientas", sut.FkKlientas);
				args.Add("?automobilis", sut.FkAutomobilis);
				args.Add("?grvieta", sut.FkGrazinimoVieta);
				args.Add("?pavieta", sut.FkPaemimoVieta);
			});

		return (int)nr;
	}

	public static void UpdateSutartis(SutartisCE sutCE)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}sutartys`
			SET
				`sutarties_data` = ?sutdata,
				`nuomos_data_laikas` = ?nuomdata,
				`planuojama_grazinimo_data_laikas` = ?plgrlaikas,
				`faktine_grazinimo_data_laikas` = ?fkgrlaikas,
				`pradine_rida` = ?prrida,
				`galine_rida` = ?glrida,
				`kaina` = ?kaina,
				`degalu_kiekis_paimant` = ?dkiekispa,
				`dagalu_kiekis_grazinus` = ?dkiekisgr,
				`busena` = ?busena,
				`fk_klientas` = ?klientas,
				`fk_darbuotojas` = ?darbuotojas,
				`fk_automobilis` = ?automobilis,
				`fk_grazinimo_vieta` = ?grvieta,
				`fk_paemimo_vieta` = ?pavieta
			WHERE nr=?nr";

		Sql.Update(query, args => {
			//make a shortcut
			var sut = sutCE.Sutartis;

			//
			args.Add("?sutdata", sut.SutartiesData);
			args.Add("?nuomdata", sut.NuomosDataLaikas);
			args.Add("?plgrlaikas", sut.PlanuojamaGrDataLaikas);
			args.Add("?fkgrlaikas", sut.FaktineGrDataLaikas);
			args.Add("?prrida", sut.PradineRida);
			args.Add("?glrida", sut.GalineRida);
			args.Add("?kaina", sut.Kaina);
			args.Add("?dkiekispa", sut.DegaluKiekisPaimant);
			args.Add("?dkiekisgr", sut.DegaluKiekisGrazinant);
			args.Add("?busena", sut.FkBusena);
			args.Add("?darbuotojas", sut.FkDarbuotojas);
			args.Add("?klientas", sut.FkKlientas);
			args.Add("?automobilis", sut.FkAutomobilis);
			args.Add("?grvieta", sut.FkGrazinimoVieta);
			args.Add("?pavieta", sut.FkPaemimoVieta);

			args.Add("?nr", sut.Nr);
		});
	}

	public static void DeleteSutartis(int nr)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}sutartys` where nr=?nr";
		Sql.Delete(query, args => {
			args.Add("?nr", nr);
		});
	}

	public static List<SutartiesBusena> ListSutartiesBusena()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}sutarties_busenos` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<SutartiesBusena>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Name = dre.From<string>("name");
			});

		return result;
	}

	public static List<UzsakytaPaslaugaM> ListUzsakytaPaslauga(int sutartisId)
	{
		var query =
			$@"SELECT 
				up.fk_paslauga AS up_fk_paslauga,
				up.fk_kaina_galioja_nuo AS up_fk_kaina_galioja_nuo,
				up.kiekis AS up_kiekis,
				up.kaina AS up_kaina,
				p.pavadinimas AS p_pavadinimas,
				pk.kaina AS pk_kaina,
				pk.galioja_nuo AS pk_galioja_nuo
			FROM 
				`{Config.TblPrefix}uzsakytos_paslaugos` AS up
				LEFT JOIN `{Config.TblPrefix}paslaugos` AS p 
					ON up.fk_paslauga=p.id
				LEFT JOIN `{Config.TblPrefix}paslaugu_kainos` as pk
					ON pk.fk_paslauga=p.id AND pk.galioja_nuo=up.fk_kaina_galioja_nuo
			WHERE up.fk_sutartis = ?sutartisId
			ORDER BY up.fk_paslauga ASC, up.fk_kaina_galioja_nuo ASC";

		var drc =
			Sql.Query(query, args => {
				args.Add("?sutartisId", sutartisId);
			});

		var result =
			Sql.MapAll<UzsakytaPaslaugaM>(drc, (dre, t) => {
				t.FkPaslaugosKaina =
					//we use JSON here to make serialization/deserializaton of composite key more convenient
					JsonConvert.SerializeObject(new {
						FkPaslauga = dre.From<int>("up_fk_paslauga"),
						GaliojaNuo = dre.From<DateTime>("up_fk_kaina_galioja_nuo")
					});				
				t.Kiekis = dre.From<int>("up_kiekis");
				t.Kaina = dre.From<decimal>("up_kaina");

				var serviceName = dre.From<string>("p_pavadinimas");
				var unitPrice = dre.From<decimal>("pk_kaina");
				var unitPriceValidFrom = dre.From<DateTime>("pk_galioja_nuo");				
				t.Pavadinimas = $"{serviceName} {unitPrice} EUR ({unitPriceValidFrom.ToString("yyyy-MM-dd")})";
			});

		return result;
	}

	public static void InsertUzsakytaPaslauga(int sutartisId, UzsakytaPaslaugaM up)
	{
		//deserialize 'PaslaugosKaina' foreign keys from 'UzsakytaPaslauga' view model key
		var fks =
			JsonConvert.DeserializeAnonymousType(
				up.FkPaslaugosKaina,
				//this creates object of correct shape that is filled in by the JSON deserializer
				new {
					FkPaslauga = 1,
					GaliojaNuo = DateTime.Now
				}
			);

		//
		var query =
			$@"INSERT INTO `{Config.TblPrefix}uzsakytos_paslaugos`
				(
					fk_sutartis,
					fk_kaina_galioja_nuo,
					fk_paslauga,
					kiekis,
					kaina
				)
				VALUES(
					?fk_sutartis,
					?galioja_nuo,
					?fk_paslauga,
					?kiekis,
					?kaina
				)";

		Sql.Insert(query, args => {
			args.Add("?fk_sutartis", sutartisId);
			args.Add("?galioja_nuo", fks.GaliojaNuo);
			args.Add("?fk_paslauga", fks.FkPaslauga);
			args.Add("?kaina", up.Kaina);
			args.Add("?kiekis", up.Kiekis);
		});
	}

	public static void DeleteUzsakytaPaslaugaForSutartis(int sutartis)
	{
		var query =
			$@"DELETE FROM a
			USING `{Config.TblPrefix}uzsakytos_paslaugos` as a
			WHERE a.fk_sutartis=?fkid";

		Sql.Delete(query, args => {
			args.Add("?fkid", sutartis);
		});
	}
}