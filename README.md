**Table of Contents**

-   Getting St<span id="_Hlt494699468" class="anchor"><span
    id="_Hlt494699469" class="anchor"></span></span>arted

-   Finding P2Pool<span id="_Hlt494702758" class="anchor"></span> Nodes

-   Hosting P2Pool N<span id="_Hlt494702760" class="anchor"></span>ode

-   How to Mi<span id="_Hlt494702764" class="anchor"></span>ne

<span id="Getting_Started" class="anchor"></span>**Getting Started**

First off, you'll need to download the OCM. You can find the most recent
version [HERE](https://github.com/vertcoin/One-Click-Miner/releases).

Once you have the OCM downloaded and installed, you'll find a new icon
on your desktop that says 'Vertcoin One-Click Miner'. Double-click the
icon to launch the miner.

![](media/image1.png){width="4.187399387576553in"
height="2.708299431321085in"}

You should see a screen similar to the one below.

![](media/image2.png){width="6.073759842519685in"
height="3.8130293088363953in"}

<span id="Find_P2Pool" class="anchor"></span>

**Finding P2Pool Nodes**

First things first, let's find a pool to mine to. Click the 'Find P2Pool
Nodes' button in the top-center of the OCM. You should see a screen that
looks like this.

![](media/image3.png){width="6.925199037620297in"
height="2.7534995625546808in"}

Click the 'Scan' button to scan for active nodes on the P2Pool network.
The 'Network 1' and 'Network 2' tabs will populate with pools showing
included fees, locations, latencies, etc. It is recommended to select
pools with the lowest latency (ideally &lt; 100ms).

*\*If you are a smaller miner with 2 graphics cards or less or are using
your CPU, it is recommended to use Network 2. If you are a larger miner
with multiple cards and/or a hash rate larger than 100Mh, it is
recommended to use Network 1.*

![](media/image4.png){width="6.925199037620297in"
height="2.7251990376202975in"}

Once you have selected your pools, copy the Vertcoin wallet address you
wish to mine to into the box and click 'Add Selected Pool(s)'.

![](media/image5.png){width="6.925199037620297in"
height="2.72799978127734in"}

Now that you’ve added pools, the OCM main window should look something
like this.

![](media/image6.png){width="5.157099737532809in"
height="3.771699475065617in"}

<span id="Host_P2Pool" class="anchor"></span>

**Hosting P2Pool Node**

To host a local P2Pool Node, you need to be running the Vertcoin Core
Wallet. This can be downloaded
[HERE](https://github.com/vertcoin/vertcoin/releases). Once you have the
Vertcoin Core Wallet installed and fully synchronized, all you have to
do is click the ‘Run Local Node’ checkbox that’s located in the
top-right-hand corner of the OCM main window.

![](media/image7.png){width="4.886099081364829in"
height="3.8859590988626422in"}

If you haven't run a local node before or if there is an update
available, you will see a pop-up message prompting a downloaded like the
image below. Click 'OK' to download and install P2Pool. You will also be
prompted to add a wallet address and password to your local node address
so the OCM can add your new node to the pool list.

![](media/image8.png){width="5.0528291776028in"
height="3.37549978127734in"}

If you selected an alternate location for the Vertcoin Data Directory
when installing the Vertcoin Core Wallet, you will be prompted by the
OCM to select the directory you chose. This is the directory that stores
the Vertcoin blockchain for P2Pool to reference for mining. Click ‘OK’
to browse and select the directory.

![](media/image9.png){width="5.2715693350831145in"
height="3.9172090988626422in"}

![](media/image10.png){width="4.781919291338583in"
height="4.708989501312336in"}

Once you’ve clicked the ‘Run P2Pool Node’ checkbox and have completed
the other steps, you will see the P2Pool status text change to “Loading”
and then “Running: Network X”.

![](media/image11.png){width="4.8444291338582675in"
height="3.677599518810149in"}

To change the Network in which your local P2Pool Node is running, click
the ‘Settings’ menu option at the top of the OCM. All you have to do is
click the drop-down box in the P2Pool section under Network, and select
which network you wish to host your node on. Once selected, close the
Settings menu and restart your P2Pool Node to change networks.

![](media/image12.png){width="6.925in" height="3.4666699475065617in"}

<span id="How_to_Mine" class="anchor"></span>

**How to Mine**

Now it's time to get your miner started! There are two ways to mine
Vertcoin, via your graphics card or your CPU (processor). There are
essentially two types of graphics cards, AMD and Nvidia. Once you've
decided what you want to mine with, click the drop-down box below the
Miner status box in the top-left-hand corner of the OCM and select your
hardware type. See below.

![](media/image13.png){width="4.969298993875766in"
height="3.750799431321085in"}

Once you've selected your hardware type, click the 'Start' button. If
you haven't started the miner before or if there is an update available,
you will see a pop-up message prompting a downloaded like the image
below. Click 'OK' to download and install the miner.

![](media/image14.png){width="4.896499343832021in"
height="3.729899387576553in"}

Now that your miner is downloaded, click the 'Start' button again to
start mining. You will see 'Waiting for Share' or 'Running' in the Miner
status window if your miner is running. You will start to see your hash
rate climb at the bottom of the OCM window once you start to solve
shares supplied by the pool. It is not uncommon to see a hash rate of
0.00 kh/s if mining with a low hash rate on p2pool. The lower your hash
rate, the longer it takes to solve a share on the pool. See below for an
image of the OCM mining.

![](media/image15.png){width="4.792339238845145in"
height="3.7192694663167103in"}
