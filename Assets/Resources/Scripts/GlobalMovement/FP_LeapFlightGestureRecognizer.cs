using Leap;
using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_LeapFlightGestureRecognizer : FP_NetworkedObject
{

    // HandModelBases and Hands
    [SerializeField] HandModelBase leftHandBase, rightHandBase;
    Hand leftHand, rightHand;
    // Buffer for the activations from our gesture
    int activationsBuffer;
    // The platform to fly to 
    int destinationPlatform;

    // Indicates gesture
    bool gesture;
    bool lastGesture;

    // Use this for initialization
    void Start () {
        destinationPlatform = -1;
        activationsBuffer = 0; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called 60 times per second
    void FixedUpdate () {
        // If hands are null, we skip all further calculations for this frame
        if (!updateLeapHands())
        {
            return;
        }
        RegisterActivations();

        gesture = IsConsideredGesture();
        if (gesture != lastGesture)
        {
            // If gesture changed from last update, we send the change to the server
            if (localActor != null)
            {
                localActor.RequestSetBool(FP_NetworkCodes.B_LEAP_FLIGHT_GESTURE, gesture);
            } 
        }
        lastGesture = gesture;
        //print(IsConsideredGesture()); 
    }

    bool updateLeapHands()
    {
        leftHand = leftHandBase.GetLeapHand();
        rightHand = rightHandBase.GetLeapHand();

        return leftHand != null && rightHand != null;
    }

    // If the activationsBuffer is higher than 5, we consider the activations enough to count as our transport gesture
    public bool IsConsideredGesture()
    {
        if (activationsBuffer > 5)
        {
            return true;
        }
        return false;
    }

    // Getter for destinationPlatform
    public int GetDestinationPlatform()
    {
        return destinationPlatform;
    } 
    
    // Checks if the gesture is registered and adds or subtracts accordingly to or from our activationsBuffer
    private void RegisterActivations()
    {
        // If the hands are moved in opposite directions and are close together, we add 3 to the activationsBuffer when it's around 100
        if (IsOppositeDirection() && IsCloserThan() && activationsBuffer < 100)
        {
            activationsBuffer += 3;
        }
        // Else if there is still something left to subtract, we subtract 1
        else if (activationsBuffer > 0)
        {
            activationsBuffer -= 1;
        }
        //print(activationsBuffer); DEBUG
    }

    // Checks if the y-direction of the hands' velocities is opposite
    private bool IsOppositeDirection()
    {
        return ((leftHand.PalmVelocity.ToVector3().y * rightHand.PalmVelocity.ToVector3().y) < -0.4f);
    }

    // Checks if the hands are closer than a certain distance
    private bool IsCloserThan()
    {
        return (Vector3.Distance(leftHand.GetPalmPose().position, rightHand.GetPalmPose().position) < 0.3f);
    }

    //=============== Triggers ===============

    // Called if 1 finger is extended
    public void OneFingerActivate()
    {
        destinationPlatform = 1;
        if (localActor != null)
        {
            localActor.RequestSetInt(FP_NetworkCodes.I_LEAP_FLIGHT_DESTINATION, destinationPlatform);
        }
        print(destinationPlatform);
    }

    // Called if 2 fingers are extended
    public void TwoFingerActivate()
    {
        destinationPlatform = 2;
        if (localActor != null)
        {
            localActor.RequestSetInt(FP_NetworkCodes.I_LEAP_FLIGHT_DESTINATION, destinationPlatform);
        }
        print(destinationPlatform);
    }

    // Called if 3 fingers are extended
    public void ThreeFingerActivate()
    {
        destinationPlatform = 3;
        if (localActor != null)
        {
            localActor.RequestSetInt(FP_NetworkCodes.I_LEAP_FLIGHT_DESTINATION, destinationPlatform);
        }
        print(destinationPlatform);
    }
}
