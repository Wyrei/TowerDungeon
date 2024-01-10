using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
     public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static UnitSelections _instance;
    public static UnitSelections Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        AddUnitToSelection(unitToAdd);
        unitToAdd.GetComponent<PlayerBehavior>().SetSelected(true);
        ActivateChildObject(unitToAdd);
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            AddUnitToSelection(unitToAdd);
            unitToAdd.GetComponent<PlayerBehavior>().SetSelected(true);
            ActivateChildObject(unitToAdd);
        }
        else
        {
            RemoveUnitFromSelection(unitToAdd);
            unitToAdd.GetComponent<PlayerBehavior>().SetSelected(false);
            DeactivateChildObject(unitToAdd);
        }
    }

    public void DragSelectMove(Vector3 targetPosition)
    {
        foreach (var unit in unitsSelected)
        {
            PlayerBehavior playerBehavior = unit.GetComponent<PlayerBehavior>();
            if (playerBehavior != null)
            {
                playerBehavior.MoveTo(targetPosition);
            }
        }
    }

    public void MoveSelectedUnits(Vector3 targetPosition)
    {
        foreach (var unit in unitsSelected)
        {
            PlayerBehavior playerBehavior = unit.GetComponent<PlayerBehavior>();
            if (playerBehavior != null)
            {
                playerBehavior.MoveTo(targetPosition);
            }
        }
    }

    public void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {
            SetUnitSelectedState(unit, false);
            DeactivateChildObject(unit);
        }
        unitsSelected.Clear();
    }

    private void AddUnitToSelection(GameObject unit)
    {
        unitsSelected.Add(unit);
        SetUnitSelectedState(unit, true);
    }

    private void RemoveUnitFromSelection(GameObject unit)
    {
        unitsSelected.Remove(unit);
        SetUnitSelectedState(unit, false);
    }

    private void SetUnitSelectedState(GameObject unit, bool isSelected)
    {
        PlayerBehavior playerBehavior = unit.GetComponent<PlayerBehavior>();
        if (playerBehavior != null)
        {
            playerBehavior.SetSelected(isSelected);
        }
    }

    private void DeactivateChildObject(GameObject unit)
    {
        Transform childTransform = unit.transform.GetChild(0);
        if (childTransform != null)
        {
            childTransform.gameObject.SetActive(false);
        }
    }

    private void ActivateChildObject(GameObject unit)
    {
        Transform childTransform = unit.transform.GetChild(0);
        if (childTransform != null)
        {
            childTransform.gameObject.SetActive(true);
        }
    }

}