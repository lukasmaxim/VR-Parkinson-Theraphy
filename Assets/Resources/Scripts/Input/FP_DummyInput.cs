using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FP_DummyInput : FP_NetworkedObject {

    // Update is called once per frame
    void Update () {

        if (isServer)
        {
            return;
        }

		if (Input.GetKeyDown(KeyCode.A))
        {
            localActor.RequestSetBool(0x00, true);
        }
	}
}
