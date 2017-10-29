using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchPadMove : MonoBehaviour {

	public Transform cameraRigTransform;
	public Transform headRotation;


	private GameObject[] MainCamera;
	private Transform transMainCamera;

	private SteamVR_TrackedObject trackedObj;
	// 추적할 오브젝트의 레퍼런스를 선언



	private  AudioSource[] audioSources = new AudioSource[1];
	public  AudioClip[] audioClips = new AudioClip[1];

	bool check = false;

	private SteamVR_Controller.Device Controller
	{
		// 컨트롤러에 쉽게 접근하기 위해서 
		// 추적한 오브젝트의 인덱스를 사용하여
		// 컨트롤러의 입력으로 전달한다. 
		get 
		{
			return SteamVR_Controller.Input ((int)trackedObj.index);
		}
	}

	void Awake()
	{
		// Debug.Log ("Awake");
		// 컨트롤러에 삽입되어 있는 
		// SteamVR_TrackedObject 스크립트의 
		// 레퍼런스를 trackedObj로 전달한다. 
		trackedObj = GetComponent<SteamVR_TrackedObject> ();

		// 오디오 소스 
		for (int i = 0; i <1 ; i++) {
			audioSources [i] = gameObject.AddComponent <AudioSource> () as AudioSource;
			audioSources [i].Stop ();
			audioSources [i].clip = audioClips [i];
			audioSources [i].loop = false;
			audioSources [i].playOnAwake = false;
		}
	}
	void Start()
	{
		MainCamera = GameObject.FindGameObjectsWithTag ("MainCamera");
		transMainCamera = MainCamera[0].GetComponent<Transform> ();

	
	}
	// Update is called once per frame
	void Update () {
		if (Controller.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
			Play (0);
			cameraRigTransform.position += transMainCamera.forward.normalized / 10;
			if (cameraRigTransform.position.y < 0.0f) {
				cameraRigTransform.position = new Vector3 (cameraRigTransform.position.x, 0.0f, cameraRigTransform.position.z);
			}	
		}
	}
		
	public void Play (int index)
	{
		if(!audioSources[index].isPlaying){
			audioSources [index].Play ();
		}
	}


}
