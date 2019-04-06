using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace _05_Lab_CS.Model
{
    internal class MyProcess
    {
        private Process _p;
        private string _name;
        private int _id;
        private string _startTime;
        private int _threadsNumber;
        private string _path;
        private bool _active;
        private PerformanceCounter _cpu;
        private PerformanceCounter _ram;

        internal MyProcess(Process p)
        {
            _p = p;
            try
            {
                _startTime = p.StartTime.ToString();
                _path = p.MainModule.FileName;
            }
            catch (Win32Exception)
            {
                _startTime = "denied";
                _path = "denied";
            }
            CountMemory();
            _name = p.ProcessName;
            _id = p.Id;
            _threadsNumber = CountThreadsNumber();
            _active = p.Responding;
        }

        public string Name
        {
            get { return _name; }
        }

        public int ID
        {
            get { return _id; }
        }

        public string StartTime
        {
            get { return _startTime; }
        }

        public int ThreadsNumber
        {
            get { return _threadsNumber; }
        }

        public string Path
        {
            get { return _path; }
        }

        public bool Active
        {
            get { return _active; }
        }


        public Process RealProcess
        {
            get { return _p; }
        }

        private void CountMemory()
        {
            _ram = new PerformanceCounter("Process", "Working Set", _p.ProcessName);
            _cpu = new PerformanceCounter("Process", "% Processor Time", _p.ProcessName);

        }

        private int CountThreadsNumber()
        {
            return Process.GetProcessesByName(_p.ProcessName).Length;
        }

        public PerformanceCounter Ram
        {
            get => _ram;
        }

        public PerformanceCounter Cpu
        {
            get => _cpu;
        }
    }
}