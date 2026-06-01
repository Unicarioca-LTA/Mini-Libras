using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarregarCorPincel : MonoBehaviour
{

    private GameObject corPincel, corPintura;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DesativarCor()
    {
        //Escolher cor da ponta do pincel
        for (int i = 1; i <= 12; i++)
        {
            corPincel = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/pontaPincel/ponta" + i);
            corPincel.SetActive(false);

            corPincel = GameObject.Find("Canvas - fase/Panel - fase/fundo- fase/Pinturas/Pintura" + i);
            corPincel.SetActive(false);

        }
    }
}
