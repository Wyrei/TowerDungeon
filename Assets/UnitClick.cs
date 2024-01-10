using UnityEditor.UIElements;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;

    [SerializeField] private GameObject groundMarker;

    [SerializeField] private LayerMask clickable;
    [SerializeField] private LayerMask ground;

    void Start()
    {
        myCam = Camera.main;
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
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                UnitSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
            }
            else
            {
                UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
            }
        }
        else
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                UnitSelections.Instance.DeselectAll();
            }
        }
    }

    private void HandleRightMouseClick()
    {
        RaycastHit hit;
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            Vector3 targetPosition = hit.point;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                UnitSelections.Instance.DragSelectMove(targetPosition);
            }
            else
            {
                UnitSelections.Instance.MoveSelectedUnits(targetPosition);
            }
        }
    }
}