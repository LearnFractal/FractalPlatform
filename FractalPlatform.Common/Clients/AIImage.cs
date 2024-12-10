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

using SkiaSharp;
using System;
using System.IO;

namespace FractalPlatform.Common.Clients
{
	public class AIImage
	{
		public string Type { get; set; }

		public string Url { get; set; } 

		public string Base64 { get; set; }

		public byte[] Bytes { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public static byte[] ScaleImage(byte[] imageBytes, int maxWidth, int maxHeight)
		{
			SKBitmap image = SKBitmap.Decode(imageBytes);

			var ratioX = (double)maxWidth / image.Width;
			var ratioY = (double)maxHeight / image.Height;
			var ratio = Math.Min(ratioX, ratioY);

			var newWidth = (int)(image.Width * ratio);
			var newHeight = (int)(image.Height * ratio);

			var info = new SKImageInfo(newWidth, newHeight);
			image = image.Resize(info, SKFilterQuality.High);

			using var ms = new MemoryStream();
			image.Encode(ms, SKEncodedImageFormat.Jpeg, 100);
			return ms.ToArray();
		}

		public static AIImage FromBytes(byte[] bytes, string type = "jpeg")
		{
			if (bytes.Length > 1024 * 1024)
			{
				bytes = ScaleImage(bytes, 1024, 1024);
			}

			var base64 = Convert.ToBase64String(bytes);

			return new AIImage 
			{
				Type = type,
				Base64 = base64,
				Url = null 
			}; 
		}

        public static AIImage FromBytesAndSize(byte[] bytes, int width, int height, string type = "jpeg")
        {
            return new AIImage
            {
                Type = type,
                Bytes = bytes,
                Width = width,
				Height = height,
            };
        }

        public static AIImage FromUrl(string url)
		{
			return new AIImage
			{
				Type = "url",
				Base64 = null,
				Url = url
			};
		}

		public static AIImage FromFile(string fileName, string type = "jpeg")
		{
			return FromBytes(File.ReadAllBytes(fileName), type);
		}
	}
}
