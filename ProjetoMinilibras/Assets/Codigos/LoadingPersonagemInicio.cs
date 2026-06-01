using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.IO;

public class LoadingPersonagemInicio : MonoBehaviour
{
    int globalSexo;
    public Text txtSexo, txtNome;
    public GameObject esq,dir;
    private string pessoa;
    private string nome;
    private int idGlobal;

    private GameObject proximo;

    //variaveis usadas no get do banco de dados
    private string corPele, cabelo, camisa, aparelho, oculoss, sexo;

    //variaveis do set do personagem no unity
    private GameObject personagem, roupa, cabeca, cabeloImg, franja, aparelhoImg, oculosImg;

    //Conexão com o Banco
    private string databaseName;
    private string databasePath;

    private SqliteConnection Connection => new SqliteConnection($"Data Source = {this.databasePath};");

    private void Awake()
    {
        this.databaseName = "dbLibras.db";
        this.databasePath = Path.Combine(Application.persistentDataPath, this.databaseName);
    }

    public void CarregarPersonagemStart()
    {
        using (var dbConnection = Connection)
        {
            dbConnection.Open();
            esq.SetActive(false);
            using (var dbCmd = dbConnection.CreateCommand())
            {
                Debug.Log(txtSexo.text);
                string sqlQuery;
                if (txtSexo.text.ToString() == "Menina")
                {
                    Debug.Log("Menina1");
                    pessoa = "Menina";
                    sqlQuery = "SELECT * FROM crianca where sexo = 1 ORDER BY ROWID ASC LIMIT 1";
                }
                else
                {
                    Debug.Log("Menino1");
                    pessoa = "Menino";
                    sqlQuery = "SELECT * FROM crianca where sexo = 2 ORDER BY ROWID ASC LIMIT 1";
                }
                
                dbCmd.CommandText = sqlQuery;

                using (var reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idGlobal = reader.GetInt32(0);
                        nome = reader.GetString(1);
                        cabelo = reader.GetString(2);
                        corPele = reader.GetString(3);
                        oculoss = reader.GetString(4);
                        aparelho = reader.GetString(5);
                        camisa = reader.GetString(6);
                        sexo = reader.GetString(7);

                        Debug.Log("idGlobal = " + idGlobal);
                        Debug.Log(corPele);
                        Debug.Log(nome);
                        CarregarPersonagem.personagemID = idGlobal;
                        CarregaPersonagem();
                        proximo = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/objetosGerais/setaEsquerda");
                        VerificarProximo(idGlobal, "esquerda");
                        proximo = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/objetosGerais/setaDireta");
                        VerificarProximo(idGlobal, "direita");
                    }
                }
            }
        }
    }

    public void CarregarSetaDireita()
    {
        using (var dbConnection = Connection)
        {
            dbConnection.Open();
            esq.SetActive(true);
            using (var dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery;

                Desativar();
                GameObject panelSexo = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina");

                int id = CarregarPersonagem.personagemID;

                if (panelSexo.activeSelf == true)
                {
                    Debug.Log("Menina2");
                    sqlQuery = "SELECT * FROM crianca where id > " + id+ " and sexo = 1 ORDER BY ROWID ASC LIMIT 1";
                }
                else
                {
                    Debug.Log("Menino2");
                    Debug.Log(pessoa);
                    sqlQuery = "SELECT * FROM crianca where id > " + id + "  and sexo = 2 ORDER BY ROWID ASC LIMIT 1";
                }

                dbCmd.CommandText = sqlQuery;

                using (var reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idGlobal = reader.GetInt32(0);
                        nome = reader.GetString(1);
                        cabelo = reader.GetString(2);
                        corPele = reader.GetString(3);
                        oculoss = reader.GetString(4);
                        aparelho = reader.GetString(5);
                        camisa = reader.GetString(6);
                        sexo = reader.GetString(7);

                        Debug.Log("idGlobal = " + idGlobal);
                        Debug.Log(corPele);
                        Debug.Log(nome);
                        CarregarPersonagem.personagemID = idGlobal;
                        CarregaPersonagem();
                    }
                    VerificarProximo(idGlobal,"direita");
                }
            }
        }
    }

    public void CarregarSetaEsquerda()
    {
        using (var dbConnection = Connection)
        {
            dbConnection.Open();
            dir.SetActive(true);
            using (var dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery;

                GameObject panelSexo = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina");
                Desativar();

                int id = CarregarPersonagem.personagemID;

                if (panelSexo.activeSelf == true)
                {
                    Debug.Log("Menina3");
                    sqlQuery = "SELECT * FROM crianca where id < " + id + " and sexo = 1 ORDER BY ROWID DESC LIMIT 1";
                }
                else
                {
                    Debug.Log("Menino3");
                    sqlQuery = "SELECT * FROM crianca where id < " + id + "  and sexo = 2 ORDER BY ROWID DESC LIMIT 1";
                }

                dbCmd.CommandText = sqlQuery;

                using (var reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idGlobal = reader.GetInt32(0);
                        nome = reader.GetString(1);
                        cabelo = reader.GetString(2);
                        corPele = reader.GetString(3);
                        oculoss = reader.GetString(4);
                        aparelho = reader.GetString(5);
                        camisa = reader.GetString(6);
                        sexo = reader.GetString(7); 

                        Debug.Log("idGlobal = " + idGlobal);
                        Debug.Log(corPele);
                        Debug.Log(nome);
                        CarregarPersonagem.personagemID = idGlobal;
                        CarregaPersonagem();
                    }
                    VerificarProximo(idGlobal, "esquerda");
                }
            }
        }
    }

    private void VerificarProximo(int id, string lado) 
    {
        int idG = 0;

        using (var dbConnection = Connection)
        {
            dbConnection.Open();

            using (var dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery;
                if (lado == "esquerda")
                {
                    sqlQuery = "SELECT id FROM crianca where id<" + id + " and sexo="+sexo+" ORDER BY ROWID ASC LIMIT 1";
                }
                else
                {
                    sqlQuery = "SELECT id FROM crianca where id>" + id + " and sexo=" + sexo + " ORDER BY ROWID ASC LIMIT 1";
                }

                dbCmd.CommandText = sqlQuery;

                using (var reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idG  = reader.GetInt32(0);  
                    }
                    if (idG==0)
                    {
                        proximo.SetActive(false);
                    }
                }
            }
        }
    }

    public void Desativar()
    {
        //corpo
        personagem = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/personagem1");
        personagem.SetActive(false);
        //cabeca
        cabeca = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/cabecaGeral/Cabeca1");
        cabeca.SetActive(false);
        personagem = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/personagem1");
        personagem.SetActive(false);
        //cabeca
        cabeca = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/cabecaGeral/Cabeca1");
        cabeca.SetActive(false);
        personagem = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/personagem2");
        personagem.SetActive(false);
        //cabeca
        cabeca = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/cabecaGeral/Cabeca2");
        cabeca.SetActive(false);
        personagem = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/personagem2");
        personagem.SetActive(false);
        //cabeca
        cabeca = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/cabecaGeral/Cabeca2");
        cabeca.SetActive(false);
        personagem = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/personagem3");
        personagem.SetActive(false);
        //cabeca
        cabeca = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/cabecaGeral/Cabeca3");
        cabeca.SetActive(false);
        personagem = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/personagem3");
        personagem.SetActive(false);
        //cabeca
        cabeca = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/cabecaGeral/Cabeca3");
        cabeca.SetActive(false);

        //cabelo
        cabeloImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgCabelo/Cabelo1");
        cabeloImg.SetActive(false);
        //franja
        franja = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgFranja/Franja1");
        franja.SetActive(false);
        cabeloImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgCabelo/Cabelo1");
        cabeloImg.SetActive(false);
        cabeloImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgCabelo/Cabelo2");
        cabeloImg.SetActive(false);
        //franja
        franja = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgFranja/Franja2");
        franja.SetActive(false);
        cabeloImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgCabelo/Cabelo2");
        cabeloImg.SetActive(false);
        cabeloImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgCabelo/Cabelo3");
        cabeloImg.SetActive(false);
        //franja
        franja = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgFranja/Franja3");
        franja.SetActive(false);
        cabeloImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/cabecaGeral/Cabelo3");
        cabeloImg.SetActive(false);
        //franja
        franja = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgFranja/Franja1");
        franja.SetActive(false);

        //roupa
        roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgRoupaGeral/imgRoupa1");
        roupa.SetActive(false);
        roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgRoupaGeral/imgRoupa1");
        roupa.SetActive(false);
        roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgRoupaGeral/imgRoupa2");
        roupa.SetActive(false);
        roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgRoupaGeral/imgRoupa2");
        roupa.SetActive(false);
        roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgRoupaGeral/imgRoupa3");
        roupa.SetActive(false);
        roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgRoupaGeral/imgRoupa3");
        roupa.SetActive(false);
        roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgRoupaGeral/imgRoupa4");
        roupa.SetActive(false);
        roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgRoupaGeral/imgRoupa4");
        roupa.SetActive(false);

        //acess
        aparelhoImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/AcessoriosImgs/imgAparelho1");
        aparelhoImg.SetActive(false);
        aparelhoImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/AcessoriosImgs/imgAparelho1");
        aparelhoImg.SetActive(false);
        aparelhoImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/AcessoriosImgs/imgAparelho2");
        aparelhoImg.SetActive(false);
        aparelhoImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/AcessoriosImgs/imgAparelho2");
        aparelhoImg.SetActive(false);
        aparelhoImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/AcessoriosImgs/imgAparelho3");
        aparelhoImg.SetActive(false);
        aparelhoImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/AcessoriosImgs/imgAparelho3");
        aparelhoImg.SetActive(false);

        //oculos
        oculosImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/AcessoriosImgs/imgOculos");
        oculosImg.SetActive(false);
        oculosImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/AcessoriosImgs/imgOculos");
        oculosImg.SetActive(false);
    }

    public void CarregaPersonagem()
    {
        CarregarCorpo();
        CarregarCabelo();
        CarregarRoupa();
        CarregarAcess();
        txtNome.text = nome;
    }

    private void CarregarCorpo()
    {
        switch (corPele)
        {
            case "1":
                if (sexo == "1") //menina
                {
                    //corpo
                    personagem = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/personagem1");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/cabecaGeral/Cabeca1");
                    cabeca.SetActive(true);
                }
                else
                {
                    personagem = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/personagem1");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/cabecaGeral/Cabeca1");
                    cabeca.SetActive(true);
                }
                break;
            case "2":
                if (sexo == "1") //menina
                {
                    personagem = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/personagem2");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/cabecaGeral/Cabeca2");
                    cabeca.SetActive(true);
                }
                else
                {
                    personagem = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/personagem2");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/cabecaGeral/Cabeca2");
                    cabeca.SetActive(true);
                }
                break;
            case "3":
                if (sexo == "1") //menina
                {
                    personagem = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/personagem3");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/cabecaGeral/Cabeca3");
                    cabeca.SetActive(true);
                }
                else
                {
                    personagem = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/personagem3");
                    personagem.SetActive(true);
                    //cabeca
                    cabeca = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/cabecaGeral/Cabeca3");
                    cabeca.SetActive(true);
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
                    cabeloImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgCabelo/Cabelo1");
                    cabeloImg.SetActive(true);

                    //franja
                    franja = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgFranja/Franja1");
                    franja.SetActive(true);
                }
                else
                {
                    cabeloImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgCabelo/Cabelo1");
                    cabeloImg.SetActive(true);
                }
                break;
            case "2":
                if (sexo == "1") //menina
                {
                    cabeloImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgCabelo/Cabelo2");
                    cabeloImg.SetActive(true);
                    //franja
                    franja = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgFranja/Franja2");
                    franja.SetActive(true);
                }
                else
                {
                    cabeloImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgCabelo/Cabelo2");
                    cabeloImg.SetActive(true);
                }
                break;
            case "3":
                if (sexo == "1") //menina
                {
                    cabeloImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgCabelo/Cabelo3");
                    cabeloImg.SetActive(true);
                    //franja
                    franja = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgFranja/Franja3");
                    franja.SetActive(true);
                }
                else
                {
                    cabeloImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/cabecaGeral/Cabelo3");
                    cabeloImg.SetActive(true);
                    //franja
                    franja = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgFranja/Franja1");
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
                    roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgRoupaGeral/imgRoupa1");
                    roupa.SetActive(true);
                }
                else
                {
                    roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgRoupaGeral/imgRoupa1");
                    roupa.SetActive(true);
                }
                break;
            case "2":
                if (sexo == "1") //menina
                {
                    roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgRoupaGeral/imgRoupa2");
                    roupa.SetActive(true);

                }
                else
                {
                    roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgRoupaGeral/imgRoupa2");
                    roupa.SetActive(true);
                }

                break;
            case "3":
                if (sexo == "1") //menina
                {
                    roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgRoupaGeral/imgRoupa3");
                    roupa.SetActive(true);

                }
                else
                {
                    roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgRoupaGeral/imgRoupa3");
                    roupa.SetActive(true);

                }
                break;
            case "4":
                if (sexo == "1") //menina
                {
                    roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgRoupaGeral/imgRoupa4");
                    roupa.SetActive(true);

                }
                else
                {
                    roupa = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgRoupaGeral/imgRoupa4");
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
                    aparelhoImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/AcessoriosImgs/imgAparelho1");
                    aparelhoImg.SetActive(true);
                }
                else
                {
                    aparelhoImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/AcessoriosImgs/imgAparelho1");
                    aparelhoImg.SetActive(true);
                }
                break;
            case "2":
                if (sexo == "1")
                {
                    aparelhoImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/AcessoriosImgs/imgAparelho2");
                    aparelhoImg.SetActive(true);
                }
                else
                {
                    aparelhoImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/AcessoriosImgs/imgAparelho2");
                    aparelhoImg.SetActive(true);
                }
                break;
            case "3":
                if (sexo == "1")
                {
                    aparelhoImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/AcessoriosImgs/imgAparelho3");
                    aparelhoImg.SetActive(true);
                }
                else
                {
                    aparelhoImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/AcessoriosImgs/imgAparelho3");
                    aparelhoImg.SetActive(true);
                }
                break;
        }

        //Escolher Óculos
        if (oculoss == "1")
        {
            if (sexo == "1")
            {
                oculosImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/AcessoriosImgs/imgOculos");
                oculosImg.SetActive(true);
            }
            else
            {
                oculosImg = GameObject.Find("Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/AcessoriosImgs/imgOculos");
                oculosImg.SetActive(true);
            }

        }
    }

}
