using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{

    CharacterController controller;

    void Start()
    {
        controller = FindObjectOfType<CharacterController>();
        if (controller == null)
        {
            Debug.LogWarning("No OtherScript found in the scene!");
        }
    }

    void Update()
    {
        
    }

    public void OnJumpButtonClick()
    {
        Debug.Log("Jump button clicked!");
        if (controller != null)
        {
            controller.OnJumpInput();
            controller.OnJumpUpInput();
        }
            
    }

    public void OnAttackButtonClick()
    {
        Debug.Log("Attack button clicked!");
        if (controller != null)
        {
            controller.SetIsHoldingAttackButton(true);
            StartCoroutine(WaitAndStopAttack());
        }

    }

    private IEnumerator WaitAndStopAttack()
    {
        yield return new WaitForSecondsRealtime(0.5f); // Poczekaj 3 sekundy
        Debug.Log("STOP ATTACK");
        controller.SetIsHoldingAttackButton(false);
    }
}
