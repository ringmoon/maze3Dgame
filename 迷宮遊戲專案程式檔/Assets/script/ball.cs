using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour {

    Vector3 direction;
	// Use this for initialization
	void Start () {
        direction = GameObject.Find("player").gameObject.transform.forward*30f;
        this.GetComponent<Rigidbody>().AddForce(direction);
        Destroy(gameObject,5);
    }
	
	// Update is called once per frame
	void Update () {
      
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "plane")
        {
            Destroy(gameObject);
        }
    }
}
