using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_PathTraveller : MonoBehaviour
{

    public delegate void TravelFinishedDelegate();

    public Transform[] path;
    public float speed;

    int currentNode;
    float timer;

    bool isTravelling;
    TravelFinishedDelegate callback;

    public void FollowPath(Transform[] path, TravelFinishedDelegate callback)
    {
        if (isTravelling)
        {
            return;
        }

        this.path = path;
        this.callback = callback;
        currentNode = 0;
        timer = 0;
        isTravelling = true;
    }

    void checkNextNode()
    {
        if (currentNode == path.Length - 1)
        {
            isTravelling = false;
            if (this.callback != null)
            {
                this.callback();
            }
        }
        else
        {
            currentNode++;
            timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTravelling)
        {
            return;
        }

        timer += Time.deltaTime * speed;
        if (this.transform.position != path[currentNode].position)
        {
            this.transform.position = Vector3.Slerp(this.transform.position, path[currentNode].position, timer);
        }
        else
        {
            checkNextNode();
        }
    }
}
