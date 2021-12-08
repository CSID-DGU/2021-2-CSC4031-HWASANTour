using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanRotation : MonoBehaviour
{
    public GameObject character;

    // Update is called once per frame
    void Update()
    {
        character.transform.rotation = Quaternion.identity;
    }
}
