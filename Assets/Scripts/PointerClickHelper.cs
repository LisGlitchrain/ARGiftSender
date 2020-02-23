using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerClickHelper : MonoBehaviour
{
    GiftController giftController;
    public UnityEvent onClickEvent;

    // Start is called before the first frame update
    void Start()
    {
        giftController = FindObjectOfType<GiftController>();
    }


    public void AddMeAsEventTrigger()
    {
        giftController.selectedButton = this;
    }

    public void RemoveMeAsEventTrigger()
    {
        if (giftController.selectedButton = this)
            giftController.selectedButton = null;       
    }

    public void OnClick()
    {
        onClickEvent?.Invoke();
    }
}
