using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetasIntro : MonoBehaviour
{
    public GameObject intro1, intro2, intro3, intro4;
    public int atual;

   public void direita()
    {
        switch (atual)
        {
            case 1:
                intro1.SetActive(false);
                intro2.SetActive(true);
                atual = 2;
                break;
            case 2:
                intro2.SetActive(false);
                intro3.SetActive(true);
                atual = 3;
                break;
            case 3:
                intro3.SetActive(false);
                intro4.SetActive(true);
                atual = 4;
                break;
            case 4:
                intro4.SetActive(false);
                intro1.SetActive(true);
                atual = 1;
                break;
        }
    }

    public void esquerda()
    {
        switch (atual)
        {
            case 1:
                intro1.SetActive(false);
                intro4.SetActive(true);
                atual = 4;
                break;
            case 2:
                intro2.SetActive(false);
                intro1.SetActive(true);
                atual = 1;
                break;
            case 3:
                intro3.SetActive(false);
                intro2.SetActive(true);
                atual = 2;
                break;
            case 4:
                intro4.SetActive(false);
                intro3.SetActive(true);
                atual = 3;
                break;
        }
    }
}
