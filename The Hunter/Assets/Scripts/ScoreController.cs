using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var score = player.GetComponent<CharacterController>().score;
        gameObject.GetComponent<TextMeshProUGUI>().SetText(score.ToString());
    }
}
