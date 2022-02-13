using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace InterviewTask.Services {

    public interface ICustomLogger {
        void Insert(string ip, string datetime, string path, string error = null);
    }

    public class CustomLogger : ICustomLogger {

        public void Insert(string ip, string datetime, string request, string error) {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\log.txt";
            //if (!File.Exists(path)) {File.CreateText(path);
            using (StreamWriter streamwriter = File.AppendText(path)) {
                
                streamwriter.WriteLine(datetime);
                streamwriter.WriteLine("Path:" + request);
                streamwriter.WriteLine("IP Address: " + ip);   
                if(error != string.Empty) streamwriter.WriteLine(error);
                streamwriter.WriteLine();
                streamwriter.Close();
            }
        }

    }
}