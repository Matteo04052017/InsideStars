using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProcessLabel : MonoBehaviour {
    private GameObject textGO;
    private GameObject canvas;
    private Text text;

    // Use this for initialization
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    void Update()
    {
        if (canvas == null)
            return;

        if (gameObject.transform.parent != null)// && !(gameObject.transform.parent.gameObject.tag == "newobjcube" || gameObject.transform.parent.gameObject.tag == "storagecube"))
        {
            Destroy(textGO);
            return;
        }

        // Offset position above object bbox (in world space)
        float offsetPosY = gameObject.transform.position.y;

        // Final position of marker above GO in world space
        Vector3 offsetPos = new Vector3(gameObject.transform.position.x, offsetPosY, gameObject.transform.position.z);

        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

        if (text == null)
        {
            // Create the Text GameObject.
            textGO = new GameObject();
            textGO.transform.parent = canvas.transform;
            text = textGO.AddComponent<Text>();
            text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            text.fontSize = 25;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.black;
        }

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "HBurning")
            text.text = "He4";
        else
            text.text = "H";
        text.rectTransform.position = screenPoint;
    }

    private void OnDestroy()
    {
        Destroy(textGO);
    }
}
