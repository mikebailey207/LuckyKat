using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Countdown : MonoBehaviour
{
    public float countdown = 3;
    public GameObject countdownTextGO;
    public TextMeshProUGUI countdownText;

    public static Countdown cdSingleton;
    public bool canStart = false;
    [SerializeField]
    GameObject helix;
    private void OnEnable()
    {
        if (cdSingleton == null) cdSingleton = this;
        else if (cdSingleton != null) Destroy(gameObject);
        canStart = true;
    }
    void Update()
    {
        if(canStart) PerformCountdown();      
    }
    public void PerformCountdown()
    {
        countdownText.text = countdown.ToString("0");
        countdown -= Time.deltaTime;
        StartCoroutine(RemoveCountDown());
        if (countdown < 0.5f) countdownText.text = "SPLASHDOWN";

    }
    IEnumerator RemoveCountDown()
    {
        yield return new WaitForSeconds(3);
        countdownTextGO.SetActive(false);
        helix.GetComponent<MobileAndMouseRotater>().enabled = true;
    }
}
