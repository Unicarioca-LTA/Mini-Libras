using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCenaCozinha : MonoBehaviour
{
    public string SceneName;

    public void Cozinha()
    {
        SceneManager.UnloadScene(SceneName);
    }

    public void CozinhaFixo()
    {
        SceneManager.LoadScene("Cozinha", LoadSceneMode.Single);
    }

    public void Agua()
    {
        SceneManager.LoadScene("Cozinha-Agua", LoadSceneMode.Additive);
    }

    public void Biscoito()
    {
        SceneManager.LoadScene("Cozinha-Biscoito", LoadSceneMode.Additive);
    }

    public void Bolo()
    {
        SceneManager.LoadScene("Cozinha-Bolo", LoadSceneMode.Additive);
    }

    public void Copo()
    {
        SceneManager.LoadScene("Cozinha-Copo", LoadSceneMode.Additive);
    }

    public void Fogao()
    {
        SceneManager.LoadScene("Cozinha-Fogao", LoadSceneMode.Additive);
    }

    public void Geladeira()
    {
        SceneManager.LoadScene("Cozinha-Geladeira", LoadSceneMode.Additive);
    }

    public void Leite()
    {
        SceneManager.LoadScene("Cozinha-Leite", LoadSceneMode.Additive);
    }

    public void Macarrao()
    {
        SceneManager.LoadScene("Cozinha-Macarrao", LoadSceneMode.Additive);
    }

    public void Ovos()
    {
        SceneManager.LoadScene("Cozinha-Ovos", LoadSceneMode.Additive);
    }

    public void Panela()
    {
        SceneManager.LoadScene("Cozinha-Panela", LoadSceneMode.Additive);
    }

    public void Pao()
    {
        SceneManager.LoadScene("Cozinha-Pao", LoadSceneMode.Additive);
    }

    public void Microondas()
    {
        SceneManager.LoadScene("Cozinha-Microondas", LoadSceneMode.Additive);
    }

    public void Liquidificador()
    {
        SceneManager.LoadScene("Cozinha-Liquidificador", LoadSceneMode.Additive);
    }
}
