using System;
using _05_Lab_CS.Model;
using _05_Lab_CS.Tools;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using _05_Lab_CS.Views;

namespace _05_Lab_CS.ViewModels
{
    class TaskManagerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MyProcess> _processes;
        private static Thread _workingThread;
        private InfoView _infoWindow;
        private MyProcess _selected;
      

        private ICommand _removeProcessCommand;
        private ICommand _infoCommand;
        private ICommand _openFileCommand;



        public ObservableCollection<MyProcess> Processes
        {
            get => _processes;
            private set
            {
                _processes = value;
                OnPropertyChanged();

            }
        }




        public MyProcess Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged();

            }
        }

        internal TaskManagerViewModel()
        {
            _processes = new ObservableCollection<MyProcess>();
            foreach (Process p in Process.GetProcesses())
            {
            try { 
                if (!p.HasExited)
                    _processes.Add(new MyProcess(p));
            }catch (Win32Exception) { }
                

            }

            _workingThread = new Thread(new ThreadStart(WorkingThreadProcess));
            _workingThread.Start();
        }


        private void WorkingThreadProcess()
        {
            while (true)
            {
                Thread.Sleep(3000);
              
               Process[] prs = Process.GetProcesses();
               ObservableCollection<MyProcess> myPrs = new ObservableCollection<MyProcess>();
                foreach (Process p in prs)
                {
                    try
                    {
                        if (!p.HasExited)
                        {
                            myPrs.Add(new MyProcess(p));
                        }
                    }
                    catch (Win32Exception) { }
                }

                Processes = myPrs;
            }
        }

    

        internal void StopWorkingThread()
        {
            _workingThread.Join(2000);
            _workingThread.Abort();
            _workingThread = null;
        }

        public ICommand removeProcessCommand
        {
            get { return _removeProcessCommand ?? (_removeProcessCommand = new RelayCommand<object>(removeProcessImplementation, o => ItemSelected())); }
        }

        public ICommand infoCommand
        {
            get { return _infoCommand ?? (_infoCommand = new RelayCommand<object>(infoCommandImplementation, o => ItemSelected())); }
        }

        public ICommand openFileCommand
        {
            get { return _openFileCommand ?? (_openFileCommand = new RelayCommand<object>(openFileCommandImplementation, o => ItemSelected())); }
        }

        private bool ItemSelected()
        {
            return Selected != null;
        }

        private void removeProcessImplementation(object obj)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                Process process = Process.GetProcessById(Selected.ID);
                try
                {
                    process.Kill();
                }
                catch (Win32Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
        }

        

        private async void infoCommandImplementation(object o)
        {
            try
            {
                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(delegate
                    {
                        Process process = Process.GetProcessById(Selected.ID);
                        _infoWindow?.Close();
                        try
                        {
                            _infoWindow = new InfoView(Selected);
                            _infoWindow.Show();
                        }
                        catch (Win32Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void openFileCommandImplementation(object o)
        {
            await Task.Run(() =>
            {
                
                string path = System.IO.Directory.GetParent(Selected.Path).FullName;
                try
                {
                    Process.Start("explorer.exe", path);
                }
                catch (Win32Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
   
}
