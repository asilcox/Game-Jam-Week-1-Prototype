using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Health playerHealth;

    void Start()
    {
        playerHealth = player.GetComponent<Health>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerHealth.SetHealth(playerHealth.GetHealth() - 20);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerHealth.SetHealth(playerHealth.GetHealth() + 5);
        }
    }
}
