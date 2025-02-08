using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YourNamespace
{
    public partial class EventView : Form, IEventView
    {
        public event EventHandler LoadEvents;
        public event EventHandler AddEvent;
        public event EventHandler UpdateEvent;
        public event EventHandler DeleteEvent;

        public EventView()
        {
            InitializeComponent();
        }

        public void DisplayEvents(List<EventModel> events)
        {
            // Display events in a ListView or DataGridView
        }

        public EventModel GetEventDetails()
        {
            // Get event details from input fields
            return new EventModel();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadEvents?.Invoke(this, EventArgs.Empty);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEvent?.Invoke(this, EventArgs.Empty);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateEvent?.Invoke(this, EventArgs.Empty);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
