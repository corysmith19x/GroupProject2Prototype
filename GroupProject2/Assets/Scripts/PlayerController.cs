using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

    //to do:different speeds, and stunning the guard with zones where the rock piles would be
{
	public float mouseX;
	public float mouseY;
	public float moveX;
	public float moveZ;
    public float moveSpeed = 15f;
    public float mouseSensitivity = 4f;

    private CharacterController controller;
    private Transform playerCamera;
    private float verticalRotation = 0f;

    public Transform pebbleSpawn;
    public GameObject pebblePrefab;
    public int pebbles;

	public GameObject iM;
    RockPileScript rockPile;

    void Start()
    {
		iM = GameObject.Find("InterfaceManager");
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;

        // Lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
		
		// Movement
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
		
        // Mouse Look
		if (iM.GetComponent<InterfaceManager>().isPaused == true)
		{
			iM.GetComponent<InterfaceManager>().Pause();
		}
		else 
		{
			mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
			mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

			verticalRotation -= mouseY;
			verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

			playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
			transform.Rotate(Vector3.up * mouseX);
			
			Vector3 move = transform.right * moveX + transform.forward * moveZ;
			controller.Move(move * moveSpeed * Time.deltaTime);

			if (Input.GetButtonDown("Fire") && pebbles > 0) ThrowPebble();
        
			if (Input.GetButtonDown("Interact") && rockPile != null)
			{
				rockPile.Collapse();
				rockPile = null;
			}
		}
		
		if (Input.GetButtonDown("Cancel")) Application.Quit();
    }

    void ThrowPebble()
    {
        GameObject newPebble = Instantiate(pebblePrefab, pebbleSpawn.position, transform.rotation);
        pebbles--;
        newPebble.GetComponent<Rigidbody>().velocity = transform.forward * 20;
        newPebble.GetComponent<Rigidbody>().AddForce(3 * Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pebble")
        {
            Destroy(other.gameObject);
            pebbles++;
        }
        if (other.gameObject.tag == "RockPile")
        {
           rockPile = other.gameObject.GetComponent<RockPileScript>();
        }
    }
	
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RockPile")
        {
            rockPile = null;
        }
    }
}
