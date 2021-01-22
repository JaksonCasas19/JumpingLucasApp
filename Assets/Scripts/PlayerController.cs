using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public GameObject game;
	public GameObject enemyGenerator;
	public AudioClip jumpClip;
	public AudioClip dieClip;
	public AudioClip pointClip;
	public ParticleSystem dust;

	private float startY;

	private AudioSource audioPLayer;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		audioPLayer = GetComponent<AudioSource> ();
		startY = transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {
		bool isGrounded = transform.position.y == startY;
		bool gamePlaying = game.GetComponent<GameController> ().gameState == GameState.Playing;
		bool userAction = Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0);

		if (isGrounded && gamePlaying && userAction ) {
			UpdateState ("PlayerJump");
			audioPLayer.clip = jumpClip;
			audioPLayer.Play ();
		}
	}
	public void UpdateState(string state = null){
		if (state != null) {
			animator.Play (state);
		}

	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Enemy") {
			UpdateState ("PlayerDie");
			game.GetComponent<GameController> ().gameState = GameState.Ended;
			enemyGenerator.SendMessage ("CancelGenerator", true);
			game.SendMessage ("ResetTimeScale", 0.5f);

			game.GetComponent<AudioSource> ().Stop ();
			audioPLayer.clip = dieClip;
			audioPLayer.Play ();
			DustStop ();
		} else if (other.gameObject.tag == "Point") {
			game.SendMessage ("IncreasePoints");
			audioPLayer.clip = pointClip;
			audioPLayer.Play ();
		}

	}
	void GameReady(){
		game.GetComponent<GameController> ().gameState = GameState.Ready;
	}
	void DustPlay(){
		dust.Play ();
	}
	void DustStop(){
		dust.Stop();
	}


}
