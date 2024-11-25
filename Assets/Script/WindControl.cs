using UnityEngine;
using TMPro;

public class WindControl : MonoBehaviour
{
    public float WindDirection;
    public int WindPower;
    public float WindForce;
    public int turnCount = 4;
    public static WindControl instance;

    public GameObject WindSymbol;
    public TextMeshProUGUI WindText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        WindChange();

    }
    public void WindChange()
    {
        if (turnCount == 4)
        {
            WindDirection = Random.Range(0, 2) == 0 ? -1f : 1f;
            WindPower = Random.Range(1, 6);
            WindForce = WindDirection * (WindPower/8f);

            if (WindDirection == 1f)
            {
                WindSymbol.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                WindSymbol.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            turnCount = 0;
            WindText.text = WindPower.ToString();
        }
        turnCount += 1;
    }
}