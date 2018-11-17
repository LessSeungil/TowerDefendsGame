using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{

    public Vector3 positionOffset;
    [HideInInspector]
    public GameObject Nodes;

    public GameObject NodePrefabs;


    BuildManager buildManager;


    void Start()
    {
        
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {


        if (EventSystem.current.IsPointerOverGameObject())
            return;
        Ray ray;
        RaycastHit Hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out Hit, Mathf.Infinity))
        {
           if (Hit.collider.tag.Equals("Nodes"))
            {
                if (!buildManager.CanBuild)
                    return;

                BuildNode(buildManager.GetTurretToBuild());
            }
        }

    }

    void BuildNode(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        Nodes = (GameObject)Instantiate(NodePrefabs, GetBuildPosition(), Quaternion.identity);

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Tile build!");
    }

    void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;
    }

    void OnMouseExit()
    {

    }
}
