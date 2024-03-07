using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f;
    private Transform PlayerBody;
    private SC_WormController Player;

    private float stoppingDistance = 1.0f;
    public int damage = 10;
    public float damageInterval = 1.0f;
    private float damageTimer;

    private AudioSource killedSound;

    private EnemyManager enemyManager;

    void Start()
    {
        damageTimer = damageInterval;
        Player = FindObjectOfType<SC_WormController>();
        PlayerBody = GameObject.FindGameObjectWithTag("EnemyTarget").transform;
        killedSound = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();
        damageTimer += Time.deltaTime; 
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, PlayerBody.position) > stoppingDistance)
        {
            transform.LookAt(PlayerBody);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }


    private void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    void OnTriggerStay(Collider other)
    {   
        if ((other.CompareTag("Player") || other.CompareTag("EnemyTarget")) && damageTimer >= damageInterval)
        {   
            if (Player != null)
            {
                //Debug.Log("Applying damage");
                Player.TakeDamage(damage);
                damageTimer = 0; 
            }
        }

        if (other.CompareTag("Mouth"))
        {
            enemyManager.EnemyKilled(gameObject);
            Destroy(gameObject); 
            killedSound.Play();
        }
    }



}
