using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Character obj = other.GetComponent<Character>();
        if(obj != null)
        {
            obj.RestPos();
        }
    }
}
