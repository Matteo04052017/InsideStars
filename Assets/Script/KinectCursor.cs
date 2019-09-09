using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class KinectCursor : MonoBehaviour
{
    public JointType jointId = JointType.HandLeft;
    public float easing = 0.03f;

    private GameObject sxWall;
    private GameObject dxWall;
    private GameObject upWall;
    private GameObject downWall;

    private Transform tr;

    // Use this for initialization
    void Start()
    {
        // get local component
        tr = GetComponent<Transform>();

        if (sxWall == null)
            sxWall = GameObject.Find("sx");
        if (dxWall == null)
            dxWall = GameObject.Find("dx");
        if (upWall == null)
            upWall = GameObject.Find("up");
        if (downWall == null)
            downWall = GameObject.Find("down");
    }

    // Update is called once per frame
    void Update()
    {
        if (BodySourceView.bodyTracked)
        {
            // fetch joint positions for the closest body tracked
            Vector3 joint = BodySourceView.closestJointObjs[(int)jointId].position;

            // easing towards X
            float targetX = joint.x;
            float posX = tr.position.x;
            float dx = targetX - posX;
            if (Mathf.Abs(dx) > 1)
            {
                posX += dx * easing;
            }

            // easing towards Y
            float targetY = joint.y;
            float posY = tr.position.y;
            float dy = targetY - posY;
            if (Mathf.Abs(dy) > 1)
            {
                posY += dy * easing;
            }

            // update cursor position
            if (posX < sxWall.transform.position.x ||
                posX > dxWall.transform.position.x ||
                posY > upWall.transform.position.y ||
                posY < downWall.transform.position.y)
                return;

            tr.position = new Vector3(posX, posY, tr.position.z);
        }
    }
}
