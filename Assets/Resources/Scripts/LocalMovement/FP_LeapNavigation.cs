using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class FP_LeapNavigation : MonoBehaviour {

    [SerializeField]
    HandModelBase leftHandBase, rightHandBase;

    [SerializeField]
    float speed;

    [SerializeField]
    Transform restrictionCenter;

    [SerializeField]
    float restrictionRadius;

    Hand leftHand, rightHand;
    bool leftPose, rightPose;

    #region Update
    void Update () {

        if (!updateLeapHands())
        {
            return;
        }

        if (rightPose && leftPose)
        {
            updateFreeMovement();
        } else if (leftPose)
        {
            updateLeftRotation();
        } else if (rightPose)
        {
            updateRightRotation();
        }
	}


    // If only the left hand does the movement pose, rotate self to the left
    void updateLeftRotation()
    {
        this.transform.RotateAround(this.transform.position, Vector3.up, -speed * 50 * Time.deltaTime);
    }

    // If only the right hand does the movement pose, rotate self to the right
    void updateRightRotation()
    {
        this.transform.RotateAround(this.transform.position, Vector3.up, speed * 50 * Time.deltaTime);
    }

    // If both hands to the movement pose, allow free movement in the direction of the index finger
    void updateFreeMovement()
    {
        
        Vector3 direction = (leftHand.Arm.Direction.Normalized.ToVector3() + rightHand.Arm.Direction.ToVector3()) / 2;
        direction.y = 0;
        Vector3 newPosition = this.transform.position + (direction * speed * Time.deltaTime);

        if (Vector3.Distance(newPosition, restrictionCenter.position) < restrictionRadius)
        {
            this.transform.position = newPosition;
        }
    }


    bool updateLeapHands()
    {
        leftHand = leftHandBase.GetLeapHand();
        rightHand = rightHandBase.GetLeapHand();

        return leftHand != null && rightHand != null;
    }
    #endregion

    #region Event Callbacks for Leap Pose Detectors

    public void OnLeftPose()
    {
        leftPose = true;
    }

    public void OffLeftPose()
    {
        leftPose = false;
    }

    public void OnRightPose()
    {
        rightPose = true;
    }

    public void OffRightPose()
    {
        rightPose = false;
    }

    #endregion
}
