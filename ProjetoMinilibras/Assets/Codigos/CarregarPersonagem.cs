
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.UI;

public class CarregarPersonagem : MonoBehaviour
{
    public static int personagemID;
    public bool roupas;
    private string corPele, cabelo, camisa, aparelho, oculoss, sexo;
    private string nome;
    private int idGlobal;
    private GameObject personagem, roupa, cabeca, cabeloImg, franja, aparelhoImg, oculosImg;


    //Banco de Dados
    private string databaseName;
    private string databasePath;

    private SqliteConnection Connection => new SqliteConnection($"Data Source = {this.databasePath};");

    // Start is called before the first frame update
    void Start()
    {
        this.databaseName = "dbLibras.db";
        this.databasePath = Path.Combine(Application.persistentDataPath, this.databaseName);
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

                        personagemID = idGlobal;

                        Debug.Log("idGlobal = "+idGlobal);
                        
                    }
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

                        Debug.Log("idLocal = "+idLocal);
                        Debug.Log(corPele);
                        Debug.Log(nome);

                    }
                }
            }
        }
    }

    public void CarregaPersonagem()
    {
        if (roupas == false)
        {
            if (sexo == "1")
            {
                roupa = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/Roupa");
                roupa.SetActive(true);
                switch (corPele)
                {
                    case "1":
                        personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/Corpo1");
                        personagem.SetActive(true);
                        cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/cabecaGeral/Cabeca1");
                        cabeca.SetActive(true);
                        break;
                    case "2":
                        personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/Corpo2");
                        personagem.SetActive(true);
                        cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/cabecaGeral/Cabeca2");
                        cabeca.SetActive(true);
                        break;
                    case "3":
                        personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/Corpo3");
                        personagem.SetActive(true);
                        cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menina/cabecaGeral/Cabeca3");
                        cabeca.SetActive(true);
                        break;
                }
            }
            else
            {
                roupa = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/Roupa");
                roupa.SetActive(true);
                switch (corPele)
                {
                    case "1":
                        personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/Corpo1");
                        personagem.SetActive(true);
                        cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/cabecaGeral/Cabeca1");
                        cabeca.SetActive(true);
                        break;
                    case "2":
                        personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/Corpo2");
                        personagem.SetActive(true);
                        cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/cabecaGeral/Cabeca2");
                        cabeca.SetActive(true);
                        break;
                    case "3":
                        personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/Corpo3");
                        personagem.SetActive(true);
                        cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/cabecaGeral/Cabeca3");
                        cabeca.SetActive(true);
                        break;
                }
            }
        }
        else
        {
            CarregarCorpo();
            CarregarRoupa();
        }
        CarregarCabelo();
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
                }
                else
                {
                    personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/personagem1");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/cabecaGeral/Cabeca1");
                    cabeca.SetActive(true);
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
                }
                else
                {
                    personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/personagem2");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/cabecaGeral/Cabeca2");
                    cabeca.SetActive(true);
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
                }
                else
                {
                    personagem = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/personagem3");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Panel menino/cabecaGeral/Cabeca3");
                    cabeca.SetActive(true);
                }
                break;
        }
    }

    private void CarregarCabelo() {
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
                if (sexo == "1") {
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
