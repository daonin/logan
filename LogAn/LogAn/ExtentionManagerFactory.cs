namespace LogAn
{
    public static class ExtentionManagerFactory
    {
        private static IExtentionManager _customManager;

        public static IExtentionManager Create()
        {
            if (_customManager != null)
                return _customManager;
            else
                return new FileExtentionManager();
        }

        public static void SetManager(IExtentionManager manager)
        {
            _customManager = manager;
        }
    }
}