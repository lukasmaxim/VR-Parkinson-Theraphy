using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Actor : NetworkBehaviour {

    public Character character;
    public new Transform transform;

    [SyncVar]
    private string prefabName = "";

    //this part is for object sharing
    //*******************************
    List<NetworkIdentity> sharedObjects; // shared objects on the server or localActor
    //*******************************


    protected virtual void Awake()
    {
        transform = base.transform;
    }

    // Use this for initialization
    void Start () {

        if (isServer || isLocalPlayer)
        {
            if (isLocalPlayer)
            {
                // Inform the local player about his new character
                LocalPlayerController.Singleton.SetActor(this);
                CmdInitialize(prefabName);
            }
         

            //this part is for object sharing
            //*******************************
            if (isServer)
            {
                // find objects that can be manipulated 
                // TIPP : you can use a specific tag for all GO's that can be manipulated by players
            }
            if (isLocalPlayer) 
            {
                // find objects that can be manipulated 
                // assign this Actor to the localActor field of the AuthorityManager component of each shared object
                foreach (AuthorityManager go in GameObject.FindObjectsOfType<AuthorityManager>())
                {
                    go.localActor = this;
                }
                foreach (FP_NetworkedObject go in GameObject.FindObjectsOfType<FP_DummyInput>()) {
                    go.localActor = this;
                }
                foreach (FP_NetworkedObject go in GameObject.FindObjectsOfType<FP_LeapFlightGestureRecognizer>())
                {
                    go.localActor = this;
                }
                foreach (FP_NetworkedObject go in GameObject.FindObjectsOfType<FP_ViveFlightGestureRecognizer>())
                {
                    go.localActor = this;
                }
                foreach (FP_NetworkedObject go in GameObject.FindObjectsOfType<FP_BallSpawner>())
                {
                    go.localActor = this;
                }
                foreach (FP_NetworkedObject go in GameObject.FindObjectsOfType<FP_WireSpawner>())
                {
                    go.localActor = this;
                }
            }
            //*******************************

        }
        else
        {
            // Initialize on startup
            Initialize(prefabName);
        }

    }

    public void Update()
    {
 
    }

    /// <summary>
    /// Updates the actor position and rotation.
    /// This function should be called only by the <see cref="LocalPlayerController"/>.
    /// </summary>
    public void UpdateActorLeft(Vector3 leftPos, Quaternion leftRot) //runs only on LocalPlayer!
    {
        if (character != null)
        {
            character.UpdateCharacterLeft(leftPos, leftRot);
        }
    }

    /// <summary>
    /// Updates the actor position and rotation.
    /// This function should be called only by the <see cref="PlayerController"/>.
    /// </summary>
    public void UpdateActorRight(Vector3 rightPos, Quaternion rightRot) //runs only on LocalPlayer!
    {
        if (character != null)
        {
            character.UpdateCharacterRight(rightPos, rightRot);
        }
    }

    public void SetRightCharacterActive(bool active)
    {
        character.SetRightActive(active);
    }

    public void SetLeftCharacterActive(bool active)
    {
        character.SetLeftActive(active);
    }

    /// <summary>
    /// Initialize the player locally.
    /// </summary>
    /// <param name="prefab">Prefab character name.</param>
    public void Initialize(string prefab)
    {
        prefabName = prefab;
        name = name.Replace("(Clone)", "");

    }

    /// <summary>
    /// Spawns the character of actor on all clients.
    /// This runs on server only.
    /// </summary>
    /// <param name="prefab">Prefab name of the character.</param>
    private void SpawnCharacter(string prefab)
    {
        // Spawn character
        GameObject modelPrefab = Resources.Load("Prefabs/" + prefab) as GameObject;
        GameObject model = (GameObject)Instantiate(modelPrefab, transform.position, transform.rotation) as GameObject;
        NetworkServer.Spawn(model);

        // Attach character to player
        AttachCharacter(model.GetComponent<Character>());
    }

    /// <summary>
    /// Initializes the character on server to inform all clients. 
    /// This command calls the Initialize() method and spawns the character.
    /// </summary>
    [Command]
    public void CmdInitialize(string prefab)
    {
        if (prefab.Length > 0)
        {
            CreateCharacter(prefab);
        }
    }

    /// <summary>
    /// Creates the character and initializes on server.
    /// </summary>
    /// <param name="prefab">The character prefab name.</param>
    [Server]
    public void CreateCharacter(string prefab)
    {
        SpawnCharacter(prefab);
        Initialize(prefab);
    }

    /// <summary>
    /// Main routine to attach the character to this actor
    /// This runs only on Server.
    /// </summary>
    /// <param name="newCharacter">New character to attach.</param>
    [Server]
    public void AttachCharacter(Character newCharacter)
    {
        newCharacter.AttachToActor(netId);
    }

    // Various Network methods to communicate state changes
    //*********************************************************

    public void RequestSetBool(int key, bool value)
    {
        print("Bool requested (" + key + "): " + value);
        if (isLocalPlayer)
        {
            print("Was local player, forwarding...");
            CmdSetBool(key, value);
        }
    }

    [Command]
    public void CmdSetBool(int key, bool value)
    {
        print("Server Bool requested (" + key + "): " + value);
        GameObject.FindObjectOfType<FP_NetworkedValueManager>().SetBool(key, value);
    }

    public void RequestSetInt(int key, int value)
    {
        print("Int requested (" + key + "): " + value);
        if (isLocalPlayer)
        {
            print("Was local player, forwarding...");
            CmdSetInt(key, value);
        }
    }

    [Command]
    public void CmdSetInt(int key, int value)
    {
        print("Server Int requested (" + key + "): " + value);
        GameObject.FindObjectOfType<FP_NetworkedValueManager>().SetInt(key, value);
    }

    public void RequestSpawnPrefabForMe(string prefabName, Vector3 position)
    {
        print("SpawnForMe requested (" + prefabName + ")");
        if (isLocalPlayer)
        {
            print("Was local player, forwarding...");
            CmdSpawnPrefabForMe(this.GetComponent<NetworkIdentity>(), prefabName, position);
        }
    }

    [Command]
    public void CmdSpawnPrefabForMe(NetworkIdentity netID, string prefabName, Vector3 position)
    {
        print("Server SpawnForMe requested (" + prefabName + ")");
        GameObject instance = Instantiate(Resources.Load("Prefabs/"+prefabName, typeof(GameObject))) as GameObject;
        instance.transform.position = position;
        NetworkServer.SpawnWithClientAuthority(instance, netID.connectionToClient);

        TargetNotifyClientOfSpawn(netID.connectionToClient, instance);
    }

    // Returns the GO to where it belongs
    [TargetRpc]
    void TargetNotifyClientOfSpawn(NetworkConnection connection, GameObject go)
    {
        GameObject ball = go;
        print("Ball: " + ball);
        GameObject.FindObjectOfType<FP_BallSpawner>().ball = ball;
    }


    public void RequestSpawnWireSegment(Vector3 position, Vector3 lastPosition, bool firstSegment)
    {
        CmdSpawnWireSegment(position, lastPosition, firstSegment);
    }

    [Command]
    public void CmdSpawnWireSegment(Vector3 position, Vector3 lastPosition, bool firstSegment)
    {
        GameObject sphere = Instantiate(Resources.Load("Prefabs/Exercise2Sphere", typeof(GameObject))) as GameObject;
        // position
        sphere.transform.position = position;
        // assign tag
        sphere.tag = "Wire";

        NetworkServer.Spawn(sphere);

        // if there is already a sphere spawned, connect the previous one with this one through a cylinder
        if (!firstSegment)
        {
            GameObject cylinder = Instantiate(Resources.Load("Prefabs/Exercise2Cylinder", typeof(GameObject))) as GameObject;
            // position
            cylinder.transform.position = (position - lastPosition) / 2.0f + lastPosition;
            // assign tag
            cylinder.tag = "Wire";
            // scale
            var v3T = cylinder.transform.localScale;
            v3T.y = (position - lastPosition).magnitude * 0.5f;
            cylinder.transform.localScale = v3T;
            // rotate
            cylinder.transform.rotation = Quaternion.FromToRotation(Vector3.up, position - lastPosition);

            NetworkServer.Spawn(cylinder);
            RpcScaleWireSegment(cylinder, v3T);
        }
    }

    [ClientRpc]
    public void RpcScaleWireSegment(GameObject go, Vector3 scale)
    {
        go.transform.localScale = scale;
    }

    public void RequestRemoveWire()
    {
        print("actor: request removal");
        CmdRemoveWire();
    }

    [Command]
    public void CmdRemoveWire()
    {
        print("Command removal");
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Wire"))
        {
            print("for each... ");
            NetworkServer.Destroy(go);
        }
    }

    //*********************************************************


    //this part is for object sharing
    // fill in the implementation
    //*******************************

    // should only be run on localPlayer (by the AuthorityManager of a shared object)
    // ask the server for the authority over an object with NetworkIdentity netID
    public void RequestObjectAuthority(NetworkIdentity netID)
    {
        if (!isLocalPlayer)
            return;

        print("Requesting authority for " + netID);
        CmdAssignObjectAuthorityToClient(netID);
    }

    // should only be run on localPlayer (by the AuthorityManager of a shared object)
    // ask the server to remove the authority over an object with NetworkIdentity netID
    public void ReturnObjectAuthority(NetworkIdentity netID)
    {
        if (!isLocalPlayer)
            return;

        print("Returning authority for " + netID);
        CmdRemoveObjectAuthorityFromClient(netID);
    }

    // run on the server
    // netID is NetworkIdentity of a shared object the authority if which should be passed to the client
    [Command]
    void CmdAssignObjectAuthorityToClient(NetworkIdentity netID)
    {
        netID.GetComponent<AuthorityManager>().AssignClientAuthority(connectionToClient);
    }

    // run on the server
    // netID is NetworkIdentity of a shared object the authority if which should be removed from the client
    [Command]
    void CmdRemoveObjectAuthorityFromClient(NetworkIdentity netID)
    {
        netID.GetComponent<AuthorityManager>().RemoveClientAuthority(connectionToClient);
    }
    //*******************************
}
