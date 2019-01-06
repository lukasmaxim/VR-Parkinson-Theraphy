using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class FP_TravelManager : NetworkBehaviour {

    [SerializeField]
    FP_PathTraveller pathTraveller;

    // Indicates leap gesture
    [SyncVar]
    public bool leapFlightGesture;
    // Indicates vive gesture
    [SyncVar]
    public bool viveFlightGesture;
    // The platform the players are currently on
    [SyncVar]
    public int currentPlatform;
    // The platform the players want to go
    [SyncVar]
    public int destinationPlatform;
    // The selected path to travel
    Transform[] path;


    public override void OnStartServer()
    {
        currentPlatform = 1;
        //viveFlightGesture = true;
    }

    // Update is called once per frame
    void Update () {
        print("curr: " + currentPlatform + "; dest: " + destinationPlatform + "; leap: " + leapFlightGesture + "; vive: " + viveFlightGesture);
        // Travel to 1
        if (currentPlatform != 1 && destinationPlatform == 1 && leapFlightGesture && viveFlightGesture)
        {
            print("Travelling from " + currentPlatform + " to " + destinationPlatform);
            path = GameObject.FindGameObjectWithTag(currentPlatform + "-1").transform.Cast<Transform>().ToArray();
            pathTraveller.FollowPath(path, delegate () { if (isServer) { currentPlatform = 1; } });
        }
        // Travel to 2
        if (currentPlatform != 2 && destinationPlatform == 2 && leapFlightGesture && viveFlightGesture)
        {
            print("Travelling from " + currentPlatform + " to " + destinationPlatform);
            path = GameObject.FindGameObjectWithTag(currentPlatform + "-2").transform.Cast<Transform>().ToArray();
            pathTraveller.FollowPath(path, delegate () { if (isServer) { currentPlatform = 2; } });
        }
        // Travel to 3
        if (currentPlatform != 3 && destinationPlatform == 3 && leapFlightGesture && viveFlightGesture)
        {
            print("Travelling from " + currentPlatform + " to " + destinationPlatform);
            path = GameObject.FindGameObjectWithTag(currentPlatform + "-3").transform.Cast<Transform>().ToArray();
            pathTraveller.FollowPath(path, delegate () { if (isServer) { currentPlatform = 3; } });
        }
    }
}
