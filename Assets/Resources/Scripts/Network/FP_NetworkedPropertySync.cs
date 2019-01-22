using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FP_NetworkedPropertySync : NetworkBehaviour {

    public void SetScale(Vector3 scale)
    {
        this.transform.localScale = scale;
        CmdSetScale(scale);
    }

    [Command]
    public void CmdSetScale(Vector3 scale)
    {
        this.transform.localScale = scale;
        RpcSetScale(scale);
    }

    [ClientRpc]
    public void RpcSetScale(Vector3 scale)
    {
        this.transform.localScale = scale;
    }

    public void SetKinematic(bool value)
    {
        this.GetComponent<Rigidbody>().isKinematic = value;
        CmdSetKinematic(value);
    }

    [Command]
    public void CmdSetKinematic(bool value)
    {
        this.GetComponent<Rigidbody>().isKinematic = value;
        RpcSetKinematic(value);
    }

    [ClientRpc]
    public void RpcSetKinematic(bool value)
    {
        this.GetComponent<Rigidbody>().isKinematic = value;
    }


    public void RequestSetGravity(bool value)
    {
        this.GetComponent<Rigidbody>().useGravity = value;
        CmdSetGravity(value);
    }

    [Command]
    public void CmdSetGravity(bool value)
    {
        this.GetComponent<Rigidbody>().useGravity = value;
        RpcSetGravity(value);
    }

    [ClientRpc]
    public void RpcSetGravity(bool value)
    {
        this.GetComponent<Rigidbody>().useGravity = value;
    }


    public void SetVelocity(Vector3 value)
    {
        //this.GetComponent<Rigidbody>().velocity = value;
        CmdSetVelocity(value);
    }

    [Command]
    public void CmdSetVelocity(Vector3 value)
    {
        this.GetComponent<Rigidbody>().velocity = value;
        RpcSetVelocity(value);
    }

    [ClientRpc]
    public void RpcSetVelocity(Vector3 value)
    {
        this.GetComponent<Rigidbody>().velocity = value;
    }
}
