using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

/// <summary>
/// Main game controller:
/// - Handles spins
/// - Betting system
/// - Win evaluation & payout
/// </summary>
public class SlotManager : MonoBehaviour
{
    [Header("Reels")]
    public Reel[] reels;

    [Header("UI References")]
    public Button spinButton;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI betText;
    public GameObject winPopup;

    [Header("Spin Timings")]
    public float spinDuration = 2f;
    public float reelStopDelay = 0.5f;

    [Header("Bet Settings")]
    public int startingBalance = 500;

    private int balance;
    private int currentBet = 10;
    private bool isSpinning;

    void Start()
    {
        balance = startingBalance;
        UpdateBalanceUI();

        statusText.text = "Ready!";
        winPopup.SetActive(false);

        spinButton.onClick.AddListener(OnSpinClicked);
    }

    public void SetBet(int betAmount)
    {
        if (isSpinning) return;
        AudioManager.instance.Buttonclick();
        currentBet = betAmount;
        betText.text = $"Bet: {currentBet}";
    }

    public void OnSpinClicked()
    {
        AudioManager.instance.Buttonclick();
        if (isSpinning) return;
        StartCoroutine(SpinRoutine());
    }

    private IEnumerator SpinRoutine()
    {
        if (balance < currentBet)
        {
            statusText.text = "Not enough balance!";
            yield break;
        }

        isSpinning = true;
        spinButton.interactable = false;
        winPopup.SetActive(false);

        balance -= currentBet;
        UpdateBalanceUI();
        statusText.text = "Spinning...";

        foreach (Reel reel in reels)
            reel.StartSpin();

        yield return new WaitForSeconds(spinDuration);

        foreach (Reel reel in reels)
        {
            reel.StopSpin();
            yield return new WaitForSeconds(reelStopDelay);
        }

        CheckWin();

        isSpinning = false;
        spinButton.interactable = true;
    }

    private void CheckWin()
    {
        string firstSymbol = reels[0].FinalSymbolName;

        foreach (Reel reel in reels)
        {
            if (reel.FinalSymbolName != firstSymbol)
            {
                statusText.text = "Try Again!";
                AudioManager.instance.LossSound();
                return;
            }
        }

        int payout = currentBet * GetMultiplier(firstSymbol);
        balance += payout;
        UpdateBalanceUI();

        statusText.text = $"WIN! +{payout}";
        AudioManager.instance.WinSound();
        winPopup.SetActive(true);
    }

    private int GetMultiplier(string symbolName)
    {
        if (symbolName.Contains("Cherry")) return 2;
        if (symbolName.Contains("Bell")) return 5;
        if (symbolName.Contains("Bar")) return 10;
        return 1;
    }

    private void UpdateBalanceUI()
    {
        balanceText.text = $"Balance: {balance}";
    }

    public void OnYesClicked()
    {
        winPopup.SetActive(false);
        statusText.text = "Ready!";
        AudioManager.instance.Buttonclick();
    }

    public void OnNoClicked()
    {
        AudioManager.instance.Buttonclick();
        winPopup.SetActive(false);
        statusText.text = "Thanks for Playing!";
    }

    public void OnExitClicked()
    {
        AudioManager.instance.Buttonclick();
#if UNITY_EDITOR
        Debug.Log("Exit clicked");
#else
        Application.Quit();
#endif
    }
}