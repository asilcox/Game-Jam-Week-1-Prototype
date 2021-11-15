using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Transform enemyGfx;

    private GameObject player;

    public EnemyCharge ec;

    public Animator animator;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        ec = GetComponent<EnemyCharge>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

        player = GameObject.FindGameObjectWithTag("Player");


        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        speed = ec.currentCharge * 2000.0f * Time.deltaTime;
        if (speed < 200.0f)
        {
            speed = 200.0f;
        }
    }

    private void AttackPlayer()
    {
        animator.SetBool("EnemyIsRunning", false);
        animator.SetBool("EnemyIsAttacking", true);

        player.GetComponent<Health>().SetHealth(player.GetComponent<Health>().GetHealth() - (0.5f * Time.deltaTime));
    }

    void FixedUpdate()
    {
        if (path == null)
            return;

        if (reachedEndOfPath)
        {
            AttackPlayer();
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            animator.SetBool("EnemyIsAttacking", false);
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        if (force.x >= 0.01f || force.x <= -0.01f)
            animator.SetBool("EnemyIsRunning", true);
        else
            animator.SetBool("EnemyIsRunning", false);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            enemyGfx.localScale = new Vector3(1f, 1f, 1f);
        } else if(force.x <= 0.01f)
        {
            enemyGfx.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
