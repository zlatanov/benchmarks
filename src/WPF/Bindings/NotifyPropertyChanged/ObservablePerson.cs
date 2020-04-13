using System.ComponentModel;

namespace WPF.Bindings.NotifyPropertyChanged
{
    public sealed class ObservablePerson : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public string Name { get; set; }


        public int Age { get; set; }
    }
}
