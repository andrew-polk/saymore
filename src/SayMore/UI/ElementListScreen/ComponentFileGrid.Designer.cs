namespace SayMore.UI.ElementListScreen
{
	partial class ComponentFileGrid
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComponentFileGrid));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this._contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._menuDeleteFile = new System.Windows.Forms.ToolStripMenuItem();
			this._panelOuter = new SilTools.Controls.SilPanel();
			this._toolStripActions = new System.Windows.Forms.ToolStrip();
			this._buttonOpen = new System.Windows.Forms.ToolStripDropDownButton();
			this._buttonRename = new System.Windows.Forms.ToolStripDropDownButton();
			this._buttonConvert = new System.Windows.Forms.ToolStripDropDownButton();
			this._buttonAddFiles = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this._buttonCreateAnnotationFile = new System.Windows.Forms.ToolStripButton();
			this.locExtender = new Localization.UI.LocalizationExtender(this.components);
			this._grid = new SayMore.UI.ElementListScreen.InternalComponentFileGrid();
			this.colIcon = new System.Windows.Forms.DataGridViewImageColumn();
			this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colDataModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._contextMenuStrip.SuspendLayout();
			this._panelOuter.SuspendLayout();
			this._toolStripActions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.locExtender)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
			this.SuspendLayout();
			// 
			// _contextMenuStrip
			// 
			this._contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuDeleteFile});
			this.locExtender.SetLocalizableToolTip(this._contextMenuStrip, null);
			this.locExtender.SetLocalizationComment(this._contextMenuStrip, null);
			this.locExtender.SetLocalizationPriority(this._contextMenuStrip, Localization.LocalizationPriority.NotLocalizable);
			this.locExtender.SetLocalizingId(this._contextMenuStrip, "UI.ComponentFileGrid._contextMenuStrip");
			this._contextMenuStrip.Name = "_contextMenuStrip";
			this._contextMenuStrip.Size = new System.Drawing.Size(129, 26);
			// 
			// _menuDeleteFile
			// 
			this.locExtender.SetLocalizableToolTip(this._menuDeleteFile, null);
			this.locExtender.SetLocalizationComment(this._menuDeleteFile, null);
			this.locExtender.SetLocalizingId(this._menuDeleteFile, "UI.ComponentFileGrid._menuDeleteFile");
			this._menuDeleteFile.Name = "_menuDeleteFile";
			this._menuDeleteFile.Size = new System.Drawing.Size(128, 22);
			this._menuDeleteFile.Text = "Delete File";
			// 
			// _panelOuter
			// 
			this._panelOuter.BackColor = System.Drawing.SystemColors.Window;
			this._panelOuter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(173)))), ((int)(((byte)(179)))));
			this._panelOuter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._panelOuter.ClipTextForChildControls = true;
			this._panelOuter.ControlReceivingFocusOnMnemonic = null;
			this._panelOuter.Controls.Add(this._grid);
			this._panelOuter.Controls.Add(this._toolStripActions);
			this._panelOuter.Dock = System.Windows.Forms.DockStyle.Fill;
			this._panelOuter.DoubleBuffered = true;
			this._panelOuter.DrawOnlyBottomBorder = false;
			this._panelOuter.DrawOnlyTopBorder = false;
			this._panelOuter.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this._panelOuter.ForeColor = System.Drawing.SystemColors.ControlText;
			this.locExtender.SetLocalizableToolTip(this._panelOuter, null);
			this.locExtender.SetLocalizationComment(this._panelOuter, null);
			this.locExtender.SetLocalizingId(this._panelOuter, "UI.ComponentFileGrid._panelOuter");
			this._panelOuter.Location = new System.Drawing.Point(0, 0);
			this._panelOuter.Margin = new System.Windows.Forms.Padding(0);
			this._panelOuter.MnemonicGeneratesClick = false;
			this._panelOuter.Name = "_panelOuter";
			this._panelOuter.PaintExplorerBarBackground = false;
			this._panelOuter.Size = new System.Drawing.Size(470, 255);
			this._panelOuter.TabIndex = 2;
			// 
			// _toolStripActions
			// 
			this._toolStripActions.BackColor = System.Drawing.SystemColors.Control;
			this._toolStripActions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this._toolStripActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonOpen,
            this._buttonRename,
            this._buttonConvert,
            this._buttonAddFiles,
            this.toolStripSeparator1,
            this._buttonCreateAnnotationFile});
			this.locExtender.SetLocalizableToolTip(this._toolStripActions, null);
			this.locExtender.SetLocalizationComment(this._toolStripActions, null);
			this.locExtender.SetLocalizationPriority(this._toolStripActions, Localization.LocalizationPriority.NotLocalizable);
			this.locExtender.SetLocalizingId(this._toolStripActions, "UI.ComponentFileGrid._toolStripActions");
			this._toolStripActions.Location = new System.Drawing.Point(0, 0);
			this._toolStripActions.Name = "_toolStripActions";
			this._toolStripActions.Padding = new System.Windows.Forms.Padding(7, 0, 7, 2);
			this._toolStripActions.Size = new System.Drawing.Size(468, 25);
			this._toolStripActions.TabIndex = 4;
			this._toolStripActions.Paint += new System.Windows.Forms.PaintEventHandler(this.HandleToolStripActionsPaint);
			// 
			// _buttonOpen
			// 
			this._buttonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._buttonOpen.Enabled = false;
			this._buttonOpen.Image = ((System.Drawing.Image)(resources.GetObject("_buttonOpen.Image")));
			this._buttonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.locExtender.SetLocalizableToolTip(this._buttonOpen, null);
			this.locExtender.SetLocalizationComment(this._buttonOpen, null);
			this.locExtender.SetLocalizingId(this._buttonOpen, "UI.ComponentFileGrid._buttonOpen");
			this._buttonOpen.Name = "_buttonOpen";
			this._buttonOpen.Size = new System.Drawing.Size(47, 20);
			this._buttonOpen.Text = "Open";
			this._buttonOpen.ToolTipText = "Open Selected File";
			this._buttonOpen.DropDownOpening += new System.EventHandler(this.HandleActionsDropDownOpening);
			// 
			// _buttonRename
			// 
			this._buttonRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._buttonRename.Enabled = false;
			this._buttonRename.Image = ((System.Drawing.Image)(resources.GetObject("_buttonRename.Image")));
			this._buttonRename.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.locExtender.SetLocalizableToolTip(this._buttonRename, null);
			this.locExtender.SetLocalizationComment(this._buttonRename, null);
			this.locExtender.SetLocalizingId(this._buttonRename, "UI.ComponentFileGrid._buttonRename");
			this._buttonRename.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
			this._buttonRename.Name = "_buttonRename";
			this._buttonRename.Size = new System.Drawing.Size(61, 20);
			this._buttonRename.Text = "Rename";
			this._buttonRename.ToolTipText = "Rename Selected File";
			this._buttonRename.DropDownOpening += new System.EventHandler(this.HandleActionsDropDownOpening);
			// 
			// _buttonConvert
			// 
			this._buttonConvert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._buttonConvert.Enabled = false;
			this._buttonConvert.Image = ((System.Drawing.Image)(resources.GetObject("_buttonConvert.Image")));
			this._buttonConvert.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.locExtender.SetLocalizableToolTip(this._buttonConvert, null);
			this.locExtender.SetLocalizationComment(this._buttonConvert, null);
			this.locExtender.SetLocalizingId(this._buttonConvert, "UI.ComponentFileGrid._buttonConvert");
			this._buttonConvert.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
			this._buttonConvert.Name = "_buttonConvert";
			this._buttonConvert.Size = new System.Drawing.Size(60, 20);
			this._buttonConvert.Text = "Convert";
			this._buttonConvert.ToolTipText = "Convert Selected File";
			this._buttonConvert.DropDownOpening += new System.EventHandler(this.HandleActionsDropDownOpening);
			// 
			// _buttonAddFiles
			// 
			this._buttonAddFiles.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._buttonAddFiles.Image = global::SayMore.Properties.Resources.Add;
			this._buttonAddFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.locExtender.SetLocalizableToolTip(this._buttonAddFiles, null);
			this.locExtender.SetLocalizationComment(this._buttonAddFiles, null);
			this.locExtender.SetLocalizingId(this._buttonAddFiles, "UI.ComponentFileGrid._buttonAddFiles");
			this._buttonAddFiles.Name = "_buttonAddFiles";
			this._buttonAddFiles.Size = new System.Drawing.Size(83, 20);
			this._buttonAddFiles.Text = "Add Files...";
			this._buttonAddFiles.Click += new System.EventHandler(this.HandleAddButtonClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
			// 
			// _buttonCreateAnnotationFile
			// 
			this._buttonCreateAnnotationFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._buttonCreateAnnotationFile.Enabled = false;
			this._buttonCreateAnnotationFile.Image = ((System.Drawing.Image)(resources.GetObject("_buttonCreateAnnotationFile.Image")));
			this._buttonCreateAnnotationFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.locExtender.SetLocalizableToolTip(this._buttonCreateAnnotationFile, null);
			this.locExtender.SetLocalizationComment(this._buttonCreateAnnotationFile, null);
			this.locExtender.SetLocalizingId(this._buttonCreateAnnotationFile, "UI.ComponentFileGrid._buttonCreateAnnotationFile");
			this._buttonCreateAnnotationFile.Name = "_buttonCreateAnnotationFile";
			this._buttonCreateAnnotationFile.Size = new System.Drawing.Size(133, 20);
			this._buttonCreateAnnotationFile.Text = "Create Annotation File...";
			this._buttonCreateAnnotationFile.ToolTipText = "Create an annotation file";
			this._buttonCreateAnnotationFile.Click += new System.EventHandler(this.HandleCreateAnnotationFileButtonClick);
			// 
			// locExtender
			// 
			this.locExtender.LocalizationManagerId = "SayMore";
			// 
			// _grid
			// 
			this._grid.AllowUserToAddRows = false;
			this._grid.AllowUserToDeleteRows = false;
			this._grid.AllowUserToOrderColumns = true;
			this._grid.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			this._grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this._grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
			this._grid.BackgroundColor = System.Drawing.SystemColors.Window;
			this._grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this._grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIcon,
            this.colName,
            this.colType,
            this.colDataModified,
            this.colSize,
            this.colDuration});
			this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this._grid.DrawTextBoxEditControlBorder = false;
			this._grid.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
			this._grid.FullRowFocusRectangleColor = System.Drawing.SystemColors.ControlDark;
			this._grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(219)))), ((int)(((byte)(180)))));
			this._grid.IsDirty = false;
			this.locExtender.SetLocalizableToolTip(this._grid, null);
			this.locExtender.SetLocalizationComment(this._grid, null);
			this.locExtender.SetLocalizingId(this._grid, "UI.ComponentFileGrid._grid");
			this._grid.Location = new System.Drawing.Point(0, 25);
			this._grid.Margin = new System.Windows.Forms.Padding(0);
			this._grid.MultiSelect = false;
			this._grid.Name = "_grid";
			this._grid.PaintFullRowFocusRectangle = true;
			this._grid.PaintHeaderAcrossFullGridWidth = true;
			this._grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this._grid.RowHeadersVisible = false;
			this._grid.RowHeadersWidth = 22;
			this._grid.SelectedCellBackColor = System.Drawing.Color.Empty;
			this._grid.SelectedCellForeColor = System.Drawing.Color.Empty;
			this._grid.SelectedRowBackColor = System.Drawing.Color.Empty;
			this._grid.SelectedRowForeColor = System.Drawing.Color.Empty;
			this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._grid.ShowWaterMarkWhenDirty = false;
			this._grid.Size = new System.Drawing.Size(468, 228);
			this._grid.StandardTab = true;
			this._grid.TabIndex = 1;
			this._grid.TextBoxEditControlBorderColor = System.Drawing.Color.Silver;
			this._grid.VirtualMode = true;
			this._grid.WaterMark = "!";
			this._grid.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.HandleFileGridColumnWidthChanged);
			this._grid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HandleFileGridKeyDown);
			// 
			// colIcon
			// 
			this.colIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colIcon.DataPropertyName = "SmallIcon";
			this.colIcon.HeaderText = "";
			this.colIcon.Name = "colIcon";
			this.colIcon.ReadOnly = true;
			this.colIcon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colIcon.Width = 5;
			// 
			// colName
			// 
			this.colName.DataPropertyName = "FileName";
			this.colName.HeaderText = "L10N:UI.ComponentFileGrid._grid.FileNameColHdgText!Name";
			this.colName.Name = "colName";
			this.colName.ReadOnly = true;
			// 
			// colType
			// 
			this.colType.DataPropertyName = "FileTypeDescription";
			this.colType.HeaderText = "L10N:UI.ComponentFileGrid._grid.FileTypeColHdgText!Type";
			this.colType.Name = "colType";
			this.colType.ReadOnly = true;
			// 
			// colDataModified
			// 
			this.colDataModified.DataPropertyName = "DateModified";
			this.colDataModified.HeaderText = "L10N:UI.ComponentFileGrid._grid.FileDateColHdgText!Date Modified";
			this.colDataModified.Name = "colDataModified";
			this.colDataModified.ReadOnly = true;
			this.colDataModified.Width = 107;
			// 
			// colSize
			// 
			this.colSize.DataPropertyName = "FileSize";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			this.colSize.DefaultCellStyle = dataGridViewCellStyle3;
			this.colSize.HeaderText = "L10N:UI.ComponentFileGrid._grid.FileSizeColHdgText!Size";
			this.colSize.Name = "colSize";
			this.colSize.ReadOnly = true;
			this.colSize.Width = 52;
			// 
			// colDuration
			// 
			this.colDuration.DataPropertyName = "DurationString";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			this.colDuration.DefaultCellStyle = dataGridViewCellStyle4;
			this.colDuration.HeaderText = "L10N:UI.ComponentFileGrid._grid.FileDurationColHdgText!Duration";
			this.colDuration.Name = "colDuration";
			this.colDuration.ReadOnly = true;
			// 
			// ComponentFileGrid
			// 
			this.Controls.Add(this._panelOuter);
			this.locExtender.SetLocalizableToolTip(this, null);
			this.locExtender.SetLocalizationComment(this, null);
			this.locExtender.SetLocalizationPriority(this, Localization.LocalizationPriority.NotLocalizable);
			this.locExtender.SetLocalizingId(this, "SayMore");
			this.Name = "ComponentFileGrid";
			this.Size = new System.Drawing.Size(470, 255);
			this._contextMenuStrip.ResumeLayout(false);
			this._panelOuter.ResumeLayout(false);
			this._panelOuter.PerformLayout();
			this._toolStripActions.ResumeLayout(false);
			this._toolStripActions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.locExtender)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private InternalComponentFileGrid _grid;
		private System.Windows.Forms.ContextMenuStrip _contextMenuStrip;
		private SilTools.Controls.SilPanel _panelOuter;
		private System.Windows.Forms.ToolStrip _toolStripActions;
		private System.Windows.Forms.ToolStripDropDownButton _buttonOpen;
		private System.Windows.Forms.ToolStripDropDownButton _buttonRename;
		private System.Windows.Forms.ToolStripDropDownButton _buttonConvert;
		private System.Windows.Forms.ToolStripButton _buttonAddFiles;
		private System.Windows.Forms.ToolStripMenuItem _menuDeleteFile;
		private System.Windows.Forms.DataGridViewImageColumn colIcon;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colType;
		private System.Windows.Forms.DataGridViewTextBoxColumn colDataModified;
		private System.Windows.Forms.DataGridViewTextBoxColumn colSize;
		private System.Windows.Forms.DataGridViewTextBoxColumn colDuration;
		private System.Windows.Forms.ToolStripButton _buttonCreateAnnotationFile;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private Localization.UI.LocalizationExtender locExtender;

	}
}
