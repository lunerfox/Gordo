using UnityEngine;
using System.Collections;

//Simple Script to change the color of a material.

public class MaterialColorChanger : MonoBehaviour {

    public Color materialColor = Color.white;

    private Renderer render;

	// Use this for initialization
	void Start () {
        //Grab the renderer and sets the color of the material.
        render = GetComponent<Renderer>();
        render.material.SetColor("_Color", materialColor);
    }
}
