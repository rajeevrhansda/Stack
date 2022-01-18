using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudio : MonoBehaviour
{
	private UIManager _uiManager;
	AudioSource audioSrc;
	public static DontDestroyAudio instance = null;

	// Use this for initialization
	void Awake()
	{
		//there's no audio
		if (instance == null)
			instance = this;
		//audio already exists and this instance is not the original
		else if (instance != this)
			Destroy(this.gameObject);
		DontDestroyOnLoad(this.gameObject);
		audioSrc = GetComponent<AudioSource>();
		_uiManager = FindObjectOfType<UIManager>();
		//audioSrc.mute = false;
		MUTE();

	}
	private void Start()
	{

	}

	public void MUTE()
	{
		int value = PlayerPrefs.GetInt("Sound", 1);
		if (value == 1)
		{
			audioSrc.mute = false;

		}
		else
		{
			audioSrc.mute = true;

		}
	}
	public void MUTEON()
	{
		audioSrc.mute = true;

	}
	public void MUTEOFF()
	{
		audioSrc.mute = false;

	}




}
