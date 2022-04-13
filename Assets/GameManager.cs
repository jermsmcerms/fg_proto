using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FightAI {
    public class GameManager : MonoBehaviour {
        [SerializeField] AudioSource menuMusic;

        private void Awake() {
            menuMusic = GetComponent<AudioSource>();
            menuMusic.loop = true;
            Application.targetFrameRate = 60;
        }

        private void Start() {
            menuMusic.Play();
        }

        // Update is called once per frame
        void Update() {
        
        }
    }
}
