using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	public static int Money;
	public int startMoney;
    
    [SerializeField]Text textMoney;
   

    public static int Lives;
	public int startLife;
    
    [SerializeField] Text Life;

    public static int Rounds;
    [SerializeField]Text Round;

    void Start ()
	{
		Money = startMoney;
		Lives = startLife;

		Rounds = 0;
        
    }
    private void Update()
    {
        textMoney.text = Money.ToString();
        Life.text = "Life :" + Lives.ToString();
        Round.text = "Wave : " + Rounds.ToString();
    }

}
