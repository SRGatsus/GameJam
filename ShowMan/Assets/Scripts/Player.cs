using System;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float movementSpeed = 10;
	public float turningSpeed = 60;
	private Animation _anim;
	 public float MaxMoveSpeed = 8;
    
        public AudioSource DashSound;
        public AudioSource StepSound;
    
        public CharacterController controllerComponent;


        private Vector3 moveSpeed;
        private float grabCooldown;
        private float dashingTimeLeft;
        

	private void Start()
	{
		_anim = GetComponent<Animation>();
		if (_anim == null)
		{
			Debug.Log("error");
		}
	}

	void Update()
	{
		float ySpeed = moveSpeed.y;
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		moveSpeed.y = 0;
		if(moveHorizontal!=0||moveVertical!=0){
			if (dashingTimeLeft <= 0)
			{
				_anim.Play("Run");
				Vector3 target = MaxMoveSpeed *
				                 new Vector3(moveHorizontal, 0, moveVertical).normalized;
				moveSpeed = Vector3.MoveTowards(moveSpeed, target, Time.deltaTime * 300);

				if (moveSpeed.magnitude > 0.1f)
				{
					transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveSpeed),
						Time.deltaTime * 720);
				}
			}
			else
			{
				moveSpeed = MaxMoveSpeed * 5 * moveSpeed.normalized;
			}
			
			
		}
		else
		{
			moveSpeed=Vector3.zero;
			_anim.Play("Idle");
		}
		dashingTimeLeft -= Time.deltaTime;

		moveSpeed.y = ySpeed + Physics.gravity.y * Time.deltaTime;
		controllerComponent.Move(moveSpeed * Time.deltaTime);
	}

}