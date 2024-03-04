using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestWinTrigger : MonoBehaviour
{
    public GameObject iM;

    // Start is called before the first frame update
    void Start()
    {
        iM = GameObject.Find("InterfaceManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            iM.GetComponent<InterfaceManager>().winScene();
        }
    }
}
