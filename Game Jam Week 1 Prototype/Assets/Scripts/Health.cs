using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100.0f;
    [SerializeField]
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("TitleScreen");
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void SetHealth(float h)
    {
        currentHealth = h;
    }
}
