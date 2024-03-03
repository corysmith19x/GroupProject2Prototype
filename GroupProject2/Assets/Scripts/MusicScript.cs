using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioSource aloneSource;
    public AudioSource nearbySource;
    public AudioSource chaseSource;

	private void Start()
	{
        Alone();
	}

	public void Alone() 
    {
        aloneSource.volume = 1;
        nearbySource.volume = 0;
        chaseSource.volume = 0;
    }

    public void Nearby()
    {
        aloneSource.volume = 0;
        nearbySource.volume = 1;
        chaseSource.volume = 0;
    }

    public void Chase()
    {
        aloneSource.volume = 0;
        nearbySource.volume = 0;
        chaseSource.volume = 1;
    }
}
