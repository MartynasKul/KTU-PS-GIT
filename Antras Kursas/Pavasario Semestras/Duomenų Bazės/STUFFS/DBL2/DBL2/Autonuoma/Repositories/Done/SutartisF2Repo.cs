namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Newtonsoft.Json;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.SutartisF2;


/// <summary>
/// Database operations related to 'Sutartis' entity.
/// </summary>
public class SutartisF2Repo
{
	public static List<SutartisL> ListSutartis()
	{
		var query =
			$@"SELECT
				s.nr,
				s.sutarties_data as data,
				CONCAT(d.vardas,' ', d.pavarde) as darbuotojas,
				CONCAT(n.vardas,' ',n.pavarde) as pirkejas

			FROM
				`{Config.TblPrefix}pardavimo_sutartis` s
				LEFT JOIN `{Config.TblPrefix}darbuotojas` d ON s.fk_DARBUOTOJAStabelio_nr = d.tabelio_nr,
				LEFT JOIN `{Config.TblPrefix}klientas` n ON s.fk_KLIENTASasmens_kodas = n.asmens_kodas

			ORDER BY s.nr DESC";

		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<SutartisL>(drc, (dre, t) => {
				t.Nr = dre.From<int>("nr");
				t.fk_DARBUOTOJAStabelio_nr = dre.From<int>("darbuotojas");
				t.fk_KLIENTASasmens_kodas = dre.From<int>("pirkejas");
				t.Data = dre.From<DateTime>("data");
			});

		return result;
	}

	public static SutartisCE FindSutartisCE(int nr)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}pardavimo_sutartis` WHERE nr=?nr";
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
				sut.Data = dre.From<DateTime>("sutarties_data");
				sut.PradRida = dre.From<int>("pradine_rida");
				sut.Kaina = dre.From<decimal>("kaina");
				sut.PirkTipas = dre.From<string>("pirkimo_tipas");
				sut.Defektai = dre.From<string>("defektai");
				sut.fk_Automobiliu_SalonasASid = dre.From<int>("fk_Automobiliu_SalonasASid ");
				sut.fk_AUTOMOBILISid = dre.From<int>("fk_AUTOMOBILISid");
				sut.fk_DARBUOTOJAStabelio_nr = dre.From<int>("fk_DARBUOTOJAStabelio_nr");
				sut.fk_KLIENTASasmens_kodas = dre.From<int>("fk_KLIENTASasmens_kodas");

			});

		return result;
	}

	public static int InsertSutartis(SutartisCE sutCE)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}pardavimo_sutartis`
			(
				`nr`,
				`sutarties_data`,
				`pradine_rida`,
				`kaina`,
				`pirkimo_tipas`,
				`defektai`,
				`fk_Automobiliu_SalonasASid`,
				`fk_AUTOMOBILISid`,
				`fk_DARBUOTOJAStabelio_nr`,
				`fk_KLIENTASasmens_kodas`
			)
			VALUES(
				?nr,
				?sutarties_data,
				?pradine_rida,
				?kaina,
				?pirkimo_tipas,
				?defektai,
				?fk_Automobiliu_SalonasASid,
				?fk_AUTOMOBILISid,
				?fk_DARBUOTOJAStabelio_nr,
				?fk_KLIENTASasmens_kodas

			)";

		var nr =
			Sql.Insert(query, args => {
				//make a shortcut
				var sut = sutCE.Sutartis;

				//
				args.Add("?nr", sut.Nr);
				args.Add("?sutarties_data", sut.Data);
				args.Add("?pradine_rida", sut.PradRida);
				args.Add("?kaina", sut.Kaina);
				args.Add("?pirkimo_tipas", sut.PirkTipas);
				args.Add("?defektai", sut.Defektai);
				args.Add("?fk_Automobiliu_SalonasASid", sut.fk_Automobiliu_SalonasASid);
				args.Add("?fk_AUTOMOBILISid", sut.fk_AUTOMOBILISid);
				args.Add("?fk_DARBUOTOJAStabelio_nr", sut.fk_DARBUOTOJAStabelio_nr);
				args.Add("?fk_KLIENTASasmens_kodas", sut.fk_KLIENTASasmens_kodas);
				
			});

		return (int)nr;
	}

	public static void UpdateSutartis(SutartisCE sutCE)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}pardavimo_sutartis`
			SET
				`sutarties_data` = ?sutarties_data,
				`pradine_rida` = ?pradine_rida,
				`kaina` = ?kaina,
				`pirkimo_tipas` = ?pirkimo_tipas,
				`defektai` = ?defektai,
				`fk_Automobiliu_SalonasASid` = ?fk_Automobiliu_SalonasASid,
				`fk_AUTOMOBILISid` = ?fk_AUTOMOBILISid,
				`fk_DARBUOTOJAStabelio_nr` = ?fk_DARBUOTOJAStabelio_nr,
				`fk_KLIENTASasmens_kodas` = ?fk_KLIENTASasmens_kodas,
			WHERE nr=?nr";

		Sql.Update(query, args => {
			//make a shortcut
			var sut = sutCE.Sutartis;

			//
			args.Add("?sutarties_data", sut.Data);
			args.Add("?pradine_rida", sut.PradRida);
			args.Add("?kaina", sut.Kaina);
			args.Add("?pirkimo_tipas", sut.PirkTipas);
			args.Add("?defektai", sut.Defektai);
			args.Add("?fk_Automobiliu_SalonasASid", sut.fk_Automobiliu_SalonasASid);
			args.Add("?fk_AUTOMOBILISid", sut.fk_AUTOMOBILISid);
			args.Add("?fk_DARBUOTOJAStabelio_nr", sut.fk_DARBUOTOJAStabelio_nr);
			args.Add("?fk_KLIENTASasmens_kodas", sut.fk_KLIENTASasmens_kodas);

			args.Add("?nr", sut.Nr);
		});
	}

	public static void DeleteSutartis(int nr)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}pardavimo_sutartis` where nr=?nr";
		Sql.Delete(query, args => {
			args.Add("?nr", nr);
		});
	}
/*
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

	public static List<SutartisCE.UzsakytaPaslaugaM> ListUzsakytaPaslauga(int sutartisId)
	{
		var query =
			$@"SELECT *
			FROM `{Config.TblPrefix}uzsakytos_paslaugos`
			WHERE fk_sutartis = ?sutartisId
			ORDER BY fk_paslauga ASC, fk_kaina_galioja_nuo ASC";

		var drc =
			Sql.Query(query, args => {
				args.Add("?sutartisId", sutartisId);
			});

		var result =
			Sql.MapAll<SutartisCE.UzsakytaPaslaugaM>(drc, (dre, t) => {
				t.Paslauga =
					//we use JSON here to make serialization/deserializaton of composite key more convenient
					JsonConvert.SerializeObject(new {
						FkPaslauga = dre.From<int>("fk_paslauga"),
						GaliojaNuo = dre.From<DateTime>("fk_kaina_galioja_nuo")
					});
				t.Kiekis = dre.From<int>("kiekis");
				t.Kaina = dre.From<decimal>("kaina");
			});

		for( int i = 0; i < result.Count; i++ )
			result[i].InListId = i;

		return result;
	}

	public static void InsertUzsakytaPaslauga(int sutartisId, SutartisCE.UzsakytaPaslaugaM up)
	{
		//deserialize 'Paslauga' foreign keys from 'UzsakytaPaslauga' view model key
		var fks =
			JsonConvert.DeserializeAnonymousType(
				up.Paslauga,
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
	}*/
}