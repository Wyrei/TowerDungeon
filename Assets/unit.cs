using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }
}
