using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GravityWell : MonoBehaviour
{
    //The total number of rigid bodies in the scene for the gravity well to impact
    public Rigidbody[] RigidBodies;

    //The max range the gravity well can influce at
    public float gravityRange;

    //The strength of the gravity pull
    public float strength;

    //The curve determineing relative strength at distance
    public AnimationCurve gravityDistanceCurve;

    // Use this for initialization
    void Start()
    {
        ScanForRigidBodies();
    }

    void ScanForRigidBodies()
    {
        RigidBodies = FindObjectsOfType<Rigidbody>();
    }

    void FixedUpdate()
    {
        ScanForRigidBodies();

        foreach (Rigidbody RB in RigidBodies)
        {

            //Determining gravity force on rigid body
            float gravityDistance = Mathf.Clamp((gameObject.transform.position - RB.gameObject.transform.position).magnitude, 0, Mathf.Infinity);

            if(gravityDistance < gravityRange)
            {
                //Determine direction
                Vector3 gravityDirection = (gameObject.transform.position - RB.gameObject.transform.position).normalized;

                //Applying gravity force to rigid body
                RB.AddForce(gravityDirection * gravityDistanceCurve.Evaluate(gravityDistance / gravityRange) * strength);
            }
            
        }
    }

    void OnTriggerEnter(Collider _collider)
    {
        Debug.Log("Trigger Enter");
        //If you collider with a player
        if (_collider.gameObject.tag == "Player")
        {
            Debug.Log("Hit player");
            //Change the objects colour
            Destroy(_collider.gameObject);
        }
        //If you collider with another level entity
        else if (_collider.gameObject.tag == "LevelEntity")
        {
            Debug.Log("Hit entity");
            //Change the objects colour
            Destroy(_collider.gameObject);
        }
    }
}
