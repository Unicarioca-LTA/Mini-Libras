using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class switchAcessorios : MonoBehaviour
{
    public GameObject objeto, objeto2, objeto3, franja;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //switchAcessorio();
    }


    public void switchAcessorio() {

        if ((objeto.gameObject.activeSelf == true))
        {
            objeto.gameObject.SetActive(false);
        }
        else{
            objeto.gameObject.SetActive(true);
            objeto2.gameObject.SetActive(false);
            objeto3.gameObject.SetActive(false);
        }
    }

    public void switchOculos()
    {

        if ((objeto.gameObject.activeSelf == true))
        {
            objeto.gameObject.SetActive(false);
        }
        else
        {
            objeto.gameObject.SetActive(true);
        }
    }

    public void switchRoupa()
    {

        
            objeto.gameObject.SetActive(true);
            objeto2.gameObject.SetActive(false);
            objeto3.gameObject.SetActive(false);
        
    }

    public void switchFranjaEspecial()
    {

        if ((objeto.gameObject.activeSelf == true))
        {
            franja.gameObject.SetActive(false);
            objeto.gameObject.SetActive(false);
        }
        else
        {
            franja.gameObject.SetActive(true);
            objeto.gameObject.SetActive(true);
            objeto2.gameObject.SetActive(false);
            objeto3.gameObject.SetActive(false);
        }


        
    }
}
