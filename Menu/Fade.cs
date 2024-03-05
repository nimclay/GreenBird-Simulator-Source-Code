using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUIGroup;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    [SerializeField] private bool StartOut = false;
    [SerializeField] private float timer = 3;

    [SerializeField] private float fadeSpeed = 0.5f;

    [SerializeField] private bool Controllable;

    public Image arrow;
    public Sprite up;
    public Sprite Down;
    public Color color;
    public Color color2;

    public bool occilating;
    public bool hurtBar;

    public movement PlayerMoveCode;
    // Start is called before the first frame update
    void Start()
    {
        if(StartOut){
            StartCoroutine(StartFadeOut());
        }
    }
    public void ShowUI(){
        fadeIn= true;
        fadeOut = false;
    }
    public void hideUI(){
        fadeOut = true;
        fadeIn=false;
    }

    void Update(){
        if(Controllable){
            if(!fadeIn&&!fadeOut){
            if(Input.GetKeyDown(KeyCode.DownArrow)||Input.GetAxis("NumPad Y")<0){
                if(PlayerMoveCode.moveAble){
                ShowUI();
                arrow.sprite = up;
                arrow.color = color;}
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow)||Input.GetAxis("NumPad Y")>0){
                if(PlayerMoveCode.moveAble){
                hideUI();
                arrow.sprite = Down;
                arrow.color = color2;}
            }}
        }
        if(hurtBar){
            if(occilating){
                
                if(fadeIn){
                    if(myUIGroup.alpha < 1){
                        myUIGroup.alpha += Time.deltaTime*fadeSpeed;
                        if(myUIGroup.alpha>=1){
                            fadeIn = false;
                            hideUI();
                        }
                    }
                }
                if(fadeOut){
                    if(myUIGroup.alpha >= 0){
                        myUIGroup.alpha -= Time.deltaTime*fadeSpeed;
                        if(myUIGroup.alpha==0){
                            fadeOut = false;
                            ShowUI();
                        }
                    }
                }
                if(!fadeIn&&!fadeOut){
                    hideUI();
                }
            }
            else{
                fadeIn = false;
                hideUI();
                if(fadeOut){
                    if(myUIGroup.alpha >= 0){
                        myUIGroup.alpha -= Time.deltaTime*fadeSpeed;
                        if(myUIGroup.alpha==0){
                            fadeOut = false;
                        }
                    }
                }
            }
        }
        else{
        if(fadeIn){
            if(myUIGroup.alpha < 1){
                myUIGroup.alpha += Time.deltaTime*fadeSpeed;
                if(myUIGroup.alpha>=1){
                    fadeIn = false;
                }
            }
            else if(myUIGroup.alpha==1){
                fadeIn=false;
            }
        }
        if(fadeOut){
            if(myUIGroup.alpha > 0){
                myUIGroup.alpha -= Time.deltaTime*fadeSpeed;
                if(myUIGroup.alpha==0){
                    fadeOut = false;
                }
            }
            else if(myUIGroup.alpha==0){
                fadeOut=false;
            }
        }}
    }

    IEnumerator StartFadeOut(){
        yield return new WaitForSeconds(timer);
        hideUI();
    }
}
