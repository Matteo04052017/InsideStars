using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationChangeScene : MonoBehaviour {
    public int Second2Change = 30;
    private float StartTime; 
	// Use this for initialization
	void Start () {
        StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - StartTime > Second2Change)
            SceneManager.LoadScene("HBurning");
	}
}
