using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using System;
public class VerificarBanco : MonoBehaviour
{
    public GameObject HistoricoOn, HistoricoOff, CriarOn, CriarOff;
    public GameObject ContinuarOn, ContinuarOff;
    private string databaseName;
    private string databasePath;

    private SqliteConnection Connection => new SqliteConnection($"Data Source = {this.databasePath};");

    // Start is called before the first frame update
    void Start()
    {
        this.databaseName = "dbLibras.db";
        this.databasePath = Path.Combine(Application.persistentDataPath, this.databaseName);
        print("Banco de dados Path: " + this.databasePath);
        habilitarBotoes();
    }
    private void habilitarBotoes()
    {
        using (var dbConnection = Connection)
        {
            dbConnection.Open();

            using (var dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "select id from crianca order by id asc limit 1";
                int id = 0;
                dbCmd.CommandText = sqlQuery;

                using (var reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }

                    if (id == 0)
                    {
                        HistoricoOn.SetActive(false);
                        ContinuarOn.SetActive(false);
                        CriarOn.SetActive(false);
                        HistoricoOff.SetActive(true);
                        ContinuarOff.SetActive(true);
                        CriarOff.SetActive(true);
                    }
                    else
                    {
                        HistoricoOn.SetActive(true);
                        ContinuarOn.SetActive(true);
                        CriarOn.SetActive(true);
                        HistoricoOff.SetActive(false);
                        ContinuarOff.SetActive(false);
                        CriarOff.SetActive(false);
                    }
                }
            }
        }
    }
}
