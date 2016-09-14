using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class LogAnalyzer
    {
        private IExtentionManager _manager;

        public IExtentionManager ExtentionManager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        public LogAnalyzer()
        {
            //_manager = ExtentionManagerFactory.Create();
        }

        public virtual IExtentionManager GetExtentionManager()
        {
            return ExtentionManagerFactory.Create();
        }
        public bool WasLastFileNameValid { get; set; }

        public bool IsValidLogFileName(string fileName)
        {
            WasLastFileNameValid = false;

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Filename has to be provided.");

            bool result = GetExtentionManager().IsValid(fileName);

            WasLastFileNameValid = result;
            return result;
        }

    }
}
