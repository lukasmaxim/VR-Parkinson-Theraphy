using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_WireTracerProxy : FP_NetworkedObject {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WireChangeMaterial(Material currentMaterial)
    {
        print("Requesting material: " + currentMaterial.name);
        print("Localactor: " + localActor);
        localActor.RequestChangeMaterial(currentMaterial.name);
    }
}
