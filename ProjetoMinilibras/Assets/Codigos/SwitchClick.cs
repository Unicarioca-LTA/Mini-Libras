using System.Diagnostics;
using TMPro;
using UnityEngine;

public class SwitchClick : MonoBehaviour
{
    [SerializeField]
    private NumerosContagemControl control;
    [SerializeField]
    private TMP_Text contador;
    private string _txtSaved;
    public void SwitchActive(GameObject obj)
    {
        if (obj.activeSelf)
        {
            obj.SetActive(false);
            RecoveryCount();
        }

        else
        {
            obj.SetActive(true);
            SaveCount();
        }
    }

    private void SaveCount()
    {      
            _txtSaved = contador.text; 
            contador.text = "0";
            NumerosContagemControl.SetCont(0);
    }

    public void RecoveryCount()
    {
        contador.text = _txtSaved;
        NumerosContagemControl.SetCont(int.Parse(_txtSaved));
    }
}
