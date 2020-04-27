using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


    public Transform meta;
    public float filtriranje = 5f;

    Vector3 pomak;
	
	void Start () {

        pomak = transform.position - meta.position;
        


	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        Vector3 targetCamPos = meta.position + pomak;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, filtriranje * Time.deltaTime);
	}
}
