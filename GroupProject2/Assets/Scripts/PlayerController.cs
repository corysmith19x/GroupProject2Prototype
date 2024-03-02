using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour

    //to do:different speeds, and stunning the guard with zones where the rock piles would be
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 0.15f;

    private CharacterController controller;
    private Transform playerCamera;
    private float verticalRotation = 0f;

    public Transform pebbleSpawn;
    public GameObject pebblePrefab;
    public int pebbles;
	
	public GameObject pause;
	public GameObject arrow;
	public GameObject arrow1;
	public bool pauseState;
	public bool pauseStart;
	public bool unpauseStart;

    RockPileScript rockPile;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;

        // Lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
		
		pauseState = false;
		pauseStart = false;
		arrow.SetActive(false);
		arrow1.SetActive(false);
    }

    void Update()
    {
		if (pauseState == false)
		{
			if (!unpauseStart)
			{
				Time.timeScale = 1;
				pause.SetActive(false);
				arrow.SetActive(false);
				arrow1.SetActive(false);
				unpauseStart = true;
			}
			
			float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
			float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

			verticalRotation -= mouseY;
			verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

			playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
			transform.Rotate(Vector3.up * mouseX);

			// Movement
			float moveX = Input.GetAxis("Horizontal");
			float moveZ = Input.GetAxis("Vertical");

			Vector3 move = transform.right * moveX + transform.forward * moveZ;
			controller.Move(move * moveSpeed * Time.deltaTime);
			
			if (Input.GetButtonDown("Fire") && pebbles > 0) ThrowPebble();
		
			if (Input.GetButtonDown("Interact") && rockPile != null)
			{
				rockPile.Collapse();
				rockPile = null;
			}
			
			if (Input.GetButtonDown("Submit"))
				pauseState = true;
		}
		
		if (pauseState == true)
		{
			if (!pauseStart)
			{
				Time.timeScale = 0;
				pause.SetActive(true);
				arrow.SetActive(true);
				pauseStart = true;
			}
		
			if(Input.GetButtonDown("Vertical"))
			{
				arrow.SetActive(!arrow.activeSelf);
				arrow1.SetActive(!arrow1.activeSelf);
			}
			
			if (Input.GetButtonDown("Submit"))
			{
				if (arrow.activeSelf)
					pauseState = false;
			}
		}
		
		if (Input.GetButtonDown("Cancel")) Application.Quit();
    }

    void ThrowPebble()
    {
        GameObject newPebble = Instantiate(pebblePrefab, pebbleSpawn.position, transform.rotation);
        pebbles--;
        newPebble.GetComponent<Rigidbody>().velocity = transform.forward * 10;
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
