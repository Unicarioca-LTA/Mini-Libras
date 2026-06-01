using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using System;

public class CarregarPersonagemCores : MonoBehaviour
{

    private string corPele, cabelo, camisa, aparelho, oculoss, sexo;
    private string nome;
    private int idGlobal;
    private Text txtIdSecreto;

    private GameObject personagem, roupa, cabeca, braco, cabeloImg, franja, aparelhoImg, oculosImg;


    //Banco de Dados
    private string databaseName;
    private string databasePath;

    private SqliteConnection Connection => new SqliteConnection($"Data Source = {this.databasePath};");

    void Awake()
    {
        this.databaseName = "dbLibras.db";
        this.databasePath = Path.Combine(Application.persistentDataPath, this.databaseName);
        txtIdSecreto = GameObject.FindWithTag("id").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PersonagemLoading();
        RetornarPersonagem();
        CarregaPersonagem();
    }

    public void PersonagemLoading()
    {
        using (var dbConnection = Connection)
        {
            dbConnection.Open();

            using (var dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT id FROM loading";

                dbCmd.CommandText = sqlQuery;

                using (var reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idGlobal = reader.GetInt32(0);

                        Debug.Log("idGlobal = " + idGlobal);

                    }
                    txtIdSecreto.text = idGlobal.ToString();
                }
            }
        }
    }

    public void RetornarPersonagem()
    {
        using (var dbConnection = Connection)
        {
            dbConnection.Open();

            using (var dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM crianca where id =" + idGlobal;

                dbCmd.CommandText = sqlQuery;

                using (var reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idLocal = reader.GetInt32(0);
                        nome = reader.GetString(1);
                        cabelo = reader.GetString(2);
                        corPele = reader.GetString(3);
                        oculoss = reader.GetString(4);
                        aparelho = reader.GetString(5);
                        camisa = reader.GetString(6);
                        sexo = reader.GetString(7);

                        Debug.Log("idLocal = " + idLocal);
                        Debug.Log(corPele);
                        Debug.Log(nome);
                    }
                }
            }
        }
    }

    public void CarregaPersonagem()
    {
        CarregarCorpo();
        CarregarCabelo();
        CarregarRoupa();
        CarregarAcess();
    }

    private void CarregarCorpo()
    {
        switch (corPele)
        {
            case "1":
                if (sexo == "1") //menina
                {
                    //corpo
                    personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/personagem1");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/cabecaGeral/Cabeca1");
                    cabeca.SetActive(true);
                    //bracos
                    braco = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/bracos/braco1");
                    braco.SetActive(true);
                }
                else
                {
                    personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/personagem1");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/cabecaGeral/Cabeca1");
                    cabeca.SetActive(true);
                    //bracos
                    braco = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/bracos/braco1");
                    braco.SetActive(true);
                }
                break;
            case "2":
                if (sexo == "1") //menina
                {
                    personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/personagem2");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/cabecaGeral/Cabeca2");
                    cabeca.SetActive(true);
                    //bracos
                    braco = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/bracos/braco2");
                    braco.SetActive(true);
                }
                else
                {
                    personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/personagem2");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/cabecaGeral/Cabeca2");
                    cabeca.SetActive(true);
                    //bracos
                    braco = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/bracos/braco2");
                    braco.SetActive(true);
                }
                break;
            case "3":
                if (sexo == "1") //menina
                {
                    personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/personagem3");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/cabecaGeral/Cabeca3");
                    cabeca.SetActive(true);
                    //bracos
                    braco = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/bracos/braco3");
                    braco.SetActive(true);

                }
                else
                {
                    personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/personagem3");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/cabecaGeral/Cabeca3");
                    cabeca.SetActive(true);
                    //bracos
                    braco = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/bracos/braco3");
                    braco.SetActive(true);
                    
                }
                break;
        }
    }

    private void CarregarCabelo()
    {
        switch (cabelo)
        {
            case "1":
                if (sexo == "1") //menina
                {
                    cabeloImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/imgCabelo/Cabelo1");
                    cabeloImg.SetActive(true);

                    //franja
                    franja = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/imgFranja/Franja1");
                    franja.SetActive(true);
                }
                else
                {
                    cabeloImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/imgCabelo/Cabelo1");
                    cabeloImg.SetActive(true);
                }
                break;
            case "2":
                if (sexo == "1") //menina
                {
                    cabeloImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/imgCabelo/Cabelo2");
                    cabeloImg.SetActive(true);
                    //franja
                    franja = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/imgFranja/Franja2");
                    franja.SetActive(true);
                }
                else
                {
                    cabeloImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/imgCabelo/Cabelo2");
                    cabeloImg.SetActive(true);
                }
                break;
            case "3":
                if (sexo == "1") //menina
                {
                    cabeloImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/imgCabelo/Cabelo3");
                    cabeloImg.SetActive(true);
                    //franja
                    franja = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/imgFranja/Franja3");
                    franja.SetActive(true);
                }
                else
                {
                    cabeloImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/cabecaGeral/Cabelo3");
                    cabeloImg.SetActive(true);
                    //franja
                    franja = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/imgFranja/Franja1");
                    franja.SetActive(true);
                }
                break;
        }
    }

    private void CarregarRoupa()
    {
        switch (camisa)
        {
            case "1":
                if (sexo == "1") //menina
                {
                    roupa = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/imgRoupaGeral/imgRoupa1");
                    roupa.SetActive(true);

                }
                else
                {
                    roupa = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/imgRoupaGeral/imgRoupa1");
                    roupa.SetActive(true);
                }
                break;
            case "2":
                if (sexo == "1") //menina
                {
                    roupa = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/imgRoupaGeral/imgRoupa2");
                    roupa.SetActive(true);

                }
                else
                {
                    roupa = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/imgRoupaGeral/imgRoupa2");
                    roupa.SetActive(true);
                }

                break;
            case "3":
                if (sexo == "1") //menina
                {
                    roupa = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/imgRoupaGeral/imgRoupa3");
                    roupa.SetActive(true);

                }
                else
                {
                    roupa = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/imgRoupaGeral/imgRoupa3");
                    roupa.SetActive(true);

                }
                break;
            case "4":
                if (sexo == "1") //menina
                {
                    roupa = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/imgRoupaGeral/imgRoupa4");
                    roupa.SetActive(true);

                }
                else
                {
                    roupa = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/imgRoupaGeral/imgRoupa4");
                    roupa.SetActive(true);

                }
                break;
        }

    }

    private void CarregarAcess()
    {
        //Escolher Aparelho Auditivo
        switch (aparelho)
        {
            case "1":
                if (sexo == "1")
                {
                    aparelhoImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/AcessoriosImgs/imgAparelho1");
                    aparelhoImg.SetActive(true);
                }
                else
                {
                    aparelhoImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/AcessoriosImgs/imgAparelho1");
                    aparelhoImg.SetActive(true);
                }
                break;
            case "2":
                if (sexo == "1")
                {
                    aparelhoImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/AcessoriosImgs/imgAparelho2");
                    aparelhoImg.SetActive(true);
                }
                else
                {
                    aparelhoImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/AcessoriosImgs/imgAparelho2");
                    aparelhoImg.SetActive(true);
                }
                break;
            case "3":
                if (sexo == "1")
                {
                    aparelhoImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/AcessoriosImgs/imgAparelho3");
                    aparelhoImg.SetActive(true);
                }
                else
                {
                    aparelhoImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/AcessoriosImgs/imgAparelho3");
                    aparelhoImg.SetActive(true);
                }
                break;
        }

        //Escolher Óculos
        if (oculoss == "1")
        {
            if (sexo == "1")
            {
                oculosImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/AcessoriosImgs/imgOculos");
                oculosImg.SetActive(true);
            }
            else
            {
                oculosImg = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/AcessoriosImgs/imgOculos");
                oculosImg.SetActive(true);
            }

        }
    }

    

}
