using TMPro;
using UnityEngine;

public class CoinHud : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI SilverText;
    private int GoldAmount = 0;
    private int SilverAmount = 0;

    void Start()
    {
        GoldText.text = "0";
        SilverText.text = "0";
    }

    void Update()
    {

    }

    public void IncreaseCoin(string type)
    {
        if (type == "Gold")
        {
            GoldAmount += 5;
            GoldText.text = GoldAmount.ToString();
        }

        if (type == "Silver")
        {
            SilverAmount += 1;
            SilverText.text = SilverAmount.ToString();
        }
    }
}
