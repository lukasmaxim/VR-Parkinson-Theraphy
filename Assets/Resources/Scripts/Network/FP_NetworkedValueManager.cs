using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FP_NetworkedValueManager : NetworkBehaviour {

    [SerializeField]
    FP_TravelManager travelManager;

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

            case FP_NetworkCodes.B_LEAP_FLIGHT_GESTURE:
                travelManager.leapFlightGesture = value;
                break;

            case FP_NetworkCodes.B_VIVE_FLIGHT_GESTURE:
                travelManager.viveFlightGesture = value;
                break;
        }
    }

    public void SetInt(int key, int value)
    {
        switch(key)
        {
            case FP_NetworkCodes.I_LEAP_FLIGHT_DESTINATION:
                travelManager.destinationPlatform = value;
                break;
        }
    }

    [ClientRpc]
    public void RpcPrint(string value)
    {
        print("Client: " + value);
    }
}
