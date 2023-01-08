using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip chopping;
	[SerializeField] AudioClip drainWater;
	[SerializeField] AudioClip dropOff;
	[SerializeField] AudioClip dropOrgan;
	[SerializeField] AudioClip organSpawn;

	public void PlayChopping()
	{
		audioSource.PlayOneShot(chopping);
	}

	public void PlayDrainWater()
	{
		audioSource.PlayOneShot(drainWater);
	}

	public void PlayDropOff()
	{
		audioSource.PlayOneShot(dropOff);
	}

	public void PlayDropOrgan()
	{
		audioSource.PlayOneShot(dropOrgan);
	}

	public void PlayOrganSpawn()
	{
		audioSource.PlayOneShot(organSpawn);
	}

}
