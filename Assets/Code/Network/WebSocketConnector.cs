﻿using System;
using System.Collections.Generic;
using BestHTTP.Cookies;
using BestHTTP.SocketIO;

public class WebSocketConnector
{
	private const string URL = Config.SOCKET_URL;

	public Socket connect(string charId)
	{
		SocketOptions options = new SocketOptions();
		options.AdditionalQueryParams = new Dictionary<string, string>();
		List<Cookie> cookies = BestHTTP.Cookies.CookieJar.GetAll();
		foreach (Cookie cookie in cookies)
		{
			if (cookie.Name == "unicorn" && cookie.Domain == Config.DOMAIN)
			{
				options.AdditionalQueryParams.Add("unicorn", cookie.Value);
				break;
			}
		}
		options.AdditionalQueryParams.Add("id", charId);
		var manager = new SocketManager(new Uri(URL), options);
		manager.Encoder = new SimpleJsonEncoder();
		manager.Open();
		return manager.Socket;
	}
}
