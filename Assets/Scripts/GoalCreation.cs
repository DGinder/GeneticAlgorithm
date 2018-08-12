using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCreation : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Vector3 cPos = new Vector3(.5f, .9f, 10f);
        transform.position = Camera.main.ViewportToWorldPoint(cPos);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
