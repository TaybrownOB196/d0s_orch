FROM mysql:8.0.17

EXPOSE 3306

ENV MYSQL_DATABASE temp

ADD ["setup_users.sql", "/docker-entrypoint-initdb.d/01_setup_users.sql"]
ADD ["create_tables.sql", "/docker-entrypoint-initdb.d/005_create_tables.sql"]
ADD ["insert_data.sql", "/docker-entrypoint-initdb.d/010_insert_data.sql"]