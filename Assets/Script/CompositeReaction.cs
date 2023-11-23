using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompositeReaction : MonoBehaviour
{
    public bool Simulate = false;
    public GameObject RaggioGamma;
    public GameObject Composite;
    public GameObject He3;
    public GameObject He4;
    public GameObject Be8;
    public GameObject C12;
    public GameObject O16;

    public float Be8ReactionTime = 10;

    private bool onCommand = false;
    private float Be8Rection_startTime = 0;
    private MyParticleScript MyParticleScript;

    private List<Vector3> childPosition = new List<Vector3>();

    private System.Random random = new System.Random();

    // Use this for initialization
    void Start()
    {
        if (Simulate)
            MyParticleScript = GameObject.Find("MyPersonalParticleSystem").GetComponent<MyParticleScript>();
        onCommand = false;
        if (tag == "Be8")
            Be8Rection_startTime = Time.time;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            childPosition.Add(gameObject.transform.GetChild(i).localPosition);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "Be8" && gameObject.transform.parent != null)
        {
            //sono nello storage, fermo il tempo
            Be8Rection_startTime = Time.time;
        }
        if (tag == "Be8" && ((Time.time - Be8Rection_startTime) > Be8ReactionTime))
        {
            Be8InverseRection();
        }

        SetupGameObject();

        Rigidbody r = gameObject.GetComponent<Rigidbody>();
        if (r != null)
        {
            if (!Simulate)
                r.position = new Vector3(r.position.x, r.position.y, -2.69f);
            else
            {
                if (GameObject.Find("Plane") != null && r.position.z < GameObject.Find("Plane").transform.position.z)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (Simulate)
            {
                if (gameObject.transform.position.z < GameObject.Find("Plane").transform.position.z)
                {
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }

    private void SetupGameObject()
    {
        if (gameObject.transform.parent != null)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
        else
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            if (gameObject.GetComponent<Rigidbody>() == null)
            {
                Rigidbody r = gameObject.AddComponent<Rigidbody>();
                r.useGravity = false;
            }
        }

        for (int i = 0; i < childPosition.Count; i++)
        {
            gameObject.transform.GetChild(i).localPosition = childPosition[i];
        }
    }

    private static int numberCollisions = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider>() != null && Simulate)
        {
            //Vector3 toReflect = gameObject.GetComponent<Rigidbody>().velocity;
            //Vector3 normal = collision.gameObject.transform.position;
            //if (collision.gameObject.name == "sx")
            //    normal = Vector3.right;
            //if (collision.gameObject.name == "dx")
            //    normal = Vector3.left;
            //if (collision.gameObject.name == "up")
            //    normal = Vector3.down;
            //if (collision.gameObject.name == "down")
            //    normal = Vector3.up;
            //if (collision.gameObject.name == "front")
            //    normal = Vector3.back;
            //if (collision.gameObject.name == "back")
            //    normal = Vector3.forward;
            //gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Reflect(toReflect, normal) * .1f);


            Vector3 force2center = gameObject.transform.position - GameObject.Find("Centre").transform.position;
            force2center.z = 0;
            gameObject.GetComponent<Rigidbody>().AddForce(force2center);

            return;
        }

        if (collision.gameObject.GetComponent<BoxCollider>() != null)
            return;

        if (Simulate && MyParticleScript == null)
            return;

        numberCollisions++;
        if (Simulate && (numberCollisions % UnityEngine.Random.Range(1, MyParticleScript.SimulationRandomity) != 0))
            return;

        if (gameObject.tag == "kinect" || collision.gameObject.tag == "kinect")
            return;

        if (gameObject.tag == "cube" || collision.gameObject.tag == "cube")
            return;

        Scene scene = SceneManager.GetActiveScene();

        if (gameObject.tag == "Be8" && collision.gameObject.tag == "He4" && scene.name.StartsWith("HeBurning"))
            C12Reaction(collision);

        if (gameObject.tag == "He4" && collision.gameObject.tag == "He4" && scene.name.StartsWith("HeBurning"))
            Be8Rection(collision);

        if (gameObject.tag == "D" && collision.gameObject.transform.childCount == 0)
            Elio3Reaction(collision);

        // reazione che coinvolge solo due atomi di elio3
        if (gameObject.tag == "He3" && collision.gameObject.tag == "He3")
            Elio4Reaction(collision);

        if (gameObject.tag == "C12" && collision.gameObject.tag == "He4" && scene.name.StartsWith("HeBurning"))
            O16Reaction(collision);
    }

    private void O16Reaction(Collision collision)
    {
        if (gameObject.tag != "C12")
            return;

        GameObject o16 = Instantiate(O16, gameObject.transform.position, Quaternion.identity);
        o16.name = o16.name + random.NextDouble().ToString(".0000");
        //if (Simulate)
        //    c12.transform.localScale = MyParticleScript.SimulationScale;
        o16.GetComponent<CompositeReaction>().Simulate = Simulate;
        o16.GetComponent<Rigidbody>().AddTorque(transform.up * UnityEngine.Random.Range(-1, 1));
        CreateGammaRay();

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    private void C12Reaction(Collision collision)
    {
        if (gameObject.tag != "Be8")
            return;

        GameObject c12 = Instantiate(C12, gameObject.transform.position, Quaternion.identity);
        c12.name = c12.name + random.NextDouble().ToString(".0000");
        //if (Simulate)
        //    c12.transform.localScale = MyParticleScript.SimulationScale;
        c12.GetComponent<CompositeReaction>().Simulate = Simulate;
        c12.GetComponent<Rigidbody>().AddTorque(transform.up * UnityEngine.Random.Range(-1, 1));
        CreateGammaRay();

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    private void CreateGammaRay()
    {
        //raggio gamma
        GameObject raggiogamma = Instantiate(RaggioGamma);
        raggiogamma.GetComponent<ShowLabel>().Simulate = Simulate;
        VolumetricLines.VolumetricLineBehavior behavior = raggiogamma.GetComponent<VolumetricLines.VolumetricLineBehavior>();
        behavior.StartPos = gameObject.transform.position;
        float x = -1, y = -1;
        if (UnityEngine.Random.Range(-20000, 20000) % 2 == 0)
            x = 1;
        if (UnityEngine.Random.Range(-20000, 20000) % 2 == 0)
            y = 1;
        behavior.EndPos = new Vector3(x, y, behavior.StartPos.z);
        raggiogamma.transform.position = behavior.StartPos - behavior.EndPos;
        raggiogamma.GetComponent<Rigidbody>().AddForce((behavior.EndPos - behavior.StartPos) * 15);
        Destroy(raggiogamma, 15);
    }

    private void Be8InverseRection()
    {
        float scaleFactor = .9f;
        GameObject he4 = Instantiate(He4, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - (He4.gameObject.transform.localScale.y / scaleFactor), 0), Quaternion.identity);
        he4.name = he4.name + random.NextDouble().ToString(".0000");
        //if (Simulate)
        //    he4.transform.localScale = MyParticleScript.SimulationScale;
        he4.GetComponent<CompositeReaction>().Simulate = Simulate;
        he4.GetComponent<Rigidbody>().AddTorque(transform.up * UnityEngine.Random.Range(-1, 1));
        he4 = Instantiate(He4, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (He4.gameObject.transform.localScale.y / scaleFactor), 0), Quaternion.identity);
        he4.name = he4.name + random.NextDouble().ToString(".0000");
        //if (Simulate)
        //    he4.transform.localScale = MyParticleScript.SimulationScale;
        he4.GetComponent<CompositeReaction>().Simulate = Simulate;
        he4.GetComponent<Rigidbody>().AddTorque(transform.up * UnityEngine.Random.Range(-1, 1));
        Destroy(gameObject);
    }

    private void Be8Rection(Collision collision)
    {
        if (collision.gameObject.GetComponent<CompositeReaction>().onCommand)
            return;

        onCommand = true;

        GameObject be8 = Instantiate(Be8, gameObject.transform.position, gameObject.transform.rotation);
        be8.name = be8.name + random.NextDouble().ToString(".0000");
        if (Simulate)
            be8.transform.localScale = MyParticleScript.SimulationScale;
        be8.GetComponent<CompositeReaction>().Simulate = Simulate;
        be8.tag = "Be8";
        be8.GetComponent<Rigidbody>().AddTorque(transform.up * UnityEngine.Random.Range(-1, 1));

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    private void Elio3Reaction(Collision collision)
    {
        GameObject he3 = Instantiate(He3, gameObject.transform.position, gameObject.transform.rotation);
        he3.name = he3.name + random.NextDouble().ToString(".0000");
        he3.tag = "He3";
        he3.GetComponent<CompositeReaction>().Simulate = Simulate;
        Destroy(collision.gameObject);
        Destroy(gameObject);
        CreateGammaRay();
    }

    private void Elio4Reaction(Collision collision)
    {
        if (collision.gameObject.GetComponent<CompositeReaction>().onCommand)
            return;

        onCommand = true;

        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        int i = 1;
        foreach (var item in children)
        {
            if (item.gameObject.tag == "N")
                item.localPosition = item.position = new Vector3(0, 0, -.2f);

            if (item.gameObject.tag == "H")
            {
                item.localPosition = item.position = new Vector3(0, 0.2f * i, 0);
                i = i * -1;
            }
        }

        bool first = true;
        children = collision.gameObject.GetComponentsInChildren<Transform>();
        foreach (var item in children)
        {
            if (item.gameObject.tag == "N")
            {
                item.SetParent(gameObject.transform);
                item.localPosition = item.position = new Vector3(0, 0, .2f);
            }
            if (item.gameObject.tag == "H")
            {
                float ForceZ = 0;
                //if (Simulate)
                //    ForceZ = UnityEngine.Random.Range(0, MyParticleScript.MaxZ.transform.position.z);

                item.SetParent(null);
                item.name = item.name + random.NextDouble().ToString(".0000");
                item.gameObject.AddComponent<Rigidbody>();
                item.gameObject.AddComponent<MoveWithMouse>();
                item.gameObject.layer = LayerMask.NameToLayer("Hands");
                item.gameObject.GetComponent<Rigidbody>().useGravity = false;
                if (first)
                {
                    item.localPosition = item.position = new Vector3(item.position.x + 1, item.position.y, item.position.z);

                    item.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, UnityEngine.Random.Range(-1, 1), ForceZ));
                }
                else
                {
                    item.localPosition = item.position = new Vector3(item.position.x - 1, item.position.y, item.position.z);
                    item.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-1, 1), 0, ForceZ));
                }

                if (item.GetComponent<DeuterioReaction>() != null)
                    item.GetComponent<DeuterioReaction>().Simulate = Simulate;

                if (item.GetComponent<ShowLabel>() == null)
                    item.gameObject.AddComponent<ShowLabel>();

                first = false;
            }
        }
        gameObject.tag = "He4";

        //if (Simulate)
        //    gameObject.transform.localScale = MyParticleScript.SimulationScale;

        Destroy(collision.gameObject);

        onCommand = false;

    }
}