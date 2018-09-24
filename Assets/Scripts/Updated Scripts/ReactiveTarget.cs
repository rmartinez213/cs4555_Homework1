﻿using UnityEngine;
using System.Collections;

public class ReactiveTarget : MonoBehaviour {

	public void ReactToHit() {
		WanderingAI behavior = GetComponent<WanderingAI>();
        HidingAI hiding = GetComponent<HidingAI>();
//        SceneController enemies = GetComponent<SceneController>();
//        if (enemies == null) {
//            Debug.LogError("Cant find SceneController ");
//        }
        
		if (behavior != null) {
            behavior.SetAlive(false);   
		}
        if (hiding != null) {
            hiding.SetAlive(false);
        }

		StartCoroutine(Die());
	}

	private IEnumerator Die() {

		this.transform.Rotate(-90, 0, 0);
        //What I added
		//Vector3 pos = transform.position;
		//pos.y = -1.25f;
		//this.transform.position = pos;

		yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }


}