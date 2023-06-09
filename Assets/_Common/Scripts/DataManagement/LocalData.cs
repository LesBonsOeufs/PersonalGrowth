using System;

namespace Com.GabrielBernabeu.Common.DataManagement
{
    [Serializable]
    public struct LocalData
    {
        public string username;
        public string password;

        public LocalData(string username = null, string password = null)
        {
            this.username = username;
            this.password = password;
        }
    }
}