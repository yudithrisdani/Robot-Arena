using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	void OnTriggerStay(Collider obj)
	{
		if (obj.gameObject.tag == "Box")
		{
			obj.GetComponent<Animator>().enabled = true;
		}
	}
}