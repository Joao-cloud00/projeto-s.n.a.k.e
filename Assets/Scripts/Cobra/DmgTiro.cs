using UnityEngine;

public class DmgTiro : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Acertou Tiro");
            Destroy(other.gameObject);
        }

    }
}
