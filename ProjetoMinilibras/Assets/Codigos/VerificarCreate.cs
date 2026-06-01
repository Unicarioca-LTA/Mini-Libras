using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.IO;

public class VerificarCreate : MonoBehaviour
{
    public Button btnMenino, btnMenina;
    public GameObject voltarHistorico;
    public bool Historico;

    //descorbeta dos sexos
    private bool menino, menina;

    //Conexão com o Banco
    private string databaseName;
    private string databasePath;

    private SqliteConnection Connection => new SqliteConnection($"Data Source = {this.databasePath};");

    private void Awake()
    {
        this.databaseName = "dbLibras.db";
        this.databasePath = Path.Combine(Application.persistentDataPath, this.databaseName);
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckSexo();
        
        if (menino == true && menina == false)
        {
            if (Historico == true)
            {
                voltarHistorico.SetActive(false);
            }
            btnMenino.GetComponent<Button>().onClick.Invoke();

        }
        else if(menino == false && menina == true)
        {
            if (Historico == true)
            {
                voltarHistorico.SetActive(false);
            }
            btnMenina.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void CheckSexo()
    {
        using (var dbConnection = Connection)
        {
            dbConnection.Open();
            using (var dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery, sqlQuery2;
                int idTest = 0;

                sqlQuery = "SELECT id FROM crianca where sexo = 2 ORDER BY ROWID ASC LIMIT 1";
                sqlQuery2 = "SELECT id FROM crianca where sexo = 1 ORDER BY ROWID ASC LIMIT 1";
                
                dbCmd.CommandText = sqlQuery;

                using (var reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        idTest = reader.GetInt32(0);
                    }

                    if (idTest == 0)
                    {
                        Debug.Log("Não achou menino." );
                        menino = false;
                    }
                    else
                    {
                        menino = true;
                    }
                }

                idTest = 0;
                dbCmd.CommandText = sqlQuery2;
                using (var reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        idTest = reader.GetInt32(0);
                    }

                    if (idTest == 0)
                    {
                        menina = false;
                        Debug.Log("Não achou menina."+idTest);
                    }
                    else
                    {
                        menina = true;
                    }
                }
            }
        }
    }

    public void repeatCheck()
    {
        CheckSexo();

        if (menino == true && menina == false)
        {
            btnMenino.GetComponent<Button>().onClick.Invoke();

        }
        else if (menino == false && menina == true)
        {
            btnMenina.GetComponent<Button>().onClick.Invoke();
        }
    }

}
