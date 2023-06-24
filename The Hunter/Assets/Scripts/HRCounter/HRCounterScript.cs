using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HRCounterScript : MonoBehaviour
{
    [SerializeField]
    GameObject HRTextObject;

    private TextMeshProUGUI HRValueComponent;

    void Start()
    {
        var ecgReceiver = GameObject.Find("ECGReceiver");
        if (ecgReceiver != null )
        {
            this.gameObject.SetActive(true);
            var ecgReceiverScript = ecgReceiver.GetComponent<ECGReceiver>();
            if (ecgReceiverScript != null)
            {
                Debug.Log("TU");
                HRValueComponent = HRTextObject.GetComponent<TextMeshProUGUI>();
                ecgReceiverScript.receivedHR.AddListener(SetCounter);
            }
        }
        else
            this.gameObject.SetActive(false);
    }

    private void SetCounter(int hr)
    {
        HRValueComponent.SetText(Convert.ToString(hr));
    }
}
