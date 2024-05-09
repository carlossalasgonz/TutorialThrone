using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyController : VirtualActor {

    [SerializeField] private float damage = 1.0f;

    private void Start() {
        GameManager.Instance.Enemies.Add(this);
    }

    void Update() {
        Vector2 targetPosition = GameManager.Instance.Player.transform.position;
        Vector2 selfPosition = this.transform.position;
        Vector2 chaseVector = targetPosition - selfPosition; //Chase towards target
        base.Move( chaseVector );
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.GetInstanceID() == GameManager.Instance.Player.gameObject.GetInstanceID())
            GameManager.Instance.Player.Health = -damage;
    }

    protected override void Die() {
        base.Die();
        GameManager.Instance.Score = 1;
    }
}
