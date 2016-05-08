using UnityEngine;
using System.Collections;

public class SortingLayer : MonoBehaviour 
{
	public string sortingLayerName;
	public int sortingOrder = 2;

	void Start () 
	{
		this.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = sortingLayerName;
		this.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 2;
	}
}
