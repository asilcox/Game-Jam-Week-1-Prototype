using UnityEngine;
using System.Collections.Generic;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private EnemyCharge enemyHealth;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            enemyHealth.DischargeEnemy(20, new HashSet<GameObject>() );
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            enemyHealth.ChargeEnemy(5, new HashSet<GameObject>() );
        }
    }
}
