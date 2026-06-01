using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using UnityEngine.UI;

public class BancoPersonagens : MonoBehaviour
{
    public GameObject membro1, membro2, cabelo1, cabelo2, cabelo3, roupa1, roupa2, roupa3, apr1, apr2, apr3, oculos;
    public InputField input;
    char corPele, cabelo, camisa, aparelho, oculoss='0',sex;
    string nome;
    public bool sexo;
    private int idGlobal;

    //Banco de Dados
    private string databaseName;
    private string databasePath;
    private SqliteConnection Connection => new SqliteConnection($"Data Source = {this.databasePath};");

    public void getMembros()
    {
        if ((membro1.activeSelf == true))
        {
            corPele = '1';
        }
        else if ((membro2.activeSelf == true))
        {
            corPele = '2';
        }else
        {
            corPele = '3';
        }
    }

    public void getCabelo()
    {
        if ((cabelo1.activeSelf == true))
        {
            cabelo = '1';
        }
        else if ((cabelo2.activeSelf == true))
        {
            cabelo = '2';
        }
        else if ((cabelo3.activeSelf == true))
        {
            cabelo = '3';
        }
        else
        {
            cabelo = '0';
        }

    }

    public void getRoupa()
    {
        if ((roupa1.activeSelf == true))
        {
            camisa = '1';
        }
        else if ((roupa2.activeSelf == true))
        {
            camisa = '2';
        }
        else if ((roupa3.activeSelf == true))
        {
            camisa = '3';
        }
        else
        {
            camisa = '4';
        }
    }

    public void getAcess()
    {
        if ((oculos.activeSelf == true))
        {
            oculoss = '1';
        }

        //aparelho
        if ((apr1.activeSelf == true))
        {
            aparelho = '1';
        }
        else if ((apr2.activeSelf == true))
        {
            aparelho = '2';
        }
        else if ((apr3.activeSelf == true))
        {
            aparelho = '3';
        }
        else
        {
            aparelho = '0';
        }
    }

    public void guardarPersonagem() {

        nome = input.text;
        if (sexo)
        {
            sex = '1'; //menina
        }
        else
        {
            sex = '2'; ////menino
        }

        this.databaseName = "dbLibras.db";
        this.databasePath = Path.Combine(Application.persistentDataPath, this.databaseName);

        using (var dbConnection = Connection)
        {
            dbConnection.Open();

            using (var dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO crianca (nome, cabelo, corPele, oculos, aparelho, camisa, sexo) VALUES(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\")", nome, cabelo, corPele, oculoss, aparelho, camisa, sex);
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteNonQuery();

                acharId();

                string sqlQuery2 = String.Format("UPDATE loading SET ID = " + idGlobal);

                dbCmd.CommandText = sqlQuery2;
                dbCmd.ExecuteNonQuery();
            }
        }
    }
    
    public void acharId()
    {
        using (var dbConnection = Connection)
        {
            dbConnection.Open();

            using (var dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT MAX(id) FROM crianca";

                dbCmd.CommandText = sqlQuery;

                using (var reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idGlobal = reader.GetInt32(0);

                        Debug.Log("idGlobal = " + idGlobal);

                    }
                }
            }
        }
    }
}
