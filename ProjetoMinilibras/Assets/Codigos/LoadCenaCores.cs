using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCenaCores : MonoBehaviour
{
    public string SceneName;

    public void Cores()
    {
        SceneManager.UnloadScene(SceneName);
    }

    public void FixoCores()
    {
        SceneManager.LoadScene("Cores", LoadSceneMode.Single);
    }

    public void Cores_Amarelo()
    {
        SceneManager.LoadScene("Cores-Amarelo", LoadSceneMode.Additive);
    }

    public void Cores_Laranja()
    {
        SceneManager.LoadScene("Cores-Laranja", LoadSceneMode.Additive);
    }

    public void Cores_Vermelho()
    {
        SceneManager.LoadScene("Cores-Vermelho", LoadSceneMode.Additive);
    }

    public void Cores_Rosa()
    {
        SceneManager.LoadScene("Cores-Rosa", LoadSceneMode.Additive);
    }

    public void Cores_Roxo()
    {
        SceneManager.LoadScene("Cores-Roxo", LoadSceneMode.Additive);
    }

    public void Cores_Verde()
    {
        SceneManager.LoadScene("Cores-Verde", LoadSceneMode.Additive);
    }

    public void Cores_Azul()
    {
        SceneManager.LoadScene("Cores-Azul", LoadSceneMode.Additive);
    }

    public void Cores_Preto()
    {
        SceneManager.LoadScene("Cores-Preto", LoadSceneMode.Additive);
    }

    public void Cores_Branco()
    {
        SceneManager.LoadScene("Cores-Branco", LoadSceneMode.Additive);
    }

    public void Cores_Marrom()
    {
        SceneManager.LoadScene("Cores-Marrom", LoadSceneMode.Additive);
    }

    public void Cores_Prateado()
    {
        SceneManager.LoadScene("Cores-Prateado", LoadSceneMode.Additive);
    }

    public void Cores_Cinza()
    {
        SceneManager.LoadScene("Cores-Cinza", LoadSceneMode.Additive);
    }

    public void Cores_Dourado()
    {
        SceneManager.LoadScene("Cores-Dourado", LoadSceneMode.Additive);
    }

    public void Cores_Lilas()
    {
        SceneManager.LoadScene("Cores-Lilas", LoadSceneMode.Additive);
    }
}
