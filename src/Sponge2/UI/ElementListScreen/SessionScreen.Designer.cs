using SilUtils;
using Sponge2.UI.LowLevelControls;

namespace Sponge2.UI.ElementListScreen
{
	partial class SessionScreen
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._componentEditorsTabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this._elementListSplitter = new System.Windows.Forms.SplitContainer();
			this._sessionsListPanel = new Sponge2.UI.LowLevelControls.ListPanel();
			this._componentsSplitter = new System.Windows.Forms.SplitContainer();
			this._componentGridPanel = new SilUtils.Controls.SilPanel();
			this._componentFileGrid = new Sponge2.UI.ElementListScreen.ComponentFileGrid();
			this._componentEditorsTabControl.SuspendLayout();
			this._elementListSplitter.Panel1.SuspendLayout();
			this._elementListSplitter.Panel2.SuspendLayout();
			this._elementListSplitter.SuspendLayout();
			this._componentsSplitter.Panel1.SuspendLayout();
			this._componentsSplitter.Panel2.SuspendLayout();
			this._componentsSplitter.SuspendLayout();
			this._componentGridPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// _componentEditorsTabControl
			// 
			this._componentEditorsTabControl.Controls.Add(this.tabPage1);
			this._componentEditorsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._componentEditorsTabControl.Location = new System.Drawing.Point(0, 0);
			this._componentEditorsTabControl.Name = "_componentEditorsTabControl";
			this._componentEditorsTabControl.SelectedIndex = 0;
			this._componentEditorsTabControl.Size = new System.Drawing.Size(315, 197);
			this._componentEditorsTabControl.TabIndex = 7;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(307, 171);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// _elementListSplitter
			// 
			this._elementListSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
			this._elementListSplitter.Location = new System.Drawing.Point(0, 0);
			this._elementListSplitter.Name = "_elementListSplitter";
			// 
			// _elementListSplitter.Panel1
			// 
			this._elementListSplitter.Panel1.Controls.Add(this._sessionsListPanel);
			// 
			// _elementListSplitter.Panel2
			// 
			this._elementListSplitter.Panel2.Controls.Add(this._componentsSplitter);
			this._elementListSplitter.Size = new System.Drawing.Size(503, 350);
			this._elementListSplitter.SplitterDistance = 182;
			this._elementListSplitter.SplitterWidth = 6;
			this._elementListSplitter.TabIndex = 9;
			this._elementListSplitter.TabStop = false;
			// 
			// _sessionsListPanel
			// 
			this._sessionsListPanel.CurrentItem = null;
			this._sessionsListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._sessionsListPanel.Items = new object[0];
			// 
			// 
			// 
			this._sessionsListPanel.ListView.BackColor = System.Drawing.SystemColors.Window;
			this._sessionsListPanel.ListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._sessionsListPanel.ListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this._sessionsListPanel.ListView.Font = new System.Drawing.Font("Segoe UI", 9F);
			this._sessionsListPanel.ListView.FullRowSelect = true;
			this._sessionsListPanel.ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this._sessionsListPanel.ListView.HideSelection = false;
			this._sessionsListPanel.ListView.Location = new System.Drawing.Point(0, 30);
			this._sessionsListPanel.ListView.Name = "lvItems";
			this._sessionsListPanel.ListView.Size = new System.Drawing.Size(180, 284);
			this._sessionsListPanel.ListView.TabIndex = 0;
			this._sessionsListPanel.ListView.UseCompatibleStateImageBehavior = false;
			this._sessionsListPanel.ListView.View = System.Windows.Forms.View.Details;
			this._sessionsListPanel.Location = new System.Drawing.Point(0, 0);
			this._sessionsListPanel.MinimumSize = new System.Drawing.Size(165, 0);
			this._sessionsListPanel.Name = "_sessionsListPanel";
			this._sessionsListPanel.ReSortWhenItemTextChanges = false;
			this._sessionsListPanel.Size = new System.Drawing.Size(182, 350);
			this._sessionsListPanel.TabIndex = 8;
			this._sessionsListPanel.Text = "Sessions";
			// 
			// _componentsSplitter
			// 
			this._componentsSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
			this._componentsSplitter.Location = new System.Drawing.Point(0, 0);
			this._componentsSplitter.Name = "_componentsSplitter";
			this._componentsSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// _componentsSplitter.Panel1
			// 
			this._componentsSplitter.Panel1.Controls.Add(this._componentGridPanel);
			// 
			// _componentsSplitter.Panel2
			// 
			this._componentsSplitter.Panel2.Controls.Add(this._componentEditorsTabControl);
			this._componentsSplitter.Size = new System.Drawing.Size(315, 350);
			this._componentsSplitter.SplitterDistance = 147;
			this._componentsSplitter.SplitterWidth = 6;
			this._componentsSplitter.TabIndex = 0;
			this._componentsSplitter.TabStop = false;
			// 
			// _componentGridPanel
			// 
			this._componentGridPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(173)))), ((int)(((byte)(179)))));
			this._componentGridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._componentGridPanel.ClipTextForChildControls = true;
			this._componentGridPanel.ControlReceivingFocusOnMnemonic = null;
			this._componentGridPanel.Controls.Add(this._componentFileGrid);
			this._componentGridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._componentGridPanel.DoubleBuffered = true;
			this._componentGridPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this._componentGridPanel.Location = new System.Drawing.Point(0, 0);
			this._componentGridPanel.MnemonicGeneratesClick = false;
			this._componentGridPanel.Name = "_componentGridPanel";
			this._componentGridPanel.PaintExplorerBarBackground = false;
			this._componentGridPanel.Size = new System.Drawing.Size(315, 147);
			this._componentGridPanel.TabIndex = 1;
			// 
			// _componentFileGrid
			// 
			this._componentFileGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this._componentFileGrid.Location = new System.Drawing.Point(0, 0);
			this._componentFileGrid.Name = "_componentFileGrid";
			this._componentFileGrid.Size = new System.Drawing.Size(313, 145);
			this._componentFileGrid.TabIndex = 0;
			// 
			// SessionScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._elementListSplitter);
			this.Name = "SessionScreen";
			this.Size = new System.Drawing.Size(503, 350);
			this._componentEditorsTabControl.ResumeLayout(false);
			this._elementListSplitter.Panel1.ResumeLayout(false);
			this._elementListSplitter.Panel2.ResumeLayout(false);
			this._elementListSplitter.ResumeLayout(false);
			this._componentsSplitter.Panel1.ResumeLayout(false);
			this._componentsSplitter.Panel2.ResumeLayout(false);
			this._componentsSplitter.ResumeLayout(false);
			this._componentGridPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl _componentEditorsTabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private ListPanel _sessionsListPanel;
		private System.Windows.Forms.SplitContainer _elementListSplitter;
		private System.Windows.Forms.SplitContainer _componentsSplitter;
		private SilUtils.Controls.SilPanel _componentGridPanel;
		private ComponentFileGrid _componentFileGrid;
	}
}
