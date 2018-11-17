using UnityEngine;
using System.Collections;

[System.Serializable]
public class TurretBlueprint {

	public GameObject prefab;
	public int cost;

	public GameObject upgradedPrefab;
    public GameObject upgradedPrefab2;
    public int upgradeCost;
    public int upgradeCost2;

    public int GetSellAmount ()
	{
		return cost / 2;
	}

}
