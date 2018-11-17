using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	private Transform target;
	private EnemyHP targetEnemy;

    

	[Header("General")]

	public float range = 15f;

	[Header("Use Bullets (default)")]
	public GameObject bulletPrefab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("Use Laser")]

    public GameObject LaserTurret;

    public bool useLaser = false;

	public int damageDelayTime = 30;
	public int damage = 5;
    public int UpgradeDamage=10;
    public int UpgradeDamage2=15;

    public LineRenderer lineRenderer;

    public GameObject rayImpact; 
    public GameObject rayMuzzle; 

    [Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";

	public Transform partToRotate;
    public Transform partToRotate2;

	public float turnSpeed = 10f;

	public Transform firePoint;

    

    // Use this for initialization
    void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);

	}
	
	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<EnemyHP>();
		} else
		{
			target = null;
		}

	}

	// Update is called once per frame
	void Update () {
		if (target == null)
		{
            rayMuzzle.gameObject.SetActive(false);
            rayImpact.gameObject.SetActive(false);
            if (useLaser)
			{
				if (lineRenderer.enabled)
				{
                    lineRenderer.enabled = false;
                }
			}

			return;
		}

		LockOnTarget();

		if (useLaser)
		{
            rayMuzzle.gameObject.SetActive(true);
            rayImpact.gameObject.SetActive(true);
            Laser();
        }
        else
		{
            
            if (fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		}

	}

    
	void LockOnTarget ()
	{
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

        Quaternion Right = Quaternion.identity;

        partToRotate2.Rotate(new Vector3(.0f, .0f, 10.0f));
    }

	void Laser ()
	{
        if (LaserTurret.gameObject.tag.Equals("Upgrade"))
        {
            Debug.Log("UpgradeDamage");
            targetEnemy.LaserHit(UpgradeDamage, damageDelayTime);
        }

        else if (LaserTurret.gameObject.tag.Equals("Upgrade2"))
        {
            Debug.Log("UpgradeDamage2");
            targetEnemy.LaserHit(UpgradeDamage2, damageDelayTime);
        }
        else
        {
            targetEnemy.LaserHit(damage, damageDelayTime);
        }


	if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
            rayMuzzle.gameObject.transform.position = firePoint.position;
            rayImpact.gameObject.transform.position = target.position;
        }

	lineRenderer.SetPosition(0, firePoint.position);
	lineRenderer.SetPosition(1, target.position);

	Vector3 dir = firePoint.position - target.position;

    rayMuzzle.transform.position = firePoint.position;
    rayMuzzle.transform.rotation = Quaternion.LookRotation(dir);

    rayImpact.transform.position = target.position + (dir.normalized /2);
    rayImpact.transform.rotation = Quaternion.LookRotation(dir);

    }

	void Shoot ()
	{
        
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();
        
        if (bullet != null)
			bullet.Seek(target);
        
    }

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
