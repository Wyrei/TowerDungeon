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
                if (hit.collider.CompareTag("Player"))
                {
                    playerBehever PlayerBehavior = hit.collider.gameObject.GetComponent<playerBehever>();
                    if (PlayerBehavior != null)
                    {
                        PlayerBehavior.setSelected(true);
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