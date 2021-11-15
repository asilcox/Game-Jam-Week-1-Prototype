using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject pickupPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PickupSpawner());
    }

    public IEnumerator PickupSpawner()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));

            if (Random.Range(0, 100.0f) > 85.0f)
            {
                Vector2 pos = transform.position;
                Instantiate(pickupPrefab, new Vector3(Random.Range(pos.x - 2.0f, pos.x + 2.0f), Random.Range(pos.y - 2.0f, pos.y + 2.0f)), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
