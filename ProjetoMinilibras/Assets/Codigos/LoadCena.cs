using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCena : MonoBehaviour
{

    public void Fazenda()
    {
        SceneManager.LoadScene("Fazenda", LoadSceneMode.Single);
    }

    public void Cores()
    {
        SceneManager.LoadScene("Cores", LoadSceneMode.Single);
    }

    public void TelaInical()
    {
        SceneManager.LoadScene("telaInicial", LoadSceneMode.Single);
    }

    public void TelaInical0()
    {
        SceneManager.LoadScene("TelaInicial0", LoadSceneMode.Single);
    }

    public void CriarPersonagem()
    {
        SceneManager.LoadScene("criarPersonagem", LoadSceneMode.Single);
    }

    public void MenuCenarios()
    {
        SceneManager.LoadScene("menuCenarios", LoadSceneMode.Single);
    }

    public void Historico()
    {
        SceneManager.LoadScene("Historico", LoadSceneMode.Single);
    }

    public void CorpoHumano()
    {
        SceneManager.LoadScene("Corpo_humano", LoadSceneMode.Single);
    }

    public void Cidade()
    {
        SceneManager.LoadScene("Cidade", LoadSceneMode.Single);
    }

    public void Escola()
    {
        SceneManager.LoadScene("Escola", LoadSceneMode.Single);
    }

    public void Mercado()
    {
        SceneManager.LoadScene("Mercado", LoadSceneMode.Single);
    }

    public void Banheiro()
    {
        SceneManager.LoadScene("Banheiro", LoadSceneMode.Single);
    }

    public void Cozinha()
    {
        SceneManager.LoadScene("Cozinha", LoadSceneMode.Single);
    }

    public void Parque()
    {
        SceneManager.LoadScene("Parque", LoadSceneMode.Single);
    }

    public void Quarto()
    {
        SceneManager.LoadScene("Quarto", LoadSceneMode.Single);
    }

    public void Alfabeto()
    {
        SceneManager.LoadScene("Alfabeto", LoadSceneMode.Single);
    }

    public void Numeros()
    {
        SceneManager.LoadScene("Numeros", LoadSceneMode.Single);
    }

    public void LooadScene(string cena)
    {
        SceneManager.LoadScene(cena, LoadSceneMode.Single);
    }

    public void SceneAdd(string cena)
    {
        SceneManager.LoadScene(cena, LoadSceneMode.Additive);
    }

    public void SceneUnload(string cena)
    {
        SceneManager.UnloadSceneAsync(cena);
    }
}
