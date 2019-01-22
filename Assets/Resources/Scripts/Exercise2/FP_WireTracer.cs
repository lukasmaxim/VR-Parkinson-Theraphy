using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_WireTracer : MonoBehaviour {

    [SerializeField] Material activeMaterial, inactiveMaterial;

    FP_WireTracerProxy wireTracerProxy;

    // Use this for initialization
    void Start()
    {
        wireTracerProxy = GameObject.FindObjectOfType<FP_WireTracerProxy>();
    }

    void OnTriggerEnter(Collider other)
    {
        //print("Enter");
        // If it's a wire that's intersecting with our controller, we change the wire's color
        if (other.gameObject.tag == "Wire")
        {
            //print("OnTriggerEnter!");
            wireTracerProxy = GameObject.FindObjectOfType<FP_WireTracerProxy>();

            print("WireTracer: " + (wireTracerProxy != null));
            print("Mat: " + (inactiveMaterial != null));

            wireTracerProxy.WireChangeMaterial(inactiveMaterial);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //print("Exit");
        // If it's a wire that's intersecting with our controller, we change the wire's color
        if (other.gameObject.tag == "Wire")
        {
            //print("OnTriggerExit!");
            wireTracerProxy = GameObject.FindObjectOfType<FP_WireTracerProxy>();

            print("WireTracer: " + (wireTracerProxy != null));
            print("Mat: " + (activeMaterial != null));

            wireTracerProxy.WireChangeMaterial(activeMaterial);
        }
    }
}
