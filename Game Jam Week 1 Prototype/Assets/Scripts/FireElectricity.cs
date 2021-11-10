using UnityEngine;

public class FireElectricity : MonoBehaviour
{
    [SerializeField]
    private GameObject electricityOrigin;

    [SerializeField]
    private ParticleSystem ps;
    private ParticleSystem.MainModule pMain;

    private Color color = new Color(0.0f, 0.894f, 1.0f);

    void Start()
    {
        pMain = ps.main;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawLine(electricityOrigin.transform.position, worldPosition, color, 1, false);
            float dist = Vector3.Distance(electricityOrigin.transform.position, worldPosition);
            pMain.startSize = new ParticleSystem.MinMaxCurve(0.1f, dist / 100.0f);
            ps.Play();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            ps.Stop();
        }
    }
}
