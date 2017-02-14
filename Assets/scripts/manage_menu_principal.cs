using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class manage_menu_principal : MonoBehaviour {

	private RectTransform canvas_menu;
	private RectTransform canvas_options;
	private Toggle btn_mute;
	Camera main_camera;

	private string condition_menu;
	private float lerpTime = 5.0f;
	private float currentLerpTime = 1.0f;
	private float perc = 1;

	private BinaryFormatter bf;
	private FileStream file;
	private Color background;
	public Color background_menu = new Color32(255, 110, 153, 255);
	public Color background_options = new Color32(110, 230, 255, 255);

	// Use this for initialization
	void Start () {
		main_camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		canvas_menu = GameObject.Find ("Canvas_menu").GetComponent<RectTransform>();
		canvas_options = GameObject.Find ("Canvas_options").GetComponent<RectTransform>();
		btn_mute = GameObject.Find ("musique").GetComponent<Toggle>();
		background = background_menu;

		if (File.Exists (Application.persistentDataPath + "/optionsInfo.dat")) {
			loadOptions ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (currentLerpTime <= 1.0f) {
			currentLerpTime += Time.deltaTime;
			perc = currentLerpTime / lerpTime;

			if (condition_menu == "menu_principal") {
				canvas_menu.anchoredPosition = new Vector2 (Mathf.Lerp(canvas_menu.anchoredPosition.x, 0, perc), canvas_menu.anchoredPosition.y);
				canvas_options.anchoredPosition = new Vector2 (Mathf.Lerp(canvas_options.anchoredPosition.x, 900, perc), canvas_options.anchoredPosition.y);
				main_camera.backgroundColor = Color32.Lerp (main_camera.backgroundColor, background, perc);
			}
			if (condition_menu == "menu_options") {
				canvas_menu.anchoredPosition = new Vector2 (Mathf.Lerp(canvas_menu.anchoredPosition.x, -900, perc), canvas_menu.anchoredPosition.y);
				canvas_options.anchoredPosition = new Vector2 (Mathf.Lerp(canvas_options.anchoredPosition.x, 0, perc), canvas_options.anchoredPosition.y);
				main_camera.backgroundColor = Color32.Lerp (main_camera.backgroundColor, background, perc);
			}
		}
	}

	public void changeToScene (string scene) {
		SceneManager.LoadScene (scene);
	}

	public void goToMenu (string condition) {
		condition_menu = condition;
		currentLerpTime = 0.0f;
		if (condition_menu == "menu_principal") {
			background = background_menu;
		}
		if (condition_menu == "menu_options") {
			background = background_options;
		}
	}

	public void goToDTDA () {
		Application.OpenURL ("http://www.dtda.fr");
	}

	public void saveOptions () {
		bf = new BinaryFormatter ();
		file = File.Create (Application.persistentDataPath + "/optionsInfo.dat");

		optionsData data = new optionsData ();
		data.etatMusic = btn_mute.isOn;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void loadOptions () {
		if (File.Exists (Application.persistentDataPath + "/optionsInfo.dat")) {
			bf = new BinaryFormatter ();
			file = File.Open (Application.persistentDataPath + "/optionsInfo.dat", FileMode.Open);
			optionsData data = (optionsData)bf.Deserialize (file);
			file.Close ();

			btn_mute.isOn = data.etatMusic;
		}
	}
}

[Serializable]
class optionsData{
	public bool etatMusic;
}

[Serializable]
public class scores
{
	public string pseudo;
	public int score;
}

[Serializable]
public class list_scores
{
	public List<scores> data;
}