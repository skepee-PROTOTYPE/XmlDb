# Xml DataBase

**Features**
1. Creates an xml file based database.
2. Load a database from an existing xml file database.
3. No limitations on number of tables.
4. No limitations on number of fields.
5. CRUD operations on tables.
6. Primary key setting property on tables.
7. By using a dictionary the tables fields can be set very easily.


**Description**

XmlDb is a class to create and use a xml file based database. 
The information is stored in an xml file that is created when it is passed in the class costructor. 

The use of this class is for a relatively small amount of data with very simple objects. This class can be used when you need to store simple data without creating a pure DataBase (MSSql, Oracle, etc...).


The class does not handle (for now) relationships between tables but it covers the setting of a primary key.
Basically, if a field with name "id" is used to create the table, primary key property is automatically set for that field.


The project is based on three different classes:

***
**XmlDb**: used for the entire Database. 


**Public Methods**
- ***AddTable***
- ***LoadXmlDb***
- ***UpdateItem***
- ***RemoveItem***
- ***AddItem***
- ***AddListItem***
- ***GetListItems***

***
**XmlTable**: used for CRUD operations.

**Internal Methods**
- ***CreateTable***
- ***Update***
- ***Remove***
- ***Add***
- ***List***

***
**XmlLog**: used for logging.

**Internal Methods**
- ***Add***



***Example of database creation with log***

```
  // Database creation with log file
  XmlDb mydb = new XmlDb(@"D:\xmldbtest.xml", @"D:\xmldbtestlog.xml");

  // Creation of table "Security" with 3 fields:
  Dictionary<string, Type> myfields = new Dictionary<string, Type>
  {
      { "Id", typeof(int) },
      { "Name", typeof(string) },
      { "Type", typeof(string) }
  };

  mydb.AddTable("Security", myfields);

  // Adding item to Security table
  mydb.AddItem("Security", new Dictionary<string, string>
  {
      {"Name", "NameSecurity0" },
      {"Type", "TypeSecurity0"}
  });

  // Updating an item to Security table
  mydb.UpdateItem("Security", new Dictionary<string, string>
  {
      {"Name", "NameSecurity1"},
      {"Type", "TypeSecurity1"}
  },
   new Dictionary<string, string>
  {
      {"Name", "NameSecurity2"},
      {"Type", "TypeSecurity2"}
  });

  // Removing an item to Security table
  mydb.RemoveItem("Security", new Dictionary<string, string>
  {
      {"Name", "NameSecurity0"},
      {"Type", "TypeSecurity0"}
  });

```



***Example of loading en existing database without log***
```
XmlDb mydb = new XmlDb(@"D:\xmldb.xml", string.Empty);

mydb.LoadXmlDb();
```

