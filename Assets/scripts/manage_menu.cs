using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manage_menu : MonoBehaviour {

	private Animator anim_game;
	private Animator anim_menu;
	private GameObject the_game;
	private RectTransform canvas_start;
	private RectTransform canvas_game;
	private RectTransform canvas_menu;
	private RectTransform canvas_end;

	private GameObject[] lettres_actives;
	private string condition_menu;
	private float lerpTime = 5.0f;
	private float currentLerpTime = 1.0f;
	private float perc = 1;

	// Use this for initialization
	void Start () {
		anim_game = GameObject.Find ("Canvas_game").GetComponent<Animator>();
		anim_menu = GameObject.Find ("Canvas_menu").GetComponent<Animator>();
		the_game = GameObject.Find ("the_game");
		canvas_start = GameObject.Find ("Canvas_start").GetComponent<RectTransform>();
		canvas_game = GameObject.Find ("Canvas_game").GetComponent<RectTransform>();
		canvas_menu = GameObject.Find ("Canvas_menu").GetComponent<RectTransform>();
		canvas_end = GameObject.Find ("Canvas_end").GetComponent<RectTransform>();

		pauseGame (true);
	}

	// Update is called once per frame
	void Update () {
		if (currentLerpTime <= 1.0f) {
			currentLerpTime += Time.deltaTime;
			perc = currentLerpTime / lerpTime;

			if (condition_menu == "game_open") {
				canvas_start.anchoredPosition = new Vector2 (canvas_start.anchoredPosition.x, Mathf.Lerp(canvas_start.anchoredPosition.y, 640, perc));
				the_game.transform.position = new Vector2 (the_game.transform.position.x, Mathf.Lerp(the_game.transform.position.y, 303, perc));
				canvas_game.anchoredPosition = new Vector2 (canvas_game.anchoredPosition.x, Mathf.Lerp(canvas_game.anchoredPosition.y, 0, perc));
				canvas_menu.anchoredPosition = new Vector2 (canvas_menu.anchoredPosition.x, Mathf.Lerp(canvas_menu.anchoredPosition.y, 0, perc));
				canvas_end.anchoredPosition = new Vector2 (canvas_end.anchoredPosition.x, Mathf.Lerp(canvas_end.anchoredPosition.y, -640, perc));
			}
			if (condition_menu == "menu_open") {
				the_game.transform.position = new Vector2 (Mathf.Lerp(the_game.transform.position.x, 430, perc), the_game.transform.position.y);
				canvas_game.anchoredPosition = new Vector2 (Mathf.Lerp(canvas_game.anchoredPosition.x, -900, perc), canvas_game.anchoredPosition.y);
				canvas_menu.anchoredPosition = new Vector2 (Mathf.Lerp(canvas_menu.anchoredPosition.x, 0, perc), canvas_menu.anchoredPosition.y);
			}
			if (condition_menu == "menu_close") {
				the_game.transform.position = new Vector2 (Mathf.Lerp(the_game.transform.position.x, 459, perc), the_game.transform.position.y);
				canvas_game.anchoredPosition = new Vector2 (Mathf.Lerp(canvas_game.anchoredPosition.x, 0, perc), canvas_game.anchoredPosition.y);
				canvas_menu.anchoredPosition = new Vector2 (Mathf.Lerp(canvas_menu.anchoredPosition.x, 900, perc), canvas_menu.anchoredPosition.y);
			}
			if (condition_menu == "end_open") {
				canvas_start.transform.position = new Vector2 (canvas_start.transform.position.x, Mathf.Lerp(canvas_start.transform.position.y, 1280, perc));
				the_game.transform.position = new Vector2 (the_game.transform.position.x, Mathf.Lerp(the_game.transform.position.y, 320, perc));
				canvas_game.anchoredPosition = new Vector2 (canvas_game.anchoredPosition.x, Mathf.Lerp(canvas_game.anchoredPosition.y, 640, perc));
				canvas_menu.anchoredPosition = new Vector2 (canvas_menu.anchoredPosition.x, Mathf.Lerp(canvas_menu.anchoredPosition.y, 640, perc));
				canvas_end.anchoredPosition = new Vector2 (canvas_end.anchoredPosition.x, Mathf.Lerp(canvas_end.anchoredPosition.y, 0, perc));
			}
		}
	}

	public void changeToScene (string scene) {
		SceneManager.LoadScene (scene);
	}

	public void goToMenu (string condition) {
		condition_menu = condition;

		if (condition == "game_open") {
			pauseGame (false);
		}

		if (condition == "menu_open") {
			pauseGame (true);
		}

		if (condition == "menu_close") {
			pauseGame (false);
		}

		if (condition == "end_open") {
			pauseGame (true);
		}
	}

	public void pauseGame (bool etat) {
		currentLerpTime = 0.0f;

		if(etat){
			lettres_actives = GameObject.FindGameObjectsWithTag("letter");
			foreach (GameObject lettre_active in lettres_actives) {
				lettre_active.GetComponent<Rigidbody2D> ().isKinematic = true;
				lettre_active.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			}
			GameObject.Find ("Generate_letter").GetComponent<generate_letter> ().pause = true;
			GameObject.Find ("Kill_letter").GetComponent<kill_letter> ().pause = true;
		}else{
			lettres_actives = GameObject.FindGameObjectsWithTag("letter");
			foreach (GameObject lettre_active in lettres_actives) {
				lettre_active.GetComponent<Rigidbody2D> ().isKinematic = false;
			}
			GameObject.Find ("Generate_letter").GetComponent<generate_letter> ().pause = false;
			GameObject.Find ("Kill_letter").GetComponent<kill_letter> ().pause = false;
		}
	}
}