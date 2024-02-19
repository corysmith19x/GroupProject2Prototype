using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 lurePosition = new Vector3(transform.position.x, 0.5f, transform.position.z);   
        GameObject.FindObjectOfType<GuardScript>().InvestigateSound(lurePosition);
        Destroy(this.gameObject);   
    }
}
