using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private bool isDead;
    public AudioSource backgroundMusic;
    public void Die()
    {
        if (isDead) return;
        isDead = true;
        backgroundMusic.Stop();

        Debug.Log("Player died");

        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        var move = GetComponent<PlayerMovement>();
        if (move != null)
            move.enabled = false;

        GameManager.Instance.GameOver();
    }
}
