using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using System.Collections.Generic;

public class FireElectricity : MonoBehaviour
{
    [SerializeField]
    private GameObject electricityOrigin;

    [SerializeField]
    private ParticleSystem ps;
    private ParticleSystem.MainModule pMain;

    [SerializeField]
    private Transform target = null;

    [SerializeField]
    private LayerMask layerMask;

    private bool m_bFire = false;
    private bool m_bPlaying = false;

    private ParticleSystem.Particle[] m_Particles;
    private Color color = new Color(0.0f, 0.894f, 1.0f);

    void Start()
    {
        pMain = ps.main;
    }

    public void HandleShooting(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) { m_bFire = true; }
        if (ctx.canceled) { m_bFire = false; }
    }

    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        if (m_bFire)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawLine(electricityOrigin.transform.position, worldPosition, color, 1, false);
            float dist = Vector3.Distance(electricityOrigin.transform.position, worldPosition);
            pMain.startSize = new ParticleSystem.MinMaxCurve(0.1f, dist / 100.0f);

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, mousePos, layerMask);

            RaycastHit2D[] hitArr = hits.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).ToArray<RaycastHit2D>();

            if( hitArr.Length > 0 && hitArr[0].collider != null)
            {
                target = hitArr[0].collider.gameObject.transform;
            }

            if (target != null)
            {
                EnemyCharge ec = target.GetComponent<EnemyCharge>();
                ec.ChargeEnemy(Random.Range(30.0f, 40.0f) * Time.deltaTime, new HashSet<GameObject> { target.gameObject });
            }


            //RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, layerMask);
            //RaycastHit2D hit = Physics2D.GetRayIntersection(new Ray(rayOrig, rayDest), Mathf.Infinity, layerMask);

            if (!m_bPlaying)
            {
                m_bPlaying = true;
                ps.Play();
            }
        }
        else
        {
            m_bPlaying = false;
            ps.Stop();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            ps.Stop();
        }
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        m_Particles = new ParticleSystem.Particle[ps.particleCount];
        int numParticles = ps.GetParticles(m_Particles);
        
        for (var i = 0; i < m_Particles.Length; i++)
        {
            ParticleSystem.Particle p = m_Particles[i];
            

            Vector3 particleWorldPosition;

            if (ps.main.simulationSpace == ParticleSystemSimulationSpace.Local)
            {
                particleWorldPosition = transform.TransformPoint(p.position);
            } else if(ps.main.simulationSpace == ParticleSystemSimulationSpace.Custom)
            {
                particleWorldPosition = ps.main.customSimulationSpace.TransformPoint(p.position);
            }
            else
            {
                particleWorldPosition = p.position;
            }

            Vector3 directionToTarget = (target.position - particleWorldPosition).normalized;
            Vector3 seekForce = (directionToTarget * 10.0f) * Time.deltaTime;

            p.velocity = seekForce;
            //p.position = p.position + (target.position - p.position) / (p.remainingLifetime) * Time.deltaTime;
            p.position = Vector3.Lerp(p.position, target.position, (p.startLifetime - p.remainingLifetime) / p.startLifetime);
            
            m_Particles[i] = p;
        }
        ps.SetParticles(m_Particles, numParticles);
    }
}
