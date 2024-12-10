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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace FractalPlatform.Common.Clients
{
	public class AIClient
	{
		private const string _apiKey = "";
		private const string _apiUrl = "https://api.openai.com/v1/chat/completions";
        private const string _apiImageUrl = "https://api.openai.com/v1/images/generations";

        private RESTClient _client;
		private ILog _logger;

		public AIClient(RESTClient client, ILog logger)
		{
			_client = client;
			_logger = logger;
		}

		private List<CodeBlock> GetCodeBlocks(string response)
		{
			var result = new List<CodeBlock>();

			var label = "```";

			var startIndex = 0;

			while (true)
			{
				var startCodeBlock = response.IndexOf(label, startIndex);

				if (startCodeBlock >= 0)
				{
					var endCodeBlock = response.IndexOf(label, startCodeBlock + label.Length);

					var text = response.Substring(startCodeBlock + label.Length, endCodeBlock - startCodeBlock - label.Length).Trim();

					string language;

					if (text.StartsWith("json"))
					{
						language = "json";

						text = text.Substring(4);
					}
					else if (text.StartsWith("html"))
					{
						language = "html";

						text = text.Substring(4);
					}
					else if (text.StartsWith("c#"))
					{
						language = "c#";

						text = text.Substring(2);
					}
					else
					{
						language = "Unknown";
					}

					result.Add(new CodeBlock
					{
						Language = language,
						Text = text.Trim()
					});

					startIndex = endCodeBlock + label.Length;
				}
				else
				{
					break;
				}
			}

			return result;
		}

		public AIResponse GenerateImage(string text,
								   string model = AIModel.DallE3,
								   int width = 1024,
								   int height = 1024)
		{
			return Generate(text,
							model,
							new AIImage 
							{ 
								Width = width,
								Height = height 
							});
		}

        public AIResponse Generate(string text,
								   string model = AIModel.Llama31,
								   AIImage image = null)
		{
			if (model.StartsWith("llama") ||
				model.StartsWith("llava"))
			{
				var url = "http://localhost:11434/api/generate";
				object question;

				if (image == null)
				{
					question = new
					{
						model = model,
						prompt = text,
						stream = false
					};
				}
				else
				{
					question = new
					{
						model = model,
						prompt = text,
						stream = false,
						images = new[] { image.Base64 }
					};
				}

				var json = JsonConvert.SerializeObject(question);

				var result = _client.Post(url, json);

				dynamic response = JsonConvert.DeserializeObject(result);

				return new AIResponse
				{
					Text = (string)response.response,
					CodeBlocks = GetCodeBlocks((string)response.response)
				};
			}
			else if (model.StartsWith("gpt"))
			{
				_client.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

				object requestBody;

				if (image == null)
				{
					requestBody = new
					{
						model = model,
						messages = new[]
						{
							new
							{
								role = "user",
								content = text
							}
						}
					};
				}
				else
				{
					requestBody = new
					{
						model = model,
						messages = new object[]
						{
							new
							{
								role = "user",
								content = new object [] {
									new {
										type = "text",
										text = text
									},
									new {
										type = "image_url",
										image_url = new
										{
											url = image.Url != null ?
												  image.Url :
												  $"data:image/{image.Type};base64,{image.Base64}"
										}
									}
								}
							}
						}
					};
				}

				var json = JsonConvert.SerializeObject(requestBody);

				var jsonResponse = _client.Post(_apiUrl, json);

				dynamic result = JsonConvert.DeserializeObject(jsonResponse);

				var content = (string)result.choices[0].message.content;

				return new AIResponse
				{
					Text = content,
					CodeBlocks = GetCodeBlocks(content)
				};
			}
			else if (model.StartsWith("dall"))
			{
				_client.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

				var requestBody = new
				{
					prompt = text,
					quality = "standard",
					n = 1,
					size = image != null ? $"{image.Width}x{image.Height}" : "1024x1024"
				};

				var json = JsonConvert.SerializeObject(requestBody);

				var jsonResponse = _client.Post(_apiImageUrl, json);

				JsonNode responseJson = JsonNode.Parse(jsonResponse);
				string imageUrl = responseJson?["data"]?[0]?["url"]?.ToString();

				if (!string.IsNullOrEmpty(imageUrl))
				{
					byte[] imageData = _client.GetByteArray(imageUrl);

					return new AIResponse
					{
						Image = AIImage.FromBytesAndSize(imageData,
														 image?.Width ?? 0,
														 image?.Height ?? 0,
														 "png")
					};
				}
				else
				{
					return null;
				}
			}
			else
			{
				throw new NotImplementedException($"AI {model} model is not supported");
			}
		}
	}
}
