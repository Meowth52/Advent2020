using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Advent2020
{
    public class MainView : INotifyPropertyChanged
    {
        private string _OutText;
        public string OutText
        {
            get
            {
                return _OutText;
            }
            set
            {
                if (value == _OutText)
                    return;
                _OutText = value;
                OnPropertyChanged();
            }
        }
        private string _InText;
        public string InText
        {
            get
            {
                return _InText;
            }
            set
            {
                if (value == _InText)
                    return;
                _InText = value;
                OnPropertyChanged();
            }
        }
        public string KeyPresses;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}