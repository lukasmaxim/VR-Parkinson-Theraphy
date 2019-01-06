using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public enum GestureTriggerState
{
    Pressing,
    NonPressing
}

public class FP_ViveFlightGestureRecognizer : FP_NetworkedObject
{
   
    // Controllers and TriggerStates
    [SerializeField] SteamVR_Behaviour_Pose leftController, rightController;
    CatcherTriggerState leftTriggerState, rightTriggerState;
    // Buffer for the activations from our gesture
    int activationsBuffer;

    // Use this for initialization
    void Start() {
        leftTriggerState = CatcherTriggerState.NonPressing;
        rightTriggerState = CatcherTriggerState.NonPressing;
        activationsBuffer = 0;
    }

    // Update is called once per frame
    void Update() {

    }

    // FixedUpdate is called 60 times per second
    void FixedUpdate()
    {
        RegisterActivations();
        //print(IsConsideredGesture()); DEBUG
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

    // Checks if the gesture is registered and adds or subtracts accordingly to or from our activationsBuffer
    private void RegisterActivations()
    {
        // If all both triggers are pressed, the controllers are moved in opposite directions and are close together, we add 3 to the activationsBuffer when it's around 100
        if (leftTriggerState == CatcherTriggerState.Pressing && rightTriggerState == CatcherTriggerState.Pressing && IsOppositeDirection() && IsCloserThan() && activationsBuffer < 100)
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

    // Checks if the y-direction of the controllers' velocities is opposite
    private bool IsOppositeDirection()
    {
        return((leftController.GetVelocity().y * rightController.GetVelocity().y) < -0.15f);
    }

    // Checks if the controllers are closer than a certain distance
    private bool IsCloserThan()
    {
        return (Vector3.Distance(leftController.poseAction.GetLocalPosition(leftController.inputSource), rightController.poseAction.GetLocalPosition(rightController.inputSource)) < 0.5f);
    }

    //=============== Triggers ===============

    // Called if left trigger is pressed
    public void LeftDown()
    {
        leftTriggerState = CatcherTriggerState.Pressing;
    }

    // Called if left trigger is released
    public void LeftUp()
    {
        leftTriggerState = CatcherTriggerState.NonPressing;
    }

    // Called if right trigger is pressed
    public void RightDown()
    {
        rightTriggerState = CatcherTriggerState.Pressing;
    }

    // Called if right trigger is released
    public void RightUp()
    {
        rightTriggerState = CatcherTriggerState.NonPressing;
    }
}
