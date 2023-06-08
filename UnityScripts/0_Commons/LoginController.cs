using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoginController : MonoBehaviour
{

    
    public class LoginDTO // 로그인 API 통신 시, 결과값을 담을 Response DTO
    {
        public long id;
        public string nickname;
        public int amount;
    }


    public TMP_InputField inputID;
    public TMP_InputField inputPW;

    public GameObject loginPanel, signupPanel, notificationPanel; // 로그인, 회원가입, 알림창


    public TMP_Text notification_Title_Text, notification_Message_Text;
    public TMP_Text loginEmail, loginPassword, signupEmail, signupPassword, signupConfirmPassword, signupUserName;

    void Start()
    {
        OpenLoginPanel(); // 시작 시 로그인 패널을 연다
    }

    public void OpenLoginPanel() // 로그인패널 열기
    {
        Debug.Log("OpenLoginPanel() method called");
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
        notificationPanel.SetActive(false);
    }
    public void OpenSignUpPanel() // 회원가입 패널 열기
    {
        Debug.Log("OpenSignUpPanel() method called");
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
        notificationPanel.SetActive(false);
    }

    // 알림창 띄우기
    private void showNotificationMessage(string title, string message)
    {
        notification_Title_Text.text = "" + title;
        notification_Message_Text.text = "" + message;

        notificationPanel.SetActive(true);
    }
    public void CloseNotification_Panel()
    {
        notification_Title_Text.text = "";
        notification_Message_Text.text = "";
        notificationPanel.SetActive(false);

    }
        public void LoginUser() // 로그인하는 메서드
    {
        // Login-1. 널체크
        if(string.IsNullOrEmpty(loginEmail.text.Trim()) && string.IsNullOrEmpty(loginPassword.text.Trim())) // 이메일, 패스워드 비어있는지 확인
        {
            showNotificationMessage("Error", "빈 값이 있는지 확인해주세요"); // 에러메세지 
            return;
        }
        //Debug.Log("로그인 진행");
        //Debug.Log(loginEmail.text + " : " + loginPassword.text);

        // Login-2. API url, data 설정
        string url = MyAPIClient.BACKEND_URL + "/api/main/users/login"; // 로그인으로 쏠 URL
        string data = "{\"email\":\"" + loginEmail.text.Replace("\u200B", "") // body 에 담아서 보낼 data
                                      + "\", \"password\":\"" + loginPassword.text.Replace("\u200B", "") + "\"}";

        // Login-3. API 전송 및 후처리
        StartCoroutine(MyAPIClient.Instance.PostRequest<string>(url, data, loginSuccess, loginError)); 
    }

    private void loginSuccess(string result)
    {
        Debug.Log("로그인 성공: " + result);
        LoginDTO loginDto = JsonUtility.FromJson<LoginDTO>(result);
        UserInfo.instance.userId = loginDto.id;
        UserInfo.instance.userName = loginDto.nickname;
        UserInfo.instance.amount = loginDto.amount;

        Debug.Log("유저정보 저장 = " + UserInfo.instance.userId + " , " + UserInfo.instance.userName + " , "
        + UserInfo.instance.amount);

        // Main_ingame 으로 신 이동
        SceneManager.LoadScene("HaeSuk_Lobby");
    }

    // 회원가입 실패
    private void loginError(string result)
    {
        Debug.Log("로그인 에러");
        showNotificationMessage("Error :", "로그인 실패! 다시 확인해주세요");
    }





    //  회원가입 버튼 클릭
    public void SignUpUser() // 회원가입 하기
    {
        // Signup-1. null 체크
        if(string.IsNullOrEmpty(signupEmail.text.Trim()) && string.IsNullOrEmpty(signupPassword.text.Trim())&& string.IsNullOrEmpty(signupConfirmPassword.text.Trim()) && string.IsNullOrEmpty(signupUserName.text.Trim()))
        {
            showNotificationMessage("Error", "빈 항목이 있습니다. 다시 확인해주세요!!");
            return;
        }
        // Signup-2. 패스워드 일치 확인

        if (!signupConfirmPassword.text.Trim().Equals(signupPassword.text.Trim()))
        {
            showNotificationMessage("Error", "비밀번호가 일치하지 않습니다!");
            return;
        }

        // Signup-3. url, data 설정

        string url = MyAPIClient.BACKEND_URL + "/api/main/users/check/duplicate"; // 여기까진 잘 간다.
        string data = "{\"info\":\"" + signupUserName.text.Replace("\u200B", "")+ "\", \"type\":\"" + "NICKNAME" + "\"}";
        Debug.Log(url);
        //Debug.Log(data);

        // Signup-4. API 전송 및 후처리
        StartCoroutine(MyAPIClient.Instance.PostRequest<string>(url, data, dupleNicknameSuccess, dupleNicknameError)); // geturl 로 전송   
    }

    // Signup-8. 유저생성
    void CreateUser(string email, string password, string nickname)
    {
        // url 및 data 설정
        string url = MyAPIClient.BACKEND_URL + "/api/main/users/register";
        string data = "{\"email\":\"" + email + "\", \"password\":\"" + password + "\", \"nickname\":\"" + nickname + "\"}";

        // 서버에 해당 값을 전송
        StartCoroutine(MyAPIClient.Instance.PostRequest<string>(url, data, signInSuccess, signInError));
    }

    // 1. 로그인하기

    bool isSigned = false;

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Return)) // 엔터를 눌렀을 때 - 로그인
        {
            LoginUser();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (EventSystem.current.currentSelectedGameObject == inputID.gameObject)
            {

                inputPW.Select();
                inputPW.ActivateInputField();
            }
            else if (EventSystem.current.currentSelectedGameObject == inputPW.gameObject)
            {

                inputID.Select();
                inputID.ActivateInputField();
            }

        }
    }






    // API 통신이 성공하거나 실패했을 때 사용하는 메서드

    // Signup-9. 회원가입 성공
    private void signInSuccess(string result)
    {
        Debug.Log("Sign up successful: " + result);
        showNotificationMessage("id생성완료", "회원가입성공! 이메일 확인을 진행해주세요.");
        
        signupEmail.text = "";
        signupUserName.text = "";
        signupPassword.text = "";
        signupConfirmPassword.text = "";

    }

    // Signup-10. 회원가입 실패
    private void signInError(string result)
    {
        Debug.LogError("Sign up error: ");
        showNotificationMessage("회원가입실패", "회원가입에 실패하였습니다. 통신여부를 확인해주세요.");
    }

    // Signup-5. 닉네임 중복검사 
    private void dupleNicknameSuccess(string result)
    {
        //Debug.Log("nickname duple check success: " + result);

        string url = MyAPIClient.BACKEND_URL + "/api/main/users/check/duplicate"; 
        string data = "{\"info\":\"" + signupEmail.text.Replace("\u200B", "") + "\", \"type\":\"" + "EMAIL" + "\"}";
        StartCoroutine(MyAPIClient.Instance.PostRequest<string>(url, data, dupleEmailSuccess, dupleEmailError)); // geturl 로 전송
    }

    private void dupleNicknameError(string result)
    {
        Debug.LogError("duple check error: " + result);
        showNotificationMessage("Error :", " 닉네임이 중복되었습니다");
    }

    // Signup-7. 이메일 중복검사 성공시 
    private void dupleEmailSuccess(string result)
    {
        //Debug.Log("email check success: " + result);

        // 유저 생성
        CreateUser(signupEmail.text.Replace("\u200B", ""), signupPassword.text.Replace("\u200B", ""), signupUserName.text.Replace("\u200B", ""));

    }

    private void dupleEmailError(string result)
    {
        Debug.LogError("duple check error: " + result);
        showNotificationMessage("Error :", "이메일이 중복되었습니다");
    }




}