using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JimJam.Interface
{
    public class Juicer : MonoBehaviour
    {
        [SerializeField] private bool useSfx = true;
        [SerializeField] private bool useCursors = true;
        [SerializeField] private AudioClip[] buttonClickSfx;
        [SerializeField] private Texture2D[] cursors;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            var buttons = FindObjectsOfType<JuicyButton>();
            foreach (var button in buttons)
            {
                if (useCursors)
                {
                    button.onHover.AddListener(delegate { ChangeCursor(1);});
                    button.onExit.AddListener(delegate { ChangeCursor(0);});    
                }
                if (useSfx)
                {
                    button.onDown.AddListener(delegate { ButtonClick(0);});
                    button.onClick.AddListener(delegate { ButtonClick(1);});
                }
            }
        }

        public void ChangeCursor(int state)
        {
            Cursor.SetCursor(cursors[state],Vector2.zero, CursorMode.Auto);    
        }

        public void ButtonClick(int state)
        {
            if (buttonClickSfx[state] == null)
            {
                Debug.LogWarning("Missing button click sfx");
            }
            _audioSource.PlayOneShot(buttonClickSfx[state]);
        }
    }
}

