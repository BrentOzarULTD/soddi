
StackOverflow Data Dump Import v.11

  ClickOnce Installer: http://skysanders.net/tools/se/soddi/publish.htm

  (c) 2010 Sky Sanders
  licensed under MIT/GPL - see license.txt
  
  info:http://skysanders.net/tools/se
  msi :http://skysanders.net/files/soddi.11.msi
  bin :http://skysanders.net/files/soddi.11.zip
  src :http://bitbucket.org/bitpusher/soddi/


SODDI is a .Net 3.5 sp1 executable written in C# that quickly and cleanly imports StackOverflow Data Dump XML files into 
MS Sql Server 2000/05/08, MySql Server 5.1 and SQLite3. (MySql and SQLite drivers are included)

SODDI can be run as a command line utility or, when invoked with no arguments or GUI argument, will
present a Windows Form interface.

Quick Start:
The quickest route to your own copy of the StackOverflow databases is to use the ClickOnce installer,
browse to the uncompressed data dump, accept the default SQLite provider selection and click 'Import'.


USAGE:

soddi.exe source:"" target:"" [batch:5000] [split] [indices] [fulltext] [[meta] [so] [su] [sf]] [gui]


SOURCE          The directory containing the individual site directories.
                NOTE: do not include trailing slash in quoted path as the arg
                parser will interpret it as an escaped quote and puke.

TARGET          A valid ADO.Net connection string, including the provider invariant
                name.
                
                Platform specific connection string details:
                
                Sql Server: Database must exist. Data will be loaded into tables segregated by
                schema named as the site data being imported. e.g. so.Users, meta.Users.
                The tables are dropped before import.
                
                MySql: Connection string should include server, each site's data will be loaded
                into a database named as the site imported. The databases will be dropped and 
                recreated before import.
                
                SQLite: Connection string should specify a directory. The data will be imported
                into seperate .db3 files, each named as the site imported. Existing data files
                will be overwritten.
                
                The target database/datafile/schema names can be modified by explicitely specifying
                sites to import and appending the desired schema as a parameter value or editing
                the Sites list item schema in the GUI.

                
-- OPTIONAL ARGUMENTS

SPLIT           Normalize post tags by splitting the concatenated Posts.Tags field into individual 
				rows in a separate PostTags table.

INDICES         Enables useful indexes on each table.

FULLTEXT        Enables a full text index on Posts.Body and Posts.Title - SqlServer only.

BATCH           Number of rows inserted in each transaction. Default 5000.

GUI             Presents a Windows Forms interface. If SODDI is invoked with arguments and GUI, the UI
                will be populated with the supplied arguments.
                
                The console window will remain open to recieve all debug and error output.

META|SO|SU|SF   Specifies which sites to import. If none are specified, all site directories found in 
                SOURCE will be imported.
                
                To specify a different target name simply treat the site name as a parameter.
                
                e.g. 
                
                Sql Server - SO:StackOverflowData will load the data from the XXXXX SO directory 
                into the database specified in the connection string and the schema 'StackOverflowData'
                
                MySql - SO:StackOverflowData will load the data from the XXXXX SO directory 
                into a new database named StackOverflowData on the server specified in the connection string.
                
                SQLite - SO:StackOverflowData will load the data from the XXXXX SO directory into a new 
                db3 file named StackOverflowData.db3 in the directory specified in the connection string.
                
                In GUI mode you may edit the schema item in the Sites list.
                
Options are not case sensitive.

Example command lines.

GUI Mode:
	soddi 

SQLite - all sites:
	soddi source:"F:\Export-030110" target:"data source=c:\temp;version=3;new=True;Provider=System.Data.SQLite"

MySql - all sites:
	soddi source:"F:\Export-030110" target:"server=localhost;user id=root;password=p@ssW0rd;Provider=MySql.Data.MySqlClient"

MySql - Meta StackOverflow and StackOverflow data into specified databases:
	soddi source:"F:\Export-030110" target:"server=localhost;user id=root;password=p@ssW0rd;Provider=MySql.Data.MySqlClient" meta:MetaDb so:SoDb
	
Sql Server - all sites:
	soddi source:"F:\Export-030110" target:"data source=(local);initial catalog=SOData;integrated security=true;Provider=System.Data.SqlClient"
	
Sql Server - StackOverflow data only (SO):
	soddi source:"F:\Export-030110" target:"data source=(local);initial catalog=SOData;integrated security=true;Provider=System.Data.SqlClient" so

Sql Server - StackOverflow data only into schema dbo:
	soddi source:"F:\Export-030110" target:"data source=(local);initial catalog=SOData;integrated security=true;Provider=System.Data.SqlClient" so:dbo


Sql Server - StackOverflow data only, split tags and add indices:
	soddi source:"F:\Export-030110" target:"data source=(local);initial catalog=SOData;integrated security=true;Provider=System.Data.SqlClient" so split indices

 
The latest data dump can be found at
http://blog.stackoverflow.com/category/cc-wiki-dump/

04/01/2010 - Sky Sanders <sky.sanders@gmail.com>

04/09/2010 - Explicitly set platform to x86 to allow same binaries to run on x64.