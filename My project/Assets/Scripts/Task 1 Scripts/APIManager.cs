using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    private const string apiUrl = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

    public IEnumerator FetchClientsData(System.Action<ClientListData> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                ClientListData clientsData = JsonUtility.FromJson<ClientListData>(json);
                callback?.Invoke(clientsData);
            }
            else
            {
                Debug.LogError("API request failed: " + request.error);
            }
        }
    }
}
