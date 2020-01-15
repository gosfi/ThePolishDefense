﻿using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{

    private Transform target;
    private int waypointIndex = 0;

    private Enemy enemy;

    private Animator anim;
    private void Start() { Initialize(); }
    private void Update() { Refresh(); }
    private void FixedUpdate() { PhysicsRefresh(); }

    public void Initialize()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
        anim = GetComponent<Animator>();
    }

    public void PhysicsRefresh()
    {
        Vector3 dir = target.position - transform.position;
        transform.LookAt(target);
        transform.Translate(dir.normalized * enemy.speed * Time.fixedDeltaTime, Space.World);
        //Rigidbody rb;
        //rb.MovePosition

    }

    public void Refresh()
    {
        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            //check the next way point
            if (waypointIndex >= Waypoints.points.Length - 1)
            {
                enemy.isHittable = false;
                WaveSpawner.EnemiesAlive--;
                Destroy(gameObject);
                return;
            }

            waypointIndex++;
            target = Waypoints.points[waypointIndex];
        }

        enemy.speed = enemy.startSpeed;

        /*if (Vector3.Distance(transform.position, EnemyManager.Instance.spawnPoint.position)<=0.3f){
            enemy.isHittable = false;
        }
        else
        {
            enemy.isHittable = true;
        }*/
    }


}
