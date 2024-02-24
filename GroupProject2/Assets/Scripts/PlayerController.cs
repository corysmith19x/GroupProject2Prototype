using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour

    //to do:different speeds, and stunning the guard with zones where the rock piles would be
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 0.2f;

    private CharacterController controller;
    private Transform playerCamera;
    private float verticalRotation = 0f;

    public Transform pebbleSpawn;
    public GameObject pebblePrefab;
    public int pebbles;
	
	public GameObject pauseMenu;
	public bool pauseState;

    RockPileScript rockPile;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;

        // Lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
		
		pauseState = false;
    }

    void Update()
    {
		if (!pauseState)
		{
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
		
			if (Input.GetButtonDown("Cancel")) QuitGame();
		}
		
		if (Input.GetButtonDown("Submit") && !pauseState)
			PauseGame();
		else if (Input.GetButtonDown("Submit") && pauseState)
			UnpauseGame();
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
	
	public void PauseGame()
	{
		pauseState = true;
		pauseMenu.SetActive(true);
		Time.timeScale = 0;
	}
	
	public void UnpauseGame()
	{
		pauseState = false;
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}
