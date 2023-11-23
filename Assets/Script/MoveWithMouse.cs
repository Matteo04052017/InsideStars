using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMouse : MonoBehaviour
{
    private bool moving;

    private float lastCollision = -1;
    private float ssBetweenCollision = 0.5f;
    private Vector3 validDirection = Vector3.up;  // What you consider to be upwards
    private float contactThreshold = 30;          // Acceptable difference in degrees
    private Vector3 storageLocalScale = Vector3.zero;
    private bool justStored = false;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
    }

    public Vector3 GetStorageLocalScale()
    {
        return storageLocalScale;
    }

    public void SetMoving()
    {
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        DetectObjectWithRaycast();
        //Debug.Log(gameObject.name + " moving " + moving);
        if (moving)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            Rigidbody rb = this.GetComponent<Rigidbody>();
            if (rb != null)
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public bool DetectObjectWithRaycast()
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
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            moving = false;
        }
        return moving;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (justStored)
            return;

        //Debug.Log("OnCollisionEnter: " + gameObject.name + " - " + collision.gameObject.name);

        if (collision.gameObject.tag == "storagecube" && (Time.time - lastCollision) > ssBetweenCollision)
        {
            Debug.Log("Storagecube collision enter");
            justStored = true;
            bool boolValidDirection = false;
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                if (Vector3.Angle(collision.contacts[i].normal, validDirection) <= contactThreshold)
                    boolValidDirection = true;
            }

            if (!boolValidDirection)
            {
                Debug.Log("Invalid direction for storage");
                return;
            }

            lastCollision = Time.time;
            if (collision.gameObject.transform.childCount == 0)
            {
                Debug.Log("Store Object");
                storageLocalScale = transform.localScale;
                transform.SetParent(collision.gameObject.transform);
                transform.localPosition = new Vector3(0, 0, -1.2f);
                if (tag == "C12" || tag == "Be8" || tag == "O16")
                    transform.localScale = new Vector3(1, 1, 1);
                gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                return;
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("OnCollisionExit: " + gameObject.name + " - " + collision.gameObject.name);

        if (collision.gameObject.tag == "storagecube")
        {
            justStored = false;
        }
    }
 }