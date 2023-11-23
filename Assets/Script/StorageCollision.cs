using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StorageCollision : MonoBehaviour
{
    public bool kinect = true;
    private bool take_object = false;
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

        if (!kinect && (gameObject.transform.childCount > 0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject.name == gameObject.transform.name)
                    {
                        Debug.Log("Take Object");
                        take_object = true;
                        GameObject tofree = gameObject.transform.GetChild(0).gameObject;
                        tofree.transform.SetParent(null);
                        MoveWithMouse _movewithmouse = tofree.GetComponent<MoveWithMouse>();
                        if (tofree.tag == "C12" || tofree.tag == "Be8" || tofree.tag == "O16")
                            tofree.transform.localScale = _movewithmouse.GetStorageLocalScale();
                        tofree.layer = LayerMask.NameToLayer("Hands");
                        _movewithmouse.SetMoving();
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                take_object = false;
            }
        }
    }
}