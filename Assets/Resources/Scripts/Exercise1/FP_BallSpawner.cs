using Leap;
using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

enum PinchState
{
    pinching,
    notPinching
}

public class FP_BallSpawner : FP_NetworkedObject {

    // HandModelBases and Hands
    [SerializeField] HandModelBase leftHandBase, rightHandBase;
    Hand leftHand, rightHand;

    PinchState leftPinchState;
    PinchState rightPinchState;
    bool activeBallPresent;

    public GameObject ball;

	// Use this for initialization
	void Start () {
        leftPinchState = PinchState.notPinching;
        rightPinchState = PinchState.notPinching;
    }
	
	// Update is called once per frame
	void Update () {

        if (isServer)
        {
            return;
        }

        // If hands are null, we skip all further calculations for this frame
        if (!updateLeapHands())
        {
            return;
        }

        if (ball == null)
        {
            CheckSpawnGesture();
        } else if(ball != null && leftPinchState == PinchState.notPinching && rightPinchState == PinchState.notPinching)
        {
            ReleaseBall();
        } else
        {
            HandleBall();
        }
	}

    void CheckSpawnGesture()
    {
        if(leftPinchState == PinchState.pinching && rightPinchState == PinchState.pinching && !activeBallPresent)
        {
            SpawnBall();
            activeBallPresent = true;
        }
    }

    void HandleBall()
    {
        if (!ball.GetComponent<Rigidbody>().isKinematic) {
            ball.GetComponent<FP_NetworkedPropertySync>().SetKinematic(true);
        }
        Vector3 position = (leftHand.GetIndex().TipPosition.ToVector3() + rightHand.GetIndex().TipPosition.ToVector3()) / 2;
        float size = Vector3.Distance(leftHand.GetIndex().TipPosition.ToVector3(), rightHand.GetIndex().TipPosition.ToVector3()) * 0.6f;

        ball.transform.position = position;
        ball.GetComponent<FP_NetworkedPropertySync>().SetScale(new Vector3(size, size, size));
    }

    void ReleaseBall()
    {
        ball.GetComponent<FP_NetworkedPropertySync>().SetKinematic(false);

        Rigidbody rb = ball.GetComponent<Rigidbody>();

        Vector3 force = (leftHand.PalmVelocity.ToVector3() + rightHand.PalmVelocity.ToVector3());
        force = force / 4;
        rb.AddForce(force, ForceMode.Impulse);

        localActor.ReturnObjectAuthority(ball.GetComponent<NetworkIdentity>());

        activeBallPresent = false;
        ball = null;
    }

    void SpawnBall()
    {
        Vector3 position = (leftHand.GetIndex().TipPosition.ToVector3() + rightHand.GetIndex().TipPosition.ToVector3()) / 2;
        localActor.RequestSpawnPrefabForMe("ExerciseBall", position);
    }

    public void leftHandPinch()
    {
        leftPinchState = PinchState.pinching;
    }

    public void leftHandNotPinching()
    {
        leftPinchState = PinchState.notPinching;
    }

    public void rightHandPinch()
    {
        rightPinchState = PinchState.pinching;
    }

    public void rightHandNotPinching()
    {
        rightPinchState = PinchState.notPinching;
    }

    bool updateLeapHands()
    {
        leftHand = leftHandBase.GetLeapHand();
        rightHand = rightHandBase.GetLeapHand();

        return leftHand != null && rightHand != null;
    }
}
