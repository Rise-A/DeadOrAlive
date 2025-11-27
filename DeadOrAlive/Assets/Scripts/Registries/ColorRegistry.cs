using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Registry", menuName = "Color Registry")]
public class ColorRegistry : ScriptableObject
{
    public List<Color> clothingColors;
    public List<string> clothingColorNames;
    public List<Color> hairColors;
    public List<string> hairColorNames;
}
