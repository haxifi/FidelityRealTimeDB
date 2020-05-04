# FidelityRealTimeDB
RethinkDB RealTime

## How to start rethinkdb
``docker-compose -f rethinkdb.yml up -d``


### Set db param:
    
    this.dbName = "test";
    this.tableName = "test";
    this.connection = new classes.Connection("localhost", 28015, "123456");

