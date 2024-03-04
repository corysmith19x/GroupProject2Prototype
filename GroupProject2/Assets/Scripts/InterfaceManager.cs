using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public GameObject pauseScreen;
	public GameObject arrow;
	public GameObject arrow1;
	
	public GameObject pC;
    public bool isPaused = false;
	public bool pauseStart = false;
	private bool m_isAxisInUse = false;
	
	void Start()
	{
		pC = GameObject.Find("Player");
	}
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (isPaused == true) UnPause();
            else Pause();
        }
    }

    public void Pause()
    {
		if (pauseStart == false)
		{
			pauseScreen.SetActive(true);
			arrow.SetActive(true);
			arrow1.SetActive(false);
			isPaused = true;
			pauseStart = true;
			Time.timeScale = 0;
		}
		
		if (Input.GetAxisRaw("Vertical") != 0)
		{
			if (m_isAxisInUse == false)
			{
				if (arrow.activeSelf == true)
				{
					arrow.SetActive(false);
					arrow1.SetActive(true);
				}
				else if (arrow1.activeSelf == true)
				{
					arrow.SetActive(true);
					arrow1.SetActive(false);
				}
				
				m_isAxisInUse = true;
			}
		}
		
		if (Input.GetAxisRaw("Vertical") == 0) m_isAxisInUse = false;
		
		if (Input.GetButtonDown("Cancel")) Application.Quit();
    }

    public void UnPause()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
		pauseStart = false;
        Time.timeScale = 1;
    }
}
