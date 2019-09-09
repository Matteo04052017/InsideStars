using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsSafeLoadKinectManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Camera.main.GetComponent<KinectManager>().HandCursor1 = GameObject.Find("righthand");
        Camera.main.GetComponent<KinectManager>().HandCursor2 = GameObject.Find("lefthand");
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.GetComponent<KinectManager>().HandCursor1 == null ||
           Camera.main.GetComponent<KinectManager>().HandCursor2 == null)
        {
            Camera.main.GetComponent<KinectManager>().HandCursor1 = GameObject.Find("righthand");
            Camera.main.GetComponent<KinectManager>().HandCursor2 = GameObject.Find("lefthand");
        }
    }
}
