using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class kill_letter : MonoBehaviour {

	private ArrayList lettres = new ArrayList();

	public int points = 0;
	private int erreurs = 0;
	private int erreurs_max = 5;

	public bool pause = true;
	public ParticleSystem explosion;
	public AudioSource as_music;
	public AudioSource as_good;
	public AudioSource as_fail;
	public AudioSource as_error;

	Text txt_score;
	Animator heart1;
	Animator heart2;
	Animator heart3;
	Animator heart4;
	Animator heart5;

	// Use this for initialization
	void Start () {
		// txt_score = GetComponent<Text>();
		txt_score = GameObject.Find("Score").GetComponent<Text>();
		heart1 = GameObject.Find("heart1").GetComponent<Animator>();
		heart2 = GameObject.Find("heart2").GetComponent<Animator>();
		heart3 = GameObject.Find("heart3").GetComponent<Animator>();
		heart4 = GameObject.Find("heart4").GetComponent<Animator>();
		heart5 = GameObject.Find("heart5").GetComponent<Animator>();

		Instantiate (as_music, Vector3.zero, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.anyKeyDown) {

			if (!pause) {
				lettres = GameObject.Find ("Generate_letter").GetComponent<generate_letter> ().lettres;
				
				if (lettres.Count != 0 && lettres [0].ToString () == Input.inputString) {
					
					var array_letter = GameObject.FindGameObjectsWithTag ("letter");
					if (array_letter.Length != 0) {
						array_letter [0].GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
						array_letter [0].GetComponent<BoxCollider2D> ().enabled = false;
						array_letter [0].GetComponent<Animator> ().Play ("letter_touch");
						array_letter [0].tag = "Untagged";

						var fx_good = Instantiate (as_good, array_letter [0].transform.position, array_letter [0].transform.rotation);
						Destroy (fx_good.gameObject, 1.0f);

						Destroy (array_letter [0], 0.30f);
						delete_lettre ();
						lettre_reussie ();
					}

				} else {
					if (!Input.GetKeyDown (KeyCode.Escape) && !Input.GetKeyDown (KeyCode.LeftApple) && !Input.GetKeyDown (KeyCode.RightApple) && !Input.GetKeyDown (KeyCode.LeftAlt) && !Input.GetKeyDown (KeyCode.RightAlt) && !Input.GetKeyDown (KeyCode.LeftShift) && !Input.GetKeyDown (KeyCode.RightShift) && !Input.GetMouseButtonDown (0) && !Input.GetMouseButtonDown (1) && !Input.GetMouseButtonDown (2)) {
						lettre_ratee ();

						var fx_error = Instantiate (as_error, Vector3.zero, Quaternion.identity);
						Destroy (fx_error.gameObject, 1.0f);
					} else {
						string etat_menu = GameObject.Find ("_manage").GetComponent<manage_menu> ().condition_menu;
						if (Input.GetKeyDown (KeyCode.Escape) && (etat_menu == "game_open" || etat_menu == "menu_close")) {
							GameObject.Find ("_manage").GetComponent<manage_menu> ().goToMenu ("menu_open");
						}
					}
				}
			} else {
				string etat_menu = GameObject.Find ("_manage").GetComponent<manage_menu> ().condition_menu;
				if (Input.GetKeyDown (KeyCode.Escape) && etat_menu == "menu_open") {
					GameObject.Find ("_manage").GetComponent<manage_menu> ().goToMenu ("menu_close");
				}
			}
		}

		txt_score.text = points.ToString();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "letter")
		{
			delete_lettre();
			lettre_ratee();
			Destroy(other.gameObject, 0.0f);

			var boum = Instantiate (explosion, other.transform.position, other.transform.rotation);
			boum.transform.SetParent (GameObject.Find("the_game").transform);
			Destroy(boum.gameObject, 1.0f);

			var fx_fail = Instantiate (as_fail, Vector3.zero, Quaternion.identity);
			Destroy(fx_fail.gameObject, 1.0f);

			//other.GetComponent<Renderer> ().material.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
			//SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
		}
	}

	private void delete_lettre(){
		GameObject.Find("Generate_letter").GetComponent<generate_letter>().lettres.RemoveAt(0);
	}

	private void lettre_ratee(){
		erreurs += 1;
		points -= 10;

		switch (erreurs) {
		case 1:
			heart1.Play ("heart_full");
			break;
		case 2:
			heart2.Play("heart_full");
			break;
		case 3:
			heart3.Play("heart_full");
			break;
		case 4:
			heart4.Play("heart_full");
			break;
		case 5:
			heart5.Play("heart_full");
			break;
		}

		if(erreurs >= erreurs_max){
			gameOver ();
		}
	}

	private void lettre_reussie(){
		points += 10;
	}

	private void gameOver(){
		GameObject.Find ("_manage").GetComponent<manage_menu> ().goToMenu("end_open");
		GameObject.Find("Score_end").GetComponent<Text>().text = points.ToString();
	}
}