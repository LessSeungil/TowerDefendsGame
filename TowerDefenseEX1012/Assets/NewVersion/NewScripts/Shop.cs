using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBlueprint CanBuildNode;
	public TurretBlueprint GatLingTurret;
	public TurretBlueprint Bow;
	public TurretBlueprint LaserGun;
    public TurretBlueprint Plasma;


	BuildManager buildManager;

	void Start ()
	{
		buildManager = BuildManager.instance;
	}

    public void SelectCanBuildNode()
    {
        buildManager.SelectTurretToBuild(CanBuildNode);
    }

	public void SelectGatLingTurret()
	{
		
		buildManager.SelectTurretToBuild(GatLingTurret);
	}

	public void SelectBow()
	{
		buildManager.SelectTurretToBuild(Bow);
	}

	public void SelectLaserGun()
    { 
		buildManager.SelectTurretToBuild(LaserGun);
	}

    public void SelectPlasma()
    {
        buildManager.SelectTurretToBuild(Plasma);
    }

}
