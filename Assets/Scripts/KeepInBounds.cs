using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInBounds : MonoBehaviour
{
    public Camera MainCamera;
    public BoxCollider2D _player1;
    public BoxCollider2D _player2;

    private Vector2 screenBounds;
    private float p1ObjectWidth;
    private float p1ObjectHeight;
    private float p2ObjectWidth;
    private float p2ObjectHeight;

    // Use this for initialization
    void Start() {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width * 0.965f, Screen.height, MainCamera.transform.position.z));
        p1ObjectWidth = _player1.bounds.extents.x; //extents = size of width / 2
        p1ObjectHeight = _player1.bounds.extents.y; //extents = size of height / 2
        p2ObjectWidth = _player2.bounds.extents.x; //extents = size of width / 2
        p2ObjectHeight = _player2.bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void LateUpdate() {
        Vector3 viewPos = _player1.transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + p1ObjectWidth, screenBounds.x - p1ObjectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + p1ObjectHeight, screenBounds.y - p1ObjectHeight);
        _player1.transform.position = viewPos;

        viewPos = _player2.transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + p2ObjectWidth, screenBounds.x - p2ObjectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + p2ObjectHeight, screenBounds.y - p2ObjectHeight);
        _player2.transform.position = viewPos;
    }
}
