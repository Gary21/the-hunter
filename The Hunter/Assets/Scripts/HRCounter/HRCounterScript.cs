using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HRCounterScript : MonoBehaviour
{
    [SerializeField]
    GameObject HRTextObject;

    [SerializeField] private GameObject[] spawnersObjects;
    [SerializeField] private GameObject characterControllerObject;
    private EnemySpawner[] spawners;
    private CharacterHealth characterController;


    private TextMeshProUGUI HRValueComponent;

    void Start()
    {
        foreach (var spawnerObject in spawnersObjects)
        {
            spawners.Append(spawnerObject.GetComponent<EnemySpawner>());
        }

        characterController = characterControllerObject.GetComponent<CharacterHealth>();
        var ecgReceiver = GameObject.Find("ECGReceiver");
        if (ecgReceiver != null )
        {
            this.gameObject.SetActive(true);
            var ecgReceiverScript = ecgReceiver.GetComponent<ECGReceiver>();
            if (ecgReceiverScript != null)
            {
                HRValueComponent = HRTextObject.GetComponent<TextMeshProUGUI>();
                ecgReceiverScript.receivedHR.AddListener(SetCounter);
            }
        }
        else
            this.gameObject.SetActive(false);
    }

    private void SetCounter(int hr)
    {
        SetMultipliers(hr);

        HRValueComponent.SetText(Convert.ToString(hr));
    }

    private void SetMultipliers(int hr)
    {
        float multiplier = hr / 70.0f;
        
        foreach (var spawner in spawners)
        {
            spawner.intervalMultiplier = multiplier;
        }

        characterController.hpRegenMultiplier = multiplier;
    }
}
