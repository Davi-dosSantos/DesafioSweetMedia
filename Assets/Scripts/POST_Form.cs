using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class POST_Form : MonoBehaviour
{
    public Text displayTextName;
    public Text displayTextEmail;
    public Text displayTextBirthday;
    void Start()
    {
        StartCoroutine(Upload());
    }

    public void SendForm()
    {
        string name = displayTextName.text;
        string email =  displayTextEmail.text;
        string birthday = displayTextBirthday.text;
        StartCoroutine(Upload(name , email, birthday));
    }

    IEnumerator Upload(string name, string email, string birthday)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        UnityWebRequest www = UnityWebRequest.Post("https://www.my-server.com/myform", formData);
        yield return www.SendWebRequest();
    
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        } 
        else
        {
            Debug.Log("Form upload complete!");
        } 
    
    }
}

