using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTraveller : MonoBehaviour
{

    public Transform[] path;
    public float speed;

    int currentNode;
    float timer;

    bool shouldFollow = true;

    public void FollowPath()
    {
        currentNode = 0;
        timer = 0;
        shouldFollow = true;

    }

    void checkNextNode()
    {
        if (currentNode == path.Length - 1)
        {
            shouldFollow = false;
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
        if (!shouldFollow)
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
