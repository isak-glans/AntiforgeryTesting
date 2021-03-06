﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Integration.Tests.Helpers
{
    public class PostRequestHelper
    {
        public static HttpRequestMessage Create(String path, Dictionary<string, string> formPostBodyData)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, path)
            {
                Content = new FormUrlEncodedContent(ToFormPostData(formPostBodyData))
            };
            return httpRequestMessage;
        }

        public static List<KeyValuePair<string, string>> ToFormPostData(Dictionary<string, string> formPostBodyData)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            formPostBodyData.Keys.ToList().ForEach(key =>
            {
                result.Add(new KeyValuePair<string, string>(key, formPostBodyData[key]));
            });
            return result;
        }

        public static HttpRequestMessage CreateWithCookiesFromResponse(string path, Dictionary<string, string> formPostBodyData,
            HttpResponseMessage response)
        {
            var httpRequestMessage = Create(path, formPostBodyData);
            return CookiesHelper.CopyCookiesFromResponse(httpRequestMessage, response);
        }
    }
}
