using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum TextState//해당 퀘스트 진행에 따른 상태
{
    FirstLine = 0,
    SecondLine,
    ThirdLine,
    fourLine,
    fiveLine,
    sixLine,
    sevenLine
}

public class TextPrintPlayer : MonoBehaviour
{
    TextState T_State;
    public GameObject textImage_Player;
    public Image HowToPlay;
    public Sprite[] HowToPlaySprite;
    string S_text;
    Text text_player;
    Transform PlayerPos;
    bool text_end;
    int ContextCount = 0;
    int lineCount = 0;
    int SpaceCount = 0;
    bool isDialouge = true;
    bool isNext = true;

    bool textTriggerOn;
    [SerializeField] Dialouge[] dialouge;
    float textTimer;
    // throw new System.NotImplementedException();
    private void Start()
    {
        T_State = TextState.FirstLine;
        dialouge = GetComponent<TextEvent>().getDialouge();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        interactionBase();
        PlayerPos.GetComponent<Player_Manager>().enabled = false;
        PlayerPos.GetComponent<Player_Move>().enabled = false;

    }
    private void Update()
    {
        if (textTriggerOn)
            textTimer += Time.deltaTime;
        if (textTimer >= 1.0f)
        {
            textImage_Player.gameObject.SetActive(false);
            textTimer = 0.0f;
            textTriggerOn = false;
        }
        
        if (isNext)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (lineCount == 13)
                    textImage_Player.gameObject.SetActive(false);
                isNext = false;
                interactionBase();
            }
        }
    }
    void interactionBase()
    {
        text_end = true;
        text_player = textImage_Player.GetComponentInChildren<Text>();
        StartCoroutine(TextPrint());
        switch (T_State)
        {
            case TextState.FirstLine:
                if (dialouge[lineCount].misson_state == "0")
                {
                    lineCount++;
                    break;
                }
                else
                    T_State = TextState.SecondLine;
                break;
            case TextState.SecondLine:
                if (dialouge[lineCount].misson_state == "1")
                {
                    ++SpaceCount;
                    if (SpaceCount >= 1)
                    {
                        HowToPlay.gameObject.SetActive(true);
                        HowToPlay.sprite = HowToPlaySprite[0];
                        T_State = TextState.ThirdLine;
                        SpaceCount = 0;
                    }
                    ++lineCount;
                    break;
                }
                break;
            case TextState.ThirdLine:
                if (dialouge[lineCount].misson_state == "2")
                {
                    HowToPlay.gameObject.SetActive(false);
                    lineCount++;
                    SpaceCount = 0;
                    break;
                }
                else
                    T_State = TextState.fourLine;
                break;
            case TextState.fourLine:
                if (dialouge[lineCount].misson_state == "3")
                {
                    ++SpaceCount;
                    if (SpaceCount ==1)
                    {
                        HowToPlay.gameObject.SetActive(true);
                        HowToPlay.sprite = HowToPlaySprite[1];
                    }
                    if (SpaceCount >= 2)
                    {
                        HowToPlay.gameObject.SetActive(false);
                        textImage_Player.gameObject.SetActive(false);
                        ++lineCount;
                        T_State = TextState.fiveLine;
                        PlayerPos.GetComponent<Player_Manager>().enabled = true;
                        PlayerPos.GetComponent<Player_Move>().enabled = true;
                        SpaceCount = 0;
                    }

                }
                break;
            case TextState.fiveLine:
                if (dialouge[lineCount].misson_state == "4")
                {
                    if (textTriggerOn)
                    {
                        ++SpaceCount;
                    }
                    if (SpaceCount == 1)
                    {
                        textImage_Player.gameObject.SetActive(true);
                        ++lineCount;
                        T_State = TextState.sixLine;
                        SpaceCount = 0;
                    }
                }
                break;
            case TextState.sixLine:
                if (dialouge[lineCount].misson_state == "5")
                {
                    if (textTriggerOn)
                    {
                        ++SpaceCount;
                        textTriggerOn = false;
                        textImage_Player.gameObject.SetActive(true);
                    }
                    if (SpaceCount == 1)
                        ++lineCount;
                   
                    Debug.Log(lineCount);
                }
                else
                {
                    textImage_Player.gameObject.SetActive(false);
                    T_State = TextState.sevenLine;
                }
                break;
            default:
                break;
        }
    }
    IEnumerator TextPrint()//텍스트 출력
    {
        S_text = dialouge[lineCount].contexts[ContextCount];
        text_player.text = S_text.ToString();
        isNext = true;
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "TextTrigger")
        {
            other.transform.name = "TextTriggerOff";
            textTriggerOn = true;
            interactionBase();
        }
    }
}

