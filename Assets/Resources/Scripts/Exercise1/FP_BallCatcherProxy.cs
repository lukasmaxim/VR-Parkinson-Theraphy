using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FP_BallCatcherProxy : FP_NetworkedObject {

    public void OnBallGrabbed(GameObject ball)
    {
        localActor.RequestObjectAuthority(ball.GetComponent<NetworkIdentity>());
    }

    public void OnBallDropped(GameObject ball)
    {
        localActor.ReturnObjectAuthority(ball.GetComponent<NetworkIdentity>());
    }
}
