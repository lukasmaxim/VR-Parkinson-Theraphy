using UnityEngine;
using System.Collections;

public class LocalPlayerController : MonoBehaviour {

    public Actor actor; // The network actor runs on all clients
    public Transform left;
    public Transform right;

    public static LocalPlayerController Singleton { get; private set; }
    /// <summary>
    /// Sets the player on start up.
    /// </summary>
    /// <param name="newActor">New actor.</param>
    public void SetActor(Actor newActor)
    {
        actor = newActor;

        // Initialize locally to update on all clients
        Actor actorPrefab = (Resources.Load("Prefabs/" + actor.name.Replace("(Clone)", "")) as GameObject).GetComponent<Actor>();
        actor.transform.SetParent(transform);

        var prefabName = "";
        
        prefabName = actorPrefab.character != null ? actorPrefab.character.name : "";

        actor.Initialize(prefabName);
    }

    private void Awake()
    {
        Singleton = this;
    }


    public void UpdateActorLeft(Vector3 leftPos, Quaternion leftRot)
    {
        actor.UpdateActorLeft(leftPos, leftRot);
    }

    public void UpdateActorRight(Vector3 rightPos, Quaternion rightRot)
    {
        actor.UpdateActorRight(rightPos, rightRot);
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if(actor)
        {
            if (left.gameObject.activeSelf)
                UpdateActorLeft(left.position, left.rotation);

            if (right.gameObject.activeSelf)
                UpdateActorRight(right.position, right.rotation);
        }
      
    }
}
