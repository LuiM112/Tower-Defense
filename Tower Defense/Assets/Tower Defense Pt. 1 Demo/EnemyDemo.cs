using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDemo : MonoBehaviour
{
    // todo #1 set up properties
    public int health = 100;
    public float speed = 3f;
    public int coins = 3;
    public Image healthbar;
    TurretPlatform _turretPlatform;

    public List<Transform> waypointList;

    private int targetWaypointIndex;
    //   health, speed, coin worth
    //   waypoints
    //   delegate event for outside code to subscribe and be notified of enemy death
    public delegate void EnemyDied(EnemyDemo deadEnemy);

    public event EnemyDied onEnemyDied;
    // NOTE! This code should work for any speed value (large or small)

    //-----------------------------------------------------------------------------
    void Start()
    {
        // todo #2
        //   Place our enemy at the starting waypoint
        transform.position = waypointList[0].position;
        targetWaypointIndex = 1;
    }

    //-----------------------------------------------------------------------------
    void Update()
    {
        // todo #3 Move towards the next waypoint
        int size = waypointList.Count;
        Vector3 targetPosition = waypointList[targetWaypointIndex].position;
        Vector3 movementDir = (targetPosition - transform.position).normalized;

        Vector3 newPosition = transform.position;
        newPosition += movementDir * speed * Time.deltaTime;

        transform.position = newPosition;
        // todo #4 Check if destination reaches or passed and change target
        bool enemyDied = false;
        if (enemyDied)
        {
            onEnemyDied.Invoke(this);
        }

        if (Vector3.Distance(transform.position, targetPosition) <= 0.2f)
        {
            TargetNextWaypoint();
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        healthbar.fillAmount = health / 100f;

        if (healthbar.fillAmount == 0f)
        {
            Destroy(gameObject);
            PlayerStats.Money += 4;
        }
    }

    //-----------------------------------------------------------------------------
    private void TargetNextWaypoint()
    {
        if (targetWaypointIndex >= waypointList.Count - 1)
        {
            Destroy(gameObject);
        }

        targetWaypointIndex++;
    }
}
