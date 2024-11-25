using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ChargeThrow : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject itemPrefab;
    public GameObject itemPrefabHeavy;
    public float maxThrowForce = 10f;
    public float chargeSpeed = 6f;
    public GameObject Charge;
    public Image Guage;
    public ChargeThrow anotherPlayer;
    public float PlayerHealth = 40;

    public bool leftThrow = false;
    private float currentThrowForce = 0f;
    private float FirstForce = 0f;
    private bool isCharging = false;
    public bool isPlayerTurn = true;
    public bool turnEnd = false;

    public bool isHeavyThrow = false;
    public bool isHealing = false;
    public bool isDouble = false;
    public bool timeStop;

    public int HealPoint = 10;

    public Image Healthbar;

    public float MaxTime = 10f;
    public float timeLimit = 10f;
    public TextMeshProUGUI Timer;

    private void Start()
    {
        Healthbar.fillAmount = PlayerHealth / 40;
    }
    void Update()
    {
        if (turnEnd == false)
        {
            if (Input.touchCount > 0)
            {
                Debug.Log("touch");
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        if (isPlayerTurn)
                        {
                            isCharging = true;
                            timeStop = true;
                        }
                    }
                }
                else if (touch.phase == TouchPhase.Ended && isCharging)
                {
                    timeStop = true;
                    ThrowItem();
                    isCharging = false;
                    turnEnd = true;

                    StartCoroutine(SwitchTurn());
                }
                else if (currentThrowForce >= maxThrowForce)
                {
                    timeStop = true;
                    ThrowItem();
                    isCharging = false;
                    turnEnd = true;
                    StartCoroutine(SwitchTurn());
                }
            }

            if (isCharging)
            {
                Charge.SetActive(true);
                currentThrowForce += chargeSpeed * Time.deltaTime;
                currentThrowForce = Mathf.Clamp(currentThrowForce, 0, maxThrowForce);
                Guage.fillAmount = currentThrowForce / maxThrowForce;
            }
            if (isHealing)
            {
                turnEnd = true;
                timeStop = true;
                StartCoroutine(SwitchTurn());
                HealPlayer();
                isHealing = false;
            }
            if (isCharging == false && timeStop == false)
            {
                timeLimit = timeLimit - Time.deltaTime;
                Timer.text = timeLimit.ToString("f0");
                if (timeLimit <= 0)
                {
                    turnEnd = true;
                    StartCoroutine(SwitchTurn());
                }
            }
        }
    }

    void ThrowItem()
    {
        GameObject prefab;
        prefab = itemPrefab;
        if (isHeavyThrow)
        {
            prefab = itemPrefabHeavy;
            isHeavyThrow = false;
        }
        GameObject item = Instantiate(prefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        int Directionthrow = 1;
        if (leftThrow == true)
        {
            Directionthrow = -1;
        }
        float throwAngle = 55f;

        float angleInRadians = throwAngle * Mathf.Deg2Rad;

        float xDirection = Directionthrow * Mathf.Cos(angleInRadians);
        float yDirection = Mathf.Sin(angleInRadians);

        Vector2 throwDirection = new Vector2(xDirection, yDirection).normalized;

        if (leftThrow == true)
        {
            if (WindControl.instance.WindDirection == 1f)
            {
                rb.AddForce(throwDirection * ((currentThrowForce + 5) - (WindControl.instance.WindForce * 1.2f)), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(throwDirection * ((currentThrowForce + 5) + (WindControl.instance.WindForce * 1.2f)), ForceMode2D.Impulse);
            }
        }
        else
        {
            if (WindControl.instance.WindDirection == 1f)
            {
                rb.AddForce(throwDirection * ((currentThrowForce + 5) + (WindControl.instance.WindForce * 1.2f)), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(throwDirection * ((currentThrowForce + 5) - (WindControl.instance.WindForce * 1.2f)), ForceMode2D.Impulse);
            }
        }


        FirstForce = currentThrowForce;
        if (isDouble)
        {
            StartCoroutine(SecondThrow());
        }
        currentThrowForce = 0f;
        Charge.SetActive(false);
        WindControl.instance.WindChange();
    }


    IEnumerator SwitchTurn()
    {
        Gamemanager.instance.Skillable = false;
        yield return new WaitForSeconds(4);
        if (isPlayerTurn && Gamemanager.instance.gameEndALD == false)
        {
            anotherPlayer.timeLimit = anotherPlayer.MaxTime;
            anotherPlayer.isPlayerTurn = true;
            anotherPlayer.turnEnd = false;
            anotherPlayer.timeStop = false;
        }
        Gamemanager.instance.ChangeTurn();
        Gamemanager.instance.Skillable = true;
        isPlayerTurn = !isPlayerTurn;
    }

    IEnumerator SecondThrow()
    {
        isDouble = false;
        yield return new WaitForSeconds(2);
        GameObject prefab;
        prefab = itemPrefab;
        GameObject item = Instantiate(prefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        int Directionthrow = 1;
        if (leftThrow == true)
        {
            Directionthrow = -1;
        }
        float throwAngle = 55f;

        float angleInRadians = throwAngle * Mathf.Deg2Rad;

        float xDirection = Directionthrow * Mathf.Cos(angleInRadians);
        float yDirection = Mathf.Sin(angleInRadians);

        Vector2 throwDirection = new Vector2(xDirection, yDirection).normalized;

        if (leftThrow == true)
        {
            if (WindControl.instance.WindDirection == 1f)
            {
                rb.AddForce(throwDirection * ((currentThrowForce + 5) - (WindControl.instance.WindForce * 1.2f)), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(throwDirection * ((currentThrowForce + 5) + (WindControl.instance.WindForce * 1.2f)), ForceMode2D.Impulse);
            }
        }
        else
        {
            if (WindControl.instance.WindDirection == 1f)
            {
                rb.AddForce(throwDirection * ((currentThrowForce + 5) + (WindControl.instance.WindForce * 1.2f)), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(throwDirection * ((currentThrowForce + 5) - (WindControl.instance.WindForce * 1.2f)), ForceMode2D.Impulse);
            }
        }

        FirstForce = 0f;
        Charge.SetActive(false);
        WindControl.instance.WindChange();
    }

    public void TakeDamage(int Damage)
    {
        Debug.Log("damage");
        PlayerHealth -= Damage;
        Debug.Log(PlayerHealth);
        Healthbar.fillAmount = PlayerHealth / 40;
        Debug.Log(Healthbar.fillAmount);
        if (PlayerHealth <= 0)
        {
            Gamemanager.instance.gameEnd();
        }
    }
    public void HealPlayer()
    {
        PlayerHealth += HealPoint;
        Healthbar.fillAmount = PlayerHealth / 40;
        isHealing = false;
    }
}
