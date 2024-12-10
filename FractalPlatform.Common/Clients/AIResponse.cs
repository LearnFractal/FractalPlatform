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

using System.Collections.Generic;

namespace FractalPlatform.Common.Clients
{
	public class CodeBlock
	{ 
		public string Language { get; set; }

		public string Text { get; set; }
	}

	public class AIResponse
	{
		public string Text { get; set; }

		public List<CodeBlock> CodeBlocks { get; set; }

		public AIImage Image { get; set; }
	}
}
