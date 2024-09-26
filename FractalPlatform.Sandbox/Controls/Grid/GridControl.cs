using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls.Grid;

namespace FractalPlatform.Sandbox.Controls.Grid
{
    public class GridControl : BaseControl
    {
        const int BETWEEN = 2;

        private ListView _lvGrid = new ListView();
        private Label _lblGrid = new Label();
        private Button _btnAdd = new Button();
        private Button _btnDel = new Button();

        public GridControl(MainForm parentForm, GridDOMControl domControl) : base(parentForm, domControl)
        {
            // 
            // pnlGrid
            //
            Location = domControl.Location;
            Name = Helpers.EscapeName("pnlGrid_" + domControl.Key);
            TabIndex = 1;

            Controls.Add(_lvGrid);
            Controls.Add(_lblGrid);

            Controls.Add(_btnAdd);
            Controls.Add(_btnDel);

            // 
            // lblGrid
            // 
            _lblGrid.Dock = DockStyle.Left;
            _lblGrid.Location = new Point(0, 0);
            _lblGrid.Name = Helpers.EscapeName("lblGrid_" + domControl.Key);
            _lblGrid.Size = new Size(UIConstants.LabelWidth, domControl.HeightInPx);
            _lblGrid.TabIndex = 0;
            _lblGrid.Text = GetLocalizedValue(domControl.Key);
            
            // 
            // lvGrid
            // 
            //_lvGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            _lvGrid.HideSelection = false;
            _lvGrid.Location = new Point(UIConstants.LabelWidth, 0);
            _lvGrid.Margin = new Padding(3, 4, 3, 4);
            _lvGrid.Name = Helpers.EscapeName("lvGrid_" + domControl.Key);
            _lvGrid.TabIndex = 0;
            _lvGrid.UseCompatibleStateImageBehavior = false;
            _lvGrid.FullRowSelect = true;
            _lvGrid.Font = new Font(_lvGrid.Font.FontFamily, 12);
            _lvGrid.MouseDoubleClick += _lvGrid_MouseDoubleClick;

            // 
            // btnAdd
            // 
            _btnAdd.Name = Helpers.EscapeName("btnAdd_" + domControl.Key);
            _btnAdd.Size = new Size(40, UIConstants.Height);
            _btnAdd.TabIndex = 1;
            _btnAdd.Text = "Add";
            _btnAdd.UseVisualStyleBackColor = true;
            _btnAdd.Visible = domControl.IsAllowCreate || domControl.IsAllowDelete;
            _btnAdd.Enabled = domControl.IsAllowCreate;
            _btnAdd.Click += _btnAdd_Click;

            // 
            // btnDel
            // 
            _btnDel.Name = Helpers.EscapeName("btnAdd_" + domControl.Key);
            _btnDel.Size = new Size(40, UIConstants.Height);
            _btnDel.TabIndex = 1;
            _btnDel.Text = "Del";
            _btnDel.UseVisualStyleBackColor = true;
            _btnDel.Visible = domControl.IsAllowCreate || domControl.IsAllowDelete;
            _btnDel.Enabled = domControl.IsAllowDelete;
            _btnDel.Click += _btnDel_Click;

            DataBind();

            SetValidatonStyle();
        }

        private void _btnAdd_Click(object sender, EventArgs e)
        {
            var domControl = (GridDOMControl)DOMControl;

            domControl.OnAddClick(ParentForm.DomForm.Context.FormFactory);

            ParentForm.DomForm.RefreshForm();

            DataBind();
        }

        private void _btnDel_Click(object sender, EventArgs e)
        {
            var domControl = (GridDOMControl)DOMControl;

            if (_lvGrid.SelectedItems.Count > 0)
            {
                var selectedIndexes = new List<int>();

                for (int i = 0; i < _lvGrid.SelectedItems.Count; i++)
                {
                    selectedIndexes.Add(_lvGrid.SelectedItems[i].Index);
                }

                domControl.OnDeleteClick(selectedIndexes);

                ParentForm.RefreshForm();

                DataBind();
            }
        }

        private void _lvGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var domControl = (GridDOMControl)DOMControl;

            if (_lvGrid.SelectedItems.Count > 0)
            {
                domControl.OnDoubleClick(ParentForm.DomForm.Context.FormFactory,
                                         (uint)_lvGrid.SelectedItems[0].Index,
                                         null,
                                         ParentForm.DomForm.Context.CollIdentity);

                ParentForm.RefreshForm();

                DataBind();
            }
        }

        private void DataBind()
        {
            var domControl = (GridDOMControl)DOMControl;

            _lvGrid.BeginUpdate();

            _lvGrid.Clear();

            //fill columns
            _lvGrid.View = View.Details;

            foreach (DataColumn dc in domControl.DataTable.Columns)
            {
                if (domControl.HasNumberColumn || dc.ColumnName != "Number")
                {
                    _lvGrid.Columns.Add(GetLocalizedValue(dc.ColumnName));
                }
            }

            //fill rows
            var startIndex = domControl.HasNumberColumn ? 0 : 1;

            foreach (DataRow row in domControl.DataTable.Rows)
            {
                ListViewItem item = new ListViewItem(row[startIndex].ToString());

                for (int i = startIndex + 1; i < domControl.DataTable.Columns.Count; i++)
                {
                    item.SubItems.Add(row[i].ToString());
                }

                _lvGrid.Items.Add(item);
            }

            Size = new Size(domControl.Parent.WidthInPx - 50, domControl.HeightInPx);

            _lvGrid.Height = domControl.HeightInPx - _btnAdd.Height;
            _lvGrid.Width = domControl.Parent.WidthInPx - _lblGrid.Width - 50;

            _lvGrid.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            _lvGrid.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            _lvGrid.EndUpdate();

            _btnAdd.Location = new Point((_lvGrid.Width + _lblGrid.Width) - (_btnAdd.Width + BETWEEN + _btnDel.Width),
                                         _lvGrid.Height);

            _btnDel.Location = new Point((_lvGrid.Width + _lblGrid.Width) - (_btnDel.Width),
                                         _lvGrid.Height);
        }

        protected override void ContextMenuItem_Click(object sender, EventArgs e)
        {
            if (_lvGrid.SelectedItems.Count == 1)
            {
                var domControl = (GridDOMControl)DOMControl;

                var contextMenuItem = (ToolStripMenuItem)sender;

                var action = (string)contextMenuItem.Tag;

                domControl.OnMenuClick(action, _lvGrid.SelectedItems[0].Index);

                ParentForm.RefreshForm();

                DataBind();
            }
        }
    }
}
