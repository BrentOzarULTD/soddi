
StackOverflow Data Dump Import v.1.1

  Portions of code (c) 2014 Jeremiah Peschka
  http://github.com/peschkaj/soddi
  binaries available at: https://github.com/peschkaj/soddi/releases

  StackOverflow data dump available at: https://archive.org/details/stackexchange


  Original code (c) 2010 Sky Sanders
  licensed under MIT/GPL - see license.txt

  ClickOnce Installer: http://skysanders.net/tools/se/soddi/publish.htm

  info:http://skysanders.net/tools/se
  msi :http://skysanders.net/files/soddi.11.msi
  bin :http://skysanders.net/files/soddi.11.zip
  src :http://bitbucket.org/bitpusher/soddi/



SODDI is a .Net 3.5 sp1 executable written in C# that quickly and cleanly imports StackOverflow Data Dump XML files into 
MS Sql Server 2005/08/12 (please stop actively using SQL Server 2005).

SODDI can be run as a command line utility or, when invoked with no arguments or GUI argument, will
present a Windows Form interface.


USAGE:

soddi.exe source:"" target:"" [batch:5000] [split] [indices] [fulltext] [identity] [[meta] [so] [su] [sf]] [gui]


SOURCE          The directory containing the individual site directories.
                NOTE: do not include trailing slash in quoted path as the arg
                parser will interpret it as an escaped quote and puke.

TARGET          A valid ADO.Net connection string, including the provider invariant
                name.
                
                Platform specific connection string details:
                
                Sql Server: Database must exist. Data will be loaded into tables segregated by
                schema named as the site data being imported. e.g. so.Users, meta.Users.
                The tables are dropped before import.
                
                The target database/datafile/schema names can be modified by explicitely specifying
                sites to import and appending the desired schema as a parameter value or editing
                the Sites list item schema in the GUI.

                
-- OPTIONAL ARGUMENTS

SPLIT           Normalize post tags by splitting the concatenated Posts.Tags field into individual 
				rows in a separate PostTags table.

INDICES         Enables useful indexes on each table.

FULLTEXT        Enables a full text index on Posts.Body and Posts.Title - SqlServer only.

IDENTITY        Sets ID columns as identity fields in the created database.  Will cause slower import.

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
                
                In GUI mode you may edit the schema item in the Sites list.
                
Options are not case sensitive.

Example command lines.

GUI Mode:
	soddi 

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