using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonHitbox : MonoBehaviour
{
    public PersonDocument parent;

    void OnMouseDown()
    {
        parent.OnPersonClicked();
    }
}
