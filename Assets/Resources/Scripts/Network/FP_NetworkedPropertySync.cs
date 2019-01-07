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
    }
}
