using UnityEngine;
using UnityEngine.UI;

public class ClickAnotherButton : MonoBehaviour
{
    public Button button;

    public void Click()
    {
        button.onClick.Invoke();
    }
}
