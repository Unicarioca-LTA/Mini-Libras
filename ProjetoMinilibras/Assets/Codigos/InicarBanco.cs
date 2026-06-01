using System.Collections;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.Networking;
using System;

public class InicarBanco : MonoBehaviour
{
    private string databaseName;
    private string databasePath;
    public string DatabaseName => this.databaseName;

    private SqliteConnection Connection => new SqliteConnection($"Data Source = {this.databasePath};");

    protected void Awake()
    {
        this.databaseName = "dbLibras.db";
        print("SQLiteDataSource Awake");
        if (string.IsNullOrEmpty(this.databaseName))
        {
            Debug.LogError("Database name is empty!");
            return;
        }
            CopyDatabaseFileIfNotExists();
    }

    #region Create database

    protected void CopyDatabaseFileIfNotExists()
    {
        this.databasePath = Path.Combine(Application.persistentDataPath, this.databaseName);
        Debug.LogWarning("PATH: " + this.databasePath);

        if (File.Exists(this.databasePath))
            return;

        var originDatabasePath = string.Empty;
        var isAndroid = false;

#if UNITY_EDITOR || UNITY_WP8 || UNITY_WINRT || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX

        originDatabasePath = Path.Combine(Application.streamingAssetsPath, this.databaseName);

#elif UNITY_STANDALONE_OSX

        originDatabasePath = Path.Combine(Application.dataPath, "/Resources/Data/StreamingAssets/", this.DatabaseName);
        
#elif UNITY_IOS

        originDatabasePath = Path.Combine(Application.dataPath, "Raw", this.DatabaseName);        

#elif UNITY_ANDROID

        isAndroid = true;
        originDatabasePath = "jar:file://" + Application.dataPath + "!/assets/" + this.DatabaseName;
        StartCoroutine(GetInternalFileAndroid(originDatabasePath));

#endif

        if (!isAndroid)
        {
            Debug.LogWarning($"COPY FILE: {originDatabasePath} to {this.databasePath}");
            File.Copy(originDatabasePath, this.databasePath);
        }
    }

    protected IEnumerator GetInternalFileAndroid(string path)
    {
        var request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ProtocolError ||
            request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError($"Error reading android file!: {request.error}");
            throw new Exception($"Error reading android file!: { request.error }");
        }
        else
        {
            File.WriteAllBytes(this.databasePath, request.downloadHandler.data);
            Debug.Log("File copied! ->" + this.databasePath);
        }
    }

    #endregion
}

