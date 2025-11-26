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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
