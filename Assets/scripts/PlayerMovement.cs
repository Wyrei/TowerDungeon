using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public bool IsMovingToDestination { get; set; }
    private Vector3 destination;
    [SerializeField] private float Speed = 5f;

    [SerializeField] public bool MoveToEnemy { get; set; }
    public GameObject EnemyTarget;
    [SerializeField] public bool IsMoving { get; set; }
    
    public void setSelected(bool isSelected)
    {
        if (isSelected)
        {
            Debug.Log("Player selected!");
        }
        else
        {
            Debug.Log("Player deselected!");
        }
    }

    public void MoveTo(Vector3 targetPosition)
    {
        destination = targetPosition;
        IsMovingToDestination = true;
        IsMoving = true;
        MoveToEnemy = false;
    }

    void MoveToDestination()
    {
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            IsMovingToDestination = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * Speed);
        }
    }

    void MoveToEnemyPosition()
    {
        if (EnemyTarget == null)
        {
            StopMoving();
            return;
        }

        MoveTowardTarget(EnemyTarget.transform.position);

        float distance = Vector3.Distance(transform.position, EnemyTarget.transform.position);
        if (distance <= GetComponent<PlayerAttack>().AttackRange)
        {
            StopMoving();
            GetComponent<PlayerAttack>().StartAttacking();
        }
    }

    void MoveTowardTarget(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
    }

    public void StopMoving()
    {
        MoveToEnemy = false;
        IsMoving = false;
        IsMovingToDestination = false;
    }

    void Update()
    {
        if (IsMovingToDestination)
        {
            MoveToDestination();
        }
        else if (MoveToEnemy)
        {
            MoveToEnemyPosition();
        }
    }
}

