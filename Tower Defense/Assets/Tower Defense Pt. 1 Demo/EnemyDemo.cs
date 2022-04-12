using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyDemo : MonoBehaviour
{
    // todo #1 set up properties
    public int health = 100;
    public float speed = 3f;
    public Image healthbar;
    TurretPlatform _turretPlatform;
    public float range = 15f;
    public GameObject deathEffect;
    public GameObject bulletPrefab;
    public String turretTag = "Turret";
    public Transform firePoint;
    public Transform target;
    public float savedSpeed;
    
    public float fireRate = 1f;

    private float fireCountdown = 0f;

    public List<Transform> waypointList;

    private int targetWaypointIndex;
    
    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(3);
    }
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
        InvokeRepeating("UpdateTarget",0f,0.5f);
        targetWaypointIndex = 1;
        savedSpeed = speed;
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

        if (Vector3.Distance(transform.position, targetPosition) <= 0.2f)
        {
            TargetNextWaypoint();
        }
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }
    
    void UpdateTarget()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag(turretTag);
        if (turrets != null)
        {
            foreach (GameObject turret in turrets)
            {
                target = turret.transform;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        healthbar.fillAmount = health / 100f;

        if (healthbar.fillAmount == 0f)
        {
            StartCoroutine(WaitForSceneLoad());
            GameObject effect = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect,1f);
            Destroy(gameObject);
            PlayerStats.Money += 4;
        }
    }
    
    void Shoot()
    {
        GameObject  bulletSet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletSet.GetComponent<Bullet>();
        
        if(bullet != null)
            bullet.Seek(target);
    }

    //-----------------------------------------------------------------------------
    private void TargetNextWaypoint()
    {
        if (targetWaypointIndex >= waypointList.Count - 1)
        {
            SceneManager.LoadScene(2);
            Destroy(gameObject);
        }

        targetWaypointIndex++;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    
}
