using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using _05_Lab_CS.Model;

namespace _05_Lab_CS.ViewModels
{
    class InfoViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ProcessModule> _modules;
        private ObservableCollection<ProcessThread> _threads;
        private MyProcess _process;
        private static Thread _workingThread;

        internal InfoViewModel(MyProcess p)
        {
            _process = p;
            Modules = new ObservableCollection<ProcessModule>(p.RealProcess.Modules.Cast<ProcessModule>());
            Threads = new ObservableCollection<ProcessThread>(p.RealProcess.Threads.Cast<ProcessThread>());
            _workingThread = new Thread(new ThreadStart(WorkingThreadProcess));
            _workingThread.Start();
        }

        private void WorkingThreadProcess()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Process proc = Process.GetProcessById(_process.ID);
                Modules = new ObservableCollection<ProcessModule>(proc.Modules.Cast<ProcessModule>());
                Threads = new ObservableCollection<ProcessThread>(proc.Threads.Cast<ProcessThread>());

            }
        }

        public ObservableCollection<ProcessModule> Modules
        {
            get => _modules;
            private set
            {
                _modules = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProcessThread> Threads
        {
            get => _threads;
            private set
            {
                _threads = value;
                OnPropertyChanged();
            }
        }

        


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
