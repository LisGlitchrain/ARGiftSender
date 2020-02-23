using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemSelectionHelper : MonoBehaviour
{
    public GameObject Model;
    public int giftIndex = 0;
    public bool selectionStatus;
    public bool selectable = true;

    public void Start()
    {
        
    }

    public void OnSelectionStart()
    {
        if (!selectable) return;
        selectionStatus = true;
        selectable = false;
        FindObjectOfType<GiftController>().OnSelectionStart(this);
    }

    public void OnDeselect()
    {
        selectionStatus = false;
    }

}
