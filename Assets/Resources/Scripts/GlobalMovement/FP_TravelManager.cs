using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class FP_TravelManager : NetworkBehaviour {

    [SerializeField]
    FP_PathTraveller pathTraveller;
    [SerializeField]
    FP_ExerciseStateManager exerciseStateManager;

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

    GameObject staff;


    public override void OnStartServer()
    {
        currentPlatform = 1;
    }

    // Update is called once per frame
    void Update () {
        CheckStaffStatus();
        //print("curr: " + currentPlatform + "; dest: " + destinationPlatform + "; leap: " + leapFlightGesture + "; vive: " + viveFlightGesture);
        // Travel to 1
        if (currentPlatform != 1 && destinationPlatform == 1 && leapFlightGesture && viveFlightGesture)
        {
            //print("Travelling from " + currentPlatform + " to " + destinationPlatform);
            path = GameObject.FindGameObjectWithTag(currentPlatform + "-1").transform.Cast<Transform>().ToArray();
            exerciseStateManager.deactivateExercises();
            pathTraveller.FollowPath(path, delegate () {
                if (isServer) {
                    currentPlatform = 1;
                }
                exerciseStateManager.activateExercise(1);
            });
        }
        // Travel to 2
        if (currentPlatform != 2 && destinationPlatform == 2 && leapFlightGesture && viveFlightGesture)
        {
            //print("Travelling from " + currentPlatform + " to " + destinationPlatform);
            path = GameObject.FindGameObjectWithTag(currentPlatform + "-2").transform.Cast<Transform>().ToArray();
            exerciseStateManager.deactivateExercises();
            pathTraveller.FollowPath(path, delegate () {
                if (isServer)
                {
                    currentPlatform = 2;
                }
                exerciseStateManager.activateExercise(2);
            });
        }
        // Travel to 3
        if (currentPlatform != 3 && destinationPlatform == 3 && leapFlightGesture && viveFlightGesture)
        {
            //print("Travelling from " + currentPlatform + " to " + destinationPlatform);
            path = GameObject.FindGameObjectWithTag(currentPlatform + "-3").transform.Cast<Transform>().ToArray();
            exerciseStateManager.deactivateExercises();
            pathTraveller.FollowPath(path, delegate () {
                if (isServer)
                {
                    currentPlatform = 3;
                }
                exerciseStateManager.activateExercise(3);
            });
        }
    }

    void CheckStaffStatus()
    {
        if (staff == null)
        {
            staff = GameObject.FindGameObjectWithTag("Staff");
        }

        if (currentPlatform == 2)
        {
            SetStaffActivity(true);
        } else
        {
            SetStaffActivity(false);
        }
    }

    void SetStaffActivity(bool active)
    {
        if (staff != null && staff.activeSelf != active)
        {
            staff.SetActive(active);
        }
    }
}
