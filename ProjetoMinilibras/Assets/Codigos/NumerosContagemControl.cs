using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices.WindowsRuntime;


// 1. Isolamos o namespace do Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

public class NumerosContagemControl : MonoBehaviour
{
    private GuardarAcessos salvar = new GuardarAcessos();
    public Text txtIdSecreto;
    public TMP_Text nText;
    public Button btnBau;
    public GameObject[] nStar;

    // 2. Isolamos a variável SceneAsset para existir só no Editor
#if UNITY_EDITOR
    [Tooltip("Arraste as cenas para cá no Editor")]
    public SceneAsset[] nPainels;
#endif

    // 3. Criamos uma variável invisível que vai guardar os nomes das cenas para a Build
    [HideInInspector]
    public string[] nomesDosPaineis;

    private static int cont = 0;

    // 4. O OnValidate extrai os nomes dos SceneAssets e guarda no array de strings
    private void OnValidate()
    {
#if UNITY_EDITOR
        if (nPainels != null)
        {
            // Garante que o array de strings tenha o mesmo tamanho do array de SceneAssets
            if (nomesDosPaineis == null || nomesDosPaineis.Length != nPainels.Length)
            {
                nomesDosPaineis = new string[nPainels.Length];
            }

            // Copia os nomes das cenas
            for (int i = 0; i < nPainels.Length; i++)
            {
                if (nPainels[i] != null)
                {
                    nomesDosPaineis[i] = nPainels[i].name;
                }
                else
                {
                    nomesDosPaineis[i] = string.Empty;
                }
            }
        }
#endif
    }

    private void Start()
    {
        salvar.tableName = "numero";
        salvar.StartConnection();   

    }
    public void SetStar()
    {
        GameObject star = findNextStar(false);

        if (star.GetComponent<TranslateScaleSpin2D>() != null)
        {
            star.GetComponent<TranslateScaleSpin2D>().StartMove();
            btnBau.enabled = false;
            cont++;
            nText.text = cont.ToString();
        }
    }

    public void GetStar(GameObject starSelect)
    {
        starSelect.GetComponent<TranslateScaleSpin2D>().StartMove();
        cont--;
        nText.text = cont.ToString();
    }

    private GameObject findNextStar(bool ceu)
    {
        GameObject nextStar = btnBau.gameObject;

        foreach (GameObject star in nStar)
        {
            if (star.GetComponent<TranslateScaleSpin2D>().ceu == ceu)
            {
                nextStar = star;
                break;
            }
        }

        return nextStar;
    }

    public static void SetCont(int n)
    {
        cont = n;
    }

    public void OpenPainel()
    {
        // 5. Na hora de carregar, usamos o array de strings (nomesDosPaineis) e adicionamos uma checagem de segurança
        if (cont >= 0 && cont < nomesDosPaineis.Length && !string.IsNullOrEmpty(nomesDosPaineis[cont]))
        {
            SceneManager.LoadSceneAsync(nomesDosPaineis[cont], LoadSceneMode.Additive);

            //extrair texto do número e salvar o acesso no banco de dados
            int hifenPosition = nomesDosPaineis[cont].IndexOf("-");
            salvar.RegistrarAcesso(nomesDosPaineis[cont].Substring(hifenPosition + 1).Trim().ToLower());
        }
        else
        {
            Debug.LogWarning("Índice de contagem fora dos limites ou cena vazia!");
        }
    }
    public void OpenPainel(int i)
    {
        SceneManager.LoadSceneAsync(nomesDosPaineis[i], LoadSceneMode.Additive);
    }
}