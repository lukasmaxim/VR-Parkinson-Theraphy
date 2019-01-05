using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class NetworkController : NetworkManager
{
  
    public bool host;
    public bool server;

    public static NetworkController FindInstance()
    {
        return FindObjectOfType<NetworkController>();
    }

    public class NetMsgType
    {
        public const short EmptyMessage = MsgType.Highest + 1; // Empty network message

        public class EmptyMessageMsg : MessageBase { }
    }

    /// <summary>
    /// Connect to server or create host server.
    /// </summary>
    private void Start()
    {

        if(server)
        {
            StartServer();
        }
        else if(host)
        {
            StartHost();
        }
        else
        {
            StartClient();
        }

    }
             
    // overriden functions implement only base functionality; however, additional functionality can be implemented here
    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    public override void OnStartHost()
    {
        base.OnStartHost();
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
    }

}

