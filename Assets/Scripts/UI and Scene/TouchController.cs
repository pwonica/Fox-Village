using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{

   
    public void OnPointerClick(PointerEventData eventData)
    {
        print("On pointer click active ");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("On pointer down active");

    }

    public void TestFunction()
    {
        print("Test function active");
    }

    
}
