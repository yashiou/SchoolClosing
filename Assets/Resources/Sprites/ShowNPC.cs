using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowNPC : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject NPCTXT;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData e)
    {
        NPCTXT.SetActive(true);
    }

    public void OnPointerExit(PointerEventData e)
    {
        NPCTXT.SetActive(false);
    }
}
