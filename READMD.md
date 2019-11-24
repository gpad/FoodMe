# FoodMe

How to create the DB the first time:

Startup the DB:

```shell
docker-compose up -d
```

```shell
docker exec -it food_me_sql /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P "StrongPassword1" \
   -Q 'CREATE DATABASE FoodMeDev'
```
