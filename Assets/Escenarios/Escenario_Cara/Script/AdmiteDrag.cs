using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmiteDrag : MonoBehaviour {


	public int tipo =0;
	public Color color1 = Color.red;
	public Color color2 = Color.yellow;
	private float lerp;
	public float duration  = 1.0f;
	private Material materialOriginal;

	public void OnMouseOver(){
		CntDrag.tipo = tipo;
		CntDrag.objetoQuieto = gameObject;
		GetComponent<Renderer>().material = new Material(Shader.Find("Transparent/Diffuse"));

		color1.a = 0.5f;
		color2.a = 0.5f;

		GetComponent<Renderer>().material.color = Color.Lerp (color1,color2,lerp);
	}

	public void OnMouseExit(){
		CntDrag.objetoQuieto = null;
		CntDrag.tipo = -1;
		GetComponent<Renderer> ().material = materialOriginal;
	}


	// Use this for initialization
	void Start () {
		materialOriginal = GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		lerp = Mathf.PingPong (Time.time,duration) / duration;
	}
}
