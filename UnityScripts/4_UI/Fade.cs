using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour

{
    private Canvas canvas;
    private int thisSortingOrder = 128;                         // Canvas상 우선순위

    public float animTime = 2f;                                 // Fade 애니메이션 재생 시간 (단위:초).  
    [SerializeField] private Image[] fadeImages;                // UGUI의 Image컴포넌트 참조 변수.  
    public int fadeIndex = 0;                                   // 어떤 이미지를 쓸 것인가

    private float start = 1f;                                   // Mathf.Lerp 메소드의 첫번째 값.  
    private float end = 0f;                                     // Mathf.Lerp 메소드의 두번째 값.  
    private float time = 0f;                                    // Mathf.Lerp 메소드의 시간 값.  


    public bool stopIn = false;                                 //시작할때 페이드인. 반대는 true.
    public bool stopOut = true;

    void Awake()
    {
                                                                // Image 컴포넌트를 검색해서 참조 변수 값 설정.  
        fadeImages = GetComponentsInChildren<Image>();
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        for (int i = 0; i < fadeImages.Length; i++)
        {
            if (i != fadeIndex) fadeImages[i].gameObject.SetActive(false); // 선택한 image 빼고 지우기
        }
    }

    private void Update()
    {

        if (stopIn == false && time <= 2)                       // FadeIn 애니메이션 재생.  
        {
            PlayFadeIn();
        }
        if (stopOut == false && time <= 2)                      // FadeOut 애니메이션 재생.  
        {
            PlayFadeOut();
        }
        if (time >= animTime && stopIn == false)                // FadeIn 마친 후 우선순위 최하위로 설정.
        {
            stopIn = true;
            time = 0;
            canvas.sortingOrder = 0;
        }
        if (time >= animTime && stopOut == false)               // FadeOut 종료
        {
            stopIn = true;
            stopOut = true;
            time = 0;
        }

    }

    // 흰색->투명
    private void PlayFadeIn()
    {
        // 경과 시간 계산.  
        // 2초(animTime)동안 재생될 수 있도록 animTime으로 나누기.  
        time += Time.deltaTime / animTime;

        // Image 컴포넌트의 색상 값 읽어오기.  
        Color color = fadeImages[fadeIndex].color;
        // 알파 값 계산.  
        color.a = Mathf.Lerp(start, end, time);
        // 계산한 알파 값 다시 설정.  
        fadeImages[fadeIndex].color = color;
        // Debug.Log(time);
    }

    // 투명->흰색
    private void PlayFadeOut()
    {
        canvas.sortingOrder = thisSortingOrder;
        // 경과 시간 계산.  
        // 2초(animTime)동안 재생될 수 있도록 animTime으로 나누기.  
        time += Time.deltaTime / animTime;

        // Image 컴포넌트의 색상 값 읽어오기.  
        Color color = fadeImages[fadeIndex].color;
        // 알파 값 계산.  
        color.a = Mathf.Lerp(end, start, time);  //FadeIn과는 달리 start, end가 반대다.
        // 계산한 알파 값 다시 설정.  
        fadeImages[fadeIndex].color = color;
    }

    public void FadeImage(int _idx, bool _inAndOut) // 몇 번째 인덱스의 이미지를 페이드 인/아웃 할 것인지
    {
        fadeImages[fadeIndex].gameObject.SetActive(false);
        fadeImages[_idx].gameObject.SetActive(true);
        fadeIndex = _idx;

        if (_inAndOut)
        {
            stopIn = false;
            stopOut = true;
        }
        else
        {
            stopIn = true;
            stopOut = false;
        }
    }
}