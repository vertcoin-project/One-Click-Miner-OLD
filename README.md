**Requirements**

.NET 4.5

**Note**

The information below is the initial README file uploaded by Zem of the Vertcoin foundation. The new version of OCM I've uploaded includes an array of optimizations and UI enhancements.

- Main form UI re-done. Removal of misc. Style subroutines which could have been done in the designer.
- Removal of repetitive code in Main.vb. Introduction of the ExitRoutine sub.
- Improval of Process_Check() in Main.vb. Previous implementation was clunky.
- Removal of 'BeginInvoke(New MethodInvoker(AddressOf Function))'. Invoke was being called from a non-asychnronous thread resulting in race-conditions.
- Correct implementation of 'BeginInvoke(New MethodInvoker(AddressOf Function))'.
- Removal of all/DataGridView functions which could be done via a DataSource
- Inclusion of PoolDataCollection.vb which includes PoolDataCollection and PoolDataEx class. These classes are used for the new DataGridView data source as well as more efficient JSON serialization
- Removal of OPEN.NAT dependency. Was not used.
- Removal of several MINER_SETTINGS classes. All Miner settings have been merged into a singular class.
- UI Designer Name standardization. Names/Events/Items are named appropriately.

**What's Next?**

- Finish removing repetitive code in Main.vb (e.g. additional usage of ExitRoutine)
- Begin re-working AddPool.vb, P2Pool.vb
- Remove Style functions which can/could be done in the VS designer
- Improval of DataGridView implementation, inclusion of DataSources to remove unnecessary functionality/code-bloat
- UI enhancements
- UI Designer Name standardization. Ensure Names/Events/Items are named appropriately.

**Getting Started**

Initial OCM is [HERE](https://github.com/vertcoin/One-Click-Miner/).

License
-------

Vertcoin One-Click Miner is released under the terms of the MIT license.
See http://opensource.org/licenses/MIT.
