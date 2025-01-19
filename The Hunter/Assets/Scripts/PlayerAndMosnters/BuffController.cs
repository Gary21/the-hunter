using UnityEngine;

namespace PlayerAndMosnters
{
    public class BuffController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player"))
            {
                return;
            }
            col.gameObject.GetComponent<CharacterController>().takeBuff();
            //col.gameObject.GetComponentInChildren<CharacterAttack>().takeBuff();
            col.gameObject.GetComponent<CharacterHealth>().takeBuff();
            Destroy(gameObject);
        }
    }
}