using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCenaFazenda : MonoBehaviour
{
    public string SceneName;

    public void Fazenda()
    {
        SceneManager.UnloadSceneAsync(SceneName);
    }

    public void fazendaFixo()
    {
        SceneManager.LoadScene("Fazenda", LoadSceneMode.Single);
    }

    public void Sol()
    {
        SceneManager.LoadScene("Fazenda-Sol", LoadSceneMode.Additive);
    }

    public void Nuvem()
    {
        SceneManager.LoadScene("Fazenda-Nuvem", LoadSceneMode.Additive);
    }

    public void Casa()
    {
        SceneManager.LoadScene("Fazenda-Casa", LoadSceneMode.Additive);
    }

    public void Cerca()
    {
        SceneManager.LoadScene("Fazenda-Cerca", LoadSceneMode.Additive);
    }

    public void Pato()
    {
        SceneManager.LoadScene("Fazenda-Pato", LoadSceneMode.Additive);
    }

    public void Galinha()
    {
        SceneManager.LoadScene("Fazenda-Galinha", LoadSceneMode.Additive);
    }

    public void Pintinho()
    {
        SceneManager.LoadScene("Fazenda-Pintinho", LoadSceneMode.Additive);
    }

    public void Vaca()
    {
        SceneManager.LoadScene("Fazenda-Vaca", LoadSceneMode.Additive);
    }

    public void Cavalo()
    {
        SceneManager.LoadScene("Fazenda-Cavalo", LoadSceneMode.Additive);
    }

    public void Porco()
    {
        SceneManager.LoadScene("Fazenda-Porco", LoadSceneMode.Additive);
    }

    public void Ovelha()
    {
        SceneManager.LoadScene("Fazenda-Ovelha", LoadSceneMode.Additive);
    }

    public void Trator()
    {
        SceneManager.LoadScene("Fazenda-Trator", LoadSceneMode.Additive);
    }

    public void Horta()
    {
        SceneManager.LoadScene("Fazenda-Horta", LoadSceneMode.Additive);
    }

    public void Lago()
    {
        SceneManager.LoadScene("Fazenda-Lago", LoadSceneMode.Additive);
    }
}
