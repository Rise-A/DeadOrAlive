using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Attribute : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public enum AttributeType
    {
        Clothing,
        Accessory,
        Hair,
        Facial_Hair
    }
    public AttributeType attributeType;
    [SerializeField] private string attributeToken;
    [SerializeField] private string colorToken;
    [SerializeField] private Color color = Color.white;

    public void SetColor(Color color)
    {
        this.color = color;
        SetSpriteColor(color);
    }

    public void SetSpriteColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public Color GetColor()
    {
        return color;
    }

    public string GetAttributeToken()
    {
        return attributeToken;
    }

    public string GetColorToken()
    {
        return colorToken;
    }
}
