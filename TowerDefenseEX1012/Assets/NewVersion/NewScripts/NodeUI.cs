using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

	public GameObject ui;

	public Text upgradeCost;
	public Button upgradeButton;

    public Text upgradeCost2;
    public Button upgradeButton2;

    public Text sellAmount;

	private Node target;

    private bool upgrade2;


	public void SetTarget (Node _target)
	{
		target = _target;

		transform.position = target.GetBuildPosition();

            if (!target.isUpgraded2)
            {
                upgradeCost2.text = "$" + target.turretBlueprint.upgradeCost2;
                upgradeButton2.interactable = true;
            if (!target.isUpgraded)
            {
                upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;

                upgradeButton.interactable = true;

            }
            else
            {
                upgradeCost.text = "DONE";

                upgradeButton.interactable = false;

            }
        }
            else
            {
                upgradeCost2.text = "DONE";
                upgradeCost.text = "DONE";
                upgradeButton2.interactable = false;
                
            }

            
        
		
		sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

		ui.SetActive(true);
	}

	public void Hide ()
	{
		ui.SetActive(false);
	}

	public void Upgrade (int upgradenum)
	{

            target.UpgradeTurret(upgradenum);

		
		BuildManager.instance.DeselectNode();
	}

	public void Sell ()
	{
        target.isUpgraded = false;
        target.isUpgraded2 = false;
		target.SellTurret();
		BuildManager.instance.DeselectNode();
	}

}
