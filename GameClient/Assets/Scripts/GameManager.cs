using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static private GameManager instance;
    public static GameManager Instance { get => instance; }

    [SerializeField] private PlayerController player;
    public PlayerController Player { get => player; }

    [SerializeField] private List<EnemyController> enemies = new List<EnemyController>();
    public List<EnemyController> Enemies { get => enemies; }

    private int score = 0;
    public int Score { get => score; set { score += value; } }

    private void Awake() {
        if (Instance == null)
            instance = this;
        else
            Object.Destroy(this.gameObject);
    }

    public void EndGame() {
        //Show score
    }
}
