using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtboxes : MonoBehaviour
{
    private BoxCollider2D[] _hurtboxes;

    public void ResizeForJump() {
        foreach (BoxCollider2D box in _hurtboxes) {
            box.size = new Vector2(0.8f, 1.0f);
            box.offset = Vector2.zero;
        }
    }

    public void ResetDefaultBoxSettings() {
        _hurtboxes[0].size = new Vector2(0.78f, 0.38f);
        _hurtboxes[0].offset = new Vector2(-0.04f, -0.31f);

        _hurtboxes[1].size = new Vector2(0.7f, 0.67f);
        _hurtboxes[1].offset = new Vector2(0, 0.22f);

        _hurtboxes[2].size = new Vector2(0.25f, 0.25f);
        _hurtboxes[2].offset = new Vector2(0.17f, 0.58f);
    }

    // Start is called before the first frame update
    void Start()
    {
        _hurtboxes = GetComponents<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
