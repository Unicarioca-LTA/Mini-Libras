using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControledeBlusa : MonoBehaviour
{
    public GameObject persona1, persona2, persona3, persona4;
    
    int cont = 1;

    public void bla()
    {
            cont++;
            switch (cont) {
                case 1:
                    persona1.SetActive(true);
                    persona2.SetActive(false);
                    persona3.SetActive(false);
                    persona4.SetActive(false);
                break;

                case 2:
                    persona1.SetActive(false);
                    persona2.SetActive(true);
                    persona3.SetActive(false);
                    persona4.SetActive(false);
                break;

                case 3:
                    persona1.SetActive(false);
                    persona2.SetActive(false);
                    persona3.SetActive(true);
                    persona4.SetActive(false);
                break;

                case 4:
                    persona1.SetActive(false);
                    persona2.SetActive(false);
                    persona3.SetActive(false);
                    persona4.SetActive(true);
                    cont = 0;
                break;
            }
    }
}
    

