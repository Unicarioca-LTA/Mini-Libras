using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCenaParque : MonoBehaviour
{
    public string SceneName;

    public void Parque()
    {
        SceneManager.UnloadScene(SceneName);
    }

    public void ParqueFixo()
    {
        SceneManager.LoadScene("Parque", LoadSceneMode.Single);
    }

    public void Adulto()
    {
        SceneManager.LoadScene("Parque-Adulto", LoadSceneMode.Additive);
    }

    public void Amarelinha()
    {
        SceneManager.LoadScene("Parque-Amarelinha", LoadSceneMode.Additive);
    }

    public void Arvore()
    {
        SceneManager.LoadScene("Parque-Arvore", LoadSceneMode.Additive);
    }

    public void Balanco()
    {
        SceneManager.LoadScene("Parque-Balanco", LoadSceneMode.Additive);
    }

    public void Banco()
    {
        SceneManager.LoadScene("Parque-Banco", LoadSceneMode.Additive);
    }

    public void Bola()
    {
        SceneManager.LoadScene("Parque-Bola", LoadSceneMode.Additive);
    }

    public void Brincar()
    {
        SceneManager.LoadScene("Parque-Brincar", LoadSceneMode.Additive);
    }

    public void Corda()
    {
        SceneManager.LoadScene("Parque-Corda", LoadSceneMode.Additive);
    }

    public void Crianca()
    {
        SceneManager.LoadScene("Parque-Crianca", LoadSceneMode.Additive);
    }

    public void Escorrega()
    {
        SceneManager.LoadScene("Parque-Escorrega", LoadSceneMode.Additive);
    }

    public void Gangorra()
    {
        SceneManager.LoadScene("Parque-Gangorra", LoadSceneMode.Additive);
    }

    public void Pulapula()
    {
        SceneManager.LoadScene("Parque-Pulapula", LoadSceneMode.Additive);
    }
}
