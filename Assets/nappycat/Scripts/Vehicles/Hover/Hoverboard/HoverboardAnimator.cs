using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nappycat.Vehicles.Hover
{
	public class HoverboardAnimator : MonoBehaviour {

		private HoverboardController controller;
		private Animator animator;

		// Use this for initialization
		void Awake ()
		{
			controller = GetComponent<HoverboardController> ();
			animator = GetComponentInChildren<Animator> ();
		}
		
		// Update is called once per frame
		void Update ()
		{
			animator.SetFloat ("Speed", controller.speed);	
			animator.SetFloat ("Steer", controller.steerInput);		
		}
	}
	}