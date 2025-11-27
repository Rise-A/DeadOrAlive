using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PersonDocument : MonoBehaviour
{
    public AttributeRegistry attributeRegistry;
    public ColorRegistry colorRegistry;
    public List<string> tokens;

    [Header("Attribute Parents")]
    public Transform clothesParent; // If childCount == 0, then add "naked" to tokens
    public Transform accessoriesParent;
    public Transform hairParent; // If childcount == 0, then add "bald" to tokens
    public Transform facialHairParent;

    void Start()
    {
        SetAttributes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAttributes()
    {
        AddClothes();
        AddHair();
        AddAccessories();
        AddFacialHair();
    }

    public void AddClothes()
    {
        int index = GetRandomIndex(attributeRegistry.clothes.Count + 1);

        if (index == attributeRegistry.clothes.Count)
        {
            Debug.Log("Max Index For Clothes Reached: " + index);
            tokens.Add("naked");
        }
        else
        {
            GameObject clothes = SetClothes(index);
            Attribute clothesAttribute = clothes.GetComponent<Attribute>();
            tokens.Add(clothesAttribute.GetAttributeToken());

            // For color
            int colorIndex = GetRandomIndex(colorRegistry.clothingColors.Count);
            Color clothesColor = SetClothesColor(colorIndex);
            clothesAttribute.SetColor(clothesColor);
            tokens.Add(colorRegistry.clothingColorNames[colorIndex]);
        }
    }

    public void AddAccessories()
    {
        int numAccessories = Random.Range(0, 3); // 0 = no accessories, 1 = 1 accessory, 2 = both accessories

        if (numAccessories == 1)
        {
            int index = GetRandomIndex(attributeRegistry.accessories.Count);
            GameObject accessories = SetAccessory(index);
            Attribute accessoriesAttribute = accessories.GetComponent<Attribute>();
            tokens.Add(accessoriesAttribute.GetAttributeToken());

            // For color
            int colorIndex = GetRandomIndex(colorRegistry.clothingColors.Count);
            Color clothesColor = SetClothesColor(colorIndex);
            accessoriesAttribute.SetColor(clothesColor);
            tokens.Add(colorRegistry.clothingColorNames[colorIndex]);
        }

        else if (numAccessories == 2)
        {
            GameObject accessory1 = SetAccessory(0);
            Attribute accessoriesAttribute1 = accessory1.GetComponent<Attribute>();
            tokens.Add(accessoriesAttribute1.GetAttributeToken());

            // For color
            int colorIndex1 = GetRandomIndex(colorRegistry.clothingColors.Count);
            Color clothesColor1 = SetClothesColor(colorIndex1);
            accessoriesAttribute1.SetColor(clothesColor1);
            tokens.Add(colorRegistry.clothingColorNames[colorIndex1]);



            GameObject accessory2 = SetAccessory(1);
            Attribute accessoriesAttribute2 = accessory2.GetComponent<Attribute>();
            tokens.Add(accessoriesAttribute2.GetAttributeToken());

            // For color
            int colorIndex2 = GetRandomIndex(colorRegistry.clothingColors.Count);
            Color clothesColor2 = SetClothesColor(colorIndex2);
            accessoriesAttribute2.SetColor(clothesColor2);
            tokens.Add(colorRegistry.clothingColorNames[colorIndex2]);
        }
    }

    public void AddFacialHair()
    {
        int numFacialHair = Random.Range(0, 3); // 0 = no facial hair, 1 = 1 facial hair, 2 = both facial hair

        if (numFacialHair == 1)
        {
            int index = GetRandomIndex(attributeRegistry.facialHair.Count);
            GameObject facialHair = SetFacialHair(index);
            Attribute facialHairAttribute = facialHair.GetComponent<Attribute>();
            tokens.Add(facialHairAttribute.GetAttributeToken());

            // For color
            int colorIndex = GetRandomIndex(colorRegistry.hairColors.Count);
            Color hairColor = SetHairColor(colorIndex);
            facialHairAttribute.SetColor(hairColor);
            tokens.Add(colorRegistry.hairColorNames[colorIndex]);
        }

        else if (numFacialHair == 2)
        {
            GameObject facialHair1 = SetFacialHair(0);
            Attribute facialHairAttribute1 = facialHair1.GetComponent<Attribute>();
            tokens.Add(facialHairAttribute1.GetAttributeToken());

            // For color
            int colorIndex1 = GetRandomIndex(colorRegistry.hairColors.Count);
            Color hairColor1 = SetHairColor(colorIndex1);
            facialHairAttribute1.SetColor(hairColor1);
            tokens.Add(colorRegistry.hairColorNames[colorIndex1]);



            GameObject facialHair2 = SetFacialHair(1);
            Attribute facialHairAttribute2 = facialHair2.GetComponent<Attribute>();
            tokens.Add(facialHairAttribute2.GetAttributeToken());

            // For color
            int colorIndex2 = GetRandomIndex(colorRegistry.hairColors.Count);
            Color hairColor2 = SetHairColor(colorIndex2);
            facialHairAttribute2.SetColor(hairColor2);
            tokens.Add(colorRegistry.hairColorNames[colorIndex2]);
        }
    }

    public void AddHair()
    {
        int index = GetRandomIndex(attributeRegistry.hair.Count + 1);

        if (index == attributeRegistry.hair.Count)
        {
            Debug.Log("Max Index For Hair Reached: " + index);
            tokens.Add("bald");
        }
        else
        {
            GameObject hair = SetHair(index);
            Attribute hairAttribute = hair.GetComponent<Attribute>();
            tokens.Add(hairAttribute.GetAttributeToken());

            // For color
            int colorIndex = GetRandomIndex(colorRegistry.hairColors.Count);
            Color hairColor = SetHairColor(colorIndex);
            hairAttribute.SetColor(hairColor);
            tokens.Add(colorRegistry.hairColorNames[colorIndex]);
        }
    }

    public int GetRandomIndex(int maxIndex)
    {
        int index = Random.Range(0, maxIndex);
        return index;
    }
    public GameObject SetClothes(int clothesIndex)
    {
        GameObject clothing = attributeRegistry.clothes[clothesIndex];
        GameObject clothingInstance = Instantiate(clothing, clothesParent);

        return clothingInstance;
    }

    public GameObject SetAccessory(int accessoryIndex)
    {
        GameObject accessory = attributeRegistry.accessories[accessoryIndex];
        GameObject accessoryInstance = Instantiate(accessory, accessoriesParent);

        return accessoryInstance;
    }

    public GameObject SetHair(int hairIndex)
    {
        GameObject hair = attributeRegistry.hair[hairIndex];
        GameObject hairInstance = Instantiate(hair, hairParent);

        return hairInstance;
    }

    public GameObject SetFacialHair(int facialHairIndex)
    {
        GameObject facialHair = attributeRegistry.facialHair[facialHairIndex];
        GameObject facialHairInstance = Instantiate(facialHair, facialHairParent);

        return facialHairInstance;
    }

    public Color SetClothesColor(int colorIndex)
    {
        Color color = colorRegistry.clothingColors[colorIndex];
        return color;
    }

    public Color SetHairColor(int colorIndex)
    {
        Color color = colorRegistry.hairColors[colorIndex];
        return color;
    }
}
