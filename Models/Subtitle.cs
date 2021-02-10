using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using zhsub.Models.SubtitleFiles;

namespace zhsub.Models
{
    class Subtitle
    {
        #region properties
        protected ListView _listView;
        protected GridView _gridView;

        protected GridViewColumn[] _gridViewColumns;
        protected Binding[] _displayMembers;

        protected List<object> _lineList = new List<object>();

        protected string _filePath;
        protected string _fileExtension;
        protected string _fileData;
        #endregion

        protected Subtitle() { }

        public Subtitle(ListView listView, GridView gridView)
        {
            _listView = listView;
            _gridView = gridView;
        }

        private void Clear()
        {
            _listView.ItemsSource = null;
            _listView.Items.Clear();
        }    

        protected virtual void Create() { }

        protected void New()
        {
            _listView.ItemsSource = null;
            _listView.Items.Clear();
            _gridView.Columns.Clear();

            Create();

            for (int i = 0; i < _gridViewColumns.Length; i++)
            {
                _gridView.Columns.Add(_gridViewColumns[i]);
                _gridView.Columns[i].DisplayMemberBinding = _displayMembers[i];
            }
        }

        public void Open()
        {
            var openFileDialog = new OpenFileDialog() 
            { 
                Filter = "SubRip files (*.srt)|*.srt|Lyric files (*.lrc)|*.lrc|Sbv files (*.sbv)|*.sbv|Vtt files (*.vtt)|*.vtt" 
            };

            if (openFileDialog.ShowDialog() == false) return;

            _filePath = openFileDialog.FileName;
            _fileExtension = _filePath[(_filePath.LastIndexOf('.') + 1)..];
            _fileData = File.ReadAllText(_filePath);

            switch (_fileExtension)
            {
                case "srt":
                    new Srt(_listView, _gridView).Open(_fileData, _lineList);
                    break;
            }

            _listView.ItemsSource = _lineList;
        }

        public void Save()
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = "SubRip files (*.srt)|*.srt|Lyric files (*.lrc)|*.lrc|Sbv files (*.sbv)|*.sbv|Vtt files (*.vtt)|*.vtt"
            };

            if (saveFileDialog.ShowDialog() == false) return;

            _filePath = saveFileDialog.FileName;
            _fileExtension = _filePath[(_filePath.LastIndexOf('.') + 1)..];

            switch (_fileExtension)
            {
                case "srt":
                    if (_lineList.Count == 0)
                    {
                        MessageBox.Show("null");
                        return;
                    }  
                    
                    Srt.Save(_filePath, _lineList);
                    break;
            }

            //File.WriteAllText(_filePath, _fileData);
        }
    }
}
