using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Enemy : MonoBehaviour {

	public float startSpeed = 10f;

	[HideInInspector]
	public float speed;

	public int worth = 50;

    public int Armor = 3;


    public GameObject deathEffect;

	private bool isDead = false;

    private Animator animator;

    private float deathTime;

	void Start ()
	{
		speed = startSpeed;
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", true);

    }



	public void Die ()
	{
        gameObject.tag = "Untagged";
        startSpeed = 0;
        animator.SetTrigger("Die");
		isDead = true;

		PlayerStats.Money += worth;

		GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 0.5f);

		WaveSpawner.EnemiesAlive--;
        Destroy(gameObject, 1.5f);

    }
     


}
