using UnityEngine;

namespace PlayerAndMosnters
{
    public class BuffController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player"))
            {
                return;
            }
            col.GetComponent<CharacterController>().takeBuff();
            col.GetComponent<CharacterAttack>().takeBuff();
            col.GetComponent<CharacterHealth>().takeBuff();
            Destroy(gameObject);
        }
    }
}