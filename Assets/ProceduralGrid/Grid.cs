using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public GameObject prefab;
    public int xSize = 35, ySize = 15;
    public float radius = 1.2f;
    private float MinX = 0, MaxX = 0;
    private float MinY = 0, MaxY = 0;

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2 screenSize;

    void Start()
    {
        
    }

    private void Awake()
    {
        Generate();
    }


    private void Generate()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        MinX = cameraPos.x - screenSize.x;
        MaxX = cameraPos.x + screenSize.x;
        MinY = cameraPos.y - screenSize.y;
        MaxY = cameraPos.y + screenSize.y;
        gameObject.transform.position = new Vector3(MinX, MinY, gameObject.transform.position.z);
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];

        float y = MinY, x = MinX;
        int k = 0;
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                vertices[k] = new Vector3(x + (radius * j), y + (radius * i), gameObject.transform.position.z);
                k++;
                Debug.Log("(x,y)= (" + vertices[i].x + ", " + vertices[i].y + ")");
            }
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            Instantiate(prefab, vertices[i], Quaternion.identity);
            DeuterioReaction d = prefab.GetComponent<DeuterioReaction>();
            if (d != null)
                d.Simulate = true;
        }
    }
}