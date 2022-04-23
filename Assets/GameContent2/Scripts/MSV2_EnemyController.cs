using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSV2_EnemyController : MonoBehaviour
{
    public enum enemyType { Tier1, Tier2, Tier3};
    [Header("Common settings")]
    public enemyType EnemyType;
    public Transform Player;
    public bool AIEnabled;
    MSV2_PlayerController playerController;

    [Header("Health / Damage / Moving settings")]
    public float moveSpeed;
    public float MaxHealth;
    public float DamageToTake;

    private float health;
    private float minDist = 1;
    private CircleCollider2D Collider;

    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<CircleCollider2D>();
        health = MaxHealth;
        playerController = Player.gameObject.GetComponent<MSV2_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AIEnabled)
        {
            if (Vector2.Distance(transform.position, Player.position) > minDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.gameObject.tag == "Player_WP1_Bullet")
        {
            GetDamage(playerController.GetBaseDamage);
        }
    }

    public void GetDamage(float _damageValue)
    {
        if (health != 0)
        {
            if (health >= _damageValue)
            {
                health -= _damageValue;
            }
            else
            {
                health = 0;
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerStay2D(Collider2D _other)
    {
        if(_other.gameObject.tag == "PlayerTag")
        {
            playerController.GetDamage(DamageToTake);
        }
    }
}
