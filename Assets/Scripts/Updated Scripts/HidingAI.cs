﻿//Game Programming HW1 
//===================================================================================================================
// Name        : HidingAI.cs //Homework1
// Author      : Miguel Cayetano & Robert Martinez
// Description : Script that is attached to Enemy2. Enemy 2 will turn and shoot player if within 15units.
//===================================================================================================================

using UnityEngine;
using System.Collections;

public class HidingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 0.1f;
    public float closeDistance = 15.0F; //distance between player and enemy

    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;
    public Transform target;
    private bool _alive;
    private Animator _animator;

    void Start()
    {
        _alive = true;
    }

    void Update()
    {
        if (_alive)
        {
            _animator = GetComponent<Animator>();
            //transform.Translate(0, 0, speed * Time.deltaTime); // since enemy hides, no need to move

            //If player is next to enemy2it shall lock on to them
            if (GameObject.Find("Player") && GameObject.Find("Player").GetComponent<PlayerCharacter>()._health >= 0)
            {
                Vector3 offset = GameObject.Find("Player").transform.position - transform.position;
                float sqrLen = offset.sqrMagnitude;
                if (sqrLen < closeDistance * closeDistance)
                {
                    print("The other transform is close to me!");
                    transform.LookAt(GameObject.Find("Player").transform.position);
                }
            }
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>() && hitObject.GetComponent<PlayerCharacter>()._health >= 0)
                {
                    if (GameObject.Find("Player").transform.position != null)
                    {
                        Debug.Log("The player position is " + GameObject.Find("Player").transform.position);
                    }
                    transform.LookAt(GameObject.Find("Player").transform.position);
                    //PlayerCharacter playerPosition = GetComponent<PlayerCharacter>();
                    if (_fireball == null)
                    {


                        transform.LookAt(GameObject.Find("Player").transform.position);
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = transform.TransformPoint(0,0.5f,1 * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
        else {
            _animator.SetBool("isDeadCa", true);
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
