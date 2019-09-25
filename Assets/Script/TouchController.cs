using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    public GameObject BackgroundGUI;
    public int SecondsForAction = 5;
    protected GameObject MoveThisObj;
    protected bool DisplayGUIBool = false;

    private GameObject otherhand;
    private int retry = 12;
    private float ssBetweenCollision = 0.5f;
    private string DisplayTextGUI = "";
    private GameObject textGO;
    private GameObject canvas;
    private Text text;
    private float lastCollision = -1;
    private float showLabelStartTime = -1;

    private GameObject ResetGameObject;
    private GameObject ProcBurningGameObject;

    private List<GameObject> toDisableWhenBackground;

    // Use this for initialization
    void Start()
    {
        toDisableWhenBackground = new List<GameObject>();
        GameObject storage = GameObject.Find("Storage");
        for (int i = 0; i < storage.transform.childCount; i++)
            toDisableWhenBackground.Add(storage.transform.GetChild(i).gameObject);
        ResetGameObject = GameObject.Find("Reset");
        ProcBurningGameObject = GameObject.Find("ProcBurning");
        toDisableWhenBackground.Add(ResetGameObject);
        toDisableWhenBackground.Add(ProcBurningGameObject);
        toDisableWhenBackground.Add(GameObject.Find("CubeH"));
        toDisableWhenBackground.Add(GameObject.Find("CubeHe3"));

        canvas = GameObject.Find("Canvas");

        if (gameObject.name == "lefthand")
            otherhand = GameObject.Find("righthand");
        else
            otherhand = GameObject.Find("lefthand");
    }

    // Update is called once per frame
    void Update()
    {
        if (BackgroundGUI != null && BackgroundGUI.activeSelf && !string.IsNullOrEmpty(DisplayTextGUI))
        {
            if (Time.time - showLabelStartTime > SecondsForAction)
            {
                Scene scene = SceneManager.GetActiveScene();
                if (DisplayTextGUI.StartsWith("Reset"))
                {
                    SceneManager.LoadScene(scene.name);
                    return;
                }

                if (DisplayTextGUI.StartsWith("Change"))
                {
                    if (scene.name == "HBurning")
                        SceneManager.LoadScene("HeBurning");
                    else
                        SceneManager.LoadScene("HBurning");
                    return;
                }
            }

            string text2Display = DisplayTextGUI + (SecondsForAction - ((int)(Time.time - showLabelStartTime))) + " ss.";
            ShowLabel(text2Display);
        }

        if (MoveThisObj != null)
        {
            Rigidbody rigidBody = MoveThisObj.GetComponent<Rigidbody>();
            if (rigidBody == null)
            {
                retry--;
                if (retry == 0)
                {
                    MoveThisObj = null;
                    retry = 12;
                }
            }
            else
            {
                rigidBody.angularVelocity = Vector3.zero;
                rigidBody.velocity = Vector3.zero;
                rigidBody.position = MoveThisObj.transform.position = MoveThisObj.transform.localPosition = gameObject.transform.position;
            }
        }

        DisplayGUI();
    }

    private void DisplayGUI()
    {
        float X = ResetGameObject.gameObject.transform.localPosition.x - (ResetGameObject.GetComponent<Renderer>().bounds.size.x / 2);
        float Y = ResetGameObject.gameObject.transform.localPosition.y + (ResetGameObject.GetComponent<Renderer>().bounds.size.y / 2);
        bool display = false;
        if (gameObject.transform.localPosition.x > X && gameObject.transform.localPosition.y < Y)
        {
            DisplayTextGUI = "Reset in ";
            display = true;
        }
        X = ProcBurningGameObject.gameObject.transform.localPosition.x + (ProcBurningGameObject.GetComponent<Renderer>().bounds.size.x / 2);
        Y = ProcBurningGameObject.gameObject.transform.localPosition.y + (ProcBurningGameObject.GetComponent<Renderer>().bounds.size.y / 2);
        if (gameObject.transform.localPosition.x < X && gameObject.transform.localPosition.y < Y)
        {
            DisplayTextGUI = "Change process in ";
            display = true;
        }
        // start timing
        if(!display)
            DisplayGUIBool = false;
        else
        {
            if (!DisplayGUIBool)
            {
                DisplayGUIBool = true;
                showLabelStartTime = Time.time;
                ShowBackgroung(true);
            }
        }

        if (!DisplayGUIBool && !otherhand.GetComponent<TouchController>().DisplayGUIBool)
        {
            ShowBackgroung(false);
            Destroy(textGO);
        }
    }

    private void ShowLabel(string str)
    {
        Vector3 centerScreen = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, BackgroundGUI.transform.position.z);
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(centerScreen);
        if (text == null)
        {
            textGO = new GameObject();
            textGO.transform.parent = canvas.transform;
            text = textGO.AddComponent<Text>();
            text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            text.alignment = TextAnchor.MiddleCenter;
            text.fontSize = 30;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.color = Color.white;
        }

        text.text = str;
        text.rectTransform.position = screenPoint;
    }


    private Vector3 validDirection = Vector3.up;  // What you consider to be upwards
    private float contactThreshold = 30;          // Acceptable difference in degrees
    private Vector3 storageLocalScale = Vector3.zero;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter: " + gameObject.name + " - " + collision.gameObject.name);
        if (gameObject.tag != "kinect")
            return;

        if (collision.gameObject.tag == "storagecube" && (Time.time - lastCollision) > ssBetweenCollision)
        {
            Debug.Log("Storagecube collision enter");

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
            if (MoveThisObj != null && collision.gameObject.transform.childCount == 0)
            {
                Debug.Log("Store Object");
                storageLocalScale = MoveThisObj.transform.localScale;
                MoveThisObj.transform.SetParent(collision.gameObject.transform);
                MoveThisObj.transform.localPosition = new Vector3(0, 0, -1.2f);
                if (MoveThisObj.tag == "C12" || MoveThisObj.tag == "Be8" || MoveThisObj.tag == "O16")
                    MoveThisObj.transform.localScale = new Vector3(1, 1, 1);
                MoveThisObj = null;
                return;
            }

            if (MoveThisObj == null && collision.gameObject.transform.childCount > 0)
            {
                Debug.Log("Take Object");
                GameObject tofree = collision.gameObject.transform.GetChild(0).gameObject;
                tofree.transform.SetParent(null);
                if (tofree.tag == "C12" || tofree.tag == "Be8" || tofree.tag == "O16")
                    tofree.transform.localScale = storageLocalScale;
                MoveThisObj = tofree;
                Debug.Log("MoveThisObj not null" + MoveThisObj);
                return;
            }
        }

        // crea oggetto
        if (collision.gameObject.tag == "newobjcube" && MoveThisObj == null)
        {
            GameObject obj = Instantiate(collision.gameObject.GetComponent<NewObjStorageCollision>().InstantiateOnClickObject, gameObject.transform.position, Quaternion.identity);
            MoveThisObj = obj;
            return;
        }

        // muovi oggetto
        if (MoveThisObj == null &&
            !(collision.gameObject.tag == "newobjcube" ||
              collision.gameObject.tag == "storagecube" ||
              collision.gameObject.tag == "command") &&
            (collision.gameObject.transform.parent == null ||
            !(collision.gameObject.transform.parent.gameObject.tag == "newobjcube" ||
              collision.gameObject.transform.parent.gameObject.tag == "storagecube")))
        {
            if (otherhand.GetComponent<TouchController>().MoveThisObj == collision.gameObject)
                return;

            if (collision.gameObject.transform.parent != null)
                MoveThisObj = collision.gameObject.transform.parent.gameObject;
            else
                MoveThisObj = collision.gameObject;

            if (otherhand.GetComponent<TouchController>().MoveThisObj == MoveThisObj)
                MoveThisObj = null;
        }

        //if (collision.gameObject.tag == "command")
        //{
        //    showLabelStartTime = Time.time;
        //    ShowBackgroung(true);
        //    if (collision.gameObject.name == "Reset")
        //    {
        //        DisplayTextGUI = "Reset in ";
        //    }

        //    if (collision.gameObject.name == "ProcBurning")
        //    {
        //        DisplayTextGUI = "Change process in ";
        //    }
        //}

        if (collision.gameObject.GetComponent<BoxCollider>() != null)
        {
            Debug.Log("Out of grace");
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "command")
    //    {
    //        ShowBackgroung(false);
    //        Destroy(textGO);
    //    }
    //}

    private void ShowBackgroung(bool show)
    {
        BackgroundGUI.SetActive(show);
        foreach (var item in toDisableWhenBackground)
            item.GetComponent<MeshRenderer>().enabled = !show;

        GameObject canvas = GameObject.Find("Canvas");
        for (int i = 0; i < canvas.transform.childCount; i++)
            canvas.transform.GetChild(i).gameObject.SetActive(!show);
    }
}