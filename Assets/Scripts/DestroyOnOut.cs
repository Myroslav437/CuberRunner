using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnOut : MonoBehaviour {

    public Vector2 Xbounds = new Vector2(-50f, 50f);
    public Vector2 Ybounds = new Vector2(-5f, 50f);
    public Vector2 Zbounds = new Vector2(-10f, 500f);

    private Transform[] childrenPos;

    private void Start()  {
    }

    // Update is called once per frame
    void Update() {
        if (!IsInScene()) {
            Debug.Log("Object destroyed at: " + transform.position.ToString());
            Destroy(this.gameObject);
        }
    }

    bool IsInScene() {
        bool allOutOfScene = true;
        childrenPos = GetComponentsInChildren<Transform>();

        foreach (Transform child in childrenPos) { 
            if (child.position.x > Xbounds.x && child.position.x < Xbounds.y)  {
                if (child.position.y > Ybounds.x && child.position.y < Ybounds.y) {
                    if (child.position.z > Zbounds.x && child.position.z < Zbounds.y)  {
                        allOutOfScene = false;
                        break;
                    }
                }
            }
        }

        return !allOutOfScene;
    }
}
