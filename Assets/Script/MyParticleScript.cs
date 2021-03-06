﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyParticleScript : MonoBehaviour
{
    public int SimulationRandomity = 2000;
    public Vector3 SimulationScale = new Vector3(1, 1, 1);
    public GameObject Hidrogen;
    public GameObject He3;
    private float MinX = 0, MaxX = 0;
    private float MinY = 0, MaxY = 0;
    public GameObject MinZ, MaxZ;
    public int MaxParticle;

    private List<GameObject> lGameObject;
    private int LastCreationTime;
    private Vector2 screenSize;
    private int indexCreation = 0;

    private int nObjectsX;
    private int nObjectsY;
    private int nObjectsZ;
    private int indexObjX = 0;
    private int indexObjY = 0;
    private int indexObjZ = 0;

    // Use this for initialization
    void Start()
    {
        if (Hidrogen == null || He3 == null)
            return;

        lGameObject = new List<GameObject>();
        //for (int i = 0; i < (MaxParticle/4); i++)
        //    CreateObject();
        Vector3 cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        MinX = cameraPos.x - screenSize.x;
        MaxX = cameraPos.x + screenSize.x;
        //if (SceneManager.GetActiveScene().name != "Simulation")
        //{
        //    //float storageHeight = GameObject.Find("Storage").transform.lossyScale.y;
        //    MinY = cameraPos.y - (screenSize.y / 2);// - storageHeight;
        //    MaxY = cameraPos.y + (screenSize.y / 2);// + storageHeight;
        //}
        //else
        //{
        MinY = cameraPos.y - screenSize.y;
        MaxY = cameraPos.y + screenSize.y;
        //}

        nObjectsX = (int)((MaxX - MinX) / He3.GetComponent<Collider>().bounds.size.x);
        nObjectsY = (int)((MaxY - MinY) / He3.GetComponent<Collider>().bounds.size.y);
        nObjectsZ = (int)((MaxZ.transform.position.z - MinZ.transform.position.z) / He3.GetComponent<Collider>().bounds.size.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Hidrogen == null || He3 == null)
            return;

        if (lGameObject.Count >= MaxParticle)
            return;

        for (int i = 0; i < 10; i++)
            CreateObject();
    }

    private void CreateObject()
    {
        GameObject obj;
        Vector3 position = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), Random.Range(MinZ.transform.position.z, MaxZ.transform.position.z));
        if (indexCreation >= 3)
        {
            obj = Instantiate(He3, position, Quaternion.identity);
            obj.GetComponent<CompositeReaction>().Simulate = true;
            obj.transform.position = position;
            indexCreation = 0;
            lGameObject.Add(obj);
        }
        //else
        //{
        //    obj = Instantiate(Hidrogen, position, Quaternion.identity);
        //    obj.GetComponent<DeuterioReaction>().Simulate = true;
        //    obj.transform.position = position;
        //}
        //obj.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0));
        indexCreation++;
    }
}
