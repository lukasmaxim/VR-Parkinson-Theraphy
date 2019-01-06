using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_ExerciseStateManager : MonoBehaviour {

    [SerializeField] MonoBehaviour[] exercise1;
    [SerializeField] MonoBehaviour[] exercise2;
    [SerializeField] MonoBehaviour[] exercise3;

    MonoBehaviour[][] scripts;

    // Use this for initialization
    void Start () {
		scripts = new MonoBehaviour[][]{ exercise1, exercise2, exercise3 };
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // First disables all current scripts, then enables the scripts needed for a certain exercise mode
    public void activateExercise(int exerciseNumber)
    {
        print("activateExercise entered. " + exerciseNumber);
        foreach(MonoBehaviour[] mba in scripts)
        {
            foreach(MonoBehaviour mb in mba)
            {
                mb.enabled = false;
            }
        }

        foreach(MonoBehaviour mb in scripts[exerciseNumber - 1]){
            mb.enabled = true;
        }
    }
}
