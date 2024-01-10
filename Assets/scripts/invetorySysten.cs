using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class invetorySysten : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public UnityEvent OnItemAdded;
    public UnityEvent OnItemRemoved;
    public UnityEvent OnReset;

    public void AddItem(Item item)
    {
        items.Add(item);
        OnItemAdded.Invoke();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        OnItemRemoved.Invoke();
    }

    public void ResetInventory()
    {
        items.Clear();
        OnReset.Invoke();
    }
}
