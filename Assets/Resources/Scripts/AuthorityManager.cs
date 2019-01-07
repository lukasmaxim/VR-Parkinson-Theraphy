using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


// TODO: this script should manage authority for a shared object
public class AuthorityManager : NetworkBehaviour {

    
    NetworkIdentity netID; // NetworkIdentity component attached to this game object

    // these variables should be set up on a client
    //**************************************************************************************************
    public Actor localActor; // Actor that is steering this player 

    void Start()
    {
        netID = GetComponent<NetworkIdentity>();
    }

    // should only be called on server (by an Actor)
    // assign the authority over this game object to a client with NetworkConnection conn
    public void AssignClientAuthority(NetworkConnection conn)
    {
        if (!isServer)
            return;

        netID.AssignClientAuthority(conn);
    }

    // should only be called on server (by an Actor)
    // remove the authority over this game object from a client with NetworkConnection conn
    public void RemoveClientAuthority(NetworkConnection conn)
    {
        if (!isServer)
            return;

        netID.RemoveClientAuthority(conn);
    }

}
