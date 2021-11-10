using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharge : MonoBehaviour
{
    public int currentCharge;
    public int overchargeMax;
    public int maxCharge;

    public GameObject DischargeArea;
    public GameObject EnemyGfx;

    private bool isOvercharged = false;
    private bool isDestroyed = false;

    private bool hasBeenDischarged = false;
    private bool hasBeenCharged = false;

    public float maxDistance = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentCharge = maxCharge;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDestroyed)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Charges the enemy by the given amount, taking into account
    /// if the enemy becomes overcharged.
    /// </summary>
    /// <param name="chargeAmount"></param>
    public void ChargeEnemy(int chargeAmount)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        int nextChargeAmount = currentCharge + chargeAmount;

        if( nextChargeAmount > maxCharge)
        {
            if (nextChargeAmount > maxCharge + overchargeMax)
            {
                // Set currentCharge to the maximum charge with overcharge max included
                currentCharge = maxCharge + overchargeMax;
            }
            else
            {
                // We're currently overcharged, but haven't hit overcharged max
                currentCharge += chargeAmount;
            }

            hasBeenCharged = true;
            isOvercharged = true;

            // Steadily decrement overcharge amount until we reach again max charge
            StartCoroutine(DecrementChargeAmount());
        } else
        {
            // We're not overcharged, so simply increment the current charge by charge amount
            currentCharge += chargeAmount;
            hasBeenCharged = true;
            isOvercharged = false;
        }

        // Charge Nearby Enemies, as long as an enemy is within a certain
        // radius around the current enemy and currently hasn't already
        // been charged, then charge that enemy as well.
        foreach (var enemy in enemies)
        {
            EnemyCharge ec = enemy.GetComponent<EnemyCharge>();

            if (enemy != this && ec.hasBeenCharged == false)
            {
                float distance = Vector2.Distance(enemy.transform.position, this.transform.position);
                if (distance < maxDistance)
                {
                    ec.ChargeEnemy(chargeAmount);
                }
            }
        }
    }

    /// <summary>
    /// A Coroutine that randomly decrements the currentCharge everyone second while currentCharge exceeds maxCharge.
    /// </summary>
    /// <returns></returns>
    public IEnumerator DecrementChargeAmount()
    {
        while (currentCharge > maxCharge)
        {
            yield return new WaitForSeconds(1f);
            currentCharge -= Random.Range(1, 3);
        }
    }

    /// <summary>
    /// Discharges electricity from the enemy 
    /// </summary>
    /// <param name="chargeAmount"></param>
    public void DischargeEnemy(int chargeAmount)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if( currentCharge - chargeAmount < 0)
        {
            isDestroyed = true;
        }
        else
        {
            currentCharge -= chargeAmount;
            hasBeenDischarged = true;
        }

        // Discharge Nearby Enemies, as long as an enemy is within a certain
        // radius around the current enemy and currently hasn't already
        // been discharged, then discharge from that enemy as well.
        foreach (var enemy in enemies)
        {
            EnemyCharge ec = enemy.GetComponent<EnemyCharge>();
            
            if (enemy != this && ec.hasBeenDischarged == false)
            {
                float distance = Vector2.Distance(enemy.transform.position, this.transform.position);
                if (distance < maxDistance)
                {
                    ec.DischargeEnemy(chargeAmount);
                }
            }
        }
    }
}
