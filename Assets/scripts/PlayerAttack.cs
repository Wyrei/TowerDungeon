using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool attacks;
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private float AttackTimer;
    [SerializeField] private float ResetTimer;
    [SerializeField] private AttackKindEnum State;
    [SerializeField] private float ForwardOffset;
    [SerializeField] private float ProjectileSpeed;
    [SerializeField] public float AttackRange;
    
    public void StartAttacking()
    {
        attacks = true;
    }

    void CheckEnemyInRange()
    {
        float distance = Vector3.Distance(transform.position, GetComponent<PlayerMovement>().EnemyTarget.transform.position);
        if (distance > AttackRange)
        {
            attacks = false;
        }
    }

    GameObject SpawnProjectile(Vector3 spawnPosition, Vector3 direction)
    {
        GameObject projectile = Instantiate(ProjectilePrefab, spawnPosition, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(direction * ProjectileSpeed, ForceMode.Impulse);
        return projectile;
    }

    bool MeleeAttack()
    {
        AttackTimer -= Time.deltaTime;
        if (AttackTimer <= 0)
        {
            Vector3 spawnPosition = transform.position + transform.forward * ForwardOffset;
            Instantiate(ProjectilePrefab, spawnPosition, Quaternion.identity);
            AttackTimer = ResetTimer;
            return true;
        }
        return false;
    }

    bool MagicAttack()
    {
        AttackTimer -= Time.deltaTime;
        if (AttackTimer <= 0)
        {
            Vector3 spawnPosition = transform.position + transform.forward * ForwardOffset;
            Vector3 direction = (GetComponent<PlayerMovement>().EnemyTarget.transform.position - spawnPosition).normalized;
            SpawnProjectile(spawnPosition, direction);
            AttackTimer = ResetTimer;
            return true;
        }

        return false;
    }

    void attack()
    {
        if (GetComponent<PlayerMovement>().EnemyTarget != null)
        {
            float distance = Vector3.Distance(transform.position, GetComponent<PlayerMovement>().EnemyTarget.transform.position);

            if (distance <= AttackRange)
            {
                switch (State)
                {
                    case AttackKindEnum.Melee:
                        if (MeleeAttack())
                        {
                            CheckEnemyInRange();
                        }
                        break;

                    case AttackKindEnum.Magic:
                        if (MagicAttack())
                        {
                            CheckEnemyInRange();
                        }
                        break;
                }
            }
            else
            {
                GetComponent<PlayerMovement>().MoveToEnemy = true;
            }
        }
        else
        {
            attacks = false;
            GetComponent<PlayerMovement>().StopMoving();
        }
    }

    void Update()
    {
        if (attacks)
        {
            attack();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}

