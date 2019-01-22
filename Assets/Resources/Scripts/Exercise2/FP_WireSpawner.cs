using Leap;
using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_WireSpawner : FP_NetworkedObject {

    bool spawnGestureActive;
    bool deleteGestureActive;
    [SerializeField] HandModelBase rightHandModelBase;
    Hand rightHand;

    float spawnTimer, deleteTimer;
    Vector3? lastPosition = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!UpdateHand())
        {
            return;
        }
        DrawWire();
        DeleteWire();
    }

    bool UpdateHand()
    {
        rightHand = rightHandModelBase.GetLeapHand();
        return rightHand != null;
    }

    void DrawWire()
    {
        if (spawnGestureActive)
        {
            //print("Gesture Active");
            spawnTimer += Time.deltaTime;

            if (spawnTimer > 0.1f)
            {
                Vector3 positionIndex = rightHand.GetIndex().TipPosition.ToVector3();
                bool firstSegment = (lastPosition == null);
                localActor.RequestSpawnWireSegment(positionIndex, firstSegment ? Vector3.zero : (Vector3)lastPosition, firstSegment);
                lastPosition = positionIndex;
                spawnTimer = 0;
            }

        }
    }

    void DeleteWire()
    {
        if (deleteGestureActive)
        {
            deleteTimer += Time.deltaTime;

            if(deleteTimer > 2)
            {
                //print("Requesting removal");
                localActor.RequestRemoveWire();
                deleteTimer = 0;
            }
        }
    }

    public void OnSpawnGestureActivate()
    {
        spawnGestureActive = true;
        spawnTimer = 0.0f;
        lastPosition = null;
    }

    public void OnSpawnGestureDeactivate()
    {
        spawnGestureActive = false;
    }

    public void OnDeleteGestureActivate()
    {
        deleteGestureActive = true;
        deleteTimer = 0.0f;
    }

    public void OnDeleteGestureDeactivate()
    {
        deleteGestureActive = false;
    }


}
