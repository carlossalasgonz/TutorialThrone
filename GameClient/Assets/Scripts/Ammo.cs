using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Ammo : MonoBehaviour {

    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float expireTime = 5f;
    private Coroutine expireTimer;


    private void onEnable() {
        expireTimer = StartCoroutine( deleteAfterTime() );
    }
    private void OnDisable() {
        if( expireTimer != null) {
            StopCoroutine( expireTimer );
        }
    }

    void Update() {
        if(this.isActiveAndEnabled) {
            transform.Translate(this.transform.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Enemy") {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            enemy.Health = -damage;
            this.gameObject.SetActive(false);
        }
    }
    private IEnumerator deleteAfterTime() {
        yield return new WaitForSeconds(expireTime);
        this.gameObject.SetActive(false);
    }
    private void OnDrawGizmos() {
        if (this.gameObject.active) {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine((Vector2)transform.position, (Vector2)(transform.position + transform.TransformDirection(transform.up)));
        }
    }
}
