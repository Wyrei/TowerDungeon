using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera mycam;

    public GameObject groundmarker;

    public LayerMask clickable;
    public LayerMask ground;

    private void Start()
    {
        mycam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mycam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    unitselections.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    unitselections.Instance.clickselect(hit.collider.gameObject);
                    
                    if (hit.collider.CompareTag("Target"))
                    {
                        
                        PlayerAttack playerAttack = hit.collider.gameObject.GetComponent<PlayerAttack>();
                        if (playerAttack != null)
                        {
                            playerAttack.SetEnemyTarget(hit.collider.gameObject);
                        }
                    }
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    unitselections.Instance.DeselectAll();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = mycam.ScreenPointToRay((Input.mousePosition));

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundmarker.transform.position = hit.point;
                groundmarker.SetActive(false);
                groundmarker.SetActive(true);
            }
        }
    }
}