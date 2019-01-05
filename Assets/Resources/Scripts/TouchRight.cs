using UnityEngine;
using System.Collections;

// TODO: this script CAN be used to detect the events of a right networked hand touching a shared object
// fill in the implementation and communicate touching events to either LeapGrab and ViveGrab by setting the rightHandTouching variable
// ALTERNATIVELY, implement the verification of the grabbing conditions in a way  your prefer
// TO REMEMBER: only the localPlayer (networked hands belonging to the localPlayer) should be able to "touch" shared objects

public class TouchRight : MonoBehaviour {

    // the implementation of a touch condition might be different for Vive and Leap 
    public bool vive;
    public bool leap;

}
