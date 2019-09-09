using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageCollision : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount > 0)
        {
            gameObject.transform.GetChild(0).localPosition = new Vector3(0, 0, -1.2f);
        }
    }
}