using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour
{
    private Rigidbody _rig;
    private bool _isGrounded;

    void Start()
    {
        _rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Collisions
    private void OnCollisionEnter(Collision collider)
    {
        Vector3 tempPoint = new Vector3();
        foreach(ContactPoint point in collider.contacts)
        {
            tempPoint += (Vector3)point.point;
        }
        tempPoint /= collider.contacts.Length;
        if(tempPoint.y < transform.position.y)
        {
            _isGrounded = true;
        }
    }
}
