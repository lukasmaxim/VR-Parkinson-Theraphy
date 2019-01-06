using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FP_NetworkedValueManager : NetworkBehaviour {

    public void SetBool(int key, bool value)
    {
        switch (key)
        {
            case 0x00:
                print("0x00: " + value);
                RpcPrint("0x00: " + value);
                break;
            case 0x01:
                print("0x01: " + value);
                RpcPrint("0x01: " + value);
                break;
        }
    }

    [ClientRpc]
    public void RpcPrint(string value)
    {
        print("Client: " + value);
    }
}
