using System.Collections.Generic;

namespace NetFilterApp
{
    class Config
    {
        public bool CertSelfSigned { get; set; }
        public List<string> TracingProcesses { get; set; }

        public Config()
        {
            TracingProcesses = new List<string>();
        }
    }
}
