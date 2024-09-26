using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FractalPlatform.Client.UI.DOM.Controls;
using FractalPlatform.Database.Dimensions.Language;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Sandbox.Controls
{
	public class BaseControl : Panel
	{
		private LanguageDimension _languageDimension;

		public MainForm ParentForm { get; }

		public DOMControl DOMControl { get; }

		public BaseControl(MainForm parentForm, DOMControl domControl)
		{
			ParentForm = parentForm;

			DOMControl = domControl;

			_languageDimension = domControl.ParentForm.GetLanguageDimension();

			CreateContextMenu();
		}

		protected string GetLocalizedValue(string value)
		{
			return _languageDimension.GetLocalizedValue(DOMControl.ParentForm.Context, value);
		}

		protected void SetValidatonStyle()
		{
			if (DOMControl.HasValidationError)
			{
				ForeColor = Color.Red;

				// Create the ToolTip and associate with the Form container.
				ToolTip toolTip = new ToolTip();

				// Set up the delays for the ToolTip.
				toolTip.AutoPopDelay = 3000;
				toolTip.InitialDelay = 500;
				toolTip.ReshowDelay = 300;
				// Force the ToolTip text to be displayed whether or not the form is active.
				toolTip.ShowAlways = true;

				// Set up the ToolTip text for the Button and Checkbox.
				var message = QueryHelper.Format(DOMControl.ParentForm.Context,
												 DOMControl.ValidationError.Message,
												 DOMControl.ValidationError.Params);

				toolTip.SetToolTip(this, message);

				bool hasLabel = false;

				foreach (Control control in this.Controls)
				{
					if (control is Label)
					{
						hasLabel = true;
					}
				}

				foreach (Control control in this.Controls)
				{
					if (!hasLabel || control is Label)
                    {
						control.ForeColor = Color.Red;

						toolTip.SetToolTip(control, message);
					}
                }
			}
		}

		protected string GetFilePath()
		{
			var di = new DirectoryInfo(DOMControl.ParentForm.Context.FilePath);

			if (Directory.Exists(di.FullName))
			{
				return di.FullName; //Examples assembly
			}
			else
			{
				return di.Parent.FullName; //Projects folder
			}
		}

		private void CreateContextMenu()
		{
			var contextMenu = DOMControl.ContextMenu;

			if (contextMenu != null && contextMenu.Count > 0)
			{
				var contextMenuStrip = new ContextMenuStrip();

				foreach (var contextMenuItem in contextMenu)
				{
					var menuItem = new ToolStripMenuItem(contextMenuItem.Text);

					menuItem.Tag = contextMenuItem.Action;

					menuItem.Click += ContextMenuItem_Click;

					contextMenuStrip.Items.Add(menuItem);
				}

				this.ContextMenuStrip = contextMenuStrip;
			}
		}

		protected virtual void ContextMenuItem_Click(object sender, EventArgs e)
		{
			var contextMenuItem = (ToolStripMenuItem)sender;

			var action = (string)contextMenuItem.Tag;

			DOMControl.OnMenuClick(action);

			ParentForm.RefreshForm();
		}
	}
}
