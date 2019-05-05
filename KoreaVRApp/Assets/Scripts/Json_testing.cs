using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class UserInfo
{
	public string EventCode { get; set; }
	public string Message { get; set; }
	public string auth_token { get; set; }
}
	
public class Json_testing : MonoBehaviour
{


	UserInfo testing1 = new UserInfo()
	{
		EventCode = "Login",
		Message = "Success",
		auth_token = "464564865461358654"
	};

	public string jsonString,parse;

	string GetToken(UserInfo user){
		string token;

		if (user != null) {
			token = JsonConvert.SerializeObject (user.auth_token);
			return token;
		} else {
			return token = string.Empty;
		}
	}

    void Start()
    {
//		jsonString = JsonConvert.SerializeObject(testing1);
//		parse = GetToken(testing1);

		var response = JsonConvert.DeserializeObject<Response> (jsonString);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Response 
{
	public int event_code;
	public string message;
	public string auth_token;
//	public VideoInfo video_info;
}
