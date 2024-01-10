using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit : MonoBehaviour
{
    void Start()
    {
       unitselections.Instance.unitlist.Add(this.gameObject);
    }

    void OnDestroy()
    {
        unitselections.Instance.unitlist.Remove(this.gameObject);
    }
}
