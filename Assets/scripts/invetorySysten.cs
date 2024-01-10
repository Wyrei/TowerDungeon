using System;
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

    void Update()
    {
        InitializeItems();
    }

    void InitializeItems()
    {
        foreach (Item item in items)
        {
            SetItemSprite(item);
        }
    }

    void SetItemSprite(Item item)
    {
        if (item.image != null && item.icon != null)
        {
            item.image.sprite = item.icon;
        }
        if (item.nameText != null)
        {
            item.nameText.text = item.name;
        }

        if (item.descriptionText != null)
        {
            item.descriptionText.text = item.description;
        }
    }

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
