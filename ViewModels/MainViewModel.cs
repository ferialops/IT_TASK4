using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace task4
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly Machine _machine;
        private readonly Miller _miller;
        private readonly ILoader _loader;

        public ObservableCollection<Detail> Details { get; set; }
        public ICommand StartProductionCommand { get; }

        public MainViewModel()
        {
            _machine = new Machine();
            _miller = new Miller();
            _loader = new Loader();

            Details = new ObservableCollection<Detail>();

            _machine.NewDetail += Machine_NewDetail;
            _miller.DetailReady += Miller_DetailReady;

            StartProductionCommand = new RelayCommand(StartProduction);
        }

        public void StartProduction()
        {
            _machine.ProduceDetail();
        }

        private void Machine_NewDetail(object sender, DetailEventArgs e)
        {
            _miller.ProcessDetail(e.Detail);
        }

        private void Miller_DetailReady(object sender, DetailEventArgs e)
        {
            _loader.LoadDetail(e.Detail);
            Details.Add(e.Detail);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
