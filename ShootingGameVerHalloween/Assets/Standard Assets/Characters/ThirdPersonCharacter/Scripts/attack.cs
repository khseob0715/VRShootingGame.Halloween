using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour {

	Animator animator;
	private  AudioSource[] audioSources = new AudioSource[1];
	public  AudioClip[] audioClips = new AudioClip[1];
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		animator.SetFloat ("attak", 0.0f);
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Awake ()
	{
		// 오디오 소스 
		for (int i = 0; i <1 ; i++) {
			audioSources [i] = gameObject.AddComponent <AudioSource> () as AudioSource;
			audioSources [i].Stop ();
			audioSources [i].clip = audioClips [i];
			audioSources [i].loop = false;
			audioSources [i].playOnAwake = false;
			audioSources [i].volume = 0.5f;
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			Play (0);
		}
	}
	void OnTriggerStay(Collider col){
		
		if (col.tag == "Player") {
		//	Debug.Log ("collider");
			animator.SetFloat ("attak", 0.5f);
			Play (0);
		}
			
	}
	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			animator.SetFloat ("attak", 0.0f);
		}
	}
	public  void Play (int index)
	{
		audioSources [index].Play ();
	}
}
