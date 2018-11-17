using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {
    
	private Transform target;
	private int wavepointIndex = 0;

	private Enemy enemy;
    private EnemyHP enemyhp;

	void Start()
	{
        enemy = GetComponent<Enemy>();
        enemyhp = GetComponent<EnemyHP>();

        target = Waypoints.points[0];
    }

	void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
            GetNextWaypoint();
            //dir.y = 0;
            //transform.rotation = Quaternion.LookRotation(dir);

        }

		enemy.speed = enemy.startSpeed;
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1)
		{
			EndPath();
			return;
		}

		wavepointIndex++;
		target = Waypoints.points[wavepointIndex]; //타겟을 다음 웨이 포인트로 변경한다.

        Vector3 lookDirection = target.position - transform.position;
        lookDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

	public void EndPath()
	{
		PlayerStats.Lives--;
		WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
        Destroy(enemyhp.sliderHP.gameObject);
	}

}
