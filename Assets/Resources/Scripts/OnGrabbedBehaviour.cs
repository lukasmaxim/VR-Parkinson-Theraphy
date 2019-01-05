using UnityEngine;
using System.Collections;

// TODO: define the behaviour of a shared object when it is manipulated by a client

public class OnGrabbedBehaviour : MonoBehaviour {

   
    bool grabbed;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
        // GO´s behaviour when it is in a grabbed state (owned by a client) should be defined here
        if(grabbed)
        {
            
        }
	}

    // called first time when the GO gets grabbed by a player
    public void OnGrabbed()
    {
       
       
    }

    // called when the GO gets released by a player
    public void OnReleased()
    {
        

    }
}
