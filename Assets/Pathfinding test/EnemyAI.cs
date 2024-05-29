using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    private Transform playerFront;
    private Transform playerBack;

    private Transform player;

    public float nextWaypointDistance = 3f;

    public EnemyController controller;
    private BoxCollider2D cameraBounds;

    Path path;
    int currentWaypoint = 0;
    bool reached;

    Seeker seeker;
    Rigidbody2D rb;
    Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        playerFront = GameObject.FindGameObjectWithTag("PlayerAnchorFront").transform;
        playerBack = GameObject.FindGameObjectWithTag("PlayerAnchorBack").transform;
        cameraBounds = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BoxCollider2D>();

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        player = playerFront;


        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, player.position, OnPathComplete);
        }  
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            direction = new Vector2 (0, 0);
            return;
        }

        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        controller.direction = direction;
        //Debug.Log(direction);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (Vector2.Distance(rb.position, playerBack.position) < Vector2.Distance(rb.position, playerFront.position))
        {
            player = playerBack;
        }
        else
        {
            player = playerFront;
        }

        var guo = new GraphUpdateObject(cameraBounds.bounds);
        guo.updatePhysics = true;
        AstarPath.active.UpdateGraphs(guo);
    }
}
