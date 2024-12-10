/*
# Copyright(C) 2010-2024 Viacheslav Makoveichuk (email: learn.fractal@gmail.com, skype: vyacheslavm81)
# This file is part of Fractal Platform.
#
# Fractal Platform is free software : you can redistribute it and / or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
#
# Fractal Platform is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace FractalPlatform.Common.Clients
{
    public class RESTClient
    {
        private ILog _logger;

        public RESTClient(ILog logger)
        {
            _logger = logger;
        }

        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(15);

        public AuthenticationHeaderValue Authorization { get; set; }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();

            client.Timeout = Timeout;

            if (Authorization != null)
            {
                client.DefaultRequestHeaders.Authorization = Authorization;
            }

            return client;
        }

        private void Log(string type, string url, string response)
        {
            if (_logger != null)
            {
                _logger.Log($"[{DateTime.Now}] REST: {type} =>\r\nRequest: {url}\r\nResponse:\r\n{response}");
            }
        }

        public string Get(string url)
        {
            using (var client = CreateHttpClient())
            {
                var response = client.GetStringAsync(url).Result;

                Log("GET", url, response);

                return response;
            }
        }

        public byte[] GetBytes(string url)
        {
            using (var client = CreateHttpClient())
            {
                var response = client.GetByteArrayAsync(url).Result;

                Log("GET", url, "Bytes: " + response?.Length.ToString());

                return response;
            }
        }

        public string Post(string url,
                           string content,
                           string contentType = "application/json")
        {
            using (var client = CreateHttpClient())
            {
                using (var stringContent = new StringContent(content, Encoding.UTF8, contentType))
                {
                    using (var result = client.PostAsync(url, stringContent).Result)
                    {
                        result.EnsureSuccessStatusCode();

                        var response = result.Content.ReadAsStringAsync().Result;

                        Log("POST", url, response);

                        return response;
                    }
                }
            }
        }

        public byte[] GetByteArray(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetByteArrayAsync(url).Result;

                Log("GET", url, "Bytes: " + response?.Length.ToString());

                return response;
            }
        }

        public string Post(string url, params string[] keys)
        {
            var formData = new List<KeyValuePair<string, string>>();

            for (int i = 0; i < keys.Length; i += 2)
            {
                formData.Add(new KeyValuePair<string, string>(keys[i], keys[i + 1]));
            }

            var response = Post(url, formData);

            Log("POST", url, response);

            return response;
        }

        public string Post(string url, IEnumerable<KeyValuePair<string, string>> formData)
        {
            using (var client = CreateHttpClient())
            {
                using (var formContent = new FormUrlEncodedContent(formData))
                {
                    using (var result = client.PostAsync(url, formContent).Result)
                    {
                        result.EnsureSuccessStatusCode();

                        var response = result.Content.ReadAsStringAsync().Result;

                        Log("POST", url, response);

                        return response;
                    }
                }
            }
        }
    }
}