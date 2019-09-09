using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    private Vector3 centerRotation;

    // Use this for initialization
    void Start()
    {
        centerRotation = new Vector3(gameObject.transform.position.x-.5f, gameObject.transform.position.y-.5f, gameObject.transform.position.z - .5f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.RotateAround(centerRotation, Vector3.up, 1);
    }
}
