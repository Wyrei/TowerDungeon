using UnityEditor.UIElements;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera mycam;

    [SerializeField] private GameObject groundmarker;
    
    [SerializeField] private LayerMask clickable;
    [SerializeField] private LayerMask ground;
    
    
    void Start()
    {
        mycam = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftMouseClick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            HandleRightMouseClick();
        }
    }

    private void HandleLeftMouseClick()
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

    private void HandleRightMouseClick()
    {
        RaycastHit hit;
        Ray ray = mycam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            Vector3 targetPosition = hit.point;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                unitselections.Instance.DragSelectMove(targetPosition);
            }
            else
            {
                unitselections.Instance.MoveSelectedUnits(targetPosition);
            }
        }
    }
}