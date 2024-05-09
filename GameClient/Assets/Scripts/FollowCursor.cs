using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour {

    void Update() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = worldPosition;
    }

}
