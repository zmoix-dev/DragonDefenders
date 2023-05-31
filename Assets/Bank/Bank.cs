using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI display;

    public int CurrentBalance {
        get { return currentBalance; }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentBalance = startingBalance;
        DisplayBalance();
    }

    public bool Deposit(int amount) {
        currentBalance += Mathf.Abs(amount);
        DisplayBalance();
        return true;
    }

    public bool Withdraw(int amount, bool isForced = false) {
        if (currentBalance >= amount) {
            currentBalance -= Mathf.Abs(amount);
            DisplayBalance();
            return true;
        }
        else {
            if (isForced) {
                // Lose the game
                ReloadScene();
            }
            return false;
        }
    }

    void DisplayBalance() {
        display.text = " Gold: " + currentBalance;
    }

    void ReloadScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
