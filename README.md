# Vertcoin's [One-Click Miner](https://www.vertcoin.org/mine/)

**Requirements**

.NET 4.5



**Getting Started**

A copy of this guide including images can be found [HERE](http://alwayshashing.com/ocmhowto.pdf)

First off, you'll need to download the OCM. You can find the most recent
version [HERE](https://github.com/vertcoin/One-Click-Miner/releases).

Once you have the OCM downloaded and installed, you'll find a new icon
on your desktop that says 'Vertcoin One-Click Miner'. Double-click the
icon to launch the miner.

**Finding P2Pool Nodes**

First things first, let's find a pool to mine to. Click the 'Find P2Pool
Nodes' button in the top-center of the OCM.

Click the 'Scan' button to scan for active nodes on the P2Pool network.
The 'Network 1' and 'Network 2' tabs will populate with pools showing
included fees, locations, latencies, etc. It is recommended to select
pools with the lowest latency (ideally &lt; 100ms).

*\*If you are a smaller miner with 2 graphics cards or less or are using
your CPU, it is recommended to use Network 2. If you are a larger miner
with multiple cards and/or a hash rate larger than 100Mh, it is
recommended to use Network 1.*

Once you have selected your pools, copy the Vertcoin wallet address you
wish to mine to into the box and click 'Add Selected Pool(s)'.

**Hosting P2Pool Node**

To host a local P2Pool Node, you need to be running the Vertcoin Core
Wallet. This can be downloaded
[HERE](https://github.com/vertcoin/vertcoin/releases). Once you have the
Vertcoin Core Wallet installed and fully synchronized, all you have to
do is click the ‘Run Local Node’ checkbox that’s located in the
top-right-hand corner of the OCM main window.

If you haven't run a local node before or if there is an update
available, you will see a pop-up message prompting a download. 
Click 'OK' to download and install P2Pool. You will also be
prompted to add a wallet address and password to your local node address
so the OCM can add your new node to the pool list.

If you selected an alternate location for the Vertcoin Data Directory
when installing the Vertcoin Core Wallet, you will be prompted by the
OCM to select the directory you chose. This is the directory that stores
the Vertcoin blockchain for P2Pool to reference for mining. Click ‘OK’
to browse and select the directory.

Once you’ve clicked the ‘Run P2Pool Node’ checkbox and have completed
the other steps, you will see the P2Pool status text change to “Loading”
and then “Running: Network X”.

To change the Network in which your local P2Pool Node is running, click
the ‘Settings’ menu option at the top of the OCM. All you have to do is
click the drop-down box in the P2Pool section under Network, and select
which network you wish to host your node on. Once selected, close the
Settings menu and restart your P2Pool Node to change networks.

**How to Mine**

Now it's time to get your miner started! There are two ways to mine
Vertcoin, via your graphics card or your CPU (processor). There are
essentially two types of graphics cards, AMD and Nvidia. Once you've
decided what you want to mine with, click the drop-down box below the
Miner status box in the top-left-hand corner of the OCM and select your
hardware type.

Once you've selected your hardware type, click the 'Start' button. If
you haven't started the miner before or if there is an update available,
you will see a pop-up message prompting a download. Click 'OK' to download and install the miner.

Now that your miner is downloaded, click the 'Start' button again to
start mining. You will see 'Waiting for Share' or 'Running' in the Miner
status window if your miner is running. You will start to see your hash
rate climb at the bottom of the OCM window once you start to solve
shares supplied by the pool. It is not uncommon to see a hash rate of
0.00 kh/s if mining with a low hash rate on p2pool. The lower your hash
rate, the longer it takes to solve a share on the pool.


License
-------

Vertcoin One-Click Miner is released under the terms of the MIT license.
See http://opensource.org/licenses/MIT.
