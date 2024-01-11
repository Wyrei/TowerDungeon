using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class invetorySysten : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    void Update()
    {
        InitializeItems();
    }

    void InitializeItems()
    {
        foreach (Item item in items)
        {
            SetItem(item);
        }
    }

    void SetItem(Item item)
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
}
