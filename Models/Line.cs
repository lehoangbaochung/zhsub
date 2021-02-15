using System.Windows.Controls;

namespace zhsub.Models
{
    class Line : Subtitle
    {
        public Line(ListView listView)
        {
            this.listView = listView;
        }    

        public void InsertBefore()
        {
            if (listView.SelectedItem == null) return;
        }

        public void InsertAfter()
        {
            if (listView.SelectedItem == null) return;

        }

        public void Delete()
        {
            listView.Items.Remove(listView.SelectedItem);
        }

        public void Duplicate()
        {
            if (listView.SelectedItem == null) return;

            listView.Items.Insert(listView.SelectedIndex + 1, listView.SelectedItem);
        }

        public void SelectAll()
        {
            listView.SelectAll();
        }
    }
}
