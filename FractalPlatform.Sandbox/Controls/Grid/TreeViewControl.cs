using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM.Controls.Grid;

namespace FractalPlatform.Sandbox.Controls.Grid
{
    public class TreeViewControl : BaseControl
    {
        private TreeView _tvTreeView = new TreeView();

        public TreeViewControl(MainForm parentForm, TreeViewDOMControl domControl) : base(parentForm, domControl)
        {
            // 
            // pnlTreeViewControl
            // 
            Controls.Add(_tvTreeView);
            Name = Helpers.EscapeName("pnlTreeViewControl_" + domControl.Key);
            Size = new Size(365, 200);
            TabIndex = 1;

            // 
            // tvTreeView
            // 
            _tvTreeView.Dock = DockStyle.Fill;
            _tvTreeView.Location = new Point(111, 0);
            _tvTreeView.Name = Helpers.EscapeName("tvTreeView" + domControl.Key);
            _tvTreeView.Size = new Size(365, 200);
            _tvTreeView.TabIndex = 2;

            AddNodes(_tvTreeView.Nodes, domControl.Root);

            _tvTreeView.ExpandAll();

            SetValidatonStyle();
        }

        private void AddNodes(TreeNodeCollection nodes, DOMNode domNodes)
        {
            foreach(var domNode in domNodes)
            {
                var subNode = nodes.Add(domNode.Name, domNode.Text);

                AddNodes(subNode.Nodes, domNode);
            }
        }

        protected override void ContextMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = _tvTreeView.SelectedNode;

            if (selectedNode != null)
            {
                var domControl = (TreeViewDOMControl)DOMControl;

                var contextMenuItem = (ToolStripMenuItem)sender;

                var action = (string)contextMenuItem.Tag;

                domControl.OnMenuClick(action, (int)selectedNode.Tag);
            }
        }
    }
}