using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public bool movingToDestination = false;
    private Vector3 destination;
    public float speed = 5f;

    public bool MoveToEnemy = false;
    public GameObject enemyTarget;
    private bool attacks;

    public GameObject _gameObject;
    public float attackTimer;
    public float resetTimer;

    public bool _isSelected;
    public bool isMoving = false;

    public float ProjectileSpeed = 10f;
    public float AttackRange = 1.5f;

    [SerializeField] private float ForwardOffset;

    [SerializeField] private AttackKindEnum state;

    public void SetSelected(bool isSelected)
    {
        _isSelected = isSelected;
    }

    public void MoveTo(Vector3 position)
    {
        destination = position;
        movingToDestination = true;
        isMoving = true;
        MoveToEnemy = false;
    }

    void Update()
    {
        if (_isSelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (HandleMouseClick())
                {
                    GoToPlay();
                }
                else
                {
                    movingToDestination = true;
                    isMoving = true;
                    MoveToEnemy = false;
                }
            }
        }

        if (attacks)
        {
            attack();
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (movingToDestination)
            {
                MoveToDestination();
            }
            else if (MoveToEnemy)
            {
                MoveToEnemyPosition();
            }
        }
    }

    bool HandleMouseClick()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                enemyTarget = hit.collider.gameObject;
                return true;
            }
            else
            {
                destination = hit.point;
                startMoving();
            }
        }

        return false;
    }

    void startMoving()
    {
        isMoving = true;
    }

    void MoveToDestination()
    {
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            isMoving = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
        }
    }

    void GoToPlay()
    {
        if (enemyTarget != null)
        {
            destination = enemyTarget.transform.position;
            MoveToEnemy = true;
            movingToDestination = false;
            isMoving = true;
        }
    }

    void MoveToEnemyPosition()
    {
        if (enemyTarget == null)
        {
            StopMoving();
            attacks = false;
            return;
        }

        MoveTowardTarget(enemyTarget.transform.position);

        float distance = Vector3.Distance(transform.position, enemyTarget.transform.position);
        if (distance <= AttackRange)
        {
            StopMoving();
            attacks = true;
        }
        else
        {
            attacks = false;
        }
    }

    void MoveTowardTarget(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void StopMoving()
    {
        MoveToEnemy = false;
        isMoving = false;
    }

    void CheckEnemyInRange()
    {
        float distance = Vector3.Distance(transform.position, enemyTarget.transform.position);
        if (distance > AttackRange)
        {
            attacks = false;
        }
    }

    GameObject SpawnProjectile(Vector3 spawnPosition, Vector3 direction)
    {
        GameObject projectile = Instantiate(_gameObject, spawnPosition, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(direction * ProjectileSpeed, ForceMode.Impulse);
        return projectile;
    }

    void attack()
    {
        if (enemyTarget != null)
        {
            float distance = Vector3.Distance(transform.position, enemyTarget.transform.position);

            if (distance <= AttackRange)
            {
                switch (state)
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
                MoveToEnemyPosition();
            }
        }
        else
        {
            attacks = false;
            StopMoving();
        }
    }

    bool MeleeAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            Vector3 spawnPosition = transform.position + transform.forward * ForwardOffset;
            Instantiate(_gameObject, spawnPosition, Quaternion.identity);
            attackTimer = resetTimer;
            return true;
        }
        return false;
    }

    bool MagicAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            Vector3 spawnPosition = transform.position + transform.forward * ForwardOffset;
            Vector3 direction = (enemyTarget.transform.position - spawnPosition).normalized;
            SpawnProjectile(spawnPosition, direction);
            attackTimer = resetTimer;
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}