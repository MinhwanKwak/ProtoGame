using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Aim : MonoBehaviour {
    private Vector3 screenCenter;
    public float targetingSpeed;
    public GameObject aimImage;
    int layerMask;
    public RaycastHit temp;
    public Sprite[] AimImageState;
	// Use this for initialization
	void Start () {
        screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        layerMask = 1 << LayerMask.NameToLayer("Default");
        aimImage.transform.position = screenCenter;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update () {
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out temp, Mathf.Infinity, layerMask)) // 카메라의 위치에서 카메라가 바라보는 정면으로 레이를 쏴서 충돌확인
        {
            if (temp.transform.tag == "Enemy")
            {
                aimImage.transform.localScale = new Vector3(Mathf.Lerp(aimImage.transform.localScale.x, 0.3f, Time.deltaTime* targetingSpeed), Mathf.Lerp(aimImage.transform.localScale.y, 0.3f, Time.deltaTime * targetingSpeed), 1.0f);
                if (aimImage.transform.localScale.x <= 0.3f)
                    aimImage.GetComponent<Image>().sprite = AimImageState[0];
            }
            else
            {
                aimImage.transform.localScale = new Vector3(Mathf.Lerp(aimImage.transform.localScale.x, 0.3f, Time.deltaTime* 5.0f), Mathf.Lerp(aimImage.transform.localScale.y, 0.3f, Time.deltaTime * 5.0f), 1.0f);
                aimImage.GetComponent<Image>().sprite = AimImageState[1];
                aimImage.GetComponent<Image>().color = Color.white;
            }

            Debug.DrawRay(transform.position, transform.forward * 200.0f, Color.cyan); // 이 레이는 앞서 선언한 디버그용 레이와 충돌점에서 교차한다
        }
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
    }

}
