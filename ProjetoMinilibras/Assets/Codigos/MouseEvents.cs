using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseEvents : MonoBehaviour
{
    public UnityEvent onDown, onUp;
    void OnMouseUp()
    {
        onUp.Invoke();
    }

    private void OnMouseDown()
    {
        onDown.Invoke();
    }
}