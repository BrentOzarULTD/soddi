soddi
=====

StackOverflow Data Dump Importer. Forked from https://bitbucket.org/bitpusher/soddi/ after the original author passed away.

This app takes the Stack Exchange Data Dump XML files after you've downloaded them from here: https://archive.org/details/stackexchange

And loads them into a database. If you don't want to hassle with this, just grab the Stack Overflow database in Microsoft SQL Server format from here: https://www.brentozar.com/archive/2015/10/how-to-download-the-stack-overflow-database-via-bittorrent/


Using It
--------

- Compile the latest dev branch yourself from source, or download the most recent official release: https://github.com/BrentOzarULTD/soddi/releases
- Create a folder to hold the extracted XML files (e.g. `C:\TEMP`)
- Extract files into a separate folder per StackExchange site (e.g. `122017 Stack Overflow`). The MMYYYY format is important.
- Fire up the `soddi.exe`
- Select the extracted XML folder in the **Source** text box.
- Select the Sites you want to import
- Set up your connection string
- Adjust batch size as you see fit
- Click **Import**
- Depending on the size of the import, you may want to grab a sandwich
