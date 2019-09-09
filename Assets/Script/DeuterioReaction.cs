using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeuterioReaction : MonoBehaviour
{
    public bool Simulate = false;
    public float thrust = 10;
    public Material _N;
    public GameObject Elettrone;
    public GameObject Positrone;
    public GameObject Composite;

    private bool onCommand = false;
    private MyParticleScript MyParticleScript;

    void Start()
    {
        SetupGameObject();
        if (Simulate)
            MyParticleScript = GameObject.Find("MyPersonalParticleSystem").GetComponent<MyParticleScript>();
    }

    void Update()
    {
        SetupGameObject();

        Rigidbody r = gameObject.GetComponent<Rigidbody>();
        if (r != null)
        {
            if (!Simulate)
                r.position = new Vector3(r.position.x, r.position.y, -2.69f);
            else
            {
                if (r.position.z < GameObject.Find("Plane").transform.position.z)
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
    }

    private static int numberCollisions = 0;
    private static float LastCollisionTime = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider>() != null && Simulate)
        {
            Vector3 toReflect = gameObject.GetComponent<Rigidbody>().velocity;
            Vector3 normal = collision.gameObject.transform.position;
            if (collision.gameObject.name == "sx")
                normal = Vector3.right;
            if (collision.gameObject.name == "dx")
                normal = Vector3.left;
            if (collision.gameObject.name == "up")
                normal = Vector3.down;
            if (collision.gameObject.name == "down")
                normal = Vector3.up;
            if (collision.gameObject.name == "front")
                normal = Vector3.back;
            if (collision.gameObject.name == "back")
                normal = Vector3.forward;
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Reflect(toReflect, normal) * .1f);


            Vector3 force2center = gameObject.transform.position - GameObject.Find("Centre").transform.position;
            force2center.z = 0;
            gameObject.GetComponent<Rigidbody>().AddForce(force2center);
            return;
        }

        numberCollisions++;
        if (Simulate && MyParticleScript != null && (numberCollisions % Random.Range(1, MyParticleScript.SimulationRandomity) != 0))
            return;

        if (gameObject.tag == "cube" || collision.gameObject.tag == "cube")
            return;

        if (gameObject.tag == "kinect" || collision.gameObject.tag == "kinect")
            return;

        if (gameObject.transform.parent != null)
            return;

        if (!enabled)
            return;

        if (collision.gameObject.GetComponent<BoxCollider>() != null)
            return;

        if (collision.gameObject.GetComponent<CompositeReaction>() != null)
            return;


        if (collision.gameObject.GetComponent<DeuterioReaction>() != null &&
            collision.gameObject.GetComponent<DeuterioReaction>().onCommand)
            return;

        onCommand = true;

        LastCollisionTime = Time.time;

        Rigidbody _RigidBody = gameObject.GetComponent<Rigidbody>();

        gameObject.GetComponent<Renderer>().material = _N;
        gameObject.tag = "N";

        Destroy(collision.gameObject.GetComponent<Rigidbody>());
        Destroy(_RigidBody);

        //composite
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        GameObject composite = Instantiate(Composite, pos, rot);
        composite.GetComponent<CompositeReaction>().Simulate = Simulate;
        gameObject.transform.SetParent(composite.transform);
        collision.gameObject.transform.SetParent(composite.transform);

        gameObject.transform.localPosition = gameObject.transform.position = new Vector3(0, .2f, 0);
        collision.gameObject.transform.localPosition = collision.gameObject.transform.position = new Vector3(0, -.2f, 0);

        //elettrone
        GameObject elettrone = Instantiate(Elettrone);
        elettrone.transform.position = gameObject.transform.position;
        int x = Random.Range(-1, 2);
        int y = Random.Range(-1, 2);
        elettrone.GetComponent<Rigidbody>().AddForce(new Vector3((x == 0 ? 1 : x), (y == 0 ? 1 : y), 0) * 100);
        elettrone.GetComponent<ShowLabel>().Simulate = Simulate;
        Destroy(elettrone, 15);

        //positrone
        GameObject positrone = Instantiate(Positrone);
        positrone.transform.position = gameObject.transform.position;
        x = Random.Range(-1, 2);
        y = Random.Range(-1, 2);
        positrone.GetComponent<Rigidbody>().AddForce(new Vector3((x == 0 ? 1 : x), (y == 0 ? 1 : y), 0) * 100);
        positrone.GetComponent<ShowLabel>().Simulate = Simulate;
        Destroy(positrone, 15);

        composite.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        composite.tag = "D";
    }
}
