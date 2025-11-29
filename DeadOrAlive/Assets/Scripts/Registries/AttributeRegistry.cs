using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attribute Registry", menuName = "Attribute Registry")]
public class AttributeRegistry : ScriptableObject
{
    public List<GameObject> clothes;
    public List<GameObject> hair;
    public List<GameObject> accessories;
    public List<GameObject> facialHair;
    public List<string> allAttributes;
}
