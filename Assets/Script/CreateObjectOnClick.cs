using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CreateObjectOnClick : MoveWithMouse
{
	public GameObject InstantiateOnClickObject;
    private bool click_start = false;
    private System.Random random = new System.Random();
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int layer = 9;
            int layerMask = 1 << layer;

            RaycastHit[] hits = Physics.RaycastAll(ray, 10000, layerMask);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.name == gameObject.transform.name)
                    click_start = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (click_start)
            {
                GameObject fromCubeX = GameObject.Find("CubeHe3");
                GameObject toCubeX = GameObject.Find("CubeH");
                GameObject fromCubeY = GameObject.Find("CubeHe3");
                GameObject toCubeY = GameObject.Find("ProcBurning");

                float width = fromCubeX.transform.position.x - toCubeX.transform.position.x;
                float heyght = fromCubeY.transform.position.y - toCubeY.transform.position.y;
                float samplex = (float) random.NextDouble();
                float sampley = (float)random.NextDouble();
                Vector3 pos = new Vector3((samplex * width) - fromCubeX.transform.position.x, (sampley * heyght) - fromCubeY.transform.position.y, gameObject.transform.position.z);

                GameObject newObj = Instantiate(InstantiateOnClickObject, pos, Quaternion.identity);
                newObj.name = newObj.name + samplex.ToString(".0000");

            }
            click_start = false;
        }
        
    }
}
