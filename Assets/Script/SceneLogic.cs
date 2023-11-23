using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLogic : MonoBehaviour {
    
    public GameObject BackgroundGUI;
    private string DisplayTextGUI = "";
    private bool display = false;
    public bool ResetOrChangeScene = true;
    private float showLabelStartTime = -1;
    public int SecondsForAction = 5;
    private GameObject textGO;
    private GameObject canvas;
    private Text text;
    public List<string> loadScenes = new List<string>() {"","" };

    private List<GameObject> toDisableWhenBackground;
    // Use this for initialization
    void Start () {

        toDisableWhenBackground = new List<GameObject>();
        GameObject storage = GameObject.Find("Storage");
        for (int i = 0; i < storage.transform.childCount; i++)
            toDisableWhenBackground.Add(storage.transform.GetChild(i).gameObject);
        GameObject ResetGameObject = GameObject.Find("Reset");
        GameObject ProcBurningGameObject = GameObject.Find("ProcBurning");
        toDisableWhenBackground.Add(ResetGameObject);
        toDisableWhenBackground.Add(ProcBurningGameObject);
        toDisableWhenBackground.Add(GameObject.Find("CubeH"));
        toDisableWhenBackground.Add(GameObject.Find("CubeHe3"));

        canvas = GameObject.Find("Canvas");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 10000);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.name == gameObject.transform.name)
                {
                    if (ResetOrChangeScene)
                        DisplayTextGUI = "Reset in ";
                    else
                        DisplayTextGUI = "Change process in ";

                    display = true;
                    showLabelStartTime = Time.time;
                    ShowBackgroung(true);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            display = false;
            ShowBackgroung(false);
            Destroy(textGO);
        }

        if (BackgroundGUI != null && BackgroundGUI.activeSelf && !string.IsNullOrEmpty(DisplayTextGUI) && display)
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
                    if (scene.name == "HBurning_nokinect")
                        SceneManager.LoadScene("HeBurning_nokinect");
                    else
                        SceneManager.LoadScene("HBurning_nokinect");
                    return;
                }
            }

            int sec = (SecondsForAction - ((int)(Time.time - showLabelStartTime)));
            if (sec <= 5)
            {
                string text2Display = DisplayTextGUI + (SecondsForAction - ((int)(Time.time - showLabelStartTime))) + " ss.";
                ShowLabel(text2Display);
            }
        }
    }

    private void ShowBackgroung(bool show)
    {
        BackgroundGUI.SetActive(show);
        foreach (var item in toDisableWhenBackground)
            item.GetComponent<MeshRenderer>().enabled = !show;

        GameObject canvas = GameObject.Find("Canvas");
        for (int i = 0; i < canvas.transform.childCount; i++)
            canvas.transform.GetChild(i).gameObject.SetActive(!show);
    }

    private void ShowLabel(string str)
    {
        Vector3 centerScreen = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, BackgroundGUI.transform.position.z);
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(centerScreen);
        if (text == null)
        {
            textGO = new GameObject();
            textGO.name = "textGo";
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
}
