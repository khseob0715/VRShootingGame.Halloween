using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserGun : MonoBehaviour
{
	private SteamVR_TrackedObject trackedObj;
	// 추적할 오브젝트의 레퍼런스를 선언


	public GameObject[] Parr;

	public float distance = 20.0f;

	private Ray ray;
	private RaycastHit hit;

	private TextMesh ScoreTextMesh;
	private TextMesh TimeTextMesh;

	public GameObject bullet;
	public GameObject Dead_devil;

	public static int score;
	public static int GameTime;
	private float tempTime = 0.0f;
	private bool timecheck = false;


	public static bool bGameExit = false;
	public static bool bGameRestart = false;

	private  AudioSource[] audioSources = new AudioSource[3];
	public  AudioClip[] audioClips = new AudioClip[3];

	private SteamVR_Controller.Device Controller {
		// 컨트롤러에 쉽게 접근하기 위해서 
		// 추적한 오브젝트의 인덱스를 사용하여
		// 컨트롤러의 입력으로 전달한다. 
		get {
			return SteamVR_Controller.Input ((int)trackedObj.index);
		}
	}

	void Awake ()
	{
		Debug.Log ("Awake");
		// 컨트롤러에 삽입되어 있는 
		// SteamVR_TrackedObject 스크립트의 
		// 레퍼런스를 trackedObj로 전달한다. 
		trackedObj = GetComponent<SteamVR_TrackedObject> ();

		// 오디오 소스 
		for (int i = 0; i <3 ; i++) {
			audioSources [i] = gameObject.AddComponent <AudioSource> () as AudioSource;
			audioSources [i].Stop ();
			audioSources [i].clip = audioClips [i];
			audioSources [i].loop = false;
			audioSources [i].playOnAwake = false;
		}
	}

	void OnEnable ()
	{
		Debug.Log ("onEnable");
	}

	// Use this for initialization
	void Start ()
	{

		ScoreTextMesh = GameObject.FindGameObjectWithTag ("CanvasScoreText").GetComponent<TextMesh> ();
		ScoreTextMesh.text = "Escape Town";
		TimeTextMesh = GameObject.FindGameObjectWithTag ("CanvasTimeText").GetComponent<TextMesh> ();
		audioSources [2].loop = true;
		audioSources [2].volume = 0.3f;
		audioSources [0].volume = 0.3f;
		audioSources [1].volume = 0.5f;
		Play(2);
		GameTime = 0;
	//	TimeTextMesh.text = "Time : " + GameTime + " s";
	}
		
	// Update is called once per frame
	void Update ()
	{
		
		ray.origin = this.transform.position;
		ray.direction = this.transform.forward; // 기존 앞으로 나가는 ray 

		tempTime += Time.deltaTime;

		GameTime += 1;
		tempTime = 0.0f;
	//	TimeTextMesh.text = "Time : " + GameTime + " s";
		if (!timecheck) { // 시간 보정.
			GameTime += 1; 
			timecheck = true;
		} else {
			timecheck = false;
		}
			
		if (Controller.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {  // 트리거를 눌렀을 때
			GameObject bul = Instantiate (bullet, this.transform.position + new Vector3 (0f, 0.15f, 0f), this.transform.rotation);
			// 총알 오브젝트 생성

			Play (0); // 총알 효과음

			if (Physics.Raycast (ray, out hit, 100) && hit.collider.gameObject != GameObject.Find ("Plane")) {

				int index = (int)Random.Range (0.0f, 5.0f);
				GameObject temp = Instantiate (Parr [index], hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation); 
				// 오브젝트가 파괴되었을 때 생성되는 particle 오브젝트 
				Destroy (temp, 1.0f); 
				// 파티클 오브젝트 1초 뒤 파괴 



				if (hit.collider.gameObject == GameObject.Find ("ExitButton")) {
					bGameExit = true;
				} else if (hit.collider.gameObject == GameObject.Find ("RestartButton")) {
					bGameRestart = true;	
				}
		
				Play (1); // 몬스터 죽는 효과음.
				if (!bGameExit)
					Destroy (hit.collider.gameObject);

				if (hit.collider.gameObject == GameObject.FindGameObjectWithTag ("Devil")) {
					Debug.Log ("Dead");
					GameObject Death = Instantiate (Dead_devil, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
					Destroy (Death, 2.0f);

				}
				score += 1;
				ScoreTextMesh.text = "Kill Score: " + score;
				TimeTextMesh.text = "Find the key";

							
			}
			Destroy (bul, 2.0f); // 총알 객체 파괴.

		}
	}

	void OnDrawGizmos ()  // 대소문자 주의 할 것 // 총알 쏘는 방향인 ray를 보여줌
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (ray.origin, ray.origin + ray.direction * distance);
		Gizmos.DrawWireSphere (ray.origin, 1.0f);
	}

	public  void Play (int index)
	{
		audioSources [index].Play ();
	}

}
