using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetUpSignMenu : MonoBehaviour
{
    public GameObject recordButton;
    public GameObject backButton;
    public GameObject typeButton;
    public GameObject playButton;
    public GameObject deleteRecordedButton;
    public TextMeshProUGUI signTMPro;
    public GameObject deleteTyped;
    public GameObject sendButton;
    public GameObject sentText;
    public GameObject hint;
    public GameObject sign;
    public GameObject arrow;

    void OnEnable()
    {
        recordButton.SetActive(true);
        backButton.SetActive(true);
        typeButton.SetActive(true);
        playButton.SetActive(false);
        sendButton.SetActive(true);
        deleteRecordedButton.SetActive(false);
        deleteTyped.SetActive(false);
        signTMPro.text = string.Empty;
        sentText.SetActive(false);
        hint.SetActive(true);
        sign.SetActive(true);
        arrow.SetActive(true);
    }

    public void SetSent()
    {
        recordButton.SetActive(false);
        backButton.SetActive(false);
        typeButton.SetActive(false);
        playButton.SetActive(false);
        sendButton.SetActive(false);
        deleteRecordedButton.SetActive(false);
        deleteTyped.SetActive(false);
        signTMPro.text = string.Empty;
        sentText.SetActive(true);
        hint.SetActive(false);
        sign.SetActive(false);
        arrow.SetActive(true);
    }


}
