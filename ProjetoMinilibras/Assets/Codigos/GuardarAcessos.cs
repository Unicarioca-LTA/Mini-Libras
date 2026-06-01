using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System;
using System.IO;

[Serializable]
public class AccessColumn
{
    [Tooltip("Nome da coluna na tabela (ex: cesto, chuveiro...)")]
    public string columnName;

    [Tooltip("Botão da UI que dispara o acesso (opcional).")]
    public Button uiButton;
}

public class GuardarAcessos : MonoBehaviour
{
    [Tooltip("Nome da tabela no banco (ex: banheiro, cozinha...).")]
    public string tableName = "tabela";

    [Tooltip("Colunas de contagem de acessos deste cenário.")]
    public AccessColumn[] columns;

    private string databaseName = "dbLibras.db";
    private string databasePath;

    private SqliteConnection Connection =>
        new SqliteConnection($"Data Source={databasePath};");

    private void Awake()
    {
        databasePath = Path.Combine(Application.persistentDataPath, databaseName);

        // Auto-link dos botões, se quiser usar via UI
        foreach (var col in columns)
        {
            if (col != null && col.uiButton != null && !string.IsNullOrEmpty(col.columnName))
            {
                var captured = col; // evitar closure bug
                col.uiButton.onClick.AddListener(() =>
                {
                    RegistrarAcesso(captured.columnName);
                });
            }
        }
    }

    public void StartConnection()
    {
        databasePath = Path.Combine(Application.persistentDataPath, databaseName);
    }

    /// <summary>
    /// Chame isso manualmente (ou via botão) passando o nome da coluna.
    /// Ex: RegistrarAcesso("cesto");
    /// </summary>
    public void RegistrarAcesso(string columnName)
    {
        if (string.IsNullOrEmpty(columnName))
        {
            Debug.LogError("ColumnName vazio ao tentar registrar acesso.");
            return;
        }

        using (var conn = Connection)
        {
            conn.Open();

            // 1) Verificar se já existe linha com esse id
            int? currentCount = GetCurrentCount(conn, CarregarPersonagem.personagemID, columnName);

            if (currentCount.HasValue)
            {
                // 2A) UPDATE linha existente
                UpdateCount(conn, CarregarPersonagem.personagemID, columnName, currentCount.Value + 1);
            }
            else
            {
                // 2B) INSERT nova linha com essa coluna = 1 e o resto = 0
                InsertNewRow(conn, CarregarPersonagem.personagemID, columnName);
            }
        }
    }

    /// <summary>
    /// Retorna o valor atual da coluna, ou null se não existir linha.
    /// </summary>
    private int? GetCurrentCount(SqliteConnection conn, int id, string columnName)
    {
        using (var cmd = conn.CreateCommand())
        {
            // SELECT coluna específica
            cmd.CommandText = $"SELECT {columnName} FROM {tableName} WHERE id = @id;";
            cmd.Parameters.AddWithValue("@id", id);

            using (var reader = cmd.ExecuteReader())
            {
                if (!reader.Read())
                {
                    // Não tem linha com esse id
                    return null;
                }

                if (reader.IsDBNull(0))
                {
                    return 0;
                }

                return reader.GetInt32(0);
            }
        }
    }

    private void UpdateCount(SqliteConnection conn, int id, string columnName, int newValue)
    {
        using (var cmd = conn.CreateCommand())
        {
            // IMPORTANTE: agora tem WHERE id = @id
            cmd.CommandText = $"UPDATE {tableName} SET {columnName} = @val WHERE id = @id;";
            cmd.Parameters.AddWithValue("@val", newValue);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }

    private void InsertNewRow(SqliteConnection conn, int id, string selectedColumn)
    {
        // Criamos o INSERT apenas para o ID e para a coluna que sofreu o primeiro acesso
        string sql = $"INSERT INTO {tableName} (id, {selectedColumn}) VALUES (@id, @val);";

        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = sql;

            // Passamos o ID
            cmd.Parameters.AddWithValue("@id", id);

            // Passamos 1, pois é o primeiro acesso que está criando a linha
            cmd.Parameters.AddWithValue("@val", 1);

            cmd.ExecuteNonQuery();
        }
    }

    // ---------- Helpers para chamar por índice (se preferir) ----------
    public void RegistrarAcessoPorIndice(int index)
    {
        if (index < 0 || index >= columns.Length)
        {
            Debug.LogError("Índice de coluna inválido.");
            return;
        }

        RegistrarAcesso(columns[index].columnName);
    }
}
