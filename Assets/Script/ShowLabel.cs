using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLabel : MonoBehaviour
{
    public float offset = .5f;
    public bool Simulate = false;
    private Text text;
    private GameObject textGO;
    private GameObject canvas;
    // Use this for initialization
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas == null || Simulate)
            return;

        if (gameObject.transform.parent != null)// && !(gameObject.transform.parent.gameObject.tag == "newobjcube" || gameObject.transform.parent.gameObject.tag == "storagecube"))
        {
            Destroy(textGO);
            return;
        }

        if (gameObject.GetComponent<CompositeReaction>() != null && gameObject.GetComponent<CompositeReaction>().Simulate)
            return;

        if (gameObject.GetComponent<DeuterioReaction>() != null && gameObject.GetComponent<DeuterioReaction>().Simulate)
            return;

        // Offset position above object bbox (in world space)
        float offsetPosY = gameObject.transform.position.y + (gameObject.transform.localScale.y / 2) + offset;

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
            text.fontSize = 22;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.black;
        }

        text.text = gameObject.tag;
        text.rectTransform.position = screenPoint;
    }

    private void OnDestroy()
    {
        Destroy(textGO);
    }
}
