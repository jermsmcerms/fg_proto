using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Hurtboxes : MonoBehaviour
{
    [SerializeField] private GameObject[] _hurtboxes;

    private Dictionary<string, Sprite> _hurtboxSprites;

    private bool _performingAttack;
    private int _attackCountdown;

    public void ResizeForJump() {
        foreach (GameObject go in _hurtboxes) {
            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
            if (go.name == "Chest") {
                if (_hurtboxSprites.ContainsKey("chest_air")) {
                    spriteRenderer.sprite = _hurtboxSprites["chest_air"];
                    go.transform.localPosition = Vector3.zero;
                }

                BoxCollider2D boxCollider = go.GetComponent<BoxCollider2D>();
                boxCollider.offset = Vector2.zero;
                boxCollider.size = new Vector2(1.64f, 2.36f);
            } else {
                go.SetActive(false);
            }
        }
    }

    public void ResetDefaultBoxSettings() {
        foreach (GameObject go in _hurtboxes) {
            go.SetActive(true);
            if(go.name == "Feet") {
                go.transform.localPosition = new Vector3(-0.15f, -0.73f, 0.0f);
                SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = _hurtboxSprites["feet"];

                BoxCollider2D boxCollider2D = go.GetComponent<BoxCollider2D>();
                boxCollider2D.size = new Vector2(1.69f, 1.09f);
            }

            if(go.name == "Chest") {
                go.transform.localPosition = new Vector3(-0.07f, 0.64f, 0.0f);
                
                SpriteRenderer spriteRenderer = go.GetComponent <SpriteRenderer>();
                spriteRenderer.sprite = _hurtboxSprites["chest_ground"];

                BoxCollider2D boxCollider2D = go.GetComponent<BoxCollider2D>();
                boxCollider2D.size = new Vector2(1.55f, 1.75f);
            }

            if(go.name == "Head") {
                go.transform.localPosition = new Vector3(0.2f, 1.55f, 0.0f);
                SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = _hurtboxSprites["head"];

                BoxCollider2D boxCollider = go.GetComponent<BoxCollider2D>();
                boxCollider.size = new Vector2(0.65f, 0.65f);
            }
        }
    }

    public bool PerfomingAttack() {
        return _attackCountdown > 0;
    }

    public void StartAttack(int countdown) {
        _attackCountdown = countdown;
        _performingAttack = true;
    }

    public void UpdateHurtboxes(bool isGrounded, bool isCrouching) {
        if(isGrounded) {
            if (isCrouching) {
                GameObject feetHurtbox = _hurtboxes[0];
                feetHurtbox.transform.localPosition = new Vector3(-0.15f, -0.81f, 0.0f);
                SpriteRenderer feetHurtboxSprite = feetHurtbox.GetComponent<SpriteRenderer>();
                feetHurtboxSprite.sprite = _hurtboxSprites["feet_crouch"];

                BoxCollider2D feetCollider = feetHurtbox.GetComponent<BoxCollider2D>();
                feetCollider.size = new Vector2(1.86f, 0.93f);

                GameObject chestHurtbox = _hurtboxes[1];
                chestHurtbox.transform.localPosition = new Vector3(-0.07f, .17f, 0.0f);
                SpriteRenderer chestHurtboxSprite = chestHurtbox.GetComponent<SpriteRenderer>();
                chestHurtboxSprite.sprite = _hurtboxSprites["chest_crouch"];

                BoxCollider2D chestCollider = chestHurtbox.GetComponent<BoxCollider2D>();
                chestCollider.size = new Vector2(1.6f, 1.1f);

                GameObject headHurtbox= _hurtboxes[2];
                headHurtbox.transform.localPosition = new Vector3(0.2f, 0.81f, 0.0f);
            } else {
                ResetDefaultBoxSettings();
            }
        }
        
        if(_performingAttack) {
            if(_attackCountdown > 0) {
                if(_attackCountdown == 12) {
                    _hurtboxes[2].transform.localPosition = new Vector3(1.2f, _hurtboxes[2].transform.localPosition.y, 0.0f);
                    SpriteRenderer headHurtbox = _hurtboxes[2].GetComponent<SpriteRenderer>();
                    headHurtbox.sprite = _hurtboxSprites["jab_head_1"];

                    BoxCollider2D headCollider2D = _hurtboxes[2].GetComponent<BoxCollider2D>();
                    headCollider2D.size = new Vector2(2.62f, headCollider2D.size.y);
                }
                _attackCountdown--;
            } else {
                _performingAttack = false;
                ResetDefaultBoxSettings();
            }
        }
    }

    // Awake is called when the script instance is being loaded

    private void Awake() { 
        _hurtboxSprites = new Dictionary<string, Sprite> ();
        foreach(GameObject go in _hurtboxes) {
            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer> ();
            switch(spriteRenderer.name) {
                case "Feet":
                    Sprite feetGroundSprite = Resources.Load<Sprite>("Sprites/hurtbox_feet_ground");
                    _hurtboxSprites.Add("feet", feetGroundSprite);

                    Sprite feetCrouchingSprite = Resources.Load<Sprite>("Sprites/feet_crouch");
                    _hurtboxSprites.Add("feet_crouch", feetCrouchingSprite);
                    break;

                case "Chest":
                    Sprite chestAirSprite = Resources.Load<Sprite>("Sprites/hurtbox_body_air");
                    _hurtboxSprites.Add("chest_air", chestAirSprite);

                    Sprite chestGroundSprite = Resources.Load<Sprite>("Sprites/hurtbox_chest_ground");
                    _hurtboxSprites.Add("chest_ground", chestGroundSprite);

                    Sprite chestCrouchingSprite = Resources.Load<Sprite>("Sprites/chest_crouch");
                    _hurtboxSprites.Add("chest_crouch", chestCrouchingSprite);
                    break;

                case "Head":
                    Sprite headGroundSprite = Resources.Load<Sprite>("Sprites/hurtbox_head_ground");
                    _hurtboxSprites.Add("head", headGroundSprite);
                    Sprite jabHeadPhaseOneSprite = Resources.Load<Sprite>("Sprites/hurtbox_jab_head_1");
                    _hurtboxSprites.Add("jab_head_1", jabHeadPhaseOneSprite);
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start() {        
        _performingAttack = false;
        _attackCountdown = 0;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
