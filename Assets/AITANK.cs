using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITANK : MonoBehaviour
{
    public Tank tank;                        // Reference to the tank script
    public Transform target;                 // Reference to the target transform                // Reference to the target transform
    public float moveSpeed = 70f;            // The move speed of the tank
    public float turnSpeed = 100f;           // The turn speed of the tank
    public float projectileSpeed = 13f;      // The speed of the tank's projectiles
    public float reloadSpeed = 1f;           // The reload speed of the tank
    public float hearRadius = 10f;           // The radius within which the AI tank can hear the target
    public float sightRadius = 20f;          // The radius within which the AI tank can see the target\
    public Transform[] patrolPoints;


    private bool canMove = true;             // Can the tank move?
    private bool canShoot = true;            // Can the tank shoot?
    private float nextFireTime = 0f;         // The time when the tank can shoot again
    private int currentPointIndex = 0;

    private bool CanHear()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        return (distanceToTarget <= hearRadius);
    }

    private bool CanSee()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, sightRadius);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }



    [Header("Components / Objects")]
    public Rigidbody2D rig;                 //The tank's Rigidbody2D component. 
    public GameObject projectile;           //The projectile prefab of which the tank can shoot.
    public GameObject deathParticleEffect;  //The particle effect prefab that plays when the tank dies.
    public Transform muzzle;                //The muzzle of the tank. This is where the projectile will spawn.
    public Game game;

    void Start()
    {
        // Set the tank's start values
        tank.SetStartValues();


    }

    void Update()
    {
        bool canHear = CanHear();
        bool canSee = CanSee();

        // Move towards the target only if it's within a certain range
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= tank.attackRange)
        {
            if (tank.health <= tank.maxHealth / 2)
            {
                Flee();
            }
            else
            {
                Attack();
            }

        }
      
    }



    void Attack()
    {
        Vector3 attackDirection = target.position - transform.position;
        float attackAngle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion attackRotation = Quaternion.AngleAxis(attackAngle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, attackRotation, turnSpeed * Time.deltaTime);
        transform.position += transform.up * moveSpeed * Time.deltaTime;

        // Shoot at the target only if the tank is not damaged below a certain threshold
        if (canShoot && Time.time > nextFireTime && tank.health > tank.fleeHealthThreshold)
        {
            Debug.Log("AI tank is shooting!");
            GetComponent<Tank>().Shoot();
            nextFireTime = Time.time + reloadSpeed;
        }
        else
        {
            Debug.Log("AI tank cannot shoot.");
            Debug.Log("AI tank cannot shoot.  Info:\n\nTime.time: " + Time.time + "\nNext Fire Time: " + nextFireTime + "\nTank health: " + tank.health + "\nTank Flee Threshold: " + tank.fleeHealthThreshold);
        }
    }

    void Flee()
    {
        Vector3 fleeDirection = transform.position - target.position;
        float fleeAngle = Mathf.Atan2(fleeDirection.y, fleeDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion fleeRotation = Quaternion.AngleAxis(fleeAngle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, fleeRotation, turnSpeed * Time.deltaTime);
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}