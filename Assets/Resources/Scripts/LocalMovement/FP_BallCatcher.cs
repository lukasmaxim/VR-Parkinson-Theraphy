﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public enum CatcherTriggerState
{
    Pressing,
    NonPressing
}

public class FP_BallCatcher : MonoBehaviour {

    // Controllers and TriggerStates
    [SerializeField] SteamVR_Behaviour_Pose controller;
    CatcherTriggerState triggerState;
    // The object to be caught
    GameObject objectToCatch;
    // The joint between the controller and the ball
    FixedJoint joint;

    // Use this for initialization
    void Start() {
        triggerState = CatcherTriggerState.NonPressing;
        joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update() {
        manageCatch();
    }

    // If the triggers are pressed, an objectToCatch can be caught and is attachted. Otherwise, we disconnect the object, making it fall down.
    void manageCatch()
    {
        if(objectToCatch != null)
        {
            if (triggerState == CatcherTriggerState.Pressing && joint.connectedBody == null)
            {
                joint.connectedBody = objectToCatch.GetComponent<Rigidbody>();
            }
            else if (triggerState == CatcherTriggerState.NonPressing && joint.connectedBody != null)
            {
                joint.connectedBody = null;
            }
        }
    }

    //=============== Triggers ===============

    void OnTriggerEnter(Collider other)
    {
        // If it's a ball that's intersecting with our controller, we declare it the objectToCatch
        if (other.gameObject.tag == "Ball")
        {
            objectToCatch = other.gameObject;
        }
    }

    // Triggered if the controller leaves collision area of some collider
    void OnTriggerExit(Collider other)
    {
        objectToCatch = null;
    }

    // Called if trigger is pressed
    public void TriggerDown()
    {
        triggerState = CatcherTriggerState.Pressing;
    }

    // Called if trigger is released
    public void TriggerUp()
    {
        triggerState = CatcherTriggerState.NonPressing;
    }
}