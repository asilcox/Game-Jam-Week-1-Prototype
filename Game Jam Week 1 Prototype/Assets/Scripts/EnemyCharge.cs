using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCharge : MonoBehaviour
{
    public float currentCharge;
    public float overchargeMax;
    public float maxCharge;

    public GameObject DischargeArea;
    public GameObject EnemyGfx;

    private bool isOvercharged = false;
    private bool isDestroyed = false;

    private bool hasBeenDischarged = false;
    private bool hasBeenCharged = false;
    private bool startedCoroutine = false;

    public float maxDistance = 1.0f;

    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        currentCharge = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDestroyed)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Score");
            Text text = go.GetComponent<Text>();
            int score = int.Parse(text.text);
            score += 10;
            text.text = score.ToString();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Charges the enemy by the given amount, taking into account
    /// if the enemy becomes overcharged.
    /// </summary>
    /// <param name="chargeAmount"></param>
    public void ChargeEnemy(float chargeAmount, HashSet<GameObject> targets)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float nextChargeAmount = currentCharge + chargeAmount;

        if( nextChargeAmount > maxCharge)
        {
            if (nextChargeAmount > maxCharge + overchargeMax)
            {

                isDestroyed = true;
                
                // currentCharge = maxCharge + overchargeMax;
            }
            else
            {
                // We're currently overcharged, but haven't hit overcharged max
                currentCharge += chargeAmount;
            }

            hasBeenCharged = true;
            isOvercharged = true;

            // Steadily decrement overcharge amount until we reach again max charge
            if (!startedCoroutine)
            {
                startedCoroutine = true;
                StartCoroutine(DecrementChargeAmount(gameObject));
            }
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

            if (enemy != this) //&& ec.hasBeenCharged == false)
            {
                float distance = Vector2.Distance(enemy.transform.position, this.transform.position);
                if (distance < maxDistance)
                {
                    if (!targets.Contains(enemy))
                    {
                        targets.Add(enemy);
                        ec.ChargeEnemy(chargeAmount, targets);
                    }
                }
            }
        }
    }

    /// <summary>
    /// A Coroutine that randomly decrements the currentCharge everyone second while currentCharge exceeds maxCharge.
    /// </summary>
    /// <returns></returns>
    public IEnumerator DecrementChargeAmount(GameObject go)
    {
        EnemyCharge ec = go.GetComponent<EnemyCharge>();
        while (ec.currentCharge > 1.0f)
        {
            ec.currentCharge -= Random.Range(3.0f, 5.0f);
            yield return new WaitForSeconds(1.0f);
        }
        ec.startedCoroutine = false;
    }

    /// <summary>
    /// Discharges electricity from the enemy 
    /// </summary>
    /// <param name="chargeAmount"></param>
    public void DischargeEnemy(float chargeAmount, HashSet<GameObject> targets)
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

            if (enemy != this) //&& ec.hasBeenDischarged == false)
            {
                if (enemy != null)
                {
                    float distance = Vector2.Distance(enemy.transform.position, this.transform.position);
                    if (distance < maxDistance)
                    {
                        if (!targets.Contains(enemy))
                        {
                            targets.Add(enemy);
                            ec.DischargeEnemy(chargeAmount, targets);
                        }
                    }
                }
            }
        }
    }
}
