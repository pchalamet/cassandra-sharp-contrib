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
    using System.Net;

    internal class Entry
    {
        // ReSharper disable InconsistentNaming
        public Guid id;

        public string app_name;

        public DateTime app_start_time;

        public string class_name;

        public string file_name;

        public IPAddress host_ip;

        public string host_name;

        public string level;

        public int line_number;

        public DateTime log_timestamp;

        public string logger_name;

        public string message;

        public string method_name;

        public string thread_name;

        public string throwable_str_rep;

        // ReSharper restore InconsistentNaming
    }
}