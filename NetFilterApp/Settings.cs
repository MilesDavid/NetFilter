using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace NetFilterApp
{
    class Settings
    {
        #region Fields
       
        string configPath;
        Config config;
        Logger logger;

        #endregion

        #region Properties

        public List<string> TracingProcesses
        {
            get { return config.TracingProcesses; }
        }

        public bool CertSelfSigned
        {
            get { return config.CertSelfSigned; }
            set { config.CertSelfSigned = value; }
        }

        #endregion

        #region Methods

        public Settings(string configPath, Logger logger)
        {
            this.configPath = configPath;
            config = new Config();
            this.logger = logger;
        }

        public bool addTracingProcess(string process)
        {
            if (!System.IO.File.Exists(process))
            {
                // write to log
                logger.write(string.Format("Process {0} is not exists", process));
                return false;
            }

            if (!isExistsTracingProcess(process))
            {
                config.TracingProcesses.Add(process);
                return true;
            }
            else
            {
                // write to log
                logger.write(string.Format("Process {0} is already exists in list", process));
            }

            return false;
        }

        public bool deleteTracingProcess(string process)
        {
            return config.TracingProcesses.Remove(process);
        }

        public bool isExistsTracingProcess(string process)
        {
            return config.TracingProcesses.IndexOf(process) != -1;
        }

        public void clearTracingProcessList()
        {
            config.TracingProcesses.Clear();
        }

        public bool read()
        {
            const int bufSize = 512;
            int pos = 0;
            char[] buf = new char[bufSize];

            System.IO.StreamReader configFile = new System.IO.StreamReader(configPath);
            while (configFile.ReadBlock(buf, pos, bufSize) > 0)
            {
                pos += bufSize;
                Array.Resize(ref buf, pos + bufSize);
            }

            configFile.Close();

            try
            {
                string configStr = new string(buf);
                dynamic jsonObj = JsonConvert.DeserializeObject(configStr);

                config.CertSelfSigned = jsonObj.CertSelfSigned;

                var processes = jsonObj.TracingProcesses;
                foreach (string process in processes)
                {
                    addTracingProcess(process);
                }
            }
            catch (Exception e)
            {
                // write to log
                logger.write(e.Message);
            }

            return config.TracingProcesses.Count > 0;
        }

        public bool write()
        {
            //Construct json
            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.NullValueHandling = NullValueHandling.Include;

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(configPath))
                using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                {
                    jsonSerializer.Serialize(jsonWriter, config);
                }
            }
            catch(Exception e)
            {
                // write to log
                logger.write(e.Message);
            }

            return false;
        }

        #endregion
    }
}
