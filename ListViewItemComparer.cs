﻿#region License
// Copyright © 2018 Wesley Hamilton
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at https://github.com/ffhighwind/PoE-Price-Lister
#endregion

using System.Collections;
using System.Windows.Forms;

namespace PoE_Price_Lister
{
	public class ListViewItemComparer : IComparer
	{
		public int Column { get; set; }

		public bool Ascending { get; set; }

		public bool IsNumeric { get; set; }

		public ListViewItemComparer(int columnIndex)
		{
			Column = columnIndex;
		}

		public int Compare(object x, object y)
		{
			ListViewItem itemX = x as ListViewItem;
			ListViewItem itemY = y as ListViewItem;

			int output;

			if (itemX == itemY)
				return 0;
			else if (itemX == null)
				output = -1;
			else if (itemY == null)
				output = 1;
			else if (IsNumeric) {
				if (!decimal.TryParse(itemX.SubItems[Column].Text, out decimal itemXVal)) {
					itemXVal = 0;
				}
				if (!decimal.TryParse(itemY.SubItems[Column].Text, out decimal itemYVal)) {
					itemYVal = 0;
				}

				output = decimal.Compare(itemXVal, itemYVal);
			}
			else {
				string itemXText = itemX.SubItems[Column].Text;
				string itemYText = itemY.SubItems[Column].Text;

				output = string.Compare(itemXText, itemYText);
			}

			if (Ascending)
				output = -output;
			return output;
		}
	}
}
