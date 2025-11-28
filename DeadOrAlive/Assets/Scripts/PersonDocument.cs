using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class PersonDocument : MonoBehaviour
{
    [Header("Registries")]
    public AttributeRegistry attributeRegistry;
    public ColorRegistry colorRegistry;

    [Header("References")]
    public Collider2D personHitbox;
    public List<string> tokens;
    public List<PersonSprite> personSprites;
    // public List<GameObject> attributeGameObjects;
    public GameObject personSprite;
    public GameObject hiddenSprite;
    public RoundManager roundManager;

    [Header("Attribute Parents")]
    public Transform clothesParent; // If childCount == 0, then add "naked" to tokens
    public Transform accessoriesParent;
    public Transform hairParent; // If childcount == 0, then add "bald" to tokens
    public Transform facialHairParent;

    [Header("Conditions")]
    public bool destroyPersonOnClick;
    [SerializeField] private bool isWanted;
    [SerializeField] private bool isHidden;
    [SerializeField] private bool hasBeenClicked;

    // [Header("Other")]
    // public float destroyTimer;

    void Awake()
    {
        SetAttributes();
    }

    void Start()
    {
        // SetAttributes();
        isHidden = true;
        hasBeenClicked = false;

        if (isHidden)
        {
            Invoke(nameof(HidePersonSprite), 0f);
        }

        roundManager = FindObjectOfType<RoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     foreach (PersonSprite ps in personSprites)
        //     {
        //         Debug.Log(ps.sprite.name);
        //         Debug.Log(ps.color);
        //     }
        // }
        // if (isHidden)
        // {
        //     personSprite.SetActive(false);
        //     hiddenSprite.SetActive(true);
        //     Debug.Log("isHidden is true");
        // }
        // else
        // {
        //     personSprite.SetActive(true);
        //     hiddenSprite.SetActive(false);
        //     Debug.Log("isHidden is false");
        // }
    }

    public void OnPersonClicked()
    {
        isHidden = false;

        if (!isHidden && !hasBeenClicked)
        {
            Invoke(nameof(UnhidePersonSprite), 0f);
            hasBeenClicked = true;
        }

        if (!isWanted)
        {
            roundManager.SubtractFromTimer(3);
        }

        else
        {
            roundManager.AddToTimer(5);
        }
    }

    void HidePersonSprite()
    {
        personSprite.SetActive(false);
        hiddenSprite.SetActive(true);
    }

    void UnhidePersonSprite()
    {
        personSprite.SetActive(true);
        hiddenSprite.SetActive(false);

        if (hasBeenClicked && destroyPersonOnClick)
        {
            Invoke(nameof(DestroyPerson), 1.5f);
        }
    }

    void DestroyPerson()
    {
        Destroy(gameObject);
    }

    public void SetAttributes()
    {
        AddClothes();
        AddHair();
        AddAccessories();
        AddFacialHair();
    }

    // Methods for Wanted status
    ////////////////////////////
    
    public bool GetWantedStatus()
    {
        return isWanted;
    }

    public void SetWantedStatus(bool status)
    {
        isWanted = status;
    }

    // Methods to add attributes
    ////////////////////////////
    public void AddClothes()
    {
        int index = GetRandomIndex(attributeRegistry.clothes.Count + 1);

        if (index == attributeRegistry.clothes.Count)
        {
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

            // Adding to sprite list
            PersonSprite ps = new(clothesAttribute.spriteRenderer.sprite, clothesAttribute.GetColor());
            personSprites.Add(ps);
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

            // Adding to sprite list
            PersonSprite ps = new(accessoriesAttribute.spriteRenderer.sprite, accessoriesAttribute.GetColor());
            personSprites.Add(ps);
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

            // Adding to sprite list
            PersonSprite ps1 = new(accessoriesAttribute1.spriteRenderer.sprite, accessoriesAttribute1.GetColor());
            personSprites.Add(ps1);


            GameObject accessory2 = SetAccessory(1);
            Attribute accessoriesAttribute2 = accessory2.GetComponent<Attribute>();
            tokens.Add(accessoriesAttribute2.GetAttributeToken());

            // For color
            int colorIndex2 = GetRandomIndex(colorRegistry.clothingColors.Count);
            Color clothesColor2 = SetClothesColor(colorIndex2);
            accessoriesAttribute2.SetColor(clothesColor2);
            tokens.Add(colorRegistry.clothingColorNames[colorIndex2]);

            // Adding to sprite list
            PersonSprite ps2 = new(accessoriesAttribute2.spriteRenderer.sprite, accessoriesAttribute2.GetColor());
            personSprites.Add(ps2);
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

            // Adding to sprite list
            PersonSprite ps = new(facialHairAttribute.spriteRenderer.sprite, facialHairAttribute.GetColor());
            personSprites.Add(ps);
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

            // Adding to sprite list
            PersonSprite ps1 = new(facialHairAttribute1.spriteRenderer.sprite, facialHairAttribute1.GetColor());
            personSprites.Add(ps1);


            GameObject facialHair2 = SetFacialHair(1);
            Attribute facialHairAttribute2 = facialHair2.GetComponent<Attribute>();
            tokens.Add(facialHairAttribute2.GetAttributeToken());

            // For color
            int colorIndex2 = GetRandomIndex(colorRegistry.hairColors.Count);
            Color hairColor2 = SetHairColor(colorIndex2);
            facialHairAttribute2.SetColor(hairColor2);
            tokens.Add(colorRegistry.hairColorNames[colorIndex2]);

            // Adding to sprite list
            PersonSprite ps2 = new(facialHairAttribute2.spriteRenderer.sprite, facialHairAttribute2.GetColor());
            personSprites.Add(ps2);
        }
    }

    public void AddHair()
    {
        int index = GetRandomIndex(attributeRegistry.hair.Count + 1);

        if (index == attributeRegistry.hair.Count)
        {
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

            // Adding to sprite list
            PersonSprite ps = new(hairAttribute.spriteRenderer.sprite, hairAttribute.GetColor());
            personSprites.Add(ps);
        }
    }

    public int GetRandomIndex(int maxIndex)
    {
        int index = Random.Range(0, maxIndex);
        return index;
    }

    // Methods to set the attributes to add
    ///////////////////////////////////////
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

    // Color Methods
    ////////////////
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

    // Other Methods
    ////////////////
    public List<PersonSprite> GetPersonSprites()
    {
        return personSprites;
    }
    
}

[System.Serializable]
public class PersonSprite
{
    public Sprite sprite;
    public Color color;

    public PersonSprite(Sprite sprite, Color color)
    {
        this.sprite = sprite;
        this.color = color;
    }
}
