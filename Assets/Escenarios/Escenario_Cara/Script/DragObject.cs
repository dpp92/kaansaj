using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour {

	//public CntDrag cntDrag = new CntDrag();
	public Renderer render;

	//variables to mvoe
	public Camera camara;
	public int tipo = 0;
	private static Vector3 pos;
	private static Vector2 pos2;


	//variables to personalize
	private GameObject clon;
	public  GameObject iconoClon;
	private Vector3 posClon;

	private bool bol = false;
	private float distance;
	public static float disProfundidad = 5f;
	private Ray rayo;

	public Color color1 = Color.yellow;
	public Color color2 = Color.red;
	public Material materialClon;
	private Material materialOriginal;
	public float duration = 1.0f;

	public float velocidad = 4;

	void OnGui(){
		Event e = Event.current;
		if (e.isMouse) {
			pos2 = e.mousePosition;
		}
	}

	void OnMouseUp(){
		Debug.Log ("Drag ended!!");
		GetComponent<Renderer> ().material = materialOriginal;
		Destroy (clon);
		clon = null;
		bol = false;
		if (CntDrag.objetoQuieto != null && CntDrag.tipo == tipo) {
			transform.position = CntDrag.objetoQuieto.transform.position;
		}
		CntDrag.objetoQuieto = null;
	}

	void OnMouseDrag(){
		Test ();
		actualizado = true;
	}

	// Use this for initialization
	void Start () {
		Inicializa();
		pos = transform.position;
		posClon = transform.position;
		materialOriginal = GetComponent<Renderer> ().material;
	}

	// Update is called once per frame
	void Update () {

	}


	public float lerp;
	void Colores (){
		lerp = Mathf.PingPong (Time.time, duration) / duration;
		if (!materialClon) {
			clon.GetComponent<Renderer> ().material = new Material (Shader.Find ("Transparent/Diffuse"));
			clon.GetComponent<Renderer> ().material.color = Color.Lerp (color1, color2, lerp);
		} else {
			clon.GetComponent<Renderer>().material = materialClon;

		}
		GetComponent<Renderer>().material =  new Material(Shader.Find("Transparent/Diffuse"));
		color1.a = 0.5f;
		color2.a = 0.5f;
	}




	private Vector3 vec;
	private float xClon = pos2.x;
	private float yClon = pos2.y;
	private float zClon = disProfundidad;
	public Vector3 p1;

	public void dibujaClon(){
		p1 = camara.ScreenToWorldPoint (vec);
		clon.transform.position = p1;
		
	}

	private bool actualizado = false;
	private int w = Screen.width;
	private int h = Screen.height;

	public void Inicializa(){
		xClon = pos2.x;
		yClon = h-(pos2.y);
		zClon = disProfundidad;
		actualizado = false;
	}



	private Vector3 p;
	private Matrix4x4 m;

	void Test(){

		if (Input.GetButton("Fire1")) {
			m = camara.cameraToWorldMatrix;
			p = m.MultiplyPoint (new Vector3 (0, 0, distance));
			posClon = camara.WorldToScreenPoint (p);

			if (!bol) {
				if (iconoClon) {
					clon = Instantiate (iconoClon, posClon, Quaternion.identity);

				} else {
					clon = Instantiate (gameObject, posClon, Quaternion.identity);
				}
			}
			bol = true;
		}
	}

	void Rayo(){
		rayo.origin = camara.transform.position;
		rayo.direction = transform.position;
		distance = Vector3.Distance (rayo.origin,rayo.direction);
	}


}
