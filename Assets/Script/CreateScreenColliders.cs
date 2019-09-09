using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScreenColliders : MonoBehaviour
{
    private Vector2 screenSize;

    void Start()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        UpdateColliders(cameraPos);
    }

    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        float x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        float y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        if (x != screenSize.x || y != screenSize.y)
            UpdateColliders(cameraPos);
    }

    private void UpdateColliders(Vector3 cameraPos)
    {
        GameObject sx = GameObject.Find("sx");
        sx.transform.position = new Vector3(cameraPos.x - screenSize.x, sx.transform.position.y, sx.transform.position.z);
        GameObject dx = GameObject.Find("dx");
        dx.transform.position = new Vector3(cameraPos.x + screenSize.x, dx.transform.position.y, dx.transform.position.z);
        GameObject up = GameObject.Find("up");
        up.transform.position = new Vector3(up.transform.position.x, cameraPos.y + screenSize.y, up.transform.position.z);
        GameObject down = GameObject.Find("down");
        down.transform.position = new Vector3(down.transform.position.x, cameraPos.y - screenSize.y, down.transform.position.z);

        GameObject sx2 = GameObject.Find("sx2");
        if (sx2 != null)
            sx2.transform.position = new Vector3(cameraPos.x - screenSize.x, sx.transform.position.y, sx.transform.position.z);
        GameObject dx2 = GameObject.Find("dx2");
        if (dx2 != null)
            dx2.transform.position = new Vector3(cameraPos.x + screenSize.x, dx.transform.position.y, dx.transform.position.z);
        GameObject up2 = GameObject.Find("up2");
        if (up2 != null)
            up2.transform.position = new Vector3(up.transform.position.x, cameraPos.y + screenSize.y, up.transform.position.z);
        GameObject down2 = GameObject.Find("down2");
        if (down2 != null)
            down2.transform.position = new Vector3(down.transform.position.x, cameraPos.y - screenSize.y, down.transform.position.z);

        GameObject storage = GameObject.Find("Storage");
        if (storage != null)
            storage.transform.position = new Vector3(storage.transform.position.x, down.transform.position.y + 7.5f, -2.69f);

        GameObject cubeH = GameObject.Find("CubeH");
        if (cubeH != null)
            cubeH.transform.position = new Vector3(dx.transform.position.x - 1.56f, up.transform.position.y - 1.6f, -2.69f);
        GameObject cubeHe3 = GameObject.Find("CubeHe3");
        if (cubeHe3 != null)
            cubeHe3.transform.position = new Vector3(sx.transform.position.x + 1.56f, up.transform.position.y - 1.6f, -2.69f);

        GameObject reset = GameObject.Find("Reset");
        if (reset != null)
            reset.transform.position = new Vector3(dx.transform.position.x - 1.56f, down.transform.position.y + 1.6f, -2.69f);
        GameObject procBurning = GameObject.Find("ProcBurning");
        if (procBurning != null)
            procBurning.transform.position = new Vector3(sx.transform.position.x + 1.56f, down.transform.position.y + 1.6f, -2.69f);

        GameObject storage2 = GameObject.Find("Storage (1)");
        if (storage2 != null)
            storage2.transform.position = new Vector3(storage.transform.position.x, down.transform.position.y + 7.5f, storage2.transform.position.z);

        GameObject cubeH2 = GameObject.Find("CubeH (1)");
        if (cubeH2 != null)
            cubeH2.transform.position = new Vector3(dx.transform.position.x - 1.56f, up.transform.position.y - 1.6f, cubeH2.transform.position.z);
        GameObject cubeHe32 = GameObject.Find("CubeHe3 (1)");
        if (cubeHe32 != null)
            cubeHe32.transform.position = new Vector3(sx.transform.position.x + 1.56f, up.transform.position.y - 1.6f, cubeHe32.transform.position.z);

        GameObject reset2 = GameObject.Find("Reset (1)");
        if (reset2 != null)
            reset2.transform.position = new Vector3(dx.transform.position.x - 1.56f, down.transform.position.y + 1.6f, reset2.transform.position.z);
        GameObject procBurning2 = GameObject.Find("ProcBurning (1)");
        if (procBurning2 != null)
            procBurning2.transform.position = new Vector3(sx.transform.position.x + 1.56f, down.transform.position.y + 1.6f, procBurning2.transform.position.z);
    }
}