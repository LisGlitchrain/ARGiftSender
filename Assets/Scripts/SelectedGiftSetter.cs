using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedGiftSetter : MonoBehaviour
{
    GiftController giftController;
    // Start is called before the first frame update
    void Start()
    {
        giftController = FindObjectOfType<GiftController>();
    }


    public void SelectMe()
    {
        giftController.currentGiftSelected = true;
    }

    public void DeselectMe()
    {
        if (giftController.currentGift == this.gameObject)
            giftController.currentGiftSelected = false;
    }
}
