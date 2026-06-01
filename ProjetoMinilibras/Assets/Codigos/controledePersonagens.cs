using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controledePersonagens : MonoBehaviour{
    public GameObject personagem1, personagem2, personagem3, cabeca1, cabeca2, cabeca3;
    int cont = 1;

    public void proximoPersonagem()
    {
        cont++;
        switch (cont){
            case 1:
                personagem1.SetActive(true);
                personagem2.SetActive(false);
                personagem3.SetActive(false);
                cabeca1.SetActive(true);
                cabeca2.SetActive(false);
                cabeca3.SetActive(false);
                break;

            case 2:
                personagem1.SetActive(false);
                personagem2.SetActive(true);
                personagem3.SetActive(false);
                cabeca2.SetActive(true);
                cabeca1.SetActive(false);
                cabeca3.SetActive(false);
                break;

            case 3:
                personagem1.SetActive(false);
                personagem2.SetActive(false);
                personagem3.SetActive(true);
                cabeca3.SetActive(true);
                cabeca2.SetActive(false);
                cabeca1.SetActive(false);
                cont = 0;
                break;
            }
    }
   
}
