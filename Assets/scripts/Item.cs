using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite icon;

    public Image image;
    public TextMeshProUGUI  nameText;
    public TextMeshProUGUI  descriptionText;
    
    void Update()
    {
        image.sprite = icon;
    }
}
