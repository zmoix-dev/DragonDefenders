using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;

    public int CurrentBalance {
        get { return currentBalance; }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentBalance = startingBalance;
    }

    public bool Deposit(int amount) {
        currentBalance += Mathf.Abs(amount);
        return true;
    }

    public bool Withdraw(int amount) {
        if (currentBalance >= amount) {
            currentBalance -= Mathf.Abs(amount);
            return true;
        }
        else {
            // Lose the game
            ReloadScene();
            return false;
        }
    }

    void ReloadScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
