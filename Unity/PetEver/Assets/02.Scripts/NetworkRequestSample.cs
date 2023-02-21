using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkRequestSample : MonoBehaviour
{
    string url = "http://localhost:3000/petever/";

    public enum APIType
    {
        Login,
        Logout,
        GetDogInfo
    }
    void Start()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            // 인터넷 연결이 안된경우
            ErrorCheck(-1000); // 인터넷 연결 에러
        }
        else
        {
            // 인터넷 연결이 된 경우
            StartCoroutine(FindAPIby(APIType.GetDogInfo));
        }
    }
    IEnumerator FindAPIby(APIType _type)
    {
        switch (_type)
        {
            case APIType.GetDogInfo:
                yield return StartCoroutine(API_GetDogInfo());
                break;
        }
        yield return null;
    }

    IEnumerator API_GetDogInfo()
    {
        UnityWebRequest request;
        using (request = UnityWebRequest.Get(url + "getDogInfo"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            //request.SetRequestHeader("Authorization", "Bearer " + token);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
                if (request.responseCode != 200)
                {
                    ErrorCheck(-(int)request.responseCode, "API_GetDogInfo");
                }
                else
                {
                    Debug.Log(request.downloadHandler.text);
                }
            }
        }
    }

    #region Occur Error
    int ErrorCheck(int _code)
    {
        if (_code > 0) return 0;
        else if (_code == -1000) Debug.LogError(_code + ", Internet Connect Error");
        else if (_code == -1001) Debug.LogError(_code + ", Occur token type Error");
        else if (_code == -1002) Debug.LogError(_code + ", Category type Error");
        else if (_code == -1003) Debug.LogError(_code + ", Item type Error");
        else Debug.LogError(_code + ", Undefined Error");
        return _code;
    }

    int ErrorCheck(int _code, string _funcName)
    {
        if (_code > 0) return 0;
        else if (_code == -400) Debug.LogError(_code + ", Invalid request in " + _funcName);
        else if (_code == -401) Debug.LogError(_code + ", Unauthorized in " + _funcName);
        else if (_code == -404) Debug.LogError(_code + ", not found in " + _funcName);
        else if (_code == -500) Debug.LogError(_code + ", Internal Server Error in " + _funcName);
        else Debug.LogError(_code + ", Undefined Error");
        return _code;
    }
    #endregion
}
