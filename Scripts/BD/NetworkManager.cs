using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public void CreaterUser(string userName, string pass, Action<Response> response)
    {
        StartCoroutine(CO_CreateUser(userName, pass, response));
    }
    private IEnumerator CO_CreateUser(string userName, string pass, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("userName", userName);
        form.AddField("pass", pass);
        WWW w = new WWW("http://localhost/Game/CreateUser.php", form);
        yield return w;
        //Debug.log(w.text);
        response(JsonUtility.FromJson<Response>(w.text));

        /*WWW myWWW = new WWW("http", form);   // UTF-8 encoded json file on the server
        yield return myWWW;
        string jsonData = "";
        if (string.IsNullOrEmpty(myWWW.error))
        {
            jsonData = System.Text.Encoding.UTF8.GetString(myWWW.bytes, 3, myWWW.bytes.Length - 3);  // Skip thr first 3 bytes (i.e. the UTF8 BOM)
            JSONObject json = new JSONObject(jsonData);
        }*/
    }
    public void CheckUser(string userName, string pass, Action<Response> response)
    {
        StartCoroutine(CO_CheckUser(userName, pass, response));
    }
    private IEnumerator CO_CheckUser(string userName, string pass, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("userName", userName);
        form.AddField("pass", pass);
        WWW w = new WWW("http://localhost/Game/CheckUser.php", form);
        yield return w;
        //Debug.Log(w.text);
        response(JsonUtility.FromJson<Response>(w.text));
    }


    public void UpdatePuntaje(string userName, string puntaje, Action<Response> response)
    {
        Debug.Log("puntaje");
        StartCoroutine(CO_UpdatePuntaje(userName, puntaje, response));
    }
    private IEnumerator CO_UpdatePuntaje(string userName, string puntaje, Action<Response> response)
    {
        Debug.Log(userName +" " + puntaje + " ");
        WWWForm form = new WWWForm();
        form.AddField("userName", userName);
        form.AddField("puntaje", puntaje);
        WWW w = new WWW("http://localhost/Game/UptadeInsertPuntaje.php", form);
        yield return w;
        Debug.Log(w.text);
        response(JsonUtility.FromJson<Response>(w.text));
    }
}
      
[Serializable]
    public class Response
{
    public bool done = false;
    public string message = "";

}
