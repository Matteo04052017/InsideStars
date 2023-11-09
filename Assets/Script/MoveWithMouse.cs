using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMouse : MonoBehaviour
{
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        DetectObjectWithRaycast();
        if (moving)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);

        }
    }

    public void DetectObjectWithRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int layer = 9;
            int layerMask = 1 << layer;

            RaycastHit[] hits = Physics.RaycastAll(ray, 10000, layerMask);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.name == gameObject.transform.name)
                {
                    moving = true;
                    Debug.Log("moving");
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            moving = false;
            Debug.Log("stop moving");
        }
    }
}