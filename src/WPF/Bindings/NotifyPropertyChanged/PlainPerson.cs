namespace WPF.Bindings.NotifyPropertyChanged
{
    public sealed class PlainPerson
    {
        public PlainPerson( string name )
        {
            Name = name;
        }


        public string Name { get; }


        public int Age { get; set; }
    }
}
