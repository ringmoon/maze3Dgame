using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roleManager : MonoBehaviour {
    public Animator animator;
    public float speed = 0.2f;
    public Material material1;
    public Material material2;
    public GameObject shoot;
    public Transform balls;

    // Use this for initialization
    void Start () {
        this.transform.position = new Vector3(this.transform.GetChild(0).GetComponent<buildMaze>().space, -0.5f, this.transform.GetChild(0).GetComponent<buildMaze>().space);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 vector = this.transform.forward * speed;
            this.transform.position += vector;
            animator.SetTrigger("run");
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.Rotate(new Vector3(0, -2, 0));       
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.Rotate(new Vector3(0, 2, 0));
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 vector = this.transform.forward * speed;
            this.transform.position -= vector;
            animator.SetTrigger("run");
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("shoot");
            GameObject g = Instantiate(shoot);
            g.transform.SetParent(transform);
            g.transform.localPosition = new Vector3(0.28f, 2, 1.4f);
            g.transform.SetParent(balls);
        }
        animator.SetTrigger("idle");
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "plane")
        {
            collision.collider.gameObject.GetComponent<MeshRenderer>().material = material2;
            this.speed = -0.2f;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag != "plane")
        {
            collision.collider.gameObject.GetComponent<MeshRenderer>().material = material1;
            this.speed = 0.2f;
        }
    }
}
