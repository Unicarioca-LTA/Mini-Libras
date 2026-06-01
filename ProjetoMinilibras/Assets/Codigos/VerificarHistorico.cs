using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using System;

public class VerificarHistorico : MonoBehaviour
{
    public GameObject setaDireita;

    //Conexão com o Banco
    private string databaseName;
    private string databasePath;

    private SqliteConnection Connection => new SqliteConnection($"Data Source = {this.databasePath};");

    // Start is called before the first frame update
    void Start()
    {
        this.databaseName = "dbLibras.db";
        this.databasePath = Path.Combine(Application.persistentDataPath, this.databaseName);

        using (var dbConnection = Connection)
        {
            dbConnection.Open();
            using (var dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery;
                int n = 0;

                sqlQuery = "SELECT COUNT(id) from crianca";
                dbCmd.CommandText = sqlQuery;

                using (var reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        n = reader.GetInt32(0);
                    }

                    if (n == 1)
                    {
                        setaDireita.SetActive(false);
                    }
                }
            }
        }
    }
}

