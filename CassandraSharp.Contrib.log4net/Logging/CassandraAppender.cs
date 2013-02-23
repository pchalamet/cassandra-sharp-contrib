// cassandra-sharp - high performance .NET driver for Apache Cassandra
// Copyright (c) 2011-2013 Pierre Chalamet
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace CassandraSharp.Contrib.log4net.Logging
{
    using System;
    using System.Linq;
    using System.Net;
    using CassandraSharp.CQLPoco;
    using CassandraSharp.Utils;
    using global::log4net.Appender;
    using global::log4net.Core;

    public class CassandraAppender : AppenderSkeleton
    {
        private readonly string _hostname;

        private readonly IPAddress _ipAddress;

        private ICluster _cluster;

        private IPreparedQuery<NonQuery> _insert;

        public CassandraAppender()
        {
            ClusterName = "CassandraLogging";
            Keyspace = "Logging";
            ColumnFamily = "Entries";

            _ipAddress = Dns.GetHostAddresses("localhost").Last();
            _hostname = Environment.MachineName;
        }

        public string ClusterName { get; set; }

        public string Keyspace { get; set; }

        public string ColumnFamily { get; set; }

        public string AppName { get; set; }

        public ConsistencyLevel ConsistencyLevel { get; set; }

        public override void ActivateOptions()
        {
            _cluster = ClusterManager.GetCluster(ClusterName);

            string insertCQL =
                    string.Format(
                            "insert into {0}.{1} " +
                            "(id," +
                            "app_name," +
                            "app_start_time," +
                            "class_name," +
                            "file_name," +
                            "host_ip," +
                            "host_name," +
                            "level," +
                            "line_number," +
                            "log_timestamp," +
                            "logger_name," +
                            "message," +
                            "method_name," +
                            "thread_name," +
                            "throwable_str_rep) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)",
                            Keyspace, ColumnFamily);

            ICqlCommand command = _cluster.CreatePocoCommand();
            _insert = command.Prepare(insertCQL);
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            Entry entry = new Entry();
            entry.id = TimedUuid.GenerateTimeBasedGuid(DateTime.Now);
            entry.app_name = AppName;
            entry.host_ip = _ipAddress;
            entry.host_name = _hostname;
            entry.logger_name = loggingEvent.LoggerName;
            entry.level = loggingEvent.Level.ToString();
            entry.message = loggingEvent.RenderedMessage;
            entry.app_start_time = LoggingEvent.StartTime;
            entry.thread_name = loggingEvent.ThreadName;
            entry.log_timestamp = loggingEvent.TimeStamp;
            entry.throwable_str_rep = loggingEvent.GetExceptionString();

            LocationInfo locInfo = loggingEvent.LocationInformation;
            if (locInfo != null)
            {
                entry.class_name = locInfo.ClassName;
                entry.file_name = locInfo.FileName;
                entry.line_number = int.Parse(locInfo.LineNumber);
                entry.method_name = locInfo.MethodName;
            }

            _insert.Execute(entry, ConsistencyLevel).Subscribe(_ => { }, ex => Console.WriteLine(ex));
        }
    }
}