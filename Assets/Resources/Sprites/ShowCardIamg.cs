using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowCardIamg : MonoBehaviour , IPointerEnterHandler ,IPointerExitHandler
{
    public bool OnTop;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnTop = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        OnTop = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
