using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationChangeScene : MonoBehaviour {
    public int Second2Change = 30;
    private float StartTime;
	public bool kinect = true;
	// Use this for initialization
	void Start () {
        StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - StartTime > Second2Change)
			if (kinect)
				SceneManager.LoadScene("HBurning");
            else
                SceneManager.LoadScene("HBurning_nokinect");

    }
}
