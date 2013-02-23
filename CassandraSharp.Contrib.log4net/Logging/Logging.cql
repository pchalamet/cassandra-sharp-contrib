

create keyspace logging with replication = {'class': 'SimpleStrategy', 
                                            'replication_factor' : 1};

create table logging.entries (app_name text,
                              host_ip inet,
                              host_name text,
                              logger_name text,
                              level text,
                              class_name text,
                              file_name text,
                              line_number int,
                              method_name text,
                              message text,
                              app_start_time timestamp,
                              thread_name text,
                              throwable_str_rep text,
                              log_timestamp timestamp,
                              id timeuuid,
                              primary key (id));
