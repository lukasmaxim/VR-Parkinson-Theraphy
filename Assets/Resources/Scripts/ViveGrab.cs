using UnityEngine;
using System.Collections;

// This script defines conditions that are necessary for the Vive player to grab a shared object
// TODO: values of these four boolean variables can be changed either directly here or through other components
// AuthorityManager of a shared object should be notifyed from this script

public class ViveGrab : MonoBehaviour {

    AuthorityManager am; // to communicate the fulfillment of grabbing conditions

   

    // conditions for the object control here
    bool leftHandTouching = false;
    bool rightHandTouching = false;
    bool leftTriggerDown = false;
    bool rightTriggerDown = false;
    

    // Use this for initialization
    void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
	 
        if(leftHandTouching && rightHandTouching && leftTriggerDown && rightTriggerDown)
        {
            // notify AuthorityManager that grab conditions are fulfilled
        }
        else
        {
            // grab conditions are not fulfilled
        }

    }
}
