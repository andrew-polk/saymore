using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SilUtils;

namespace SIL.Sponge
{
	/// ----------------------------------------------------------------------------------------
	/// <summary>
	///
	/// </summary>
	/// ----------------------------------------------------------------------------------------
	public interface ISpongeView
	{
		void ViewActivated(bool firstTime);
		void ViewDeactivated();
		bool IsViewActive { get; }
		bool IsOKToLeaveView(bool showMsgWhenNotOK);
	}

	/// ----------------------------------------------------------------------------------------
	/// <summary>
	/// This class manages the association between buttons on a ToolStrip control and a
	/// collection of controls. When one of the buttons on the ToolStrip is pressed, its
	/// associated view is displayed.
	/// </summary>
	/// ----------------------------------------------------------------------------------------
	public class ViewButtonManager : IDisposable
	{
		private ToolStrip m_toolStrip;
		private Control m_toolStripOwner;
		private Dictionary<ToolStripButton, Control> m_controls;
		private Dictionary<Control, bool> m_hasBeenActivatedList;

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ViewButtonManager"/> class.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		public ViewButtonManager(ToolStrip toolstrip, IEnumerable<Control> ctrls)
		{
			Debug.Assert(toolstrip.Items.Count >= ctrls.Count());

			m_toolStripOwner = toolstrip.TopLevelControl;
			m_toolStrip = toolstrip;
			m_toolStrip.ItemClicked += toolstrip_ItemClicked;
			m_controls = new Dictionary<ToolStripButton, Control>();
			m_hasBeenActivatedList = new Dictionary<Control, bool>();

			int i = 0;
			foreach (Control ctrl in ctrls)
			{
				ctrl.Dock = DockStyle.Fill;
				ctrl.Visible = false;
				Debug.Assert(m_toolStrip.Items[i] is ToolStripButton);
				m_controls[m_toolStrip.Items[i++] as ToolStripButton] = ctrl;

				if (ctrl is ISpongeView)
					m_hasBeenActivatedList[ctrl] = false;
			}
		}

		#region IDisposable Members
		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or
		/// resetting unmanaged resources.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		public void Dispose()
		{
			if (m_toolStrip != null)
				m_toolStrip.ItemClicked -= toolstrip_ItemClicked;

			m_toolStrip = null;
			m_toolStripOwner = null;
			m_controls = null;
			m_hasBeenActivatedList = null;
		}

		#endregion

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Gets all the controls associated with buttons that implement the ISpongeView
		/// interface.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		public IEnumerable<ISpongeView> Views
		{
			get
			{
				return from x in m_controls.Values
					   where x is ISpongeView
					   select x as ISpongeView;
			}
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Gets the current button for the current view.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		public ToolStripButton CurrentViewButton
		{
			get
			{
				foreach (ToolStripButton btn in m_toolStrip.Items)
				{
					if (btn.Checked)
						return btn;
				}

				return null;
			}
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Handles one of the view buttons being clicked.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		private void toolstrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			SetView(e.ClickedItem as ToolStripButton);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Sets the view associated with the specified view button.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		public void SetView(ToolStripButton btn)
		{
			if (!m_controls.ContainsKey(btn))
				return;

			ToolStripButton currbtn = CurrentViewButton;
			if (btn == currbtn)
				return;

			// Now make sure we can leave the current view.
			foreach (var vw in Views)
			{
				if (!vw.IsOKToLeaveView(true))
					return;
			}

			if (m_toolStripOwner != null)
				Utils.SetWindowRedraw(m_toolStripOwner, false);

			if (currbtn != null && m_controls.ContainsKey(currbtn))
			{
				currbtn.Checked = false;
				m_controls[currbtn].Visible = false;

				if (m_controls[currbtn] is ISpongeView)
					((ISpongeView)m_controls[currbtn]).ViewDeactivated();
			}

			Control newVw = null;
			if (btn != null)
			{
				btn.Checked = true;
				newVw = m_controls[btn];
				newVw.Visible = true;
				newVw.BringToFront();
			}

			if (m_toolStripOwner != null)
				Utils.SetWindowRedraw(m_toolStripOwner, true);

			if (newVw is ISpongeView)
			{
				((ISpongeView)newVw).ViewActivated(!m_hasBeenActivatedList[newVw]);
				m_hasBeenActivatedList[newVw] = true;
			}
		}
	}
}
