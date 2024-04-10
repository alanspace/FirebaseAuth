using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System.Net.Sockets;
using TMPro;
using Firebase.Extensions;

public class ThirdPartyLogin : MonoBehaviour
{
    //https://firebase.google.com/docs/auth/unity/facebook-login
    //check out other providers




    public TextMeshProUGUI logTxt;
    public GameObject loginBtn, SucessPopup;



    public void FacebookLoign() {
        string accessToken = "";

        if (string.IsNullOrEmpty(accessToken))
        {
            showLogMsg("Missing facebook access token");
            return;
        }

        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        Credential credential =FacebookAuthProvider.GetCredential(accessToken);
        auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            FacebookLoginSuccess(result.User.UserId);
        });
    }

    void FacebookLoginSuccess(string id) {
        
            loginBtn.SetActive(false);
            SucessPopup.SetActive(true);
            SucessPopup.transform.Find("Desc").GetComponent<TextMeshProUGUI>().text = "Id: " + id;
        
    }


    #region extra
    void showLogMsg(string msg)
    {
        logTxt.text = msg;
        logTxt.GetComponent<Animation>().Play("textFadeout");
    }
    #endregion
}
