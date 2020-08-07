using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class ServerInfo
    {
        private static string _ServerPath;
        public static string ServerPath { get => _ServerPath; set => _ServerPath = value; }
    }
}
