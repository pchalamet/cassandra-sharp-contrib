﻿// cassandra-sharp - high performance .NET driver for Apache Cassandra
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

namespace CassandraSharp.Contrib.log4net
{
    using CassandraSharp.Extensibility;
    using global::log4net;

    public class Logger : ILogger
    {
        private readonly ILog _log = LogManager.GetLogger("cassandra-sharp");

        public void Dispose()
        {
        }

        public void Debug(string format, params object[] prms)
        {
            _log.DebugFormat(format, prms);
        }

        public void Info(string format, params object[] prms)
        {
            _log.InfoFormat(format, prms);
        }

        public void Warn(string format, params object[] prms)
        {
            _log.WarnFormat(format, prms);
        }

        public void Error(string format, params object[] prms)
        {
            _log.ErrorFormat(format, prms);
        }

        public void Fatal(string format, params object[] prms)
        {
            _log.FatalFormat(format, prms);
        }
    }
}