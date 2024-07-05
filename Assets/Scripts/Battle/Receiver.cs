using UnityEngine;
using UnityEngine.EventSystems;

public class Receiver : MonoBehaviour, IPointerEnterHandler
{
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
    }
}