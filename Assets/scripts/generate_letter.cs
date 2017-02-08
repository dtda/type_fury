using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class generate_letter : MonoBehaviour {

	private int densite = 70;
	private int period = 1;
	private int time = 0;
	private string possibilites = "abcdefghijklmnopqrstuvwxyz";
	private int random_letter;
	private GameObject new_letter;
	private int niveau = 1;
	private int nbr_lettre = 0;
	private Color background = new Color32(255, 110, 153, 255);
	private float t = 1.0f;

	public bool pause = true;
	public GameObject objet;
	public ArrayList lettres = new ArrayList();

	Camera main_camera;
	GameObject the_game;
	Text txt_niveau;

	// Use this for initialization
	void Start () {
		main_camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		the_game = GameObject.Find("the_game");
		txt_niveau = GameObject.Find("Niveau").GetComponent<Text>();
		txt_niveau.text = niveau.ToString ();
		main_camera.backgroundColor = background;
	}
	
	// Update is called once per frame
	void Update () {
		if (!pause) {
			time += period;
			if (time > densite) {
				nbr_lettre++;
				time = 0;

				new_letter = Instantiate (objet, new Vector3 (transform.position.x + (Random.Range (-0.5f, 0.5f) * 5), transform.position.y, 0.0f), Quaternion.identity);
				new_letter.transform.SetParent(the_game.transform);
				random_letter = Random.Range (0, possibilites.Length);
				new_letter.GetComponent<TextMesh> ().text = possibilites [random_letter].ToString ().ToUpper ();
				lettres.Add (possibilites [random_letter].ToString ());

				change_niveau ();

				// Debug.Log (nbr_lettre);
			}

			if (t < 1.0f) {
				t += 0.001f;
				main_camera.backgroundColor = Color32.Lerp (main_camera.backgroundColor, background, t);
			}
		}
	}

	private void change_niveau(){
		switch (nbr_lettre) {
		case 5:
			niveau = 2;
			possibilites = "abcdefghijklmnopqrstuvwxyz";
			//vitesse = 50;
			densite = 65;
			background = new Color32 (255, 110, 182, 255);
			t = 0;
			break;
		case 15:
			niveau = 3;
			possibilites = "abcdefghijklmnopqrstuvwxyz";
			//vitesse = 45;
			densite = 60;
			background = new Color32 (255, 110, 202, 255);
			t = 0;
			break;
		case 30:
			niveau = 4;
			possibilites = "abcdefghijklmnopqrstuvwxyz";
			//vitesse = 40;
			densite = 55;
			background = new Color32 (255, 110, 233, 255);
			t = 0;
			break;
		case 50:
			niveau = 5;
			possibilites = "abcdefghijklmnopqrstuvwxyzéèà?,!";
			//vitesse = 37;
			densite = 50;
			background = new Color32 (255, 110, 255, 255);
			t = 0;
			break;
		case 75:
			niveau = 6;
			possibilites = "abcdefghijklmnopqrstuvwxyzéèà?,!";
			//vitesse = 35;
			densite = 45;
			background = new Color32 (219, 110, 255, 255);
			t = 0;
			break;
		case 100:
			niveau = 7;
			possibilites = "abcdefghijklmnopqrstuvwxyzéèà?,!";
			//vitesse = 33;
			densite = 40;
			background = new Color32 (192, 110, 255, 255);
			t = 0;
			break;
		case 150:
			niveau = 8;
			possibilites = "abcdefghijklmnopqrstuvwxyzéèà?,!";
			//vitesse = 30;
			densite = 35;
			background = new Color32 (154, 110, 255, 255);
			t = 0;
			break;
		case 225:
			niveau = 9;
			possibilites = "abcdefghijklmnopqrstuvwxyzéèà?,!";
			//vitesse = 27;
			densite = 30;
			background = new Color32 (110, 124, 225, 255);
			t = 0;
			break;
		case 300:
			niveau = 10;
			possibilites = "abcdefghijklmnopqrstuvwxyzéèà?,!";
			//vitesse = 25;
			densite = 25;
			background = new Color32 (57, 146, 186, 255);
			t = 0;
			break;
		case 400:
			niveau = 11;
			possibilites = "abcdefghijklmnopqrstuvwxyzéèà?,!";
			//vitesse = 25;
			densite = 20;
			background = new Color32 (57, 146, 186, 255);
			t = 0;
			break;
		}
		txt_niveau.text = niveau.ToString ();
	}
}
