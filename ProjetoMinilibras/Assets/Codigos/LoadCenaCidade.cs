using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCenaCidade : MonoBehaviour
{
    public string SceneName;

    public void Cidade()
    {
       SceneManager.UnloadSceneAsync(SceneName);
    }

    public void CidadeFixo()
    {
        SceneManager.LoadScene("Cidade", LoadSceneMode.Single);
    }

    public void Cidade_Aviao()
    {
        SceneManager.LoadScene("Cidade-Aviao", LoadSceneMode.Additive);
    }

    public void Cidade_Bicicleta()
    {
        SceneManager.LoadScene("Cidade-Bicicleta", LoadSceneMode.Additive);
    }

    public void Cidade_Caminhao()
    {
        SceneManager.LoadScene("Cidade-Caminhao", LoadSceneMode.Additive);
    }

    public void Cidade_Capacete()
    {
        SceneManager.LoadScene("Cidade-Capacete", LoadSceneMode.Additive);
    }

    public void Cidade_Carro()
    {
        SceneManager.LoadScene("Cidade-Carro", LoadSceneMode.Additive);
    }

    public void Cidade_Faixa()
    {
        SceneManager.LoadScene("Cidade-Faixa", LoadSceneMode.Additive);
    }

    public void Cidade_Moto()
    {
        SceneManager.LoadScene("Cidade-Moto", LoadSceneMode.Additive);
    }

    public void Cidade_Onibus()
    {
        SceneManager.LoadScene("Cidade-Onibus", LoadSceneMode.Additive);
    }

    public void Cidade_Ponto()
    {
        SceneManager.LoadScene("Cidade-Ponto", LoadSceneMode.Additive);
    }

    public void Cidade_Poste()
    {
        SceneManager.LoadScene("Cidade-Poste", LoadSceneMode.Additive);
    }

    public void Cidade_Predio()
    {
        SceneManager.LoadScene("Cidade-Predio", LoadSceneMode.Additive);
    }

    public void Cidade_Sinal()
    {
        SceneManager.LoadScene("Cidade-Sinal", LoadSceneMode.Additive);
    }

    public void Cidade_Taxi()
    {
        SceneManager.LoadScene("Cidade-Taxi", LoadSceneMode.Additive);
    }

    public void Cidade_Metro()
    {
        SceneManager.LoadScene("Cidade-Metro", LoadSceneMode.Additive);
    }

    public void Cidade_Trem()
    {
        SceneManager.LoadScene("Cidade-Trem", LoadSceneMode.Additive);
    }
}
