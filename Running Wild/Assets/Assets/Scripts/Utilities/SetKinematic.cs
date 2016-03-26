using UnityEngine;
using System.Collections;

public class SetKinematic : MonoBehaviour {

    void Update()
    {
        var currRigidBody = GetComponent<Rigidbody>();
        if (currRigidBody.velocity.y==0)
        {

            currRigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            this.enabled = false;
        }
    }
}
