using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour {

    public Slider sliderHP;
    [SerializeField] int maxHp;
    [SerializeField] private int curHp;
    private RectTransform rectSliderHP;
    Enemy enemy;
    

    void Start()
    {
        curHp = maxHp;

        sliderHP = Instantiate(sliderHP);
        sliderHP.transform.SetParent(GameObject.Find("Canvas").transform);
        enemy = GetComponent<Enemy>();
        rectSliderHP = sliderHP.GetComponent<RectTransform>();
        UpdateHP();
    }
    public void Hit(int damage)
    {
    
            curHp -= (damage-enemy.Armor);
    
    }

    public void LaserHit(int damage, int damageDelayTime)
    {
        
            curHp -= damage;
            StartCoroutine(LaserHitDamage(damageDelayTime));

        
    }

    IEnumerator LaserHitDamage(int damageDelayTime)
    {
        yield return new WaitForSeconds(damageDelayTime);
    }


    void LateUpdate()
    {
        UpdateHP();
    }

    public void UpdateHP()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        rectSliderHP.position = screenPos + Vector3.up * 15.0f;

        sliderHP.value = (float)curHp / maxHp;
        if (curHp <= 0.01)
        {
            enemy.Die();
            Destroy(sliderHP.gameObject);
        }
    }

}
