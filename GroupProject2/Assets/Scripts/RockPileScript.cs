using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPileScript : MonoBehaviour
{
    GuardScript guard;
    public void Collapse()
    {
        if(guard != null)
        {
            guard.StartCoroutine("Stunned");
        }
        //playanimation
        StartCoroutine("DestroyTimer");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Guard") guard = other.gameObject.GetComponent<GuardScript>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Guard") guard = null;
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(5);
        Destroy(transform.parent.gameObject);
    }
}
