using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCenaQuarto : MonoBehaviour
{
    public string SceneName;

    public void Quarto()
    {
        SceneManager.UnloadSceneAsync(SceneName);
    }

    public void QuartoFixo()
    {
        SceneManager.LoadScene("Quarto", LoadSceneMode.Single);
    }

    public void Abajur()
    {
        SceneManager.LoadScene("Quarto-Abajur", LoadSceneMode.Additive);
    }

    public void Armario()
    {
        SceneManager.LoadScene("Quarto-Armario", LoadSceneMode.Additive);
    }

    public void Cachorro()
    {
        SceneManager.LoadScene("Quarto-Cachorro", LoadSceneMode.Additive);
    }

    public void Cama()
    {
        SceneManager.LoadScene("Quarto-Cama", LoadSceneMode.Additive);
    }

    public void Computador()
    {
        SceneManager.LoadScene("Quarto-Computador", LoadSceneMode.Additive);
    }

    public void Janela()
    {
        SceneManager.LoadScene("Quarto-Janela", LoadSceneMode.Additive);
    }

    public void Lecol()
    {
        SceneManager.LoadScene("Quarto-Lencol", LoadSceneMode.Additive);
    }

    public void Porta()
    {
        SceneManager.LoadScene("Quarto-Porta", LoadSceneMode.Additive);
    }

    public void Travesseiro()
    {
        SceneManager.LoadScene("Quarto-Travesseiro", LoadSceneMode.Additive);
    }

    public void Tapete()
    {
        SceneManager.LoadScene("Quarto-Tapete", LoadSceneMode.Additive);
    }

    public void Gato()
    {
        SceneManager.LoadScene("Quarto-Gato", LoadSceneMode.Additive);
    }

    public void Pijama()
    {
        SceneManager.LoadScene("Quarto-Pijama", LoadSceneMode.Additive);
    }
}
